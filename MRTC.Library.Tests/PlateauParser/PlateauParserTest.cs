using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MRTC.Library.Parsers
{
	[TestClass]
	public class PlateauParserTest
	{
		private PlateauParser plateauParser;

		[TestInitialize]
		public void Initialize()
		{
			plateauParser = new PlateauParser();
		}

		[TestMethod]
		public async Task NorthSuccessTestAsync()
		{
			var plateau = await plateauParser.ParseAsync("1 2");
			
			Assert.AreEqual(1, plateau.MaximumXCoordinate);
			Assert.AreEqual(2, plateau.MaximumYCoordinate);
		}

		[TestMethod]
		public async Task SpacedOutSuccessTestAsync()
		{
			var plateau = await plateauParser.ParseAsync("   9    1  ");
			Assert.AreEqual(9, plateau.MaximumXCoordinate);
			Assert.AreEqual(1, plateau.MaximumYCoordinate);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task EmptyStringArgumentFailTestAsync()
		{
			await plateauParser.ParseAsync(string.Empty);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task NullStringFailTestAsync()
		{
			string nullString = null;
			await plateauParser.ParseAsync(nullString);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task NullStreamFailTestAsync()
		{
			Stream nullStream = null;
			await plateauParser.ParseAsync(nullStream);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task NullFileInfoFailTestAsync()
		{
			FileInfo nullFileInfo = null;
			await plateauParser.ParseAsync(nullFileInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(PlateauParserException), AllowDerivedTypes = true)]
		public async Task TooManyTokensFailTestAsync()
		{
			await plateauParser.ParseAsync("0 9 0 W");
		}

		[TestMethod]
		[ExpectedException(typeof(PlateauParserException), AllowDerivedTypes = true)]
		public async Task TooFewTokensFailTestAsync()
		{
			await plateauParser.ParseAsync("0 W");
		}

		[TestMethod]
		[ExpectedException(typeof(PlateauParserException), AllowDerivedTypes = true)]
		public async Task NoHeadingFailTestAsync()
		{
			await plateauParser.ParseAsync("0 9 0");
		}

		[TestMethod]
		[ExpectedException(typeof(PlateauParserException), AllowDerivedTypes = true)]
		public async Task InvalidSeparatorsFailTestAsync()
		{
			await plateauParser.ParseAsync("0.9.W");
		}

		[TestMethod]
		[ExpectedException(typeof(PlateauParserException), AllowDerivedTypes = true)]
		public async Task NoSeperatorsFailTestAsync()
		{
			await plateauParser.ParseAsync("09W");
		}

		[TestMethod]
		[ExpectedException(typeof(PlateauParserException), AllowDerivedTypes = true)]
		public async Task NegativeXFailTestAsync()
		{
			await plateauParser.ParseAsync("-1 9 W");
		}

		[TestMethod]
		[ExpectedException(typeof(PlateauParserException), AllowDerivedTypes = true)]
		public async Task NegativeYFailTestAsync()
		{
			await plateauParser.ParseAsync("1 -9 W");
		}

		[TestMethod]
		[ExpectedException(typeof(PlateauParserException), AllowDerivedTypes = true)]
		public async Task VerboseheadingFailTestAsync()
		{
			await plateauParser.ParseAsync("1 -9 West");
		}
	}
}