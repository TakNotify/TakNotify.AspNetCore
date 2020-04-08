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
        /// <returns></returns>
        public NotificationBuilder AddProvider<TProvider, TOptions>(Action<TOptions> configureOptions)
            where TProvider: NotificationProvider
            where TOptions: NotificationProviderOptions, new()
        {
            Services.Configure(configureOptions);
            Services.AddTransient<TProvider>();
            
            var sp = Services.BuildServiceProvider();
            var notification = sp.GetService<INotification>();
            var provider = sp.GetService<TProvider>();
            notification.AddProvider(provider);

            return this;
        }
    }
}
