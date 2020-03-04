using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MRTC.Library
{
	[TestClass]
	public class PlateauTest
	{
		[TestMethod]
		public void SuccessTest1()
		{
			var maximumX = 1;
			var maximumY = 2;
			var plateau = new Plateau(maximumX, maximumY);
			
			Assert.AreEqual(0, plateau.MinimumXCoordinate);
			Assert.AreEqual(0, plateau.MinimumYCoordinate);

			Assert.AreEqual(maximumX, plateau.MaximumXCoordinate);
			Assert.AreEqual(maximumY, plateau.MaximumYCoordinate);
		}

		[TestMethod]
		public void SuccessTest2()
		{
			var minimumX = 1;
			var minimumY = 2;
			var maximumX = 3;
			var maximumY = 4;
			var plateau = new Plateau(minimumX, maximumX, minimumY, maximumY);

			Assert.AreEqual(minimumX, plateau.MinimumXCoordinate);
			Assert.AreEqual(minimumY, plateau.MinimumYCoordinate);

			Assert.AreEqual(maximumX, plateau.MaximumXCoordinate);
			Assert.AreEqual(maximumY, plateau.MaximumYCoordinate);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Min_X_GreaterThan_Max_X_FailTest1()
		{
			new Plateau(2, 1, 0, 0);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Min_X_GreaterThan_Max_X_FailTest2()
		{
			new Plateau(-1, 0); // default min x is zero (0)
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Min_Y_GreaterThan_Max_Y_FailTest1()
		{
			new Plateau(0, 0, 2, 1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Min_Y_GreaterThan_Max_Y_FailTest2()
		{
			new Plateau(0, -1); // default min y is zero (0)
		}
	}
}
