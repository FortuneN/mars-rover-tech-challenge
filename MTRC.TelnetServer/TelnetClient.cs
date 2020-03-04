using System;
using System.Text;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Generic;
using MRTC.Library;

namespace MTRC.TelnetServer
{
	class TelnetClient
	{
		private static readonly InputExecutor InputExecutor = new InputExecutor();
		
		private static readonly Encoding Encoding = Encoding.ASCII;

		private static readonly string[] Introduction = new[]
		{
			Constants.CONSOLE_LINE,
			"Mars Rover Command Center :)",
			Constants.CONSOLE_LINE,
			Constants.RETURN,
			"Hint: Press <ENTER> on a blank line to submit your input",
			Constants.RETURN,
			"Example input:",
			Constants.RETURN,
			"5 5",
			"1 2 N",
			"LMLMLMLMM",
			"3 3 E",
			"MMRMMRMRRM",
			Constants.RETURN,
			Constants.CONSOLE_LINE,
			Constants.LINE_BREAK_2
		};

		private readonly TcpClient tcpClient;
		private readonly NetworkStream tcpClientStream;
		private readonly List<string> currentRequestLines = new List<string>();
		private readonly List<string> currentLine = new List<string>();

		public TelnetClient(TcpClient tcpClient)
		{
			this.tcpClient = tcpClient;
			tcpClientStream = tcpClient.GetStream();
		}

		public async Task StartSessionAsync()
		{
			try
			{
				await SendAsync(Introduction);

				int inputLength = 0;
				var inputBytes = new byte[256];

				while ((inputLength = await tcpClientStream.ReadAsync(inputBytes, 0, inputBytes.Length)) != 0)
				{
					var input = Encoding.GetString(inputBytes, 0, inputLength);

					switch (input)
					{
						case Constants.CTRL_C:

							tcpClient.Close();

							return;

						case Constants.BACK_SPACE:

							// remove last character from line

							if (currentLine.Any())
							{
								currentLine.RemoveAt(currentLine.Count - 1);
							}

							// echo back space

							await SendAsync($"{Constants.DC4}{Constants.BACK_SPACE}");

							break;

						case Constants.LINE_BREAK_1:
						case Constants.LINE_BREAK_2:

							// compose line

							var requestLine = string.Join(string.Empty, currentLine).Trim();

							// add to request lines

							currentRequestLines.Add(requestLine);

							// reset current line

							currentLine.Clear();

							// execute request if line is blank

							if (string.IsNullOrWhiteSpace(requestLine))
							{
								// compose request

								var requestText = string.Join(Constants.LINE_BREAK_1, currentRequestLines).Trim();

								// process request

								var responseText = await ProcessRequestAsync(requestText);

								// send response

								await SendAsync(
									"OUTPUT:",
									Constants.RETURN,
									responseText,
									Constants.RETURN,
									Constants.CONSOLE_LINE,
									Constants.LINE_BREAK_2
								);

								// reset lines

								currentRequestLines.Clear();
							}

							break;

						default:

							// grow line

							if (!char.IsControl(input, 0))
							{
								currentLine.Add(input);
							}

							break;
					}
				}
			}
			catch (Exception exception)
			{
				Console.WriteLine($"{tcpClient.Client.RemoteEndPoint} => {exception}");
				throw;
			}
		}

		private async Task SendAsync(params string[] text)
		{
			var bytes = Encoding.GetBytes(string.Join(Constants.LINE_BREAK_2, text));
			await tcpClientStream.WriteAsync(bytes, 0, bytes.Length);
		}

		private async Task<string> ProcessRequestAsync(string requestText)
		{
			try
			{
				var result = await InputExecutor.ExecuteAsync(requestText);

				if (!result.Rovers.Any())
				{
					return "No rovers were specified in the input";
				}

				return string.Join(Constants.LINE_BREAK_2, result.Rovers.Select(r => $"{r.FinalXCoordinate} {r.FinalYCoordinate} {(char)r.FinalHeading}"));
			}
			catch (Exception exception)
			{
				return string.Join(Constants.LINE_BREAK_2, exception.Message, exception?.InnerException?.Message);
			}
		}
	}
}
