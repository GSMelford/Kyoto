using Kyoto.Domain.PreparedMessagesSystem;

namespace Kyoto.Dto.PreparedMessageSystem;

public class PreparedMessageDto
{
    public PostEventCode PostEventCode { get; set; }
    public string Text { get; set; } = null!;
    public DateTime? TimeToSend { get; set; }
}