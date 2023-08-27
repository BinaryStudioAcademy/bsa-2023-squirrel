using Squirrel.ConsoleApp.Models.Models;

namespace Squirrel.ConsoleApp.BL.Interfaces;

public interface IConnectionFileService
{
    void CreateEmptyFile();
    ConnectionString ReadFromFile();
    void SaveToFile(ConnectionString connectionString);
}