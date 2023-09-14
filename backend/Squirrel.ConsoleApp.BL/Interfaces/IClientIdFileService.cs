namespace Squirrel.ConsoleApp.BL.Interfaces;

public interface IClientIdFileService
{
    Guid? GetClientId();
    void SetClientId(string guid);
}