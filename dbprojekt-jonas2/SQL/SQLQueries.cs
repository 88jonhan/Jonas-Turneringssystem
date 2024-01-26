static class SqlQueriesService
{
    public static Dictionary<string, string> SQLQueries = new Dictionary<string, string>()
        {
            {   "SelectUserAuth",
                    "SELECT * FROM Users WHERE username = @Username AND password = @Password AND isDeleted = 0" },
            #region Selects
            {   "SelectUser",
                    "SELECT * FROM Users WHERE id = @id AND isDeleted = 0" },
            {   "SelectTeam",
                    "SELECT * FROM Teams WHERE id = @id AND isDeleted = 0" },
            {   "SelectLeague",
                    "SELECT * FROM Leagues WHERE id = @id AND isDeleted = 0" },
            {   "SelectTournament",
                    "SELECT * FROM Tournaments WHERE id = @id AND isDeleted = 0" },
            #endregion
            #region SelectAll
            {   "SelectAll",
                    "SELECT * FROM @Table WHERE isDeleted = 0" },
            {   "SelectAllFrom",
                    "SELECT * FROM @Table WHERE @idType = @id AND isDeleted = 0" },
            // {   "SelectAllTeam", @"
            //         SELECT * FROM Team
            //         WHERE Team.isDeleted = 0
            //     " },
            // {   "SelectAllLeague", @"
            //         SELECT * FROM League
            //         WHERE League.isDeleted = 0
            //     " },
            // {   "SelectAllTournament", @"
            //         SELECT Tournament.id, Tournament.name, Tournament.created, Tournament.started, Tournament.finished FROM Tournament
            //         WHERE Tournament.isDeleted = 0
            //     " },
            #endregion
            #region SelectFroms
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
            {   "SelectUsersFromTournament", @"  
                    SELECT Users.* FROM Teams_Tournaments
                    INNER JOIN Tournaments ON Teams_Tournaments.tournamentId = Tournaments.id
                    INNER JOIN Teams ON Teams_Tournaments.teamId = Teams.id
                    INNER JOIN Users ON Teams.userid = Users.id
                    WHERE Tournaments.Id = @id AND Users.isDeleted = 0
                " },
            {   "SelectTeamsFromUser", @"  
                    SELECT * FROM Teams
                    WHERE userid = @id AND Teams.isDeleted = 0
                " },
            {   "SelectTeamsFromLeague", @"  
                    SELECT Teams.* FROM Teams_Leagues
                    INNER JOIN Leagues ON Teams_Leagues.leagueId = Leagues.id
                    INNER JOIN Teams ON Teams_Leagues.teamId = Teams.id
                    WHERE Leagues.id = @id AND Teams.isDeleted = 0
                " },
            {   "SelectTeamsFromTournament", @"  
                    SELECT Teams.* FROM Teams_Tournaments
                    INNER JOIN Tournaments ON Teams_Tournaments.tournamentId = Tournaments.id
                    INNER JOIN Teams ON Teams_Tournaments.teamId = Teams.id
                    WHERE Tournaments.Id = @id AND Teams.isDeleted = 0
                " },
            {   "SelectLeaguesFromUser", @"  
                    SELECT Leagues.* FROM Teams_Leagues
                    INNER JOIN Leagues ON Teams_Leagues.leagueId = Leagues.id
                    INNER JOIN Teams ON Teams_Leagues.teamId = Teams.id
                    INNER JOIN Users ON Teams.userid = Users.id
                    WHERE Users.Id = @id AND Leagues.isDeleted = 0
                " },
            {   "SelectLeaguesFromTeam", @"  
                    SELECT Leagues.* FROM Teams_Leagues
                    INNER JOIN Leagues ON Teams_Leagues.leagueId = Leagues.id
                    WHERE teamId = @id AND Leagues.isDeleted = 0
                " },
            {   "SelectLeaguesFromTournament", @"  
                    SELECT Leagues.* FROM Tournaments
                    INNER JOIN Leagues ON Tournaments.leagueId = Leagues.id
                    WHERE Tournaments.Id = @id AND Leagues.isDeleted = 0
                " },
            {   "SelectTournamentsFromUser", @"  
                    SELECT Tournaments.* FROM Teams_Tournaments
                    INNER JOIN Tournaments ON Teams_Tournaments.tournamentId = Tournaments.Id
                    INNER JOIN Teams ON Teams_Tournaments.teamId = Teams.Id
                    INNER JOIN Users ON Teams.userid = Users.Id
                    WHERE Users.id = @id AND Users.isDeleted = 0
                " },
            {   "SelectTournamentsFromTeam", @"  
                    SELECT Tournaments.* FROM Teams_Tournaments
                    INNER JOIN Tournaments ON Teams_Tournaments.tournamentId = Tournaments.id
                    WHERE teamId = @id AND Tournaments.isDeleted = 0
                " },
            {   "SelectTournamentsFromLeague", @"  
                    SELECT Tournaments.* FROM Leagues
                    INNER JOIN Tournaments ON Leagues.id = Tournaments.leagueId 
                    WHERE Leagues.id = @id AND Leagues.isDeleted = 0
                " },
            #endregion
            #region SelectCounts
            {   "SelectCountTeamsOnUser", @"
                    SELECT COUNT(*) FROM Teams
                    WHERE userid = @id AND Teams.isDeleted = 0
                "},
            {   "SelectCountLeaguesOnUser", @"
                    SELECT Count(*) FROM Teams_Leagues
                    INNER JOIN Leagues ON Teams_Leagues.leagueId = Leagues.id
                    INNER JOIN Teams ON Teams_Leagues.teamId = Teams.id
                    WHERE Teams.userid = @id AND Leagues.isDeleted = 0
                "},
            {   "SelectCountTournamentsOnUser", @"
                    SELECT COUNT(*) FROM Teams_Tournaments
                    INNER JOIN Tournaments ON Teams_Tournaments.tournamentId = Tournaments.id
                    INNER JOIN Teams ON Teams_Tournaments.teamId = Teams.id
                    WHERE Teams.userid = @id AND Tournaments.isDeleted = 0
                "},
            #endregion
            #region Adds
             {  "AddUser", @"
                    INSERT INTO User 
                    (username, password, firstname, lastname, created, isAdmin)
                    OUTPUT INSERTED.id
                    VALUES 
                    (@Username, @Password, @Firstname, @Lastname, @Created ,@IsAdmin)
                "},
            {   "AddTeam", @" 
                    INSERT INTO Team
                    (name, userId, created)
                    OUTPUT INSERTED.id
                    VALUES
                    (@Name, @UserId, @Created)
                "},
            {   "AddLeague", @"
                    INSERT INTO League
                    (name, userId, created, isPrivate, sportId)
                    OUTPUT INSERTED.id
                    VALUES
                    (@Name, @UserId, @Created, @IsPrivate, @SportId)
                "},
            {   "AddTournament", @"
                    INSERT INTO Tournament
                    (name, userId, leagueId, created)
                    OUTPUT INSERTED.id
                    VALUES
                    (@Name, @UserId, @leagueId, @Created)
                " },
            {   "AddMatch", @"
                    INSERT INTO Match
                    (tournamentId, groupId, team1Id, team2Id, created)
                    OUTPUT INSERTED.id
                    VALUES, 
                    (@tournamentId, @GroupId, @Team1Id, @Team2Id, @Created)
                " },
            {   "AddPlayoffMatch", @"
                    INSERT INTO Match
                    (tournamentId, team1Id, team2Id, playoffLevel, playoffMatch, created)
                    OUTPUT INSERTED.id
                    VALUES
                    (@tournamentId, @Team1Id, @Team2Id, @PlayoffLevel, @PlayoffMatch, @Created)
                " },
            {   "AddTeamToTournament", @"
                    INSERT INTO Team_Tournament
                    (teamId, tournamentId)
                    VALUES
                    (@teamId, @id2)
                " },
            {   "AddTeamToLeague", @"
                    INSERT INTO Team_League
                    (teamId, leagueId)
                    VALUES
                    (@teamId, @id2)
                " },
            #endregion
            #region Deletes //TODO: Kanske uppdatera borttagna saker till null eller "Deleted Team" istället för att ta bort dem?
                {  "DeleteUser", @"
                    UPDATE User
                    SET isDeleted = true
                    WHERE id = @id;
                " },
                {  "DeleteTeam", @"
                    UPDATE Team 
                    SET isDeleted = true
                    WHERE id = @id;
                " },
                {  "DeleteLeague", @"
                    UPDATE League 
                    SET isDeleted = true
                    WHERE id = @id;
                " },
                {  "DeleteTournament", @"
                    UPDATE Tournament 
                    SET isDeleted = true
                    WHERE id = @id;
                " },
                {  "DeleteMatch", @"
                    UPDATE Match 
                    SET isDeleted = true
                    WHERE id = @id;
                " },
            #endregion
            #region Updates 
                {  "UpdateUser", @"
                    UPDATE User
                    SET username = @Username, password = @Password, firstname = @Firstname, lastname = @Lastname, isAdmin = @IsAdmin
                    WHERE id = @id;
                " },
                {  "UpdateTeam", @"
                    UPDATE Team
                    SET name = @Name, userId = @UserId
                    WHERE id = @id;
                " },
                {  "UpdateLeague", @"
                    UPDATE League
                    SET name = @Name, userId = @UserId, isPrivate = @IsPrivate, sportId = @SportId
                    WHERE id = @id;
                " },
                {  "UpdateTournament", @"
                    UPDATE Tournament
                    (name, userId, leagueId, created)
                    SET name = @Name, userId = @UserId, leagueId = @leagueId
                    WHERE id = @id;
                " },
                {  "UpdateMatch", @"
                    UPDATE Match
                    SET tournamentId = @tournamentId, groupId = @GroupId, team1Id = @Team1Id, team2Id = @Team2Id
                    WHERE id = @id;
                " },
                {  "UpdatePlayoffMatch", @"
                    UPDATE Match
                    SET tournamentId = @tournamentId, groupId = @GroupId, team1Id = @Team1Id, team2Id = @Team2Id, playoffLevel = @PlayoffLevel, playoffMatch = @PlayoffMatch
                    WHERE id = @id;
                " },
            #endregion
        };
}
