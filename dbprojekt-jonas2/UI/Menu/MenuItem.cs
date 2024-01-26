using System.Reflection;

class MenuItem
{
    public string Title { get; set; }
    public string Action { get; set; }
    public string sType { get; set; }

    public Action<Cookie, string, Layout> Execute { get; set; }
    // public Action Return { get; set; }

    public MenuItem Parent { get; set; }
    public string sParent { get; set; }

    public List<MenuItem> Children { get; set; } = new();

    // public MenuItem(string Title, string Action, List<MenuItem> Children)
    // {
    //     this.Children = Children;
    //     this.Title = Title;
    //     Execute = this.GetType().GetMethod(Action);
    // }
}