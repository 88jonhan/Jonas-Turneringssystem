class TournamentGroup : Entity, IMenuItem
{
    #region Private fields and properties

    private int tournamentId;
    private int groupNr;
    private List<Team> teams;
    private List<Match> matches;
    private Random random = new();

    #endregion
    #region Public fields and properties

    public int TournamentId
    {
        get
        {
            return tournamentId;
        }
    }
    public int GroupNr
    {
        get
        {
            return groupNr;
        }
    }
    public List<Team> Teams
    {
        get
        {
            return teams;
        }
    }
    public List<Match> Matches
    {
        get
        {
            return matches;
        }
    }

    #endregion
    #region Constructor

    private TournamentGroup(int groupNr)
    {
        this.groupNr = groupNr;
    }

    #endregion
    #region Private methods



    #endregion
    #region Public methods

    public void Show()
    {
        Console.WriteLine("Show Groups");
    }
    public void Create()
    {
        Console.WriteLine("Create Group");
    }
    public void Change()
    {
        Console.WriteLine("Change Group");
    }
    public void Remove()
    {
        Console.WriteLine("Remove Group");
    }

    public static TournamentGroup CreateNewTournamentGroup(int groupId)
    {
        return new(groupId);
    }

    public bool GenerateMatches()
    {
        for (int i = 0; i <= Teams.Count - 1; i++)
        {
            for (int j = i + 1; j <= Teams.Count - 1; j++)
            {
                //TODO:Matchmetod som skapar matcher beroende på användarens val/turneringens inställningar?

                //randomizes home/away
                Team[] randomTeams = { Teams[i], Teams[j] };
                randomTeams = randomTeams.OrderBy(team => random.Next()).ToArray();
                Matches.Add(Match.CreateNewMatch(randomTeams[0], randomTeams[1], Id, groupNr));
            }
        }
        return true;
    }
    #endregion
}

