using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilderWpf
{
    class DependencyContainer : IDependencyContainer, IDependencyResolver
    {
        private readonly IDictionary<Type, Type> _regist = new Dictionary<Type, Type>();

        private readonly IDictionary<Type, Object> _instc = new Dictionary<Type, Object>();

        private static readonly object[] NoArguments = new object[0];

        public static DependencyContainer Self = new DependencyContainer();

        /// <summary>
        /// Registers a new mapping between the specified types.
        /// </summary>
        public void RegisterType(Type type, Type mappedTo)
        {
            if (_instc.ContainsKey(type))
                throw new Exception("A registration for this type already exists.");
            if (_regist.ContainsKey(type))
                throw new ArgumentException("The specified interface type has already been registered.");

            _regist.Add(type, mappedTo);
        }

        /// <summary>
        /// Registers a new mapping between the specified type and the specified instance of an Object.
        /// </summary>
        public void RegisterInstance(Type type, object instance)
        {
            if (_instc.ContainsKey(type))
                throw new Exception("A registration for this type already exists.");
            if (_regist.ContainsKey(type))
                throw new ArgumentException("The specified interface type has already been registered.");

            _instc.Add(type, instance);
        }

        /// <summary>
        /// Returns a registered instance corresponding to the specified type.
        /// </summary>
        public object Resolve(Type type)
        {
            if (!_instc.ContainsKey(type))
            {
                if (!_regist.ContainsKey(type)) // check if the instance exists.
                    throw new Exception($"Unable to resolve an instance of the specified type '{type.FullName}'. Are you missing a type registration?");

                var mappedTo = _regist[type] ?? type; // if null 
                var constructors = mappedTo.GetConstructors();
                if (constructors.Length > 1) // multiple so can't create that type.
                    throw new Exception($"Unable to resolve an type that has more than one constructor.");

                var constructor = constructors[0];
                var parameters = constructor.GetParameters();
                if (parameters.Length == 0)
                    _instc[type] = constructor.Invoke(NoArguments);
                else
                {
                    var args = new object[parameters.Length];
                    foreach (var parameter in parameters)
                        args[parameter.Position] = Resolve(parameter.ParameterType);

                    _instc[type] = constructor.Invoke(args);
                }
            }
            return _instc[type];
        }
    }
}
