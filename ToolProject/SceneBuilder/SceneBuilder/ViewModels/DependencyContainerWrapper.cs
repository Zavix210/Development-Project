using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilder.ViewModels
{
    /// <summary>
    /// Wrapper for Depedency Object.
    /// </summary>
    public static class DependencyContainerWrapper
    {

        public static void RegisterInstance<T>(this IDependencyContainer container, object instance)
        {
            container.RegisterInstance(typeof(T), instance);
        }

        /// <summary>
        /// Registers a mapping from a specified type to the dependency container.
        /// </summary>
        public static void RegisterType<T>(this IDependencyContainer container)
        {
            container.RegisterType(typeof(T), typeof(T));
        }

        /// <summary>
        /// Registers a mapping between two specified types to the dependency container.
        /// </summary>
        public static void RegisterType<T, TU>(this IDependencyContainer container)
        {
            container.RegisterType(typeof(T), typeof(TU));
        }

        /// <summary>
        /// Resolves an instance mapped from the specified type.
        /// </summary>
        public static T Resolve<T>(this IDependencyResolver resolver)
        {
            return (T)resolver.Resolve(typeof(T));
        }
    }
}
