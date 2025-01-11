using System.Runtime.InteropServices;

namespace DemoApp;

partial class Program
{
    // Define delegate types matching our Calculator methods
    private delegate int AddDelegate(int a, int b);
    private delegate int SubtractDelegate(int a, int b);

    static void Main(string[] args)
    {
        using var nativeHost = new NativeHosting();
        try
        {
            string runtimeConfigPath = Path.Combine(AppContext.BaseDirectory, "DemoLibrary.runtimeconfig.json");
            string assemblyPath = Path.Combine(AppContext.BaseDirectory, "DemoLibrary.dll");

            Console.WriteLine($"Loading assembly from: {assemblyPath}");
            Console.WriteLine($"Using config from: {runtimeConfigPath}");

            Console.WriteLine("Initializing runtime...");
            nativeHost.Initialize(runtimeConfigPath);

            // Load and test Add method
            Console.WriteLine("Loading Add method...");
            var add = nativeHost.GetFunction<AddDelegate>(
                assemblyPath,
                "DemoLibrary.Calculator, DemoLibrary",
                "Add");

            // Test the Add method
            int a = 5, b = 3;
            Console.WriteLine($"Calling Add({a}, {b})...");
            int result = add(a, b);
            Console.WriteLine($"Result: {a} + {b} = {result}");

            // Load and test Subtract method
            Console.WriteLine("\nLoading Subtract method...");
            var subtract = nativeHost.GetFunction<SubtractDelegate>(
                assemblyPath,
                "DemoLibrary.Calculator, DemoLibrary",
                "Subtract");

            Console.WriteLine($"Calling Subtract({a}, {b})...");
            result = subtract(a, b);
            Console.WriteLine($"Result: {a} - {b} = {result}");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }
} 