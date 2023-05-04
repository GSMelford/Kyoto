using System.ComponentModel.DataAnnotations;

namespace Kyoto.Bot.Core.Database;

public abstract class BaseModel
{
    private Guid _id;
    [Key]
    public virtual Guid Id {
        get {
            _id = _id == Guid.Empty ? Guid.NewGuid() : _id;
            return _id;
        }
        set => _id = value;
    }

    public virtual DateTime? CreationTime { get; set; } = DateTime.UtcNow;
    public virtual DateTime? ModificationTime { get; set; } = DateTime.UtcNow;
}