namespace Canthenos.DataAccessLibrary;

public interface ISqlDataAccess
{
    Task<List<T>> LoadData<T, TU>(string sql, TU parameters);
    Task SaveData<T>(string sql, T parameters);
}