using System.Collections.Generic;

namespace SimulationSystem
{
    public class MessageHandler
    {
        private List<IMessageReceiver> receivers;

        public MessageHandler()
        {
            receivers = new List<IMessageReceiver>();
        }

        /// <summary>
        /// Process a message and push it to all receivers.
        /// </summary>
        /// <param name="message"></param>
        public void ProcessMessage(Message message)
        {
            PushMessageToReceivers(message);
        }

        /// <summary>
        /// Add a recevier to the receiver list to allow it to receive messages.
        /// </summary>
        /// <param name="receiver"></param>
        public void AddReceiver(IMessageReceiver receiver)
        {
            if (!receivers.Contains(receiver))
            {
                receivers.Add(receiver);
            }
        }

        /// <summary>
        /// Remove a receiver from the receiver list to stop it receiving messages.
        /// </summary>
        /// <param name="receiver"></param>
        public void RemoveReceiver(IMessageReceiver receiver)
        {
            receivers.Remove(receiver);
        }

        /// <summary>
        /// Push the message to all registered receivers.
        /// </summary>
        /// <param name="message"></param>
        private void PushMessageToReceivers(Message message)
        {
            int route = message.Route;

            foreach (IMessageReceiver receiver in receivers)
            {
                if (receiver.IsRouteValid(route))
                {
                    receiver.ReceiveMessage(message);
                }
            }
        }
    }
}