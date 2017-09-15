using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Raspberry.System;
using Raspberry.IO.GeneralPurpose;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello new standard World!");

            var board = Board.Current;

            Console.WriteLine($"{JsonConvert.SerializeObject(board, Formatting.Indented)}");

            var settings = new Dictionary<string, string>();

            var processorIndex = string.Empty;
            foreach (var line in File.ReadAllLines("/proc/cpuinfo"))
            {
                var descriptionLength = line.IndexOf(':');
                if (!string.IsNullOrWhiteSpace(line) && descriptionLength > 0)
                {
                    var description = line.Substring(0, descriptionLength).Trim();
                    var value = line.Substring(descriptionLength + 1).Trim();

                    if (string.Equals(description, "processor", StringComparison.InvariantCultureIgnoreCase))
                        processorIndex = "." + value;

                    settings.Add(description + processorIndex, value);
                }
                else
                    processorIndex = "";
            }

            Console.WriteLine($"{JsonConvert.SerializeObject(settings, Formatting.Indented)}");

            return;

            var redLed = ConnectorPin.P1Pin07.Output();

            var connection = new GpioConnection(redLed);

            connection.Open();

            Console.WriteLine("Connection open");

            Task.Delay(TimeSpan.FromSeconds(1)).GetAwaiter().GetResult();

            connection.Close();

            Console.WriteLine("Connection closed");
        }
    }
}
