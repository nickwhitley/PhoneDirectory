namespace PhoneDirectoryLibrary.DataAccess
{
    public interface ISQLDataAccess
    {
        string ConnectionStringName { get; }

        Task<List<T>> LoadDataAsync<T, U>(string sql, U parameters);
        Task SaveDataAsync<T>(string sql, T parameters);
    }
}