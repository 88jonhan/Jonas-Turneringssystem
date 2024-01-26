class LayoutCollection : Layout
{
    public Dictionary<string, Layout> Layouts { get; set; } = new();
    public LayoutCollection()
    {
        Content = new() { Parent = this, Layouts = new(), Rows = new() };
    }
}



static class LayoutCollectionExtensions
{
    public static LayoutCollection Title(this LayoutCollection layoutCollection, string title)
    {
        layoutCollection.Title = title;
        return layoutCollection;
    }
    public static LayoutCollection Size(this LayoutCollection layoutCollection, int height, int width)
    {
        layoutCollection.Size = new Size(height, width);
        return layoutCollection;
    }
    public static LayoutCollection Position(this LayoutCollection layoutCollection, int row, int column)
    {
        layoutCollection.Position = new Position(row, column);
        return layoutCollection;
    }
    public static LayoutCollection Layouts(this LayoutCollection layoutCollection, List<string> layouts)
    {
        foreach (var layout in layouts)
        {
            Layout newLayout = new Layout()
            {
                Content = new() { Size = new(), Position = new(), Rows = new(), Layouts = new(), Align = new() }
            };
            newLayout.Content.Parent = newLayout;
            layoutCollection.Layouts.Add(layout, new Layout());
        }
        return layoutCollection;
    }

}