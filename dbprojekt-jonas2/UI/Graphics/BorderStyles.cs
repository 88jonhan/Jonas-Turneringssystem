using System.Reflection;
public class BorderStyles
{
    public Dictionary<string, char> GetBorderStyle(string borderStyle)
    {
        if (borderStyle == "")
        {
            return Single;
        }
        PropertyInfo? property;
        Type t = GetType();

        try
        {
            property = t.GetProperty(borderStyle);
        }
        catch
        {
            return Single;
        }
        if (property == null)
        {
            return Single;
        }
        var style = (Dictionary<string, char>?)property.GetValue(this);
        if (style == null)
        {
            return Single;
        }
        else
        {
            return style;
        }
    }
    public static readonly Dictionary<string, char> Single = new()
    {
        ["TopLeft"] = '┌',
        ["Top"] = '─',
        ["TopRight"] = '┐',
        ["Right"] = '│',
        ["BottomRight"] = '┘',
        ["Bottom"] = '─',
        ["BottomLeft"] = '└',
        ["Left"] = '│'
    };

    public static readonly Dictionary<string, char> SingleThick = new()
    {
        ["TopLeft"] = '▛',
        ["Top"] = '▀',
        ["TopRight"] = '▜',
        ["Right"] = '▐',
        ["BottomRight"] = '▟',
        ["Bottom"] = '▄',
        ["BottomLeft"] = '▙',
        ["Left"] = '▌'
    };

    public static readonly Dictionary<string, char> Double = new()
    {
        ["TopLeft"] = '╔',
        ["Top"] = '═',
        ["TopRight"] = '╗',
        ["Right"] = '║',
        ["BottomRight"] = '╝',
        ["Bottom"] = '═',
        ["BottomLeft"] = '╚',
        ["Left"] = '║'
    };
}