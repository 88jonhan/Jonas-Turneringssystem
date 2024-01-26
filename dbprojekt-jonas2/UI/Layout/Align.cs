class Align
{
    // public static Align Default => new();
    // public Align()
    // {
    //     Vertical = Vertical.Top;
    //     Horizontal = Horizontal.Center;
    // }

    public Vertical Vertical;
    public Horizontal Horizontal;
    public int VerticalNudge;
    public int HorizontalNudge;

    public Align()
    {
        Vertical = Vertical.Top;
        Horizontal = Horizontal.Center;
        VerticalNudge = 0;
        HorizontalNudge = 0;
    }
    public Align(Vertical Vertical, Horizontal Horizontal, int VerticalNudge, int HorizontalNudge)
    {
        this.Vertical = Vertical;
        this.Horizontal = Horizontal;
        this.VerticalNudge = VerticalNudge;
        this.HorizontalNudge = HorizontalNudge;
    }


    // public static Align Left
    // {
    //     get
    //     {
    //         return Horizontal.Left;
    //     }
    // }
    // public static Align Top
    // {
    //     get
    //     {
    //         return new Align();
    //     }
    // }
    // public static Align Center
    // {
    //     get
    //     {
    //         return new Align();
    //     }
    // }
    // public static Align Right
    // {
    //     get
    //     {
    //         return new Align();
    //     }
    // }
}