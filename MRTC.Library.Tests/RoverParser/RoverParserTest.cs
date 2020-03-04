using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MRTC.Library.Parsers
{
	[TestClass]
	public class RoverParserTest
	{
		private RoverParser roverParser;

		[TestInitialize]
		public void Initialize()
		{
			roverParser = new RoverParser();
		}

		[TestMethod]
		public async Task NorthSuccessTestAsync()
		{
			var rover = await roverParser.ParseAsync("1 2 N");

			Assert.AreEqual(1, rover.XCoordinate);
			Assert.AreEqual(2, rover.YCoordinate);
			Assert.AreEqual(Direction.North, rover.Heading);
		}

		[TestMethod]
		public async Task EastSuccessTestAsync()
		{
			var rover = await roverParser.ParseAsync("3 4 E");

			Assert.AreEqual(3, rover.XCoordinate);
			Assert.AreEqual(4, rover.YCoordinate);
			Assert.AreEqual(Direction.East, rover.Heading);
		}

		[TestMethod]
		public async Task SouthSuccessTestAsync()
		{
			var rover = await roverParser.ParseAsync("5 6 S");

			Assert.AreEqual(5, rover.XCoordinate);
			Assert.AreEqual(6, rover.YCoordinate);
			Assert.AreEqual(Direction.South, rover.Heading);
		}

		[TestMethod]
		public async Task WestSuccessTestAsync()
		{
			var rover = await roverParser.ParseAsync("7 8 W");

			Assert.AreEqual(7, rover.XCoordinate);
			Assert.AreEqual(8, rover.YCoordinate);
			Assert.AreEqual(Direction.West, rover.Heading);
		}

		[TestMethod]
		public async Task SpacedOutSuccessTestAsync()
		{
			var rover = await roverParser.ParseAsync("   9    0    W   ");

			Assert.AreEqual(9, rover.XCoordinate);
			Assert.AreEqual(0, rover.YCoordinate);
			Assert.AreEqual(Direction.West, rover.Heading);
		}

		[TestMethod]
		public async Task LowerCase1HeadingSuccessTestAsync()
		{
			var rover = await roverParser.ParseAsync("9 0 w");

			Assert.AreEqual(9, rover.XCoordinate);
			Assert.AreEqual(0, rover.YCoordinate);
			Assert.AreEqual(Direction.West, rover.Heading);
		}

		[TestMethod]
		public async Task LowerCase2HeadingSuccessTestAsync()
		{
			var rover = await roverParser.ParseAsync("9 0 e");

			Assert.AreEqual(9, rover.XCoordinate);
			Assert.AreEqual(0, rover.YCoordinate);
			Assert.AreEqual(Direction.East, rover.Heading);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task EmptyStringArgumentFailTestAsync()
		{
			await roverParser.ParseAsync(string.Empty);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task NullStringFailTestAsync()
		{
			string nullString = null;
			await roverParser.ParseAsync(nullString);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task NullStreamFailTestAsync()
		{
			Stream nullStream = null;
			await roverParser.ParseAsync(nullStream);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task NullFileInfoFailTestAsync()
		{
			FileInfo nullFileInfo = null;
			await roverParser.ParseAsync(nullFileInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(RoverParserException), AllowDerivedTypes = true)]
		public async Task TooManyTokensFailTestAsync()
		{
			await roverParser.ParseAsync("0 9 0 W");
		}

		[TestMethod]
		[ExpectedException(typeof(RoverParserException), AllowDerivedTypes = true)]
		public async Task TooFewTokensFailTestAsync()
		{
			await roverParser.ParseAsync("0 W");
		}

		[TestMethod]
		[ExpectedException(typeof(RoverParserException), AllowDerivedTypes = true)]
		public async Task NoHeadingFailTestAsync()
		{
			await roverParser.ParseAsync("0 9 0");
		}

		[TestMethod]
		[ExpectedException(typeof(RoverParserException), AllowDerivedTypes = true)]
		public async Task InvalidSeparatorsFailTestAsync()
		{
			await roverParser.ParseAsync("0.9.W");
		}

		[TestMethod]
		[ExpectedException(typeof(RoverParserException), AllowDerivedTypes = true)]
		public async Task NoSeperatorsFailTestAsync()
		{
			await roverParser.ParseAsync("09W");
		}

		[TestMethod]
		[ExpectedException(typeof(RoverParserException), AllowDerivedTypes = true)]
		public async Task NegativeXFailTestAsync()
		{
			await roverParser.ParseAsync("-1 9 W");
		}

		[TestMethod]
		[ExpectedException(typeof(RoverParserException), AllowDerivedTypes = true)]
		public async Task NegativeYFailTestAsync()
		{
			await roverParser.ParseAsync("1 -9 W");
		}

		[TestMethod]
		[ExpectedException(typeof(RoverParserException), AllowDerivedTypes = true)]
		public async Task VerboseheadingFailTestAsync()
		{
			await roverParser.ParseAsync("1 -9 West");
		}
	}
}
