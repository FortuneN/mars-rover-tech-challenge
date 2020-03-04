using System;
using System.Linq;
using System.Collections.Generic;

namespace MRTC.Library
{
	/// <summary>
	/// Represents system output 
	/// </summary>
	public class Output
	{
		/// <summary>
		/// Pleteau on which rovers roam
		/// </summary>
		public Plateau Plateau { get; internal set; }

		/// <summary>
		/// The rovers on the plateau
		/// </summary>
		public List<ResultRover> Rovers { get; internal set; }

		/// <summary>
		/// Takes snapshots of the initial rover positions
		/// </summary>
		/// <param name="initialRovers">Rovers</param>
		public void SetInitialRoverPositions(IEnumerable<Rover> initialRovers)
		{
			Rovers = initialRovers.Select(ir => new ResultRover
			{
				Id = ir.Id.ToString(),
				InitialXCoordinate = ir.XCoordinate,
				InitialYCoordinate = ir.YCoordinate,
				InitialHeading = (char)ir.Heading
			}).ToList();
		}

		/// <summary>
		/// Takes snapshots of the final rover positions
		/// </summary>
		/// <param name="finalRovers">Rovers</param>
		public void SetFinalRoverPositions(IEnumerable<Rover> finalRovers)
		{
			foreach (var fr in finalRovers)
			{
				var rover = Rovers.SingleOrDefault(x => x.Id.Equals(fr.Id, StringComparison.OrdinalIgnoreCase));
				rover.FinalXCoordinate = fr.XCoordinate;
				rover.FinalYCoordinate = fr.YCoordinate;
				rover.FinalHeading = (char)fr.Heading;
			}
		}

		/// <summary>
		/// Represents a rover in the system output
		/// </summary>
		public class ResultRover
		{
			/// <summary>
			/// Rover's ID
			/// </summary>
			public string Id { get; internal set; }

			/// <summary>
			/// X-Coordinate before movement
			/// </summary>
			public long InitialXCoordinate { get; internal set; }

			/// <summary>
			/// Y-Coordinate before movement
			/// </summary>
			public long InitialYCoordinate { get; internal set; }

			/// <summary>
			/// Heading direction before movement
			/// </summary>
			public char InitialHeading { get; internal set; }

			/// <summary>
			/// X-Coordinate after movement
			/// </summary>
			public long FinalXCoordinate { get; internal set; }

			/// <summary>
			/// Y-Coordinate after movement
			/// </summary>
			public long FinalYCoordinate { get; internal set; }

			/// <summary>
			/// Heading direction after movement
			/// </summary>
			public char FinalHeading { get; internal set; }
		}
	}
}
