using System;
using System.Linq;
using System.Threading.Tasks;

namespace MRTC.Library.Parsers
{
	/// <summary>
	/// Parser for rover command set
	/// Example input : LMLMLMLMM
	/// </summary>
	public class CommandSetParser : Parser<CommandSet>
	{
		private static readonly string SPACE = " ";
		private static readonly string DIRECTIONS = string.Join(", ", Enum.GetValues(typeof(Direction)).OfType<Direction>().Select(x => (char)x));

		/// <summary>
		/// Parse from text
		/// </summary>
		/// <param name="text">Input text</param>
		/// <returns>Parsed result</returns>
		public override Task<CommandSet> ParseAsync(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new ArgumentNullException(nameof(text));
			}

			try
			{
				var commands = new CommandSet();
				var tokens = text.ToUpper().Replace(SPACE, null).ToCharArray();

				for (var i = 0; i < tokens.Length; i++)
				{
					var number = i + 1;

					// parse token

					if (!TryParseCommand(tokens[i], out var command))
					{
						throw new CommandSetParserException($"Invalid token {number} '{tokens[i]}'. Expected one of [{DIRECTIONS}]");
					}

					// add command

					commands.Add(command);
				}

				return Task.FromResult(commands);
			}
			catch (Exception exception)
			{
				throw new CommandSetParserException("Failed to parse command set", exception);
			}
		}

		/// <summary>
		/// Attempts to parse a character into a command
		/// </summary>
		/// <param name="value">Character to parse</param>
		/// <param name="command">Parsed command</param>
		/// <returns>true if parse succeded; otherwise, false.</returns>
		public static bool TryParseCommand(char value, out Command command)
		{
			command = default;

			if (!Enum.IsDefined(typeof(Command), (int)value))
			{
				return false;
			}

			command = (Command)value;

			return true;
		}
	}
}
