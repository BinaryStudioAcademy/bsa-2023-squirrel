using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.BL.Interfaces;

public interface IConnectionFileService
{
    void CreateEmptyFile();
    DbSettings ReadFromFile();
    void SaveToFile(DbSettings dbSettings);
}