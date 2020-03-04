using System;
using System.Net;
using System.Linq;
using System.Threading;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MTRC.TelnetServer
{
	class Program
	{
        static readonly int port = 5555;
        static TcpListener tcpServer = null;

        private static readonly string[] Introduction = new[]
        {
            Constants.CONSOLE_LINE,
            "Mars Rover Command Center : Telnet Server",
            Constants.CONSOLE_LINE,
            Constants.RETURN,
        };

        static async Task Main()
        {
            try
            {
                Console.WriteLine(string.Join(Constants.LINE_BREAK_2, Introduction));

                tcpServer = new TcpListener(IPAddress.Loopback, port);
                tcpServer.Start();

                Console.WriteLine("Waiting for connections ... ");

                while (true)
                {
                    var tcpClient = await tcpServer.AcceptTcpClientAsync();

                    ThreadPool.QueueUserWorkItem(async x =>
                    {
                        var xtcpclient = x as TcpClient;
                        var ipAddress = tcpClient.Client.RemoteEndPoint.ToString().Split(':').First();

                        Console.WriteLine($"Client {ipAddress} connected");

                        var TelnetClient = new TelnetClient(xtcpclient);
                        await TelnetClient.StartSessionAsync();

                        Console.WriteLine($"Client {ipAddress} disconnected");

                    }, tcpClient);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                tcpServer.Stop();
            }

            Console.WriteLine("Press <ENTER> to exit ...");
            Console.Read();
        }
    }
}
