using Newtonsoft.Json;

namespace Squirrel.ConsoleApp.BL.Interfaces;

public interface IJsonSerializerSettingsService
{
    JsonSerializerSettings GetSettings();
}