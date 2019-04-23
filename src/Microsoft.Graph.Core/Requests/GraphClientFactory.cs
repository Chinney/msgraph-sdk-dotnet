﻿// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------
namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Net.Http.Headers;

    /// <summary>
    /// GraphClientFactory class to create the HTTP client
    /// </summary>
    public static class GraphClientFactory
    {
        /// The key for the SDK version header.
        private static readonly string SdkVersionHeaderName = CoreConstants.Headers.SdkVersionHeaderName;

        /// The version for current assembly.
        private static Version assemblyVersion = typeof(GraphClientFactory).GetTypeInfo().Assembly.GetName().Version;

        /// The value for the SDK version header.
        private static string SdkVersionHeaderValue = string.Format(
                    CoreConstants.Headers.SdkVersionHeaderValueFormatString,
                    "Graph",
                    assemblyVersion.Major,
                    assemblyVersion.Minor,
                    assemblyVersion.Build);

        /// The default value for the overall request timeout.
        private static readonly TimeSpan defaultTimeout = TimeSpan.FromSeconds(100);

        /// The default value for the baseAddress of HTTP client
        private static readonly string _baseAddress = "https://graph.microsoft.com/";

        /// Microsoft Graph service nationa cloud endpoints
        private static readonly Dictionary<string, string> cloudList = new Dictionary<string, string>
            {
                { Global_Cloud, "https://graph.microsoft.com" },
                { USGOV_Cloud, "https://graph.microsoft.us" },
                { China_Cloud, "https://microsoftgraph.chinacloudapi.cn" },
                { Germany_Cloud, "https://graph.microsoft.de" }
            };

        private static FeatureFlag featureFlags;

        /// Global endpoint
        public const string Global_Cloud = "Global";
        /// US_GOV endpoint
        public const string USGOV_Cloud = "US_GOV";
        /// China endpoint
        public const string China_Cloud = "China";
        /// Germany endpoint
        public const string Germany_Cloud = "Germany";

        /// <summary>
        /// Proxy to be used with created clients
        /// </summary>
        public static IWebProxy Proxy { get; set; }

        /// <summary>
        /// DefaultHandler is a Func that returns the HttpMessageHandler for actually making the HTTP calls.
        /// The default implementation returns a new instance of HttpClientHandler for each HttpClient.
        /// </summary>
        public static Func<HttpMessageHandler> DefaultHttpHandler = () => {
            return new HttpClientHandler
            {
                Proxy = Proxy,
                AllowAutoRedirect = false
            };
        };

        /// <summary>
        /// Creates a new <see cref="HttpClient"/> instance configured with the handlers provided.
        /// </summary>
        /// <param name="authenticationProvider">The <see cref="IAuthenticationProvider"/> to authenticate requests.</param>
        /// <param name="version">The graph version to use.</param>
        /// <param name="nationalCloud">The national cloud endpoint to use.</param>
        /// <returns></returns>
        public static HttpClient Create(IAuthenticationProvider authenticationProvider, string version = "v1.0", string nationalCloud = Global_Cloud)
        {
            HttpMessageHandler pipeline = CreatePipeline(CreateDefaultHandlers(authenticationProvider));
            return Create(pipeline, version, nationalCloud);
        }

        /// <summary>
        /// Creates a new <see cref="HttpClient"/> instance configured with the handlers provided.
        /// </summary>
        /// <param name="version">The graph version to use.</param>
        /// <param name="nationalCloud">The national cloud endpoint to use.</param>
        /// <param name="handlers">An ordered list of <see cref="DelegatingHandler"/> instances to be invoked as an
        /// <see cref="HttpRequestMessage"/> travels from the <see cref="HttpClient"/> to the network and an
        /// <see cref="HttpResponseMessage"/> travels from the network back to <see cref="HttpClient"/>.
        /// The handlers are invoked in a top-down fashion. That is, the first entry is invoked first for
        /// an outbound request message but last for an inbound response message.</param>
        /// <returns>An <see cref="HttpClient"/> instance with the configured handlers.</returns>
        public static HttpClient Create(IEnumerable<DelegatingHandler> handlers, string version = "v1.0", string nationalCloud = Global_Cloud)
        {
            HttpMessageHandler pipeline = CreatePipeline(handlers);
            return Create(pipeline, version, nationalCloud);
        }

        /// <summary>
        /// Creates a new <see cref="HttpClient"/> instance configured with the handlers provided.
        /// </summary>
        /// <param name="pipeline">The message handler that represents the HTTP pipeline.</param>
        /// <param name="version">The graph version to use.</param>
        /// <param name="nationalCloud">The national cloud endpoint to use.</param>
        /// <returns></returns>
        internal static HttpClient Create(HttpMessageHandler pipeline, string version = "v1.0", string nationalCloud = Global_Cloud)
        {
            HttpClient client = new HttpClient(pipeline);
            client.DefaultRequestHeaders.Add(SdkVersionHeaderName, SdkVersionHeaderValue);
            client.SetFeatureFlag(featureFlags);
            client.Timeout = defaultTimeout;
            client.BaseAddress = DetermineBaseAddress(nationalCloud, version);
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            return client;
        }

        /// <summary>
        /// Create a default set of middleware for calling Microsoft Graph
        /// </summary>
        /// <param name="authenticationProvider">The <see cref="IAuthenticationProvider"/> to authenticate requests.</param>
        /// <returns></returns>
        public static IList<DelegatingHandler> CreateDefaultHandlers(IAuthenticationProvider authenticationProvider)
        {
            featureFlags = FeatureFlag.AuthHandler | FeatureFlag.CompressionHandler | FeatureFlag.RetryHandler | FeatureFlag.RedirectHandler;

            return new List<DelegatingHandler> {
                new AuthenticationHandler(authenticationProvider),
                new CompressionHandler(),
                new RetryHandler(),
                new RedirectHandler()
            };
        }

        private static Uri DetermineBaseAddress(string nationalCloud, string version)
        {
            string cloud = "";
            if (!cloudList.TryGetValue(nationalCloud, out cloud))
            {
                throw new ArgumentException(String.Format("{0} is an unexpected national cloud.", nationalCloud, "nationalCloud"));
            }
            string cloudAddress = cloud + "/" + version;
            return new Uri(cloudAddress);

        }

        /// <summary>
        /// Creates an instance of an <see cref="HttpMessageHandler"/> using the <see cref="DelegatingHandler"/> instances
        /// provided by <paramref name="handlers"/>. The resulting pipeline can be used to manually create <see cref="HttpClient"/>
        /// or <see cref="HttpMessageInvoker"/> instances with customized message handlers.
        /// </summary>
        /// <param name="innerHandler">The inner handler represents the destination of the HTTP message channel.</param>
        /// <param name="handlers">An ordered list of <see cref="DelegatingHandler"/> instances to be invoked as part
        /// of sending an <see cref="HttpRequestMessage"/> and receiving an <see cref="HttpResponseMessage"/>.
        /// The handlers are invoked in a top-down fashion. That is, the first entry is invoked first for
        /// an outbound request message but last for an inbound response message.</param>
        /// <returns>The HTTP message channel.</returns>
        public static HttpMessageHandler CreatePipeline(IEnumerable<DelegatingHandler> handlers, HttpMessageHandler innerHandler = null )
        {
            if (innerHandler == null)
            {
                innerHandler = DefaultHttpHandler();
            }

            if (handlers == null)
            {
                return innerHandler;
            }

            HttpMessageHandler pipeline = innerHandler;
            IEnumerable<DelegatingHandler> reversedHandlers = handlers.Reverse();
            foreach (DelegatingHandler handler in reversedHandlers)
            {
                if (handler == null)
                {
                    throw new ArgumentNullException(nameof(handlers), "DelegatingHandler array contains null item.");
                }

                if (handler.InnerHandler != null)
                {
                    throw new ArgumentException(String.Format("DelegatingHandler array has unexpected InnerHandler. {0} has unexpected InnerHandler.", handler, "handler"));
                }

                handler.InnerHandler = pipeline;
                pipeline = handler;
            }

            return pipeline;
        }
    }
}
