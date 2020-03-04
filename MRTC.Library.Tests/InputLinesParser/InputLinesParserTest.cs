using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MRTC.Library.Parsers
{
	[TestClass]
	public class InputLinesParserTest
	{
		private readonly string pathPrefix = "InputLinesParser";
		private InputLinesParser inputLinesParser;

		[TestInitialize]
		public void Initialize()
		{
			inputLinesParser = new InputLinesParser();
		}

		[TestMethod]
		public async Task SingleLineSuccessTestAsync()
		{
			var input = "12345";
			var lines = await inputLinesParser.ParseAsync(input);

			Assert.AreEqual(1, lines.Count());
			Assert.AreEqual(input, lines.First().Text);
		}

		[TestMethod]
		public async Task MultilineUnixSuccessTestAsync()
		{
			var lines = await inputLinesParser.ParseAsync("x\ny\nz");
			Assert.AreEqual(3, lines.Count());

			Assert.AreEqual(1, lines.ElementAt(0).Number);
			Assert.AreEqual("x", lines.ElementAt(0).Text);

			Assert.AreEqual(2, lines.ElementAt(1).Number);
			Assert.AreEqual("y", lines.ElementAt(1).Text);

			Assert.AreEqual(3, lines.ElementAt(2).Number);
			Assert.AreEqual("z", lines.ElementAt(2).Text);
		}

		[TestMethod]
		public async Task MultilineWindowsSuccessTestAsync()
		{
			var lines = await inputLinesParser.ParseAsync("x\r\ny\r\nz");
			Assert.AreEqual(3, lines.Count());

			Assert.AreEqual(1, lines.ElementAt(0).Number);
			Assert.AreEqual("x", lines.ElementAt(0).Text);

			Assert.AreEqual(2, lines.ElementAt(1).Number);
			Assert.AreEqual("y", lines.ElementAt(1).Text);

			Assert.AreEqual(3, lines.ElementAt(2).Number);
			Assert.AreEqual("z", lines.ElementAt(2).Text);
		}

		[TestMethod]
		public async Task MultilineWindowsAndUnixMixedSuccessTestAsync()
		{
			var lines = await inputLinesParser.ParseAsync("x\ny\r\nz");
			Assert.AreEqual(3, lines.Count());

			Assert.AreEqual(1, lines.ElementAt(0).Number);
			Assert.AreEqual("x", lines.ElementAt(0).Text);

			Assert.AreEqual(2, lines.ElementAt(1).Number);
			Assert.AreEqual("y", lines.ElementAt(1).Text);

			Assert.AreEqual(3, lines.ElementAt(2).Number);
			Assert.AreEqual("z", lines.ElementAt(2).Text);
		}

		[TestMethod]
		public async Task SkippedLinesSuccessTestAsync()
		{
			var lines = await inputLinesParser.ParseAsync("x\n\n\nz\n");
			Assert.AreEqual(2, lines.Count());

			Assert.AreEqual(lines.ElementAt(0).Number,  1 );
			Assert.AreEqual(lines.ElementAt(0).Text  , "x");

			Assert.AreEqual(lines.ElementAt(1).Number,  4 );
			Assert.AreEqual(lines.ElementAt(1).Text  , "z");
		}

		[TestMethod]
		public async Task SimpleStreamSuccessTestAsync()
		{
			IEnumerable<InputLine> lines;
			var path = Path.Combine(pathPrefix, "-simple.txt");

			using (var stream = File.OpenRead(path))
			{
				lines = await inputLinesParser.ParseAsync(stream);
			}

			Assert.AreEqual(2, lines.Count());

			Assert.AreEqual(1, lines.ElementAt(0).Number);
			Assert.AreEqual("aaa", lines.ElementAt(0).Text);

			Assert.AreEqual(3, lines.ElementAt(1).Number);
			Assert.AreEqual("ccc", lines.ElementAt(1).Text);
		}

		[TestMethod]
		public async Task SimpleFileSuccessTestAsync()
		{
			var path = Path.Combine(pathPrefix, "-simple.txt");
			var lines = await inputLinesParser.ParseAsync(new FileInfo(path));

			Assert.AreEqual(2, lines.Count());

			Assert.AreEqual(1, lines.ElementAt(0).Number);
			Assert.AreEqual("aaa", lines.ElementAt(0).Text);

			Assert.AreEqual(3, lines.ElementAt(1).Number);
			Assert.AreEqual("ccc", lines.ElementAt(1).Text);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task EmptyStreamFailTestAsync()
		{
			var path = Path.Combine(pathPrefix, "-empty.txt");
			using (var stream = File.OpenRead(path))
			{
				await inputLinesParser.ParseAsync(stream);
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task EmptyFileFailTestAsync()
		{
			var path = Path.Combine(pathPrefix, "-empty.txt");
			await inputLinesParser.ParseAsync(new FileInfo(path));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task EmptyStringArgumentFailTestAsync()
		{
			await inputLinesParser.ParseAsync(string.Empty);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task NullStringFailTestAsync()
		{
			string nullString = null;
			await inputLinesParser.ParseAsync(nullString);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task NullStreamFailTestAsync()
		{
			Stream nullStream = null;
			await inputLinesParser.ParseAsync(nullStream);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
		public async Task NullFileInfoFailTestAsync()
		{
			FileInfo nullFileInfo = null;
			await inputLinesParser.ParseAsync(nullFileInfo);
		}
	}
}