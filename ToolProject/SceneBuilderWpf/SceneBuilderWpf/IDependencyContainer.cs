using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilderWpf
{
    public interface IDependencyContainer
    {
        void RegisterType(Type type, Type mappedTo);
        void RegisterInstance(Type type, object mappedTo);
    }
}
