using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterAutoRepositories(this IServiceCollection services, Type baseRepository)
        {
            Assembly
                .GetAssembly(baseRepository)!
                .GetTypes()
                .Where(type =>
                    type.BaseType != null
                    && type.BaseType.IsGenericType
                    && type.BaseType.GetGenericTypeDefinition() == baseRepository)
                .ToList()
                .ForEach(type => SmartAddScope(services, type));
        }
        private static void SmartAddScope(IServiceCollection services, Type ourType)
        {
            services.AddScoped(ourType, serviceProvider =>
            {
                var constructor = ourType
                    .GetConstructors()
                    .OrderByDescending(x => x.GetParameters().Length)
                    .First();

                var parameters = constructor
                    .GetParameters()
                    .Select(x =>
                        serviceProvider.GetService(x.ParameterType)
                    )
                    .ToArray();

                return constructor.Invoke(parameters);
            });
        }
    }
}
