# Jonas-Turneringssystem
Har inte rensat så mycket i koden, så finns mycket kod som inte används och säkerligen dubletter av vissa SQL-kommandon.

# Exempel-Dictionary med två värden
public static Dictionary<string, string> SQLQueries = new()
{
        {   "SelectUsersFromTeam", @"  
            SELECT * FROM Users
            INNER JOIN Teams ON Teams.userid = Users.id
            WHERE Teams.id = @id AND Users.isDeleted = 0
        " },
        {   "SelectUsersFromLeague", @"  
            SELECT Users.* FROM Teams_Leagues
            INNER JOIN Leagues ON Teams_Leagues.leagueId = Leagues.id
            INNER JOIN Teams ON Teams_Leagues.teamId = Teams.id
            INNER JOIN Users ON Teams.userid = Users.id
            WHERE Leagues.id = @id AND Users.isDeleted = 0
        " },
}

# Min generiska SelectAll-metod:
public List<dynamic> SelectAll<GET, FROM>(int id)
{
    List<dynamic> list;
    try
    {
        string QueryKey = $"Select{typeof(GET).Name}sFrom{typeof(FROM).Name}";
        string Query = SQLQueries[QueryKey];
        list = Connection.Query<dynamic>(Query, new { id }).ToList();
    }
    catch (Exception e)
    {
        throw new Exception($"Failed to select {typeof(GET).Name}", e);
    }
    return list;
}

# För att hämta en användares alla lag skriver vi:
SelectAll<Team, User>(userId)
# För att hämta en turnerings alla användare skriver vi:
SelectAll<User, Tournament>(tournamentId)