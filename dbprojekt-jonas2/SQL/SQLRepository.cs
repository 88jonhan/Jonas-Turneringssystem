using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using Dapper;
using Microsoft.VisualBasic;
using static SqlQueriesService;
class SQLRepository : ISQLConnection
{
    public IDbConnection Connection;

    public SQLRepository()
    {
        try
        {
            Connection = new SqlConnection("Server=localhost,1433;User=sa;Password=apA123!#!;Database=dbprojekt-jonas;");
        }
        catch (Exception e)
        {
            throw new Exception("Failed to connect to database", e);
        }

    }
    #region Selects
    public bool SelectUserAuth(string username, string password, out User FoundUser)
    {
        FoundUser = default;
        try
        {
            FoundUser = Connection.QuerySingleOrDefault<User>(SQLQueries[$"SelectUserAuth"], new { Username = username, Password = password });
        }
        catch
        {
            return false;
        }
        return true;
    }
    public T Select<T>(int id)
    {
        T selectedEntity;
        try
        {
            selectedEntity = Connection.QuerySingleOrDefault(SQLQueries[$"Select{typeof(T).Name}"], new { Id = id, });
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to select {typeof(T).Name}", e);
        }
        return selectedEntity;
    }
    public List<dynamic> Select<U, T>(int id)
    {
        List<dynamic> selectedEntity;
        try
        {
            selectedEntity = Connection.Query<dynamic>(SQLQueries[$"Select{typeof(T).Name}sOn{typeof(U).Name}"], new { Id = id, }).ToList();
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to select {typeof(T).Name}", e);
        }
        return selectedEntity;
    }
    public List<dynamic> SelectMultiple<U, T>(List<int> entities)
    {
        return new List<dynamic>();
    }
    #region SelectAlls
    public List<User> SelectAllUsers()
    {
        List<User> allUsers;
        try
        {
            allUsers = Connection.Query<User>(SQLQueries[$"SelectAllUsers"]).ToList();
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to select Users", e);
        }
        return allUsers;
    }

    public List<dynamic> SelectAll<T>()
    {
        List<dynamic> list;
        try
        {
            string Query = SQLQueries[$"SelectAll"].Replace("@Table", typeof(T).Name);
            list = Connection.Query<dynamic>(Query).ToList();
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to select {typeof(T).Name}", e);
        }
        return list;
    }
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

    public List<League> SelectAllLeague()
    {
        List<League> allLeague;
        try
        {
            allLeague = Connection.Query<League, User, League>(SQLQueries[$"SelectAllLeague"], (league, user) =>
            {
                league.Creator = user;
                return league;
            }, splitOn: "id").ToList();
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to select League", e);
        }
        return allLeague;
    }

    public List<Tournament> SelectAllTournament()
    {
        List<Tournament> allTournament;
        try
        {
            allTournament = Connection.Query<Tournament, User, League, User, Tournament>(SQLQueries[$"SelectAllTournament"], (tournament, tournamentCreator, league, leagueCreator) =>
            {
                tournament.Creator = tournamentCreator;
                league.Creator = leagueCreator;
                tournament.League = league;
                return tournament;
            }, splitOn: "id, id, id").ToList();
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to select Tournament", e);
        }

        allTournament.ForEach(tournament =>
        {
            tournament.Teams = SelectTeamsOnTournament(tournament);
        });
        return allTournament;
    }

    public List<Match> SelectAllMatches()
    {
        List<Match> allMatches;
        try
        {
            allMatches = Connection.Query<Match, Team, User, Team, User, Match>(SQLQueries[$"SelectAllMatches"], (match, team1, user1, team2, user2) =>
            {
                team1.Owner = user1;
                team2.Owner = user2;
                match.HomeTeam = team1;
                match.AwayTeam = team2;
                return match;
            }, splitOn: "id, id, id, id").ToList();
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to select Matches", e);
        }
        return allMatches;
    }
    #endregion
    #region SelectJoins
    public List<Team> SelectTeamsOnLeague(League league)
    {
        List<Team> TeamsInLeague;
        try
        {
            TeamsInLeague = Connection.Query<Team, User, Team>(SQLQueries[$"SelectLeagueTeam"], (team, user) =>
            {
                team.Owner = user;
                return team;
            }, new { id = league.Id }, splitOn: "id").ToList();
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to select Team from {league.Name}", e);
        }
        return TeamsInLeague;
    }

