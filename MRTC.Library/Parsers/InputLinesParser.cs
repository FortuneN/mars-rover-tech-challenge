using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MRTC.Library.Parsers
{
	/// <summary>
	/// Parser for input lines.
	/// Empty lines are skipped
	/// </summary>
	public class InputLinesParser : Parser<IEnumerable<InputLine>>
	{
		private static readonly string[] LINE_SEPERATOR = new[] { "\r\n", "\n" }; // windows/unix

		/// <summary>
		/// Parse from text
		/// </summary>
		/// <param name="text">Input text</param>
		/// <returns>Parsed result</returns>
		public override Task<IEnumerable<InputLine>> ParseAsync(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new ArgumentNullException(nameof(text));
			}

			try
			{
				var lineNumber = 0L;

				var inputLines = new List<InputLine>();

				foreach (var line in text.Split(LINE_SEPERATOR, StringSplitOptions.None))
				{
					// increament line number

					lineNumber++;

					// skip empty line

					if (string.IsNullOrWhiteSpace(line))
					{
						continue;
					}

					// create new input line

					var inputLine = new InputLine(lineNumber, line.Trim());

					// add new input line

					inputLines.Add(inputLine);
				}

				return Task.FromResult((IEnumerable<InputLine>)inputLines);
			}
			catch (Exception exception)
			{
				throw new InputLinesParserException("Failed to parse input lines", exception);
			}
		}
	}
}