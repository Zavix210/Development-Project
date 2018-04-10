public class Message
{
    private int route;
    private string identifier;
    private object data;

    public int Route { get { return route; } }
    public string Identifier { get { return identifier; } }
    public object Data { get { return data; } }

    public Message(int route, string identifier, object data)
    {
        this.route = route;
        this.identifier = identifier;
        this.data = data;
    }

    public T GetDataAs<T>()
    {
        return (T)data;
    }
}
