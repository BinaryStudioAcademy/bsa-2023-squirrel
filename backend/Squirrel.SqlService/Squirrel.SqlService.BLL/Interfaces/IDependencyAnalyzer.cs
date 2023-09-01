namespace Squirrel.SqlService.BLL.Interfaces;
public interface IDependencyAnalyzer
{
    List<string> AnalyzeDependencies(string spContent, List<string>? objectList = null);
}

