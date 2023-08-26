using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.Interfaces;

public interface IConnectionFileService
{
    void CreateEmptyFile();
    ConnectionString ReadFromFile();
    void SaveToFile(ConnectionString connectionString);
    
}