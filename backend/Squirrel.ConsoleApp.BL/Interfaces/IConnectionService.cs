using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.BL.Interfaces;

public interface IConnectionService
{
    string TryConnect(ConnectionStringDto connectionStringDto);
}