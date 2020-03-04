using System;

namespace MRTC.Library
{
	/// <summary>
	/// A line on system input
	/// </summary>
	public class InputLine
	{
		/// <summary>
		/// Line number (1-based)
		/// </summary>
		public long Number { get; private set; }

		/// <summary>
		/// Line content
		/// </summary>
		public string Text { get; private set; }

		/// <summary>
		/// Creates a line with specified line number and content text
		/// </summary>
		/// <param name="number">Line number (1-based)</param>
		/// <param name="text">Line content</param>
		public InputLine(long number, string text)
		{
			if (number < 1)
			{
				throw new ArgumentException(nameof(number), "Number must be 1 on greater");
			}

			if (string.IsNullOrWhiteSpace(text))
			{
				throw new ArgumentNullException(nameof(text));
			}

			Number = number;
			Text = text;
		}
	}
}