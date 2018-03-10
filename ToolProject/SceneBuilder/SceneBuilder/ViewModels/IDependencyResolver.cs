using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilder.ViewModels
{
    //resolves a dependency and returns the object type.
    public interface IDependencyResolver
    {
        object Resolve(Type type);
    }


    //allows types to be added to the dependency container so they can be resolved.
    public interface IDependencyContainer
    {
        void RegisterType(Type type, Type mappedTo);
        void RegisterInstance(Type type, object mappedTo);
    }
}
