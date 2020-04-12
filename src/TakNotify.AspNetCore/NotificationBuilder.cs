// Copyright (c) Frandi Dwi 2020. All rights reserved.
// Licensed under the MIT License.
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace TakNotify
{
    /// <summary>
    /// The notification service builder
    /// </summary>
    public class NotificationBuilder
    {
        /// <summary>
        /// Instantiate the <see cref="NotificationBuilder"/>
        /// </summary>
        /// <param name="services"></param>
        public NotificationBuilder(IServiceCollection services)
        {
            Services = services;
        }

        /// <summary>
        /// Collection of service descriptors
        /// </summary>
        public virtual IServiceCollection Services { get; }

        /// <summary>
        /// Add a provider to the TakNotify service
        /// </summary>
        /// <typeparam name="TProvider">The notification provider</typeparam>
        /// <typeparam name="TOptions">The options for the notification provider</typeparam>
        /// <param name="configureOptions">The options for the notification provider</param>
        /// <param name="requiresHttpClient">Whether the provider requires <see cref="HttpClient"/> or not (default is <c>false</c>)</param>
        /// <returns></returns>
        public NotificationBuilder AddProvider<TProvider, TOptions>(Action<TOptions> configureOptions, bool requiresHttpClient = false)
            where TProvider: NotificationProvider
            where TOptions: NotificationProviderOptions, new()
        {
            if (requiresHttpClient)
                Services.AddHttpClient();

            Services.Configure(configureOptions);
            Services.AddTransient<TProvider>();
            
            INotification notification = null;
            TProvider provider = null;
            try
            {
                var sp = Services.BuildServiceProvider();

                notification = sp.GetService<INotification>();
                provider = sp.GetService<TProvider>();
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("IHttpClientFactory"))
                    throw new NoHttpClientFactoryException(nameof(TProvider));

                throw;
            }

            if (notification != null && provider != null)
                notification.AddProvider(provider);

            return this;
        }
    }
}
