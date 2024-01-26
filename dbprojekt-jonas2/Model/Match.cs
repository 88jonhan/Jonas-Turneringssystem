class Match : Entity, IMenuItem
{
    #region Private fields and properties



    #endregion

    #region Public fields and properties

    public int TournamentId;
    public int GroupNr;

    public Team HomeTeam;
    public Team AwayTeam;

    public int HomeScore = 0;
    public int AwayScore = 0;

    public bool IsPlayoff = false;
    public bool IsPlayed = false;

    public int PlayoffLevel;

    public List<Exception> Exceptions = new();

    #endregion

    #region Constructor
    //groupstage match
    private Match(Team homeTeam, Team awayTeam, int tournamentId, int groupNr)
    {
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
        TournamentId = tournamentId;
        GroupNr = groupNr;
    }
    //playoff match
    private Match(Team homeTeam, Team awayTeam, int tournamentId, int playoffLevel, bool isPlayoff = false)
    {
        if (!isPlayoff)
        {
            throw new Exception("This constructor is only for playoff matches");
        }
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
        TournamentId = tournamentId;
        IsPlayoff = isPlayoff;
        PlayoffLevel = playoffLevel;
    }
    #endregion

    #region Private methods



    #endregion

    #region Public methods

    public static Match CreateNewMatch(Team HomeTeam, Team AwayTeam, int tournamentId, int groupId)
    {
        return new(HomeTeam, AwayTeam, tournamentId, groupId);
    }
    public static Match CreateNewPlayoffMatch(Team HomeTeam, Team AwayTeam, int tournamentId, int playoffLevel)
    {
        return new(HomeTeam, AwayTeam, tournamentId, playoffLevel, true);
    }
    public bool SetMatchAsPlayed()
    {
        return IsPlayed ? throw new Exception("Match already set to played") : (IsPlayed = true);
    }
    public override string ToString()
    {
        return IsPlayed ? $"{HomeTeam} ({HomeScore}) - {AwayTeam} ({AwayScore})" : $"{HomeTeam} - {AwayTeam}";
    }

    public void Show()
    {
        Console.WriteLine("Show Matches");
    }
    public void Create()
    {
        Console.WriteLine("Create Match");
    }
    public void Change()
    {
        Console.WriteLine("Change Change");
    }
    public void Remove()
    {
        Console.WriteLine("Remove Match");
    }

    #endregion
}