using ExampleSln;

namespace Example;

internal class Program
{
    static void Main(string[] args)
    {
        bool retval = AssemLdr.Load("ExamplePlugin.dll");
        Console.WriteLine(retval);
    }
}
