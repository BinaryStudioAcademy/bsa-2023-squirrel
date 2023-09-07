using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.BL.Interfaces;

public interface IConnectionStringService
{
    string BuildConnectionString(ConnectionStringDto connectionStringDto);
}