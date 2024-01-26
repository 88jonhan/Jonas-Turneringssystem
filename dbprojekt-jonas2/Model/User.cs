using System.Diagnostics;
using Dapper;

class User : Entity, IMenuItem, IDataBaseEntity
{
    #region Private fields and properties

    private bool isadmin;
    private string username;
    private string password;
    private string firstname;
    private string lastname;

    #endregion

    #region Public fields and properties

    public string Username
    {
        get { return username; }
        set { username = value; }
    }

    public string Password
    {
        get { return password; }
        set { password = value; }
    }

    public string Name
    {
        get { return $"{firstname} {lastname}"; }
    }
    public string Firstname
    {
        get { return firstname; }
        set { firstname = value; }
    }
    public string Lastname
    {
        get { return lastname; }
        set { lastname = value; }
    }

    public bool IsAdmin
    {
        get { return isadmin; }
        set { isadmin = value; }
    }

    #endregion

    #region Constructor

    public User(string username, string password, string firstname, string lastname, bool isAdmin = false)
    {
        Username = username;
        Password = password;
        Firstname = firstname;
        Lastname = lastname;
        IsAdmin = isAdmin;
    }

    public User(int Id, string firstname, string lastname, string username, string password, bool isAdmin, DateTime created)
    {
        this.Id = Id;
        Firstname = firstname;
        Lastname = lastname;
        Username = username;
        Password = password;
        IsAdmin = isAdmin;
        Created = created;
    }
    public User()
    {

    }

    #endregion

    #region Private methods



    #endregion

    #region Public methods

    public static List<dynamic> Show(Cookie cookie)
    {
        return cookie.SQLRepo.SelectAll<User>();
    }
    public void Create()
    {
        Console.WriteLine("Create Users");
    }
    public void Change()
    {
        Console.WriteLine("Change Users");
    }
    public void Remove()
    {
        Console.WriteLine("Remove Users");
    }

    public void UpdateUserContent(Cookie cookie, Layout layout)
    {
        Layout UserTeamsLayout = new Layout()
            .Parent(layout)
            .Title("UserTeams")
            .Size(layout.Parent.Size.Height / 4, 20)
            .Alignment(Vertical: Vertical.Top, Horizontal: Horizontal.Right, VerticalNudge: 2)
            .Header("Your teams", "Small")
            .Border(left: 1, top: 1, right: 1, bottom: 1);
        UserTeamsLayout.Header
            .Position(Vertical: Vertical.Top, Horizontal: Horizontal.Center);

        Layout UserLeaguesLayout = new Layout()
            .Parent(layout)
            .Title("UserLeagues")
            .Size(layout.Parent.Size.Height / 4, 20)
            .Alignment(Vertical: Vertical.Center, Horizontal: Horizontal.Right)
            .Header("Your Leagues", "Small")
            .Border(left: 1, top: 1, right: 1, bottom: 1);
        UserLeaguesLayout.Header
                    .Position(Vertical: Vertical.Top, Horizontal: Horizontal.Center);

        Layout UserTournamentsLayout = new Layout()
            .Parent(layout)
            .Title("UserTournaments")
            .Size(layout.Parent.Size.Height / 4, 20)
            .Alignment(Vertical: Vertical.Bottom, Horizontal: Horizontal.Right, VerticalNudge: -2)
            .Header("Your Tournaments", "Small")
            .Border(left: 1, top: 1, right: 1, bottom: 1);
        UserTournamentsLayout.Header
                    .Position(Vertical: Vertical.Top, Horizontal: Horizontal.Center);

        var Teams = cookie.SQLRepo.SelectAll<Team, User>(Id);
        var Leagues = cookie.SQLRepo.SelectAll<League, User>(Id);
        var Tournaments = cookie.SQLRepo.SelectAll<Tournament, User>(Id);

        cookie.Renderer.Draw(new List<Layout>() { UserTeamsLayout, UserLeaguesLayout, UserTournamentsLayout });

        cookie.Renderer.ShowContent(Teams, UserTeamsLayout);
        cookie.Renderer.ShowContent(Leagues, UserLeaguesLayout);
        cookie.Renderer.ShowContent(Tournaments, UserTournamentsLayout);

    }


    public static User CreateNewUser(string username, string password, string firstname, string lastname, bool isAdmin = false)
    {
        return new(username, password, firstname, lastname, isAdmin);
    }

    public static bool IsAuthenticated(Cookie cookie, string username, string password)
    {
        if (!cookie.SQLRepo.SelectUserAuth(username, password, out User FoundUser))
        {
            return false;
        };
        if (FoundUser == default)
        {
            return false;
        }
        cookie.ActiveUser = FoundUser;
        return true;
    }

    public bool AddToDB(SQLRepository SQLRepo)
    {
        try
        {
            SQLRepo.Add<User>(this);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public bool UpdateDB(SQLRepository SQLRepo)
    {
        try
        {
            SQLRepo.Update(this);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public bool DeleteFromDB(SQLRepository SQLRepo)
    {
        try
        {
            SQLRepo.Delete<User>(Id);
            return true;
        }
        catch (Exception)
        {
            throw new Exception("Failed to delete user");
        }
    }

    public (int TeamCount, int LeagueCount, int TournamentCount) CheckUserData(ISQLConnection SQLRepo)
    {
        return (SQLRepo.SelectCountTeamsOnUser(Id), SQLRepo.SelectCountLeaguesOnUser(Id), SQLRepo.SelectCountTournamentsOnUser(Id));
    }

    public override string ToString()
    {
        return $"{Username}, {Name}";
    }
    #endregion
}