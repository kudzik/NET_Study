// https://docs.microsoft.com/pl-pl/dotnet/core/diagnostics/metrics


// dotnet add package System.Diagnostics.DiagnosticSource

// dotnet - counters ps
// dotnet-counters ps


using System;
using System.Diagnostics.Metrics;
using System.Threading;




class Program
{
    static Meter s_meter = new Meter("HatCo.HatStore", "1.0.0");
    static Counter<int> s_hatsSold = s_meter.CreateCounter<int>("hats-sold");
    static Histogram<int> s_orderProcessingTimeMs = s_meter.CreateHistogram<int>("order-processing-time");
    static int s_coatsSold;
    static int s_ordersPending;

    static Random s_rand = new Random();

    static void Main(string[] args)
    {
        s_meter.CreateObservableCounter<int>("coats-sold", () => s_coatsSold);
        s_meter.CreateObservableGauge<int>("orders-pending", () => s_ordersPending);

        Console.WriteLine("Press any key to exit");
        while (!Console.KeyAvailable)
        {
            // Pretend our store has one transaction each 100ms that each sell 4 hats
            Thread.Sleep(100);
            s_hatsSold.Add(4);

            // Pretend we also sold 3 coats. For an ObservableCounter we track the value in our variable and report it
            // on demand in the callback
            s_coatsSold += 3;

            // Pretend we have some queue of orders that varies over time. The callback for the "orders-pending" gauge will report
            // this value on-demand.
            s_ordersPending = s_rand.Next(0, 20);

            // Last we pretend that we measured how long it took to do the transaction (for example we could time it with Stopwatch)
            s_orderProcessingTimeMs.Record(s_rand.Next(5, 15));
        }
    }
}