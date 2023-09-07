using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.BL.Interfaces;

public interface IConnectionFileService
{
    void CreateInitFile();
    DbSettings ReadFromFile();
    void SaveToFile(DbSettings dbSettings);
}