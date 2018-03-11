using System;
using System.Collections.Generic;

namespace SimulationSystem
{
    public class SimulationController
    {
        private List<SimulationComponentBase> simulationComponents;

        public SimulationController()
        {
            simulationComponents = new List<SimulationComponentBase>();
        }

        /// <summary>
        /// Get the simulation component of the desired type or NULL if it doesn't exist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetSimulationComponent<T>() where T : SimulationComponentBase
        {
            // Get the search type and cache it
            Type searchType = typeof(T);
            T searchItem = null;

            foreach (SimulationComponentBase item in simulationComponents)
            {
                // Check for a matching type
                if (item.GetType() == searchType)
                {
                    searchItem = (T)item;
                    break;
                }
            }

            return searchItem;
        }

        /// <summary>
        /// Add a simulation component of the desired type to the controller. If an instance of
        /// the desired type is already instantiated, it will be returned instead.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T AddSimulationComponent<T>() where T : SimulationComponentBase
        {
            T item = GetSimulationComponent<T>();

            // If the component is null, create an instance of it and store it
            if(item == null)
            {
                item = (T)Activator.CreateInstance(typeof(T), this);
                simulationComponents.Add(item);
            }

            return item;
        }
    }
}
