using System.Text.Json;
using System.Reflection;
using System.ComponentModel;
using System.Security.AccessControl;

class MainMenu : Menu
{
    public MainMenu(Cookie cookie, Layout parent) : base(cookie, parent)
    {
        LoadMenuItems();
    }

    public override void LoadMenuItems()
    {
        var jsonPath = "UI/Menu/MainMenu.json";
        MenuItem MainMenu = JsonSerializer.Deserialize<MenuItem>(File.ReadAllText(jsonPath));
        foreach (var item in MainMenu.Children)
        {
            if (item.Action == "SubMenu")
            {
                item.Parent = MainMenu;
                foreach (var child in item.Children)
                {
                    MainMenuItem mainMenuItem = new MainMenuItem(child);
                    child.Execute = (Cookie cookie, string Type, Layout contentLayout) => MainMenuItem.GetMethod(child.Action).Invoke(mainMenuItem, new object[] { cookie, item.sType, contentLayout });
                    child.Parent = item;
                }
            }
            else
            {
                MainMenuItem mainMenuItem = new MainMenuItem(item);
                item.Execute = (Cookie cookie, string Type, Layout contentLayout) => MainMenuItem.GetMethod(item.Action).Invoke(mainMenuItem, new object[] { cookie, item.sType, contentLayout });
            }
        }
        MenuItems = MainMenu.Children;
    }

}

class MainMenuItem
{
    public Type ItemType { get; set; }

    public MainMenuItem(MenuItem item)
    {
        ItemType = Type.GetType(item.sType);
    }
    public void Manage(Cookie cookie, string Type, Layout contentLayout)
    {
        bool Success = false;
        List<dynamic> Objects = new();
        try
        {
            switch (Type)
            {
                case "Team":
                    Objects = cookie.SQLRepo.SelectAll<Team, User>(cookie.ActiveUser.Id);
                    break;
                case "Tournament":
                    Objects = cookie.SQLRepo.SelectAll<Tournament, User>(cookie.ActiveUser.Id);
                    break;
                case "League":
                    Objects = cookie.SQLRepo.SelectAll<League, User>(cookie.ActiveUser.Id);
                    break;
            }
        }
        catch (Exception e)
        {
            cookie.Renderer.Print(e.ToString(), cookie.Layouts["MessegeBoxRight"]);
            Success = false;
        }
        if (Success)
        {
            cookie.Renderer.ShowContent(Objects, contentLayout);
            cookie.Renderer.Print($"Found {Objects.Count} objects!".ToString(), cookie.Layouts["MessageBoxRight"]);
        }

    }
    public void Create(Cookie cookie, string Type, Layout contentLayout)
    {
        Console.WriteLine($"Create {ItemType}");
    }
    public void Change(Cookie cookie, string Type, Layout contentLayout)
    {
        Console.WriteLine($"Change {ItemType}");
    }
    public void Remove(Cookie cookie, string Type, Layout contentLayout)
    {
        Console.WriteLine($"Remove {ItemType}");
    }
    public void LogOut(Cookie cookie, string Type, Layout contentLayout)
    {
        Environment.Exit(0);
    }

    public static MethodInfo GetMethod(string Action)
    {
        var type = Type.GetType("MainMenuItem");
        var method = type.GetMethod(Action);
        return method;
    }
}