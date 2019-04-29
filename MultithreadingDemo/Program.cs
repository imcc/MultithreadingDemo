using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadingDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ConsoleThreadInfo();
            // TestTask();
            await TestAwait3();
            ConsoleThreadInfo();
        }

        private static int TestTask()
        {
            var x = Task.Run(() => GetHttpRandom().GetAwaiter().GetResult()).GetAwaiter().GetResult();

            Console.WriteLine("Before Return");
            ConsoleThreadInfo();

            return x;
        }

        private static async Task TestAwait()
        {
            Console.WriteLine("Application Start");
            ConsoleThreadInfo();

            Console.WriteLine("Result:" + await GetHttpRandom());

            Console.WriteLine("Application Stopped");
            ConsoleThreadInfo();
        }


        private static async Task TestAwait3()
        {
            Console.WriteLine("Application Start");
            ConsoleThreadInfo();

            var x1 = GetHttpRandom();
            var x2 = GetHttpRandom();
            var x3 = GetHttpRandom();

            Console.WriteLine("xxxxxxxxxxxxxxx" + Environment.NewLine);

            Console.WriteLine("Result1:" + await x1);
            Console.WriteLine("Result2:" + await x2);
            Console.WriteLine("Result3:" + await x3);


            // Console.WriteLine("Result1:" + await GetHttpRandom());
            // Console.WriteLine("Result2:" + await GetHttpRandom());
            // Console.WriteLine("Result3:" + await GetHttpRandom());

            Console.WriteLine("Application Stopped");
            ConsoleThreadInfo();
        }

        private static async Task<int> GetHttpRandom()
        {
            Console.WriteLine("-----Before Http Get-----");
            ConsoleThreadInfo();

            var strResult =
                await new HttpClient {BaseAddress = new Uri("https://www.random.org")}
                    .GetStringAsync("/integers/?num=1&min=1&max=6&col=1&base=10&format=plain&rnd=new");

            Console.WriteLine("-----End Http Get-----");
            ConsoleThreadInfo();

            if (string.IsNullOrWhiteSpace(strResult)) return -1;

            return int.TryParse(strResult, out var result) ? result : -1;
        }

        private static void ConsoleThreadInfo()
        {
            Console.WriteLine(Environment.NewLine + "---------------");
            Console.WriteLine("ProcessorId:" + Thread.GetCurrentProcessorId());
            Console.WriteLine("ThreadID:" + Thread.CurrentThread.ManagedThreadId);
            Console.Write("IsBackground:" + Thread.CurrentThread.IsBackground);
            Console.WriteLine("---------------" + Environment.NewLine);
        }
    }
}