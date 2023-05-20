namespace Kyoto.Domain.PreparedMessagesSystem;

public class PostEvent
{
    public string Name { get; private set; }
    public PostEventCode Code { get; private set; }

    private PostEvent(string name, PostEventCode code)
    {
        Name = name;
        Code = code;
    }

    public static PostEvent Create(string name, PostEventCode code)
    {
        return new PostEvent(name, code);
    }
}