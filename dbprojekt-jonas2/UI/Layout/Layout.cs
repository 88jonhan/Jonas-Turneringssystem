class Layout : Box
{


    public Layout()
    {
        if (Content == null)
        {
            Content = new()
            {
                Parent = this
            };
        }
        else
        {
            Content.Parent ??= this;
            Content.Rows ??= new();
            Content.Layouts ??= new();
        }
    }

    // public Layout(Layout parent = null, string parentTitle = "")
    // {
    //     if (parentTitle == "" && parent == null)
    //     {
    //         Parent = new Layout(0)
    //         {
    //             Position = new Position(0, 0),
    //             Size = new Size(0, 0)
    //         };
    //         Content = new()
    //         {
    //             Parent = this
    //         };
    //         return;
    //     }

    //parent ??= FindParent(parentTitle);

    // if (parent == default)
    // {
    //     throw new Exception("Failed to find parent");
    // }
    // else
    // {
    //     Parent = parent;

    //     parent.Content ??= new()
    //     {
    //         Parent = this
    //     };

    //     if (parent.Content.Layouts == null)
    //     {
    //         parent.Content.Layouts = new() { this };
    //     }
    //     else
    //     {
    //         parent.Content.Layouts.Add(this);
    //     }
    // }
    // }

    public void UpdatePosition()
    {
        this.Alignment(Align.Vertical, Align.Horizontal);
    }

    // public void Remove()
    // {
    //     LayoutCollection.RemoveLayout(this);
    //     Parent.Content.Layouts.Remove(this);
    //     Renderer.Hide(this);
    // }

}

static class LayoutExtensions
{
    public static Layout Parent(this Layout layout, Layout parent)
    {
        layout.Parent = parent;
        parent.Content.Layouts.Add(layout);
        return layout;
    }
    public static Layout Title(this Layout layout, string title)
    {
        layout.Title = title;
        return layout;
    }
    public static Layout Size(this Layout layout, int height, int width, bool Static = false)
    {
        layout.Size = new(height, width);
        layout.StaticPosition = Static;
        return layout;
    }

    public static Layout Show(this Layout layout, bool isShowed)
    {
        layout.Show = isShowed;
        return layout;
    }
    public static Layout Position(this Layout layout, int row, int column)
    {
        layout.Position = new(layout.Parent.Position.Row + row, layout.Parent.Position.Column + column);
        return layout;
    }

    public static Layout BackgroundColor(this Layout layout, ConsoleColor color)
    {
        layout.BackgroundColor = color;
        return layout;
    }
    public static Layout TextColor(this Layout layout, ConsoleColor color)
    {
        layout.TextColor = color;
        return layout;
    }
    public static Layout BorderColor(this Layout layout, ConsoleColor color)
    {
        layout.BorderColor = color;
        return layout;
    }
    public static Layout Header(this Layout layout, string headerText, string size = "big")
    {
        layout.Header = new Header(headerText, size)
        {
            Parent = layout
        };
        layout.ShowHeader = true;
        return layout;
    }

    public static Layout Border(this Layout layout, int left, int top, int right, int bottom)
    {
        layout.Border = new Border(left, top, right, bottom);
        layout.ShowBorder = true;
        return layout;
    }
    public static Layout Content(this Layout layout, List<string> content, Align align, bool StaticSize = false)
    {
        layout.Content.Parent = layout;
        layout.Content.Rows = content;
        layout.Content.Align = align;
        layout.Content.StaticSize = StaticSize;
        layout.Content.Align();
        return layout;
    }

    public static Layout Alignment(this Layout layout, Vertical Vertical, Horizontal Horizontal, int VerticalNudge = 0, int HorizontalNudge = 0)
    {
        layout.Align ??= new Align(Vertical, Horizontal, VerticalNudge, HorizontalNudge);

        layout.Position = new Position(0, 0);
        switch (Vertical)
        {
            case Vertical.Top:
                layout.Position.Row = layout.Parent.Position.Row + VerticalNudge;
                break;
            case Vertical.Center:
                layout.Position.Row = layout.Parent.Position.Row + (layout.Parent.Size.Height / 2) - (layout.Size.Height / 2) + VerticalNudge;
                break;
            case Vertical.Bottom:
                layout.Position.Row = layout.Parent.Position.Row + layout.Parent.Size.Height - layout.Size.Height + VerticalNudge;
                break;
        }
        switch (Horizontal)
        {
            case Horizontal.Left:
                layout.Position.Column = layout.Parent.Position.Column + HorizontalNudge;
                break;
            case Horizontal.Center:
                layout.Position.Column = layout.Parent.Position.Column + (layout.Parent.Size.Width / 2) - (layout.Size.Width / 2) + HorizontalNudge;
                break;
            case Horizontal.Right:
                layout.Position.Column = layout.Parent.Position.Column + layout.Parent.Size.Width - layout.Size.Width + HorizontalNudge;
                break;
        }
        return layout;
    }
}

