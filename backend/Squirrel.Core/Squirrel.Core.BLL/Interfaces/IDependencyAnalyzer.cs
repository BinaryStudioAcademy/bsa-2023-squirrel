namespace Squirrel.Core.BLL.Interfaces;
public interface IDependencyAnalyzer
{
    List<string> AnalyzeDependencies(string spContent, List<string>? objectList = null);
}

