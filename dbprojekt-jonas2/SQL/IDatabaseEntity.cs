interface IDataBaseEntity
{
    public bool AddToDB(SQLRepository SQLRepo);
    public bool UpdateDB(SQLRepository SQLRepo);
    public bool DeleteFromDB(SQLRepository SQLRepo);
}
