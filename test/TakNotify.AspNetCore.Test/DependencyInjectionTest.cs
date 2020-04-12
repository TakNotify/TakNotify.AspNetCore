// Copyright (c) Frandi Dwi 2020. All rights reserved.
// Licensed under the MIT License.
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace TakNotify.AspNetCore.Test
{
    public class DependencyInjectionTest
    {
        private readonly IServiceCollection _services;
        private readonly Mock<INotification> _notification;
        private readonly Mock<ILogger<Notification>> _logger;

        public DependencyInjectionTest()
        {
            _services = new ServiceCollection();
            _notification = new Mock<INotification>();
            _logger = new Mock<ILogger<Notification>>();

            _services.AddTransient<ILogger<Notification>>(sp => _logger.Object);
        }

        [Fact]
        public void AddTakNotify_Success()
        {
            var builder = _services.AddTakNotify();

            var sp = builder.Services.BuildServiceProvider();
            var notification = sp.GetService<INotification>();

            Assert.NotNull(notification);
        }

        [Fact]
        public void AddProvider_Success()
        {
            _notification.Setup(n => n.AddProvider(It.IsAny<NotificationProvider>()))
                .Returns(_notification.Object);

            _services.AddSingleton(_notification.Object);

            var builder = new NotificationBuilder(_services);
            builder.AddProvider<DummyProvider, NotificationProviderOptions>(options => {});

            _notification.Verify(n => n.AddProvider(It.IsAny<DummyProvider>()), Times.Once);
        }

        [Fact]
        public void AddProvider_WithHttp_Success()
        {
            _notification.Setup(n => n.AddProvider(It.IsAny<NotificationProvider>()))
                .Returns(_notification.Object);

            _services.AddSingleton(_notification.Object);

            var builder = new NotificationBuilder(_services);
            builder.AddProvider<DummyHttpProvider, NotificationProviderOptions>(options => { }, true);

            _notification.Verify(n => n.AddProvider(It.IsAny<DummyHttpProvider>()), Times.Once);
        }

        [Fact]
        public void AddProviders_Success()
        {
            _notification.Setup(n => n.AddProvider(It.IsAny<NotificationProvider>()))
                .Returns(_notification.Object);

            _services.AddSingleton(_notification.Object);

            var builder = new NotificationBuilder(_services);
            builder.AddProvider<DummyProvider, NotificationProviderOptions>(options => { });
            builder.AddProvider<DummyHttpProvider, NotificationProviderOptions>(options => { }, true);

            _notification.Verify(n => n.AddProvider(It.IsAny<NotificationProvider>()), Times.Exactly(2));
        }

        [Fact]
        public void AddProvider_WithHttp_NoHttpClientFactory()
        {
            _notification.Setup(n => n.AddProvider(It.IsAny<NotificationProvider>()))
                .Returns(_notification.Object);

            _services.AddSingleton(_notification.Object);

            var builder = new NotificationBuilder(_services);
            var exception = Record.Exception(() => builder.AddProvider<DummyHttpProvider, NotificationProviderOptions>(options => { }));

            Assert.IsType<NoHttpClientFactoryException>(exception);
        }
    }
}
