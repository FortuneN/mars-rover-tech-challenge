using System;

namespace MRTC.Library
{
	/// <summary>
	/// Represents a mars rover
	/// </summary>
	public class Rover
	{
		/// <summary>
		/// Identifier of the rover
		/// </summary>
		public string Id { get; }

		/// <summary>
		/// Rover's current X coordinate
		/// </summary>
		public long XCoordinate { get; private set; }

		/// <summary>
		/// Rover's current Y coordinate
		/// </summary>
		public long YCoordinate { get; private set; }

		/// <summary>
		/// Rover's current heading direction
		/// </summary>
		public Direction Heading { get; private set; }

		/// <summary>
		/// Plateau that the rover is on
		/// </summary>
		public Plateau Plateau { get; private set; }

		/// <summary>
		/// Initializes a rover with the specified values
		/// </summary>
		/// <param name="xCoordinate">Rover's initial X coordinate</param>
		/// <param name="yCoordinate">Rover's initial Y coordinate</param>
		/// <param name="heading">Rover's initial heading direction</param>
		public Rover(long xCoordinate, long yCoordinate, Direction heading)
		{
			XCoordinate = xCoordinate;
			YCoordinate = yCoordinate;
			Heading = heading;
			Id = $"{xCoordinate}{yCoordinate}{(char)heading}";
		}

		/// <summary>
		/// Initializes a rover with the specified values
		/// </summary>
		/// <param name="xCoordinate">Rover's initial X coordinate</param>
		/// <param name="yCoordinate">Rover's initial Y coordinate</param>
		/// <param name="heading">Rover's initial heading direction</param>
		/// <param name="plateau">Plateau that the rover is on</param>
		public Rover(long xCoordinate, long yCoordinate, Direction heading, Plateau plateau): this(xCoordinate, yCoordinate, heading)
		{
			SetPlateau(plateau);
		}

		/// <summary>
		/// Sets the plateau that the rover is on
		/// The plateau sets the bounds of the rover's movements
		/// </summary>
		/// <param name="plateau">Plateau that the rover is on</param>
		/// <exception cref="RoverOutsidePlateauException">Thrown if the rover is outside the plateau's bounds</exception>
		public void SetPlateau (Plateau plateau)
		{
			if (plateau == null)
			{
				throw new ArgumentNullException(nameof(plateau));
			}

			if (XCoordinate < plateau.MinimumXCoordinate)
			{
				throw new RoverOutsidePlateauException();
			}

			if (XCoordinate > plateau.MaximumXCoordinate)
			{
				throw new RoverOutsidePlateauException();
			}

			if (YCoordinate < plateau.MinimumYCoordinate)
			{
				throw new RoverOutsidePlateauException();
			}

			if (YCoordinate > plateau.MaximumYCoordinate)
			{
				throw new RoverOutsidePlateauException();
			}

			Plateau = plateau;
		}

		/// <summary>
		/// Moves the rover 1 step forward
		/// </summary>
		/// <exception cref="RoverOutsidePlateauException">Thrown when movement would put the outside it's current plateau</exception>
		public void Move()
		{
			long newCoordinate;

			switch (Heading)
			{
				case Direction.North:

					newCoordinate = YCoordinate + 1;
					
					if (Plateau != null && newCoordinate > Plateau.MaximumYCoordinate)
					{
						throw new RoverOutsidePlateauException();
					}

					YCoordinate = newCoordinate;
					break;

				case Direction.South:
					
					newCoordinate = YCoordinate - 1;
					
					if (Plateau != null && newCoordinate < Plateau.MinimumYCoordinate)
					{
						throw new RoverOutsidePlateauException();
					}

					YCoordinate = newCoordinate;
					break;

				case Direction.East:
					
					newCoordinate = XCoordinate + 1;

					if (Plateau != null && newCoordinate > Plateau.MaximumXCoordinate)
					{
						throw new RoverOutsidePlateauException();
					}

					XCoordinate = newCoordinate;
					break;

				case Direction.West:
					
					newCoordinate = XCoordinate - 1;
					
					if (Plateau != null && newCoordinate < Plateau.MinimumXCoordinate)
					{
						throw new RoverOutsidePlateauException();
					}

					XCoordinate = newCoordinate;
					break;
			}
		}

		/// <summary>
		/// Changes the direction (Heading) that the rover is facing by -90 degrees
		/// </summary>
		public void RotateLeft()
		{
			switch (Heading)
			{
				case Direction.North:
					Heading = Direction.West;
					break;

				case Direction.East:
					Heading = Direction.North;
					break;

				case Direction.South:
					Heading = Direction.East;
					break;

				case Direction.West:
					Heading = Direction.South;
					break;
			}
		}

		/// <summary>
		/// Changes the direction (Heading) that the rover is facing by +90 degrees
		/// </summary>
		public void RotateRight()
		{
			switch (Heading)
			{
				case Direction.North:
					Heading = Direction.East;
					break;

				case Direction.East:
					Heading = Direction.South;
					break;

				case Direction.South:
					Heading = Direction.West;
					break;

				case Direction.West:
					Heading = Direction.North;
					break;
			}
		}

		/// <summary>
		/// Returns a string that represents the current Rover
		/// </summary>
		/// <returns>A string that represents the current Rover</returns>
		public override string ToString()
		{
			return Id;
		}
	}
}