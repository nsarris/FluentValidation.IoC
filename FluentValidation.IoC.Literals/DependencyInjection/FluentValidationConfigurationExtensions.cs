using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FluentValidation.IoC
{
    public static class FluentValidationConfigurationExtensions
    {
        public static FluentValidationConfiguration AddLiteralService<TLiteralService>(this FluentValidationConfiguration configuration, TLiteralService literalService)
            where TLiteralService : class, ILiteralService
        {
            configuration.Services.AddSingleton(literalService);
            configuration.Services.AddSingleton<ILiteralService, TLiteralService>();

            return configuration;
        }

        public static FluentValidationConfiguration AddLiteralService<TLiteralService>(this FluentValidationConfiguration configuration, ServiceLifetime lifetime)
            where TLiteralService : ILiteralService
        {
            configuration.Services.Add(new ServiceDescriptor(typeof(ILiteralService), typeof(TLiteralService), lifetime));

            return configuration;
        }
    }
}
