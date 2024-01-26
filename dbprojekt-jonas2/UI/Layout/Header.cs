class Header : IRenderable
{
    public Position Position { get; set; } = new();
    public Size Size { get; set; } = new();
    public string FontSize;

    public Layout Parent { get; set; } = new();

    public string Text = "";
    public string[] Content;
    public Align Align = new();

    public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;
    public ConsoleColor TextColor { get; set; } = ConsoleColor.White;

    public Header(string headerText, string size)
    {
        Text = headerText;
        if (size == "big")
        {
            FontSize = "big";
            Content = ConvertToFont();
            Size = new(Content.Length, Content.First().Length);
        }
        else
        {
            FontSize = "small";
            Content = new string[] { headerText };
            Size = new(1, headerText.Length);
        }
    }

    public string[] ConvertToFont(int fontSize = 3)
    {
        Dictionary<char, string[]> ActiveLetters = fontSize == 3 ? Fonts.LettersSmall : Fonts.LettersBig;
        string headerText = Text.ToUpper();
        string[] headerContent = new string[fontSize];
        for (int i = 0; i < fontSize; i++)
        {
            string tempstring = " ";
            foreach (char c in headerText)
            {
                tempstring += c == ' ' ? " " : ActiveLetters[c][i] + " ";
            }
            headerContent[i] = tempstring;
        }
        return headerContent;
    }
}

static class HeaderExtensions
{
    public static Header Position(this Header header, Vertical Vertical, Horizontal Horizontal, int VerticalNudge = 0, int HorizontalNudge = 0)
    {
        header.Align = new(Vertical, Horizontal, VerticalNudge, HorizontalNudge);
        header.Position = new Position(header.Parent, header, header.Align);
        return header;
    }
    public static Header BackgroundColor(this Header header, ConsoleColor color)
    {
        header.BackgroundColor = color;
        return header;
    }
    public static Header TextColor(this Header header, ConsoleColor color)
    {
        header.TextColor = color;
        return header;
    }
}

// public override void UpdateContent()
// {
//     int StartRow = Console.CursorTop;
//     int StartColumn = Console.CursorLeft;
//     switch (RelativePosition)
//     {
//         case "Left":
//             StartColumn -= Content.First().Count();
//             break;
//         case "Right":
//             StartColumn += Parent.Size.Width - 1;
//             break;

//         case "Top":
//             StartRow -= Content.Count() - 1;
//             break;
//         case "Bottom":
//             StartRow += Parent.Size.Height + 1;
//             break;
//         case "Center":
//             StartRow += Parent.Size.Height / 2;
//             break;

//     }
//     switch (RelativeAlignment)
//     {
//         case "Left":
//             break;
//         case "Right":
//             StartColumn += Parent.Size.Width - Content.First().Count() + 1;
//             break;

//         case "Top":
//             break;
//         case "Bottom":
//             StartRow += Parent.Size.Height - Content.Count() + 1;
//             break;

//         case "Center":
//             switch (RelativePosition)
//             {
//                 case "Left":
//                 case "Right":
//                     StartRow += (Parent.Size.Height / 2) - (Content.Count() / 2);
//                     break;
//                 case "Top":
//                 case "Bottom":
//                     StartColumn += (Parent.Size.Width / 2) - (Content.First().Count() / 2);
//                     break;
//                 case "Center":
//                     StartColumn += (Parent.Size.Width / 2) - (Content.First().Count() / 2);
//                     break;
//             }
//             break;
//     }
//     Console.SetCursorPosition(StartColumn, StartRow);
//     Render();
// }
