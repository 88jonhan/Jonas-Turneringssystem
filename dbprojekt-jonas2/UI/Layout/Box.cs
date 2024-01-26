abstract class Box : IRenderable
{
    public Size Size { get; set; }
    public Position Position { get; set; }
    public bool StaticPosition { get; set; }
    public bool Show { get; set; } = false;

    public Align Align;

    public Layout Parent { get; set; }

    public string Title;
    public Content Content;

    public Border Border { get; set; }
    public Header Header { get; set; }

    public bool ShowBorder { get; set; } = false;
    public bool ShowHeader { get; set; } = false;

    public ConsoleColor BorderColor { get; set; } = ConsoleColor.White;
    public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;
    public ConsoleColor TextColor { get; set; } = ConsoleColor.White;

    // public Box(Box? parent)
    // {
    //     if (parent == null)
    //     {
    //         Parent.Position = new Position(0, 0);
    //         Parent.Size = new Size(0, 0);
    //     }
    //     else
    //     {
    //         Parent.Position = parent.Position;
    //         Parent.Size = parent.Size;
    //     }
    // }
}