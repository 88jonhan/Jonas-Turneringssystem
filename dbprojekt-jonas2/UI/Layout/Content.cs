class Content
{
    public virtual Size Size { get; set; } = new();
    public virtual Position Position { get; set; } = new();
    public bool StaticSize { get; set; }

    public Layout Parent;

    public Align Align { get; set; } = new();
    public List<Layout> Layouts { get; set; } = new();
    public List<string> Rows { get; set; } = new();
    public void UpdatePosition()
    {
        if (!StaticSize)
        {
            this.Align();
        }
    }

}

static class ContentExtensions
{
    public static Content Align(this Content content)
    {
        content.Align ??= new Align();

        int LongestRow = 0;

        if (content.Align.Horizontal == Horizontal.Center | content.Align.Horizontal == Horizontal.Right)
        {
            if (content.Rows.Count > 0)
            {
                LongestRow = content.Rows.OrderByDescending(x => x.Length).ToList().First().Length;
            }
        }

        content.Size ??= new();
        content.Size.Width = LongestRow;
        content.Size.Height = content.Rows.Count;

        content.Position ??= new();
        switch (content.Align.Vertical)
        {
            case Vertical.Top:
                content.Position.Row = content.Parent.Position.Row + 1 + content.Align.VerticalNudge;
                break;
            case Vertical.Center:
                content.Position.Row = content.Parent.Position.Row + (content.Parent.Size.Height / 2) - (content.Size.Height / 2) + content.Align.VerticalNudge;
                break;
            case Vertical.Bottom:
                content.Position.Row = content.Parent.Position.Row + content.Size.Height - content.Rows.Count + content.Align.VerticalNudge;
                break;
        }
        switch (content.Align.Horizontal)
        {
            case Horizontal.Left:
                content.Position.Column = content.Parent.Position.Column + 1 + content.Align.HorizontalNudge;
                break;
            case Horizontal.Center:
                content.Position.Column = content.Parent.Position.Column + (content.Parent.Size.Width - content.Size.Width) / 2 + content.Align.HorizontalNudge;
                break;
            case Horizontal.Right:
                content.Position.Column = content.Parent.Position.Column + content.Parent.Size.Width - content.Size.Width + content.Align.HorizontalNudge;
                break;
        }
        return content;
    }
}