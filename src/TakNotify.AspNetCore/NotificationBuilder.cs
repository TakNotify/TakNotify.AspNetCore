// Copyright (c) Frandi Dwi 2020. All rights reserved.
// Licensed under the MIT License.
using Microsoft.Extensions.DependencyInjection;
using System;

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
        /// <param name="providerFactory"></param>
        public NotificationBuilder(IServiceCollection services, INotificationProviderFactory providerFactory)
        {
            Services = services;
            ProviderFactory = providerFactory;
        }

        /// <summary>
        /// Collection of service descriptors
        /// </summary>
        public virtual IServiceCollection Services { get; }

        /// <summary>
        /// The notification provider factory
        /// </summary>
        public INotificationProviderFactory ProviderFactory { get; set; }

        /// <summary>
        /// Add a provider to the TakNotify service
        /// </summary>
        /// <typeparam name="TProvider">The notification provider</typeparam>
        /// <typeparam name="TOptions">The options for the notification provider</typeparam>
        /// <param name="configureOptions">The options for the notification provider</param>
        /// <returns></returns>
        public NotificationBuilder AddProvider<TProvider, TOptions>(Action<TOptions> configureOptions)
            where TProvider: NotificationProvider
            where TOptions: NotificationProviderOptions, new()
        {
            Services.Configure(configureOptions);
            Services.AddTransient<TProvider>();
            
            var sp = Services.BuildServiceProvider();
            var provider = sp.GetService<TProvider>();
            ProviderFactory.AddProvider(provider);

            return this;
        }
    }
}
