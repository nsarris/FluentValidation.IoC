using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FluentValidation.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLiteralService<TLiteralService>(this IServiceCollection services, TLiteralService literalService)
            where TLiteralService : class, ILiteralService
        {
            services.AddSingleton(literalService);
            services.AddSingleton<ILiteralService, TLiteralService>();

            return services;
        }

        public static IServiceCollection AddLiteralService<TLiteralService>(this IServiceCollection services, ServiceLifetime lifetime)
            where TLiteralService : ILiteralService
        {
            services.Add(new ServiceDescriptor(typeof(ILiteralService), typeof(TLiteralService), lifetime));

            return services;
        }
    }
}
