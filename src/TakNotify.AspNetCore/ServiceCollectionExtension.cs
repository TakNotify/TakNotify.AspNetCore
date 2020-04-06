// Copyright (c) Frandi Dwi 2020. All rights reserved.
// Licensed under the MIT License.
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace TakNotify
{
    /// <summary>
    /// The extension for <see cref="IServiceCollection"/>
    /// to add TakNotify services
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Add TakNotify services
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns></returns>
        public static NotificationBuilder AddTakNotify(this IServiceCollection services)
        {
            if (!services.Any(sc => sc.ServiceType == typeof(INotificationProviderFactory)))
                services.AddSingleton<INotificationProviderFactory>(NotificationProviderFactory.GetInstance());

            if (!services.Any(sc => sc.ServiceType == typeof(INotification)))
                services.AddTransient<INotification, Notification>();

            return new NotificationBuilder(services, NotificationProviderFactory.GetInstance());
        }
    }
}
