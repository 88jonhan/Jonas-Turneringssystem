class League : Entity, IMenuItem
{
    #region Private fields and properties



    #endregion

    #region Public fields and properties

    public string Name { get; set; }
    public User Creator { get; set; }
    public List<User> Admins { get; set; }
    public List<Tournament> Tournaments { get; set; }
    public bool IsPrivate { get; set; }
    public int SportId { get; set; }


    #endregion

    #region Constructor

    private League(string name, User creator, int sportId)
    {
        Admins = new(){
            creator
        };
        SportId = sportId;
        Name = name;
        Creator = creator;
        Tournaments = new();
    }
    public League(int id, string name, int sportId, DateTime created, bool isPrivate)
    {
        Id = id;
        Name = name;
        SportId = sportId;
        Created = created;
        IsPrivate = isPrivate;
    }

    #endregion

    #region Private methods



    #endregion

    #region Public methods

    public static League CreateNewLeague(string name, User creator, int sportId)
    {
        /* if (name.Length < 3)
       {
            throw new Exception("League name must be at least 3 characters long");
        }
        */
        return new(name, creator, sportId);
    }

    public override string ToString()
    {
        return $"Namn: {Name}\nSkapad den: {Created}\nSkapad av: {Creator}";
    }

    #endregion
}