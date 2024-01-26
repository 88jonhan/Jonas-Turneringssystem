class Border
{
    public Dictionary<string, char> Style { get; set; }
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;

    public Border(int left, int top, int right, int bottom, string borderStyle = "")
    {
        var style = new BorderStyles().GetBorderStyle(borderStyle);
        Style = style ?? BorderStyles.Single;

        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }
}
