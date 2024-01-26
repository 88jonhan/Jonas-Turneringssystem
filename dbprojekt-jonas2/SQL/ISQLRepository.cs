using Dapper;

interface ISQLConnection
{

    public T Select<T>(int id);
    public List<dynamic> Select<T, U>(int id);
    public List<dynamic> SelectMultiple<T, U>(List<int> entities);
    public List<dynamic> SelectAll<T, U>(int id);
    public List<dynamic> SelectAll<T>();
    // public List<User> SelectAllUsers();
    // public List<Team> SelectAllTeams();
    // public List<League> SelectAllLeagues();
    // public List<Tournament> SelectAllTournaments();
    // public List<Match> SelectAllMatches();

    // public List<Team> SelectTeamsOnLeague(League league);
    // public List<Team> SelectTeamsOnTournament(Tournament tournament);

    // public List<League> SelectLeaguesOnTeam(Team team);
    // public List<Tournament> SelectTournamentsOnTeam(Team team);


    public bool SelectUserAuth(string username, string password, out User FoundUser);
    public int SelectCountTeamsOnUser(int id);
    public int SelectCountLeaguesOnUser(int id);
    public int SelectCountTournamentsOnUser(int id);


    //Add-method returns id of added entity
    public int Add<T>(DynamicParameters parameters);
    public bool Update<T>(T entity);
    public bool Delete<T>(int id);
}
