using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.BL.Interfaces;

public interface IConnectionFileService
{
    void CreateEmptyFile();
    ConnectionStringDto ReadFromFile();
    void SaveToFile(ConnectionStringDto connectionStringDto);
}