    public List<Team> SelectTeamsOnTournament(Tournament tournament)
    {
        List<Team> TeamsInTournament;
        try
        {
            TeamsInTournament = Connection.Query<Team, User, Team>(SQLQueries[$"SelectTournamentTeam"], (team, user) =>
            {
                team.Owner = user;
                return team;
            }, new { id = tournament.Id }, splitOn: "id").ToList();
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to select Team from {tournament.Name}", e);
        }
        return TeamsInTournament;
    }

    public List<League> SelectLeaguesOnTeam(Team team)
    {
        List<League> allLeagues;
        try
        {
            allLeagues = Connection.Query<League, User, League>(SQLQueries[$"SelectAllLeague"], (league, user) =>
            {
                league.Creator = user;
                return league;
            }, new { TeamId = team.Id }, splitOn: "id").ToList();
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to select League", e);
        }
        return allLeagues;
    }
    public List<Tournament> SelectTournamentsOnTeam(Team team)
    {
        List<Tournament> allTournaments;
        try
        {
            allTournaments = Connection.Query<Tournament, User, League, User, Tournament>(SQLQueries[$"SelectAllTournament"], (tournament, tournamentCreator, league, leagueCreator) =>
            {
                tournament.Creator = tournamentCreator;
                league.Creator = leagueCreator;
                tournament.League = league;
                return tournament;
            }, new { TeamId = team.Id }, splitOn: "id, id, id").ToList();
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to select Tournament", e);
        }

        allTournaments.ForEach(tournament =>
        {
            tournament.Teams = SelectTeamsOnTournament(tournament);
        });
        return allTournaments;
    }
    #endregion
    #region SelectCounts
    public int SelectCountTeamsOnUser(int id)
    {
        int TeamCount;
        try
        {
            TeamCount = Connection.QuerySingle<int>(SQLQueries[$"SelectCountTeamsOnUser"], new { id });
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to get number of Team", e);
        }
        return TeamCount;
    }
    public int SelectCountLeaguesOnUser(int id)
    {
        int LeagueCount;
        try
        {
            LeagueCount = Connection.QuerySingle<int>(SQLQueries[$"SelectCountLeaguesOnUser"], new { id });
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to get number of League", e);
        }
        return LeagueCount;
    }
    public int SelectCountTournamentsOnUser(int id)
    {

        int TournamentCount;
        try
        {
            TournamentCount = Connection.QuerySingle<int>(SQLQueries[$"SelectCountTournamentsOnUser"], new { id });
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to get number of Tournament", e);
        }
        return TournamentCount;
    }
    public List<Tournament> SelectTournamentsOnLeague(League league)
    {
        List<Tournament> allTournament;
        try
        {
            allTournament = Connection.Query<Tournament, User, League, User, Tournament>(SQLQueries[$"SelectAllTournament"], (tournament, tournamentCreator, league, leagueCreator) =>
            {
                tournament.Creator = tournamentCreator;
                league.Creator = leagueCreator;
                tournament.League = league;
                return tournament;
            }, new { LeagueId = league.Id }, splitOn: "id, id, id").ToList();
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to select Tournament", e);
        }

        allTournament.ForEach(tournament =>
        {
            tournament.Teams = SelectTeamsOnTournament(tournament);
        });
        return allTournament;
    }
    #endregion
    #endregion
    #region Adds
    public int Add<T>(T entity)
    {
        try
        {
            int entityId = Connection.ExecuteScalar<int>(SQLQueries[$"Add{typeof(T).Name}"], entity);
            return entityId;
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to add {typeof(T).Name}", e);
        }
    }
    public int Add<T>(DynamicParameters parameters)
    {
        try
        {
            int entityId = Connection.ExecuteScalar<int>(SQLQueries[$"Add{typeof(T).Name}"], parameters);
            return entityId;
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to add {typeof(T).Name}", e);
        }
    }
    public int Add<T>(int teamId, int id2)
    {
        try
        {
            int entityId = Connection.ExecuteScalar<int>(SQLQueries[$"AddTeamTo{typeof(T).Name}"], new { TeamId = teamId, Id2 = id2 });
            return entityId;
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to add {typeof(T).Name}", e);
        }
    }
    #endregion
    public bool Update<T>(T entity)
    {
        try
        {
            Connection.Execute(SQLQueries[$"Update{typeof(T).Name}"], entity);
            return true;
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to update {typeof(T).Name}", e);
        }
    }
    public bool Delete<T>(int id)
    {
        try
        {
            Connection.Execute(SQLQueries[$"Delete{typeof(T).Name}"]);
            return true;
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to delete {typeof(T).Name}", e);
        }
    }
}