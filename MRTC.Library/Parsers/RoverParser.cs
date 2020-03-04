using System;
using System.Linq;
using System.Threading.Tasks;

namespace MRTC.Library.Parsers
{
	/// <summary>
	/// Parser for rover initialization
	/// Example input : 1 2 N
	/// </summary>
	public class RoverParser : Parser<Rover>
	{
		private static readonly char[] SEPERATOR = new[] { ' ' };
		private static readonly string DIRECTIONS = string.Join(", ", Enum.GetValues(typeof(Direction)).OfType<Direction>().Select(x => (char)x));

		/// <summary>
		/// Parse from text
		/// </summary>
		/// <param name="text">Input text</param>
		/// <returns>Parsed result</returns>
		public override Task<Rover> ParseAsync(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new ArgumentNullException(nameof(text));
			}

			try
			{
				// tokenize

				var tokens = text.Trim().Split(SEPERATOR, StringSplitOptions.RemoveEmptyEntries);

				// check length

				if (tokens.Length != 3)
				{
					throw new RoverParserException("Invalid token count. Expected 3");
				}

				// parse x-coordinate

				if (!long.TryParse(tokens[0], out var xCoordinate) || xCoordinate < 0)
				{
					throw new RoverParserException($"Invalid token 1 '{tokens[0]}'. Expected a none-negative integer");
				}

				// parse y-coordinate

				if (!long.TryParse(tokens[1], out var yCoordinate) || yCoordinate < 0)
				{
					throw new RoverParserException($"Invalid token 2 '{tokens[1]}'. Expected a none-negative integer");
				}

				// parse heading direction

				if (!TryParseDirection(tokens[2], out var heading))
				{
					throw new RoverParserException($"Invalid token 3 '{tokens[2]}'. Expected one of [{DIRECTIONS}]");
				}

				// create new rover

				var rover = new Rover(xCoordinate, yCoordinate, heading);

				// return

				return Task.FromResult(rover);
			}
			catch (Exception exception)
			{
				throw new RoverParserException("Failed to parse rover", exception);
			}
		}

		private static bool TryParseDirection(string value, out Direction direction)
		{
			direction = default;

			if (value.Length != 1)
			{
				return false;
			}

			int headingChar = value.ToUpper()[0];

			if (!Enum.IsDefined(typeof(Direction), headingChar))
			{
				return false;
			}

			direction = (Direction)headingChar;

			return true;
		}
	}
}
