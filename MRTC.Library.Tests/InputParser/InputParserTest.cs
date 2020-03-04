using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MRTC.Library.Parsers
{
	[TestClass]
	public class InputParserTest
	{
		private readonly string pathPrefix = "InputParser";
		private InputParser inputParser;

		[TestInitialize]
		public void Initialize()
		{
			inputParser = new InputParser();
		}

		[TestMethod]
		public async Task TextInputTestAsync()
		{
			var input = await inputParser.ParseAsync(@"
				5 6
				1 2 N
				LMLMLMLMM
				3 5 E
				MMRMMRMRRM
			");

			Assert.IsNotNull(input.Plateau);
			Assert.IsNotNull(input.Rovers);
			Assert.AreEqual(2, input.Rovers.Count);

			Assert.AreEqual(5, input.Plateau.MaximumXCoordinate);
			Assert.AreEqual(6, input.Plateau.MaximumYCoordinate);
			
			Assert.AreEqual(1, input.Rovers[0].XCoordinate);
			Assert.AreEqual(2, input.Rovers[0].YCoordinate);
			Assert.AreEqual(Direction.North, input.Rovers[0].Heading);

			Assert.AreEqual(3, input.Rovers[1].XCoordinate);
			Assert.AreEqual(5, input.Rovers[1].YCoordinate);
			Assert.AreEqual(Direction.East, input.Rovers[1].Heading);
		}

		[TestMethod]
		public async Task FileInputTestAsync()
		{
			var filePath = Path.Combine(pathPrefix, "-simple.txt");
			var fileInfo = new FileInfo(filePath);
			var input = await inputParser.ParseAsync(fileInfo);

			Assert.IsNotNull(input.Plateau);
			Assert.IsNotNull(input.Rovers);
			Assert.AreEqual(2, input.Rovers.Count);

			Assert.AreEqual(5, input.Plateau.MaximumXCoordinate);
			Assert.AreEqual(6, input.Plateau.MaximumYCoordinate);

			Assert.AreEqual(1, input.Rovers[0].XCoordinate);
			Assert.AreEqual(2, input.Rovers[0].YCoordinate);
			Assert.AreEqual(Direction.North, input.Rovers[0].Heading);

			Assert.AreEqual(3, input.Rovers[1].XCoordinate);
			Assert.AreEqual(5, input.Rovers[1].YCoordinate);
			Assert.AreEqual(Direction.East, input.Rovers[1].Heading);
		}

		[TestMethod]
		public async Task StreamInputTestAsync()
		{
			Input input;

			var filePath = Path.Combine(pathPrefix, "-simple.txt");
			using (var stream = File.OpenRead(filePath))
			{
				input = await inputParser.ParseAsync(stream);
			}

			Assert.IsNotNull(input.Plateau);
			Assert.IsNotNull(input.Rovers);
			Assert.AreEqual(2, input.Rovers.Count);

			Assert.AreEqual(5, input.Plateau.MaximumXCoordinate);
			Assert.AreEqual(6, input.Plateau.MaximumYCoordinate);

			Assert.AreEqual(1, input.Rovers[0].XCoordinate);
			Assert.AreEqual(2, input.Rovers[0].YCoordinate);
			Assert.AreEqual(Direction.North, input.Rovers[0].Heading);

			Assert.AreEqual(3, input.Rovers[1].XCoordinate);
			Assert.AreEqual(5, input.Rovers[1].YCoordinate);
			Assert.AreEqual(Direction.East, input.Rovers[1].Heading);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task EmptyStringArgumentFailTestAsync()
		{
			string emptyString = string.Empty;
			await inputParser.ParseAsync(emptyString);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task NullStringFailTestAsync()
		{
			string nullString = null;
			await inputParser.ParseAsync(nullString);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task NullStreamFailTestAsync()
		{
			Stream nullStream = null;
			await inputParser.ParseAsync(nullStream);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task NullFileInfoFailTestAsync()
		{
			FileInfo nullFileInfo = null;
			await inputParser.ParseAsync(nullFileInfo);
		}
	}
}
