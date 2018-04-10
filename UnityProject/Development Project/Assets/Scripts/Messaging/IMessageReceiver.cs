namespace SimulationSystem
{
    public interface IMessageReceiver
    {
        bool IsRouteValid(int route);
        void ReceiveMessage(Message message);
    }
}