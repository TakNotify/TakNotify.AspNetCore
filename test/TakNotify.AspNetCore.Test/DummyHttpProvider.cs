// Copyright (c) Frandi Dwi 2020. All rights reserved.
// Licensed under the MIT License.
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace TakNotify.AspNetCore.Test
{
    public class DummyHttpProvider : NotificationProvider {
        public const string DefaultName = "DummyHttp";
        
        public DummyHttpProvider(IOptions<NotificationProviderOptions> options, IHttpClientFactory httpClientFactory)
            : base(options.Value, new NullLoggerFactory())
        {
            
        }

        public override string Name => DefaultName;

        public override Task<NotificationResult> Send (MessageParameterCollection messageParameters) {
            throw new NotImplementedException();
        }
    }
}