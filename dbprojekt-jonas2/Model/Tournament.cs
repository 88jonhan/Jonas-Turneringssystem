class Tournament : Entity, IMenuItem
{
    #region Private fields and properties
    private string name;
    private User creator;
    private League league;

    private DateTime started;
    private DateTime finished;

    private List<Team> teams;

    private List<TournamentGroup> groups;
    private List<Match> matches;

    #endregion

    #region Public fields and properties

    public string Name { get { return name; } set { name = value; } }
    public User Creator { get { return creator; } set { creator = value; } }
    public League League { get { return league; } set { league = value; } }

    public DateTime Started { get { return started; } set { started = value; } }
    public DateTime Finished { get { return finished; } set { finished = value; } }

    public List<Team> Teams { get { return teams; } set { teams = value; } }

    public List<TournamentGroup> Groups { get { return groups; } set { groups = value; } }
    //public List<Match> Matches { get { return matches; } }

    Random random = new Random();
    #endregion

    #region Constructor

    private Tournament(string name, List<Team> teams, User creator, League league)
    {
        this.teams = teams;
        this.name = name;
        this.creator = creator;
        this.league = league;
        groups = new();
    }

    public Tournament(int id, string name, DateTime created, DateTime started, DateTime finished)
    {
        Id = id;
        Name = name;
        Created = created;
        Started = started;
        Finished = finished;
    }

    #endregion

    #region Private methodss



    #endregion

    #region Public methods

    public static Tournament CreateNewTournament(string name, List<Team> teams, User creator, League league)
    {
        return new(name, teams, creator, league);
    }

    public void Show()
    {
        Console.WriteLine("Show Tournaments");
    }
    public void Create()
    {
        Console.WriteLine("Create Tournament");
    }
    public void Change()
    {
        Console.WriteLine("Change Tournament");
    }
    public void Remove()
    {
        Console.WriteLine("Remove Tournament");
    }

    public override string ToString()
    {
        if (started == DateTime.MinValue)
        {
            return $"Namn: {name}\nSkapad den: {Created}\nSkapad av: {creator}";
        }
        else if (started != DateTime.MinValue && finished == DateTime.MinValue)
        {
            return $"Namn: {name}\nStartad den: {started}\nSkapad den: {Created}\nSkapad av: {creator}";
        }
        else
        {
            return $"Namn: {name}\nStartad den: {started}\nAvslutad den: {finished}\nSkapad den: {Created}\nSkapad av: {creator}";
        }
    }

    //TODO: set and move to private
    public bool GenerateGroups()
    {
        if (Teams.Count < 7)
        { throw new Exception("Teams in tournament less than 7"); }
        else if (Teams.Count > 18)
        { throw new Exception("Teams in tournament is more than 18"); }

        groups = new List<TournamentGroup>();
        int groupCount = Teams.Count < 8 ? 1 : Teams.Count < 12 ? 2 : Teams.Count < 16 ? 3 : 4;

        for (int i = 1; i <= groupCount; i++)
        {
            groups.Add(TournamentGroup.CreateNewTournamentGroup(i));
        }
        return true;
    }

    public bool DistributeTeams()
    {
        var tempTeams = new List<Team>();
        tempTeams.AddRange(teams);

        while (tempTeams.Count > 0)
        {
            foreach (TournamentGroup group in Groups)
            {
                int randomTeamIndex;
                HashSet<int> usedIndexes = new();
                do
                { randomTeamIndex = random.Next(tempTeams.Count - 1); }
                while (usedIndexes.Contains(randomTeamIndex));

                group.Teams.Add(tempTeams[randomTeamIndex]);
                usedIndexes.Add(randomTeamIndex);
                tempTeams.Remove(Teams[randomTeamIndex]);
            }
        }
        return true;
    }

    //TODO:Set and move to private
    public bool StartTournament()
    {
        //TODO:Maybe an exception-function to check for errors in distribution?
        //tournament.CheckForExceptions();
        List<Exception> exceptions = new();
        if (started != DateTime.MinValue)
        {
            throw new Exception("Tournament is already started");
        }
        else if (finished != DateTime.MinValue)
        {
            throw new Exception("Tournament is aldready finished");
        }
        else if (Groups == null || Groups.Count < 1)
        {
            throw new Exception("Tournament has no groups");
        }
        else if (Groups.Any(group => group.Teams.Count < 1) || Groups.Any(group => group.Teams == null))
        {
            throw new Exception("One or more groups are empty");
        }
        else
        {
            started = DateTime.Now;
        }
        //TODO:Fixa mer inställningar för turneringarna
        for (int i = 0; i < Groups.Count; i++)
        {
            groups[i].GenerateMatches();
        }
        return true;
    }
    #endregion
}