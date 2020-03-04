using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MRTC.Library
{
	[TestClass]
	public class InputLineTest
	{
		[TestMethod]
		public void SuccessTest()
		{
			var input = "hello";
			var inputLine = new InputLine(45, input);

			Assert.AreEqual(45, inputLine.Number);
			Assert.AreEqual(input, inputLine.Text);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NullTextFailTest()
		{
			new InputLine(45, null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void NegativeLineNumberFailTest()
		{
			new InputLine(0, "hello");
		}
	}
}
