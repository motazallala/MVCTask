using Microsoft.Data.SqlClient;
using System.Data;

namespace MVCTask.DAL.Helper;
public static class SqlParameterHelper
{
    public static SqlParameter CreateParameter(string name, SqlDbType type, object value)
    {
        return new SqlParameter(name, type) { Value = value ?? DBNull.Value };
    }
}
