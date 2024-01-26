abstract class Entity
{
    #region Private fields and properties

    private int id;
    private DateTime created;
    private bool isDeleted;
    #endregion

    #region Public fields and properties

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public DateTime Created
    {
        get { return created; }
        set { created = value; }
    }
    public bool IsDeleted
    {
        get { return isDeleted; }
        set { isDeleted = value; }
    }

    #endregion

    #region Constructor

    public Entity()
    {
        created = DateTime.Now;
    }

    #endregion

    #region Private methodss



    #endregion

    #region Public methods

    //TODO: Add to interface?
    public bool ContainsDuplicates<T>(List<T> list)
    {
        var knownKeys = new HashSet<T>();
        return list.Any(item => !knownKeys.Add(item));
    }
    public bool ContainsDuplicates<T>(List<T> list1, List<T> list2)
    {
        return list1.Any(list2.Contains);
    }

    #endregion
}