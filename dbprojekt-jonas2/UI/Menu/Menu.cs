abstract class Menu
{
    public Layout Parent;
    public Cookie Cookie;

    public Menu(Cookie cookie, Layout parent)
    {
        Parent = parent;
        Cookie = cookie;
    }

    public List<MenuItem> MenuItems;
    public abstract void LoadMenuItems();

    readonly string Active = ">>>";
    readonly string Inactive = "   ";

    bool MenuActive = true;
    bool NewMenu = true;

    int PrevOptionIndex = 0;
    int ActiveOptionIndex = 0;

    string PrevOptionTitle;
    string ActiveOptionTitle;

    public void Run(Cookie cookie, Layout contentLayout)
    {
        Console.CursorVisible = false;
        while (MenuActive)
        {
            if (NewMenu)
            {
                ActiveOptionIndex = 0;
                Parent.Content.Rows = new List<string>();
                for (int i = 0; i < MenuItems.Count; i++)
                {
                    string ItemString;
                    if (ActiveOptionIndex == i)
                    {
                        ActiveOptionTitle = MenuItems[i].Title;
                        PrevOptionTitle = ActiveOptionTitle;
                        PrevOptionIndex = ActiveOptionIndex;
                        ItemString = Active + ActiveOptionTitle;
                    }
                    else
                    {
                        ItemString = Inactive + MenuItems[i].Title;
                    }
                    Parent.Content.Rows.Add(ItemString);
                }
                NewMenu = false;
                ClearAndUpdate();
            }
            else
            {
                UpdateContent();
            }

            ConsoleKeyInfo input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    PrevOptionIndex = ActiveOptionIndex;
                    ActiveOptionIndex = ActiveOptionIndex == 0 ? MenuItems.Count - 1 : ActiveOptionIndex - 1;
                    break;
                case ConsoleKey.DownArrow:
                    PrevOptionIndex = ActiveOptionIndex;
                    ActiveOptionIndex = ActiveOptionIndex == MenuItems.Count - 1 ? 0 : ActiveOptionIndex + 1;
                    break;
                case ConsoleKey.Enter:
                    switch (MenuItems[ActiveOptionIndex].Action)
                    {
                        case "SubMenu":
                            MenuItems = MenuItems[ActiveOptionIndex].Children;
                            NewMenu = true;
                            break;
                        default:
                            //MenuActive = false;
                            MenuItems[ActiveOptionIndex].Execute(cookie, MenuItems[ActiveOptionIndex].sType, contentLayout);
                            //Console.ReadLine();
                            break;
                    }
                    break;
                case ConsoleKey.Escape:
                case ConsoleKey.Backspace:
                    if (MenuItems[ActiveOptionIndex].Parent.Parent == null)
                    {
                        break;
                    }
                    MenuItems = MenuItems[ActiveOptionIndex].Parent.Parent.Children;
                    NewMenu = true;
                    ActiveOptionIndex = 0;
                    break;
                default:
                    continue;
            }
        }
    }
    public void UpdateContent()
    {
        List<int> RowsToUpdate = new();
        if (PrevOptionTitle != null)
        {
            Parent.Content.Rows[PrevOptionIndex] = $"{Inactive}{MenuItems[PrevOptionIndex].Title}";
            RowsToUpdate.Add(PrevOptionIndex);
        }
        Parent.Content.Rows[ActiveOptionIndex] = $"{Active}{MenuItems[ActiveOptionIndex].Title}";
        RowsToUpdate.Add(ActiveOptionIndex);
        Cookie.Renderer.UpdateContent(Parent.Content, RowsToUpdate);
    }
    public void ClearAndUpdate()
    {
        Cookie.Renderer.ClearContent(Parent);
        Cookie.Renderer.UpdateContent(Parent.Content);
    }
}