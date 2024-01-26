class Team : Entity, IMenuItem
{

    #region Private fields and properties



    #endregion

    #region Public fields and properties

    public string Name { get; set; }
    public User Owner { get; set; }

    #endregion

    #region Constructor

    private Team(string name, User owner)
    {
        Name = name;
        Owner = owner;
    }

    public Team(int id, string name, DateTime created)
    {
        Id = id;
        Name = name;
        Created = created;
    }

    #endregion

    #region Private methods



    #endregion

    #region Public methods

    public static Team CreateNewTeam(string name, User owner)
    {
        /* if (name.Length < 3)
       {
            throw new Exception("Team name must be at least 3 characters long");
        }
        */
        return new(name, owner);
    }

    public static (bool, string) Get<Team, FROM>(Cookie cookie, int idToSearch, out List<dynamic> Objects)
    {
        (bool Success, string Message) Result = (false, "");
        Objects = new();
        try
        {
            Objects = cookie.SQLRepo.SelectAll<Team, FROM>(idToSearch);
        }
        catch (Exception e)
        {
            Result.Success = false;
            Result.Message = e.ToString();
            return Result;
        }

        if (Objects.Count < 1)
        {
            Result.Success = false;
            Result.Message = "Found 0 teams";
        }
        else
        {
            Result.Success = true;
            Result.Message = $"Found {Objects.Count} teams";
        }

        return Result;
    }
    public static void Create()
    {
        Console.WriteLine("Create Team");
    }
    public void Change()
    {
        Console.WriteLine("Change Team");
    }
    public void Remove()
    {
        Console.WriteLine("Remove Team");
    }

    public override string ToString()
    {
        return $"{Name}";
    }

    #endregion
}

