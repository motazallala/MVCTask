using Microsoft.Data.SqlClient;

namespace MVCTask.DAL.Interfaces;
public interface IDatabaseService
{
    Task<T> ExecuteReaderAsync<T>(string storedProcedure, Func<SqlDataReader, Task<T>> readFunc, params SqlParameter[] parameters);
    Task ExecuteNonQueryAsync(string storedProcedure, params SqlParameter[] parameters);
}