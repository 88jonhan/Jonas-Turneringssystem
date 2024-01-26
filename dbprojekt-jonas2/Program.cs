internal class Program
{
    private static void Main(string[] args)
    {
        #region StartUp
        ConsoleUtilites.SetConsoleParameters();
        var Layouts = new LayoutBuilder().CreateMainLayouts().Layouts;

        Renderer Renderer = new();
        SQLRepository SQLRepo = new();
        Cookie Cookie = new(SQLRepo, Renderer, Layouts);
        #endregion

        //Draw main layout
        Renderer.Draw(Layouts["Main"]);

        //Start login process
        var Login = new LoginForm(Cookie, parent: Layouts["Login"]);
        Login.Run();

        if (Cookie.LoggedIn)
        {
            //hide login
            Renderer.Hide(Layouts["Login"]);
            //show menu layout
            Layouts["Menu"].Show = true;

            //Create new main menu
            MainMenu MainMenu = new(Cookie, parent: Layouts["Menu"]);
            //Add user-information to content
            Cookie.ActiveUser.UpdateUserContent(Cookie, Layouts["Content"]);

            //Update new layouts
            Renderer.Draw(Layouts["Main"]);

            //run mainmenu
            MainMenu.Run(Cookie, Layouts["Content"]);
        }
    }
}