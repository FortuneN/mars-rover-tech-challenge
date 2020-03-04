using System;
using System.Collections.Generic;

namespace MRTC.Library
{
    /// <summary>
    /// System input which specifies a plateau, rovers and sets of commands for the rovers
    /// </summary>
    public class Input
	{
        /// <summary>
        /// Plateau on which the rovers roam
        /// </summary>
        public Plateau Plateau { get; }

        /// <summary>
        /// Rovers on the plateau
        /// </summary>
		public IReadOnlyList<Rover> Rovers { get; }

        /// <summary>
        /// Sets of commands for the rovers
        /// </summary>
		public IReadOnlyList<CommandSet> CommandSets { get; }

        /// <summary>
        /// Creates input with specified plateau and rovers
        /// </summary>
        /// <param name="plateau">Plateau on which the rovers roam</param>
        /// <param name="rovers">Rovers on the plateau</param>
        /// <param name="commandSets">Sets of commands for the rovers</param>
		public Input(Plateau plateau, List<Rover> rovers, List<CommandSet> commandSets)
		{
            Plateau = plateau ?? throw new ArgumentNullException(nameof(plateau));
			Rovers = rovers ?? throw new ArgumentNullException(nameof(rovers));
            CommandSets = commandSets ?? throw new ArgumentNullException(nameof(commandSets));
        }
	}
}