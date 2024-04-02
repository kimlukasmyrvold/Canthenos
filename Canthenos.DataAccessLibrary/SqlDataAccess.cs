using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Canthenos.DataAccessLibrary;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
        new DotEnv().Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));
    }

    private static string ConnectionStringName => "DEFAULT_CONNECTION";

    public async Task<List<T>> LoadData<T, TU>(string sql, TU parameters)
    {
        var connectionString = Environment.GetEnvironmentVariable(ConnectionStringName);

        using IDbConnection connection = new SqlConnection(connectionString);
        try
        {
            return (await connection.QueryAsync<T>(sql, parameters)).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error, something went wrong when trying to get data from database: {ex.Message}");
            return new List<T>();
        }
    }

    public async Task SaveData<T>(string sql, T parameters)
    {
        var connectionString = Environment.GetEnvironmentVariable(ConnectionStringName);
        
        using IDbConnection connection = new SqlConnection(connectionString);
        try
        {
            await connection.ExecuteAsync(sql, parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error, something went wrong when trying to send data to database: {ex.Message}");
        }
    }
}