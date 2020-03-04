using System;
using System.Threading.Tasks;

namespace MRTC.Library.Parsers
{
	/// <summary>
	/// Parser for plateau initialization
	/// Example input : 5 5
	/// </summary>
	public class PlateauParser: Parser<Plateau>
	{
		private static readonly char[] SEPERATOR = new[] { ' ' };
		
		/// <summary>
		/// Parse from text
		/// </summary>
		/// <param name="text">Input text</param>
		/// <returns>Parsed result</returns>
		public override Task<Plateau> ParseAsync(string text)
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

				if (tokens.Length != 2)
				{
					throw new PlateauParserException("Invalid token count. Expected 2");
				}

				// parse minimumXCoordinate

				if (!long.TryParse(tokens[0], out var maximumXCoordinate) || maximumXCoordinate < 0)
				{
					throw new PlateauParserException($"Invalid token 1 (maximum x coordinate) '{tokens[0]}'. Expected a none-negative integer");
				}

				// parse maximumYCoordinate

				if (!long.TryParse(tokens[1], out var maximumYCoordinate) || maximumYCoordinate < 0)
				{
					throw new PlateauParserException($"Invalid token 2 (maximum y coordinate) '{tokens[1]}'. Expected a none-negative integer");
				}

				// create new plateau

				var plateau = new Plateau(maximumXCoordinate, maximumYCoordinate);

				// return

				return Task.FromResult(plateau);
			}
			catch (Exception exception)
			{
				throw new PlateauParserException("Failed to parser plateau", exception);
			}
		}
	}
}
