public static class ConsoleUtilites
{
    public static void SetConsoleParameters()
    {
        Console.OutputEncoding = Encoding.Unicode;

        //this is not optimal, but works for now
        //wont work on other platforms then windows
        Console.SetWindowSize(237, 63);
        //fullscreen for 1080*1920, 

        Console.CursorVisible = false;
        Console.Clear();
    }
}