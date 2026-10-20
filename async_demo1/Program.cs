//Console.WriteLine("Hello, World!");
using System;
using System.Threading.Tasks;

class Program
{
    // A modern .NET Console application supports an 'async Task Main' entry point
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== Breakfast Simulation Started ===");
        Console.WriteLine($"[Main Thread ID: {Environment.CurrentManagedThreadId}] Starting breakfast preparation...\n");

        // 1. Fire off the asynchronous task.
        // This starts the toast process background work on the Thread Pool immediately.
        Task<string> toastTask = MakeToastAsync();

        // 2. Demonstrate non-blocking behavior.
        // While the toaster is running independently, the main thread can continue 
        // to do other synchronous work (like pouring juice) without waiting!
        Console.WriteLine($"[Main Thread ID: {Environment.CurrentManagedThreadId}] While waiting for toast, I can pour the orange juice...");
        PourOrangeJuice();
        Console.WriteLine($"[Main Thread ID: {Environment.CurrentManagedThreadId}] Orange juice is poured.\n");

        // 3. Await the result of the background task.
        // The 'await' keyword pauses execution of Main, frees up resources, and 
        // will resume once the 'toastTask' completes.
        Console.WriteLine($"[Main Thread ID: {Environment.CurrentManagedThreadId}] Now, I must wait for the toast to pop before eating.");
        string toastResult = await toastTask;

        // 4. Execution resumes after the task finishes.
        Console.WriteLine($"\n[Main Thread ID: {Environment.CurrentManagedThreadId}] Received result: {toastResult}");
        Console.WriteLine("=== Breakfast Simulation Complete ===");
    }

    // This method returns a Task<string> - a promise that a string will be available later
    static async Task<string> MakeToastAsync()
    {
        Console.WriteLine($"  [Toast Thread ID: {Environment.CurrentManagedThreadId}] -> Putting bread into the toaster.");
        Console.WriteLine($"  [Toast Thread ID: {Environment.CurrentManagedThreadId}] -> Toasting started...");

        // Task.Delay simulates non-blocking network/disk I/O or background delays.
        // It does NOT block the current thread while waiting for the 3 seconds to pass.
        await Task.Delay(3000);

        Console.WriteLine($"  [Toast Thread ID: {Environment.CurrentManagedThreadId}] -> *DING* Toast is done!");
        return "Crispy Golden Toast";
    }

    static void PourOrangeJuice()
    {
        Console.WriteLine($"[Main Thread ID: {Environment.CurrentManagedThreadId}] ...Pouring a cold glass of OJ...");
    }
}