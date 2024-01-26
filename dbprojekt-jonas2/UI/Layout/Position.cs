class Position
{
    public int Row;
    public int Column;

    public Position()
    {
        Row = 0;
        Column = 0;
    }
    public Position(int row, int column)
    {
        Row = row;
        Column = column;
    }
    public Position(Position position)
    {
        Row = position.Row;
        Column = position.Column;
    }

    public Position(IRenderable Parent, IRenderable child, Align align)
    {
        switch (align.Vertical)
        {
            case Vertical.Top:
                Parent.Position.Row += int.Max(0, child.Size.Height - align.VerticalNudge);
                Row = Parent.Position.Row - child.Size.Height + align.VerticalNudge;
                break;
            case Vertical.Center:
                Row = Parent.Position.Row + (Parent.Size.Height / 2) - (child.Size.Height / 2) + align.VerticalNudge;
                break;
            case Vertical.Bottom:
                Parent.Position.Row -= child.Size.Height + align.VerticalNudge;
                Row = Parent.Position.Row + Parent.Size.Height + child.Size.Height - align.VerticalNudge;
                break;
        }
        switch (align.Horizontal)
        {
            case Horizontal.Left:
                //Parent.Position.Column += child.Size.Width;
                Column = Parent.Position.Column - child.Size.Width + align.HorizontalNudge;
                break;
            case Horizontal.Center:
                Column = Parent.Position.Column + (Parent.Size.Width / 2) - (child.Size.Width / 2) + align.HorizontalNudge;
                break;
            case Horizontal.Right:
                //Parent.Position.Column -= child.Size.Width;
                Column = Parent.Position.Column + Parent.Size.Width + child.Size.Width + align.HorizontalNudge;
                break;
        }
    }

    public static Position operator +(Position left, Position right)
    {
        return new(left.Row + right.Row, left.Column + right.Column);
    }
}