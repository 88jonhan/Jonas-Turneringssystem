class Cookie
{
    public ISQLConnection SQLRepo;
    public Renderer Renderer;
    public Dictionary<string, Layout> Layouts;

    public bool LoggedIn = false;
    public DateTime LoginTime;
    public User ActiveUser;

    public int TeamCount { get; set; } = 0;
    public int LeagueCount { get; set; } = 0;
    public int TournamentCount { get; set; } = 0;

    public Cookie(bool loggedIn)
    {
        LoggedIn = loggedIn;
    }
    public Cookie(ISQLConnection SQLRepo, Renderer renderer, Dictionary<string, Layout> layouts)
    {
        this.SQLRepo = SQLRepo;
        Renderer = renderer;
        Layouts = layouts;
    }

    public Cookie(ISQLConnection SQLRepo, User activeUser, (int teamCount, int leagueCount, int tournamentCount) DBDataCount)
    {
        this.SQLRepo = SQLRepo;
        LoginTime = DateTime.Now;
        LoggedIn = true;
        ActiveUser = activeUser;
        TeamCount = DBDataCount.teamCount;
        LeagueCount = DBDataCount.leagueCount;
        TournamentCount = DBDataCount.tournamentCount;
    }

    public static Cookie EmptyCookie()
    {
        return new(loggedIn: false);
    }
}