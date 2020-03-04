using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MRTC.Library.Parsers
{
	[TestClass]
	public class ParserTest
	{
		private readonly string pathPrefix = "Parser";
		private ExampleParser parser;

		[TestInitialize]
		public void Initialize()
		{
			parser = new ExampleParser();
		}

		[TestMethod]
		public async Task ParseTextAsync()
		{
			var input = "hello";
			var result = await parser.ParseAsync(input);
			Assert.AreEqual(input, result);
		}

		[TestMethod]
		public async Task ParseStreamAsync()
		{
			var path = Path.Combine(pathPrefix, "-hello.txt");

			string result;
			using (var stream = File.OpenRead(path))
			{
				result = await parser.ParseAsync(stream);
			}

			Assert.AreEqual("hello", result);
		}

		[TestMethod]
		public async Task ParseFileAsync()
		{
			var path = Path.Combine(pathPrefix, "-hello.txt");
			var result = await parser.ParseAsync(new FileInfo(path));
			Assert.AreEqual("hello", result);
		}
	}

	internal class ExampleParser : Parser<string>
	{
		public override Task<string> ParseAsync(string text)
		{
			return Task.FromResult(text);
		}
	}
}
