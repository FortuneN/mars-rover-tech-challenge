using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MRTC.Library.Parsers
{
	[TestClass]
	public class CommandSetParserTest
	{
		private CommandSetParser commandSetParser;

		[TestInitialize]
		public void Initialize()
		{
			commandSetParser = new CommandSetParser();
		}

		[TestMethod]
		public async Task SimpleSuccessTestAsync()
		{
			var input = "LMR";
			var commandSet = await commandSetParser.ParseAsync(input);
			
			Assert.AreEqual(commandSet.Count, 3);
			Assert.AreEqual(Command.RotateLeft, commandSet[0]);
			Assert.AreEqual(Command.Move, commandSet[1]);
			Assert.AreEqual(Command.RotateRight, commandSet[2]);
		}

		[TestMethod]
		public async Task SpacedOutSuccessTestAsync()
		{
			var input = "L M R M";
			var commandSet = await commandSetParser.ParseAsync(input);

			Assert.AreEqual(commandSet.Count, 4);
			Assert.AreEqual(Command.RotateLeft, commandSet[0]);
			Assert.AreEqual(Command.Move, commandSet[1]);
			Assert.AreEqual(Command.RotateRight, commandSet[2]);
			Assert.AreEqual(Command.Move, commandSet[3]);
		}

		[TestMethod]
		public async Task LowerCaseSuccessTestAsync()
		{
			var input = "lmmr";
			var commandSet = await commandSetParser.ParseAsync(input);

			Assert.AreEqual(commandSet.Count, 4);
			Assert.AreEqual(Command.RotateLeft, commandSet[0]);
			Assert.AreEqual(Command.Move, commandSet[1]);
			Assert.AreEqual(Command.Move, commandSet[2]);
			Assert.AreEqual(Command.RotateRight, commandSet[3]);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task EmptyStringArgumentFailTestAsync()
		{
			await commandSetParser.ParseAsync(string.Empty);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task NullStringFailTestAsync()
		{
			string nullString = null;
			await commandSetParser.ParseAsync(nullString);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task NullStreamFailTestAsync()
		{
			Stream nullStream = null;
			await commandSetParser.ParseAsync(nullStream);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task NullFileInfoFailTestAsync()
		{
			FileInfo nullFileInfo = null;
			await commandSetParser.ParseAsync(nullFileInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(CommandSetParserException), AllowDerivedTypes = true)]
		public async Task InvalidCommandFailTestAsync()
		{
			await commandSetParser.ParseAsync("LMX");
		}
	}
}
