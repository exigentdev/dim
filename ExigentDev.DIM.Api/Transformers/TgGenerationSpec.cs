using System.Reflection;
using TypeGen.Core.SpecGeneration;

namespace ExigentDev.DIM.Api.Transformers
{
  public class TgGenerationSpec : GenerationSpec
  {
    public TgGenerationSpec()
    {
      var dtoNamespace = "ExigentDev.DIM.Api.Dtos";

      // Get all types in the assembly that are in the "Dtos" namespace and are classes
      var dtoTypes = Assembly
        .GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.IsClass && t.Namespace != null && t.Namespace.StartsWith(dtoNamespace))
        .ToList();

      // Register each DTO class
      foreach (var dtoType in dtoTypes)
      {
        AddInterface(dtoType);
      }
    }
  }
}
