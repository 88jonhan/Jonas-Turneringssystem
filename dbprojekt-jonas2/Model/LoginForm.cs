class LoginForm
{
    Cookie Cookie;
    Layout Parent;
    public string Username = "";
    private string Password = "";
    int ActiveRow;
    int ActiveColumn;

    public LoginForm(Cookie cookie, Layout parent)
    {
        Cookie = cookie;
        Parent = parent;
    }

    public void Run()
    {
        Console.CursorVisible = true;
        ActiveRow = 1;
        ActiveColumn = Parent.Content.Position.Column;
        while (!Cookie.LoggedIn)
        {
            UserLogin(Cookie);
        }
    }

    public bool UserLogin(Cookie cookie)
    {
        Console.SetCursorPosition(ActiveColumn, Parent.Content.Position.Row + ActiveRow);
        string InputString = ActiveRow == 1 ? Username : Password;
        var input = Console.ReadKey(intercept: true);
        bool FoundUser = false;
        bool Searched = false;
        switch (input.Key)
        {
            case ConsoleKey.Enter:
                if (Username == "" && Password == "")
                {
                    break;
                }
                if (Username != "" && Password == "")
                {
                    ActiveRow = 3;
                    ActiveColumn = Parent.Content.Position.Column;
                    break;
                }
                if (Username == "" && Password != "")
                {
                    ActiveRow = 1;
                    ActiveColumn = Parent.Content.Position.Column;
                    break;
                }
                Searched = true;
                FoundUser = User.IsAuthenticated(Cookie, Username, Password);
                break;
            case ConsoleKey.LeftArrow:
                ActiveColumn = ActiveColumn == 0 ? ActiveColumn : ActiveColumn - 1;
                break;
            case ConsoleKey.RightArrow:
                ActiveColumn = ActiveColumn == 0 + Parent.Size.Width ? ActiveColumn + Parent.Size.Width : ActiveColumn + 1;
                break;
            case ConsoleKey.UpArrow:
                if (ActiveRow == 3)
                {
                    ActiveRow = 1;
                    ActiveColumn = Username.Length;
                }
                break;
            case ConsoleKey.DownArrow:
                if (ActiveRow == 1)
                {
                    ActiveRow = 3;
                    ActiveColumn = Password.Length;
                }
                break;
            case ConsoleKey.Backspace:
                if (InputString.Length > 0)
                {
                    ActiveColumn = ActiveColumn == 0 ? ActiveColumn : ActiveColumn - 1;
                    InputString = InputString[..^1];
                    if (ActiveRow == 1)
                    {
                        Username = InputString;
                    }
                    else
                    {
                        Password = InputString;
                    }
                }
                break;
            case ConsoleKey.Escape:
                break;
            default:
                ActiveColumn++;
                InputString += input.KeyChar;
                if (ActiveRow == 1)
                {
                    Username = InputString;
                }
                else
                {
                    Password = InputString;
                }
                break;
        }

        if (FoundUser)
        {
            Cookie.LoggedIn = true;
            return true;
        }
        else if (!FoundUser && Searched)
        {
            Cookie.Renderer.Print("Failed to find a user with that username and/or password", Cookie.Layouts["MessageBoxRight"]);
            Username = "";
            Password = "";
            ActiveRow = 1;
            ActiveColumn = Parent.Content.Position.Column;
            Reset();
        }
        else if (!FoundUser)
        {
            UpdateContent();
        }
        return false;
    }
    public void UpdateContent()
    {
        Parent.Content.Rows[ActiveRow] = ActiveRow == 1 ? Username : HideText(Password);
        Cookie.Renderer.UpdateContent(Parent.Content, ActiveRow);
    }
    public void Reset()
    {
        Parent.Content.Rows = new() { "Username", "", "Password", "" };
        Cookie.Renderer.UpdateContent(Parent.Content, new List<int>() { 1, 3 });
    }
    public string HideText(string input)
    {
        string Output = string.Empty;
        foreach (char c in input)
        {
            Output += "*";
        }
        return Output;
    }
}