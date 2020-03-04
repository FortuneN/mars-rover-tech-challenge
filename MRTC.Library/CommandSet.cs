using MRTC.Library.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRTC.Library
{
	/// <summary>
	/// Set of commands
	/// </summary>
	public class CommandSet: List<Command>
	{
		private readonly CommandSetParser parser;

		/// <summary>
		/// The rover for whom the commands are inteded
		/// </summary>
		public Rover Rover { get; private set; }

		/// <summary>
		/// Initializes a set of commands for a rover
		/// </summary>
		public CommandSet()
		{
			parser = new CommandSetParser();
		}

		/// <summary>
		/// Initializes a set of commands for a rover
		/// </summary>
		public CommandSet(CommandSetParser parser)
		{
			this.parser = parser ?? throw new ArgumentNullException(nameof(parser));
		}

		/// <summary>
		/// Sets the rover for whom the commands are inteded
		/// </summary>
		/// <param name="rover">The rover for whom the commands are inteded</param>
		public void SetRover(Rover rover)
		{
			Rover = rover ?? throw new ArgumentNullException(nameof(rover));
		}

		// <summary>
		/// Add commands
		/// </summary>
		/// <param name="characters">Characters representing commands</param>
		public async Task AddAsync(params char[] commands)
		{
			if (commands == null)
			{
				throw new ArgumentNullException(nameof(commands));
			}

			await AddAsync(new string(commands));
		}

		/// <summary>
		/// Add commands
		/// </summary>
		/// <param name="commands">Characters representing commands</param>
		public async Task AddAsync(IEnumerable<char> commands)
		{
			if (commands == null)
			{
				throw new ArgumentNullException(nameof(commands));
			}

			await AddAsync(new string(commands.ToArray()));
		}

		/// <summary>
		/// Add commands
		/// </summary>
		/// <param name="command">String with characters representing commands</param>
		public async Task AddAsync(string commands)
		{
			if (string.IsNullOrWhiteSpace(commands))
			{
				throw new ArgumentNullException(nameof(commands));
			}

			var commandSet = await parser.ParseAsync(commands);
			AddRange(commandSet);
		}

		/// <summary>
		/// Executes the set of commands on rover
		/// </summary>
		public void Execute()
		{
			if (Rover == null)
			{
				throw new InvalidOperationException("The rover on which the commands must be executed must be specified. Hint: Use SetRover");
			}

			foreach (var command in this)
			{
				switch (command)
				{
					case Command.Move:
						Rover.Move();
						break;

					case Command.RotateLeft:
						Rover.RotateLeft();
						break;

					case Command.RotateRight:
						Rover.RotateRight();
						break;
				}
			}
		}
	}
}