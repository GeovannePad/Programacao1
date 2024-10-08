using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Aula4
{
    public class ThreadPing
    {
        private string endereco = "google.com";
        int countPing = 0;
        bool executePing = false;

        public void StartPing()
        {
            Console.Write("Digite S a qualquer momento para interromper");

            var threadPingger = new Thread(ExecutePing);
            threadPingger.Start();
            executePing = true;

            var comandoSair = "S";
            var comando = string.Empty;

            while (!comandoSair.Equals(comando))
            {
                comando = Console.ReadLine();
            }
            executePing = false;

            while (threadPingger.IsAlive)
            {
                Console.WriteLine("Esperando finalizar...");

                threadPingger.Join();
            }

            Console.WriteLine("Thread Finalizada");
        }

        public void ExecutePing()
        {
            while (countPing < 4)
            {
                Ping pingger = new Ping();

                var pingResponse = pingger.Send(endereco);

                Console.WriteLine($"Ping {countPing}: {endereco} | Status: {pingResponse.Status} - {pingResponse.RoundtripTime}ms");
                countPing++;

                // Espera alguns segundos
                Thread.Sleep(2000);
            }
        }
    }
}
