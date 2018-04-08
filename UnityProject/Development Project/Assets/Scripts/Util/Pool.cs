using System;
using System.Collections.Generic;

namespace SimulationSystem
{
    public class Pool<T>
    {
        private Queue<T> items;
        private Func<T> instantiateAction;
        private Action<T> storedAction;
        private Action<T> releasedAction;

        public Pool(Func<T> instantiateAction, Action<T> storedAction = null, Action<T> releasedAction = null)
        {
            // The instantiate action must exist
            if (instantiateAction == null)
            {
                throw new System.Exception("Cannot have a NULL instantiate action for a pool!");
            }
            else
            {
                this.instantiateAction = instantiateAction;
                this.storedAction = storedAction;
                this.releasedAction = releasedAction;

                items = new Queue<T>();
            }
        }

        /// <summary>
        /// Get an instance of the storage type.
        /// </summary>
        /// <returns></returns>
        public T Get()
        {
            T item;

            // Is there something to return from the pool?
            if (items.Count > 0)
            {
                item = items.Dequeue();
            }
            else // There isn't anything to pull from the pool, create a new instance
            {
                item = Create();
            }

            // Invoke the released action if its present
            if(releasedAction != null)
            {
                releasedAction.Invoke(item);
            }

            return item;
        }

        /// <summary>
        /// Put an instance of the storage type into the store.
        /// </summary>
        /// <param name="item"></param>
        public void Put(T item)
        {
            // Ensure the item is valid and it's not already in the pool
            if (item != null && !items.Contains(item))
            {
                // Invoke the stored action if its present
                if (storedAction != null)
                {
                    storedAction.Invoke(item);
                }

                items.Enqueue(item);
            }
        }

        /// <summary>
        /// Create a new instance of the storage type.
        /// </summary>
        /// <returns></returns>
        private T Create()
        {
            T item = instantiateAction.Invoke();
            return item;
        }
    }
}