
// Konieczne jest zainstalowanie pakietu
// dotnet tool install --global dotnet-trace
// dotnet tool install --global dotnet-trace

// dotnet-trace collect --output trace.nettrace -- .\EventSourceDemo.exe    

// Visual Studio Event Viewer 
// ALT+F2 


// See https://aka.ms/new-console-template for more information
using System.Diagnostics.Tracing;

Console.WriteLine("Hello, World!");
DemoEventSource.Log.AppStarted("Hello World!", 12);

Console.WriteLine("End");


[EventSource(Name = "Demo")]
class DemoEventSource : EventSource
{
    public static DemoEventSource Log { get; } = new DemoEventSource();
    
    [Event(1)]
    public void AppStarted(string message, int favoriteNumber) => WriteEvent(1, message, favoriteNumber);

}