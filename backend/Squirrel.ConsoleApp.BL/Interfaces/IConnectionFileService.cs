using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.BL.Interfaces;

public interface IConnectionFileService
{
    void CreateInitFile();
    ConnectionStringDto ReadFromFile();
    void SaveToFile(ConnectionStringDto connectionStringDto);
}