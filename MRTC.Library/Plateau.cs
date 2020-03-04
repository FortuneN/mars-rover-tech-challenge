using System;

namespace MRTC.Library
{
	/// <summary>
	/// Represents a plateau that rovers can roam on
	/// </summary>
	public class Plateau
	{
		/// <summary>
		/// The lowest X coordinate of the plateau
		/// </summary>
		public long MinimumXCoordinate { get; }

		/// <summary>
		/// The highest X coordinate of the plateau
		/// </summary>
		public long MaximumXCoordinate { get; }

		/// <summary>
		/// The lowest Y coordinate of the plateau
		/// </summary>
		public long MinimumYCoordinate { get; }

		/// <summary>
		/// The highest Y coordinate of the plateau
		/// </summary>
		public long MaximumYCoordinate { get; }

		/// <summary>
		/// Initializes a plateau with the specified coordinate bounds
		/// </summary>
		/// <param name="minimumXCoordinate">The lowest X coordinate of the plateau</param>
		/// <param name="maximumXCoordinate">The highest X coordinate of the plateau</param>
		/// <param name="minimumYCoordinate">The lowest Y coordinate of the plateau</param>
		/// <param name="maximumYCoordinate">The highest Y coordinate of the plateau</param>
		public Plateau(long minimumXCoordinate, long maximumXCoordinate, long minimumYCoordinate, long maximumYCoordinate)
		{
			if (maximumXCoordinate < minimumXCoordinate)
			{
				throw new ArgumentException(nameof(maximumXCoordinate), $"{nameof(maximumXCoordinate)} must be equal to or greater than {minimumXCoordinate}");
			}

			if (maximumYCoordinate < minimumYCoordinate)
			{
				throw new ArgumentException(nameof(maximumYCoordinate), $"{nameof(maximumYCoordinate)} must be equal to or greater than {minimumYCoordinate}");
			}

			MinimumXCoordinate = minimumXCoordinate;
			MaximumXCoordinate = maximumXCoordinate;
			MinimumYCoordinate = minimumYCoordinate;
			MaximumYCoordinate = maximumYCoordinate;
		}

		/// <summary>
		/// Initializes a plateau with the specified maximum coordinate bounds.
		/// The minimum values will be zero (0)
		/// </summary>
		/// <param name="maximumXCoordinate">The highest X coordinate of the plateau</param>
		/// <param name="maximumYCoordinate"> The highest Y coordinate of the plateau</param>
		public Plateau(long maximumXCoordinate, long maximumYCoordinate) : this(0, maximumXCoordinate, 0, maximumYCoordinate)
		{
		}
	}
}