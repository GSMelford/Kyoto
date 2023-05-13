namespace Kyoto.Database.CommonModels;

public class MenuPanel : BaseModel
{
    public string Name { get; set; } = null!;
    public ICollection<MenuButton> Buttons { get; set; } = new List<MenuButton>();
}