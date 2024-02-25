using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Canthenos.DataAccessLibrary;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    private string ConnectionStringName { get; set; } = "Default";

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<List<T>> LoadData<T, TU>(string sql, TU parameters)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);

        using IDbConnection connection = new SqlConnection(connectionString);
        var data = await connection.QueryAsync<T>(sql, parameters);

        return data.ToList();
    }

    public async Task SaveData<T>(string sql, T parameters)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);

        using IDbConnection connection = new SqlConnection(connectionString);
        await connection.ExecuteAsync(sql, parameters);
    }
}