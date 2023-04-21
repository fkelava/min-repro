using System.Reflection;

namespace ExampleSln;

public static class AssemLdr
{
    public static bool Load(string fullPath) 
    {
        Assembly assem = Assembly.LoadFrom(fullPath);

        foreach (Type type in assem.GetExportedTypes())
        {
            // Boom?
            Console.WriteLine(type.Name);
        }

        return true;
    }
}