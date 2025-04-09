using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MVCTask.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCTask.DAL.ADO;
public class DatabaseService : IDatabaseService
{
    private readonly string _connectionString;

    public DatabaseService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<T> ExecuteReaderAsync<T>(string storedProcedure, Func<SqlDataReader, Task<T>> readFunc, params SqlParameter[] parameters)
    {
        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(storedProcedure, conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddRange(parameters);

        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            return await readFunc(reader);
        }

        return default;
    }

    public async Task ExecuteNonQueryAsync(string storedProcedure, params SqlParameter[] parameters)
    {
        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(storedProcedure, conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddRange(parameters);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
}
