using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squirrel.ConsoleApp.Interfaces
{
    public interface IDatabaseService
    {
        string ConnectionString { get; }
        string ExecuteQuery(string query);
        Task<string> ExecuteQueryAsync(string query);
    }
}
