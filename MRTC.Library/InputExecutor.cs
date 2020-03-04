using System;
using System.IO;
using System.Threading.Tasks;
using MRTC.Library.Parsers;

namespace MRTC.Library
{
	public class InputExecutor
	{
		private readonly InputParser inputParser;

		public InputExecutor()
		{
			inputParser = new InputParser();
		}

		public Task ExecuteAsync()
		{
			throw new NotImplementedException();
		}

		public InputExecutor(InputParser inputParser)
		{
			this.inputParser = inputParser ?? throw new ArgumentNullException(nameof(inputParser));
		}

		public async Task<Output> ExecuteAsync(string inputText)
		{
			if (string.IsNullOrWhiteSpace(inputText))
			{
				throw new ArgumentNullException(nameof(inputText));
			}

			var input = await inputParser.ParseAsync(inputText);
			return await ExecuteAsync(input);
		}

		public async Task<Output> ExecuteAsync(FileInfo inputFile)
		{
			if (inputFile == null)
			{
				throw new ArgumentNullException(nameof(inputFile));
			}

			var input = await inputParser.ParseAsync(inputFile);
			return await ExecuteAsync(input);
		}

		public async Task<Output> ExecuteAsync(Stream inputStream)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException(nameof(inputStream));
			}

			var input = await inputParser.ParseAsync(inputStream);
			return await ExecuteAsync(input);
		}

		public Task<Output> ExecuteAsync(Input input)
		{
			if (input == null)
			{
				throw new ArgumentNullException(nameof(input));
			}

			var output = new Output();

			output.Plateau = input.Plateau;
			output.SetInitialRoverPositions(input.Rovers);

			foreach (var commandSet in input.CommandSets)
			{
				commandSet.Execute();
			}

			output.SetFinalRoverPositions(input.Rovers);
			return Task.FromResult(output);
		}
	}
}