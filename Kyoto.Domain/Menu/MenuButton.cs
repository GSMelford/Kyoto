namespace Kyoto.Domain.Menu;

public class MenuButton
{
    public Guid Id { get; private set; }
    public string Text { get; private set; }
    public string Code { get; private set; }
    public Guid? MenuPanelId { get; private set; }
    public bool IsCommand { get; private set; }
    public bool IsEnable { get; private set; }
    public int Index { get; private set; }
    public int Line { get; private set; }

    private MenuButton(
        Guid id, 
        string text, 
        string code,
        Guid? menuPanelId,
        bool isCommand,
        bool isEnable,
        int index,
        int line)
    {
        Id = id;
        Text = text;
        Code = code;
        MenuPanelId = menuPanelId;
        IsCommand = isCommand;
        IsEnable = isEnable;
        Index = index;
        Line = line;
    }

    public static MenuButton Create(
        Guid id, 
        string text, 
        string code, 
        Guid? menuPanelId, 
        bool isCommand, 
        bool isEnable, 
        int index, 
        int line)
    {
        return new MenuButton(id, text, code, menuPanelId, isCommand, isEnable, index, line);
    }
}