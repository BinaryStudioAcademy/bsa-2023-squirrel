namespace Squirrel.SqlService.BLL.Interfaces;

public interface IDependencyAnalyzer
{
    ICollection<string> AnalyzeDependencies(string spContent, List<string>? objectList = null);
}

