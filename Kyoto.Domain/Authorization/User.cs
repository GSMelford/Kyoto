namespace Kyoto.Domain.Authorization;

public class User
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string? LastName { get; private set; }
    public long TelegramId { get; private set; }
    public string Username { get; private set; }
    public string PhoneNumber { get; private set; }

    private User(Guid id, string firstName, string? lastName, long telegramId, string username, string phoneNumber)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        TelegramId = telegramId;
        Username = username;
        PhoneNumber = phoneNumber;
    }

    public static User Create(string firstName, string? lastName, long telegramId, string username, string phoneNumber)
    {
        return new User(Guid.NewGuid(), firstName, lastName, telegramId, username, phoneNumber);
    }
}