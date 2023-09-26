using System.Data;
using System.Data.SqlClient;

namespace Squirrel.ConsoleApp.BL.Providers;

public abstract class BaseProvider
{
    protected SqlParameter GetParameter(string parameterName, object parameterValue, SqlDbType type)
    {
        return new SqlParameter(GetParameterName(parameterName), type) { Value = parameterValue };
    }
    
    private string GetParameterName(string parameterName) => $"@{parameterName}";
}