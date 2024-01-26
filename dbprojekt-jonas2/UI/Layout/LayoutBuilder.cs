class LayoutBuilder
{
    public LayoutCollection CreateMainLayouts()
    {
        LayoutCollection ConsoleWindow = new LayoutCollection()
            .Title("ConsoleWindow")
            .Size(Console.WindowHeight, Console.WindowWidth)
            .Position(0, 0)
            .Layouts(new() { "Main", "Login", "Menu", "Content", "MessageBoxRight", "MessageBoxLeft" });

        ConsoleWindow.Layouts["Main"]
            .Parent(ConsoleWindow)
            .Title("Main")
            .Size(50, 200)
            .Alignment(Vertical: Vertical.Top, Horizontal: Horizontal.Center)
            .Header("Jonas Coola Turneringssystem")
            .Border(left: 1, top: 1, right: 1, bottom: 1)
            .Show(true);
        ConsoleWindow.Layouts["Main"].Header
            .Position(Vertical: Vertical.Top, Horizontal: Horizontal.Center)
                .BackgroundColor(ConsoleColor.White)
                .TextColor(ConsoleColor.Black);


        ConsoleWindow.Layouts["Content"]
            .Parent(ConsoleWindow.Layouts["Main"])
            .Title("Content")
            .Size(ConsoleWindow.Layouts["Main"].Size.Height - 6, ConsoleWindow.Layouts["Main"].Size.Width - 4)
            .Alignment(Vertical: Vertical.Center, Horizontal: Horizontal.Center, VerticalNudge: 2)
            .Border(left: 1, top: 1, right: 1, bottom: 1)
            .Show(false);
        ConsoleWindow.Layouts["Content"].Content(
                new(),
                new Align(Vertical: Vertical.Center, Horizontal: Horizontal.Left, VerticalNudge: 0, HorizontalNudge: 0));

        ConsoleWindow.Layouts["MessageBoxRight"]
            .Parent(ConsoleWindow.Layouts["Main"])
            .Title("MessageBoxRight")
            .Size(5, 50)
            .Alignment(Vertical: Vertical.Top, Horizontal: Horizontal.Right, VerticalNudge: 1, HorizontalNudge: -2)
            .Border(left: 1, top: 1, right: 1, bottom: 1)
            .Show(true);
        ConsoleWindow.Layouts["MessageBoxRight"].Content(
                new(),
                new Align(Vertical: Vertical.Center, Horizontal: Horizontal.Right, VerticalNudge: 0, HorizontalNudge: 0));


        ConsoleWindow.Layouts["MessageBoxLeft"]
            .Parent(ConsoleWindow.Layouts["Main"])
            .Title("MessageBoxLeft")
            .Size(5, 50)
            .Alignment(Vertical: Vertical.Top, Horizontal: Horizontal.Left, VerticalNudge: 1, HorizontalNudge: 2)
            // .Position(1, 1)
            .Border(left: 1, top: 1, right: 1, bottom: 1)
            .Show(true);
        ConsoleWindow.Layouts["MessageBoxLeft"].Content(
                new(),
                new Align(Vertical: Vertical.Center, Horizontal: Horizontal.Left, VerticalNudge: 0, HorizontalNudge: 0));

        ConsoleWindow.Layouts["Login"]
            .Parent(ConsoleWindow.Layouts["Main"])
            .Title("Login")
            .Size(8, 20)
            .Position(4, 0)
            //.Alignment(Vertical: Vertical.Center, Horizontal: Horizontal.Center)
            .Header("Login")
            .Border(left: 1, top: 0, right: 0, bottom: 0)
            .Show(true);
        ConsoleWindow.Layouts["Login"].Header
                .Position(Vertical: Vertical.Center, Horizontal: Horizontal.Left);
        ConsoleWindow.Layouts["Login"].Content(
            new() { "Username", "", "Password", "" },
            new Align(Vertical: Vertical.Center, Horizontal: Horizontal.Left, VerticalNudge: 1, HorizontalNudge: 4),
            StaticSize: true);

        ConsoleWindow.Layouts["Menu"]
            .Parent(ConsoleWindow.Layouts["Main"])
            .Title("Menu")
            .Size(8, 20, Static: true)
            .Position(4, 0)
            //.Alignment(Vertical: Vertical.Center, Horizontal: Horizontal.Center)
            .Header("Menu")
            .Border(left: 1, top: 1, right: 1, bottom: 1)
            .Show(false);
        ConsoleWindow.Layouts["Menu"].Header
            .Position(Vertical: Vertical.Center, Horizontal: Horizontal.Left);
        ConsoleWindow.Layouts["Menu"].Content(
            new() { "", "", "", "", "" },
            new Align(Vertical: Vertical.Center, Horizontal: Horizontal.Left, VerticalNudge: 0, HorizontalNudge: 0));

        return ConsoleWindow;
    }

    // public void AddLayout(Layout layout)
    // {
    //     Layouts.Add(layout.Title, layout);
    // }
    // public void RemoveLayout(Layout layout)
    // {
    //     Layouts.Remove(layout.Title);
    // }
    // public Layout SelectLayout(string layoutTitle)
    // {
    //     try
    //     {
    //         return Layouts[layoutTitle];
    //     }
    //     catch (Exception e)
    //     {
    //         throw new Exception("Failed to find layout", e);
    //     }
    // }
}