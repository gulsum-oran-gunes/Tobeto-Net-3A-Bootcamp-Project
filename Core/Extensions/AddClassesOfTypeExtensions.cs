using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class AddClassesOfTypeExtensions
    {
        public static IServiceCollection AddSubClassesOfType
        (this IServiceCollection services, Assembly assembly, Type type,
        Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null)

        {
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
            foreach (Type? item in types)
            {
                if (addWithLifeCycle == null) { services.AddScoped(item); }
                else { addWithLifeCycle(services, type); }
            }

            return services;
        }
    }
}
