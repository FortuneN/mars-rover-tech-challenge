using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MRTC.Library
{
	[TestClass]
	public class ChallengeTest
	{
		private InputExecutor inputExecutor;

		[TestInitialize]
		public void Initialize()
		{
			inputExecutor = new InputExecutor();
		}

		[TestMethod]
		public async Task TestTextInputAsync()
		{
			var output = await inputExecutor.ExecuteAsync(@"
				5 5
				1 2 N
				LMLMLMLMM
				3 3 E
				MMRMMRMRRM
			");

			var rover1 = output.Rovers[0];
			Assert.AreEqual("1 3 N", $"{rover1.FinalXCoordinate} {rover1.FinalYCoordinate} {rover1.FinalHeading}");

			var rover2 = output.Rovers[1];
			Assert.AreEqual("5 1 E", $"{rover2.FinalXCoordinate} {rover2.FinalYCoordinate} {rover2.FinalHeading}");
		}

		[TestMethod]
		public async Task TestFileInputAsync()
		{
			var file = new FileInfo("example.txt");
			var output = await inputExecutor.ExecuteAsync(file);

			var rover1 = output.Rovers[0];
			Assert.AreEqual("1 3 N", $"{rover1.FinalXCoordinate} {rover1.FinalYCoordinate} {rover1.FinalHeading}");

			var rover2 = output.Rovers[1];
			Assert.AreEqual("5 1 E", $"{rover2.FinalXCoordinate} {rover2.FinalYCoordinate} {rover2.FinalHeading}");
		}

		[TestMethod]
		public async Task TestStreamInputAsync()
		{
			var file = new FileInfo("example.txt");
			
			using (var stream = file.OpenRead())
			{
				var output = await inputExecutor.ExecuteAsync(file);

				var rover1 = output.Rovers[0];
				Assert.AreEqual("1 3 N", $"{rover1.FinalXCoordinate} {rover1.FinalYCoordinate} {rover1.FinalHeading}");

				var rover2 = output.Rovers[1];
				Assert.AreEqual("5 1 E", $"{rover2.FinalXCoordinate} {rover2.FinalYCoordinate} {rover2.FinalHeading}");
			}
		}
	}
}