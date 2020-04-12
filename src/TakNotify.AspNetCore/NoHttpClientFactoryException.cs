// Copyright (c) Frandi Dwi 2020. All rights reserved.
// Licensed under the MIT License.
using System;
using System.Net.Http;

namespace TakNotify
{
    /// <summary>
    /// Exception to throw when <see cref="IHttpClientFactory"/> was not found in the service collection,
    /// <br/>but a provider that requires the type is trying to be added to the <see cref="INotification"/> object.
    /// <br/>Fixing this issue could be as simple as setting the 'requiresHttpClient = false' in <see cref="NotificationBuilder.AddProvider"/>
    /// </summary>
    public class NoHttpClientFactoryException : Exception
    {
        private const string messageFormat = "Unable to resolve 'System.Net.Http.IHttpClientFactory' when trying to add '{0}' provider";

        /// <summary>
        /// Create the instance of <see cref="NoHttpClientFactoryException"/>
        /// </summary>
        /// <param name="providerName">Name of the provider that requires <see cref="IHttpClientFactory"/></param>
        public NoHttpClientFactoryException(string providerName) 
            : base(string.Format(messageFormat, providerName))
        {
            ProviderName = providerName;
        }

        /// <summary>
        /// Create the instance of <see cref="NoHttpClientFactoryException"/>
        /// </summary>
        /// <param name="providerName">Name of the provider that requires <see cref="IHttpClientFactory"/></param>
        /// <param name="innerException">The inner exception</param>
        public NoHttpClientFactoryException(string providerName, Exception innerException) 
            : base(string.Format(messageFormat, providerName), innerException)
        {
            ProviderName = providerName;
        }

        /// <summary>
        /// Name of the provider that requires <see cref="IHttpClientFactory"/>
        /// </summary>
        public string ProviderName { get; }
    }
}
