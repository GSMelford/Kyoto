namespace Kyoto.Database.CommonModels;

public class MenuButton : BaseModel
{
    public Guid MenuPanelId { get; set; }
    public MenuPanel? MenuPanel { get; set; }
    public string Text { get; set; } = null!;
    public string Code { get; set; } = null!;
    public bool IsCommand { get; set; }
    public bool IsEnable { get; set; }
    public int Index { get; set; }
    public int Line { get; set; }
}