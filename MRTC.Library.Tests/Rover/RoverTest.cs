using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MRTC.Library
{
	[TestClass]
	public class RoverTest
	{
		[TestMethod]
		public void Initialization_SuccessTest1()
		{
			var xCoordinate = 0;
			var yCoordinate = 1;
			var heading = Direction.North;

			var rover = new Rover(xCoordinate, yCoordinate, heading);

			Assert.AreEqual(xCoordinate, rover.XCoordinate);
			Assert.AreEqual(yCoordinate, rover.YCoordinate);
			Assert.AreEqual(heading, rover.Heading);
		}

		[TestMethod]
		public void Initialization_SuccessTest2()
		{
			var xCoordinate = 2;
			var yCoordinate = 3;
			var heading = Direction.South;

			var rover = new Rover(xCoordinate, yCoordinate, heading);

			Assert.AreEqual(xCoordinate, rover.XCoordinate);
			Assert.AreEqual(yCoordinate, rover.YCoordinate);
			Assert.AreEqual(heading, rover.Heading);
		}

		[TestMethod]
		public void Initialization_SuccessTest3()
		{
			var xCoordinate = 4;
			var yCoordinate = 5;
			var heading = Direction.East;

			var rover = new Rover(xCoordinate, yCoordinate, heading);

			Assert.AreEqual(xCoordinate, rover.XCoordinate);
			Assert.AreEqual(yCoordinate, rover.YCoordinate);
			Assert.AreEqual(heading, rover.Heading);
		}

		[TestMethod]
		public void Initialization_SuccessTest4()
		{
			var xCoordinate = 6;
			var yCoordinate = 7;
			var heading = Direction.West;

			var rover = new Rover(xCoordinate, yCoordinate, heading);

			Assert.AreEqual(xCoordinate, rover.XCoordinate);
			Assert.AreEqual(yCoordinate, rover.YCoordinate);
			Assert.AreEqual(heading, rover.Heading);
		}

		[TestMethod]
		public void Move_North_SuccessTest()
		{
			var rover = new Rover(0, 0, Direction.North);
			rover.Move();

			Assert.AreEqual(1, rover.YCoordinate);
		}

		[TestMethod]
		public void Move_South_SuccessTest()
		{
			var rover = new Rover(0, 0, Direction.South);
			rover.Move();

			Assert.AreEqual(-1, rover.YCoordinate);
		}

		[TestMethod]
		public void Move_East_SuccessTest()
		{
			var rover = new Rover(0, 0, Direction.East);
			rover.Move();

			Assert.AreEqual(1, rover.XCoordinate);
		}

		[TestMethod]
		public void Move_West_SuccessTest()
		{
			var rover = new Rover(0, 0, Direction.West);
			rover.Move();

			Assert.AreEqual(-1, rover.XCoordinate);
		}

		[TestMethod]
		public void FacingNorth_RotateLeft_SuccessTest()
		{
			var rover = new Rover(0, 0, Direction.North);
			rover.RotateLeft();

			Assert.AreEqual(Direction.West, rover.Heading);
		}

		[TestMethod]
		public void FacingNorth_RotateRight_SuccessTest()
		{
			var rover = new Rover(0, 0, Direction.North);
			rover.RotateRight();

			Assert.AreEqual(Direction.East, rover.Heading);
		}

		[TestMethod]
		public void FacingSouth_RotateLeft_SuccessTest()
		{
			var rover = new Rover(0, 0, Direction.South);
			rover.RotateLeft();

			Assert.AreEqual(Direction.East, rover.Heading);
		}

		[TestMethod]
		public void FacingSouth_RotateRight_SuccessTest()
		{
			var rover = new Rover(0, 0, Direction.South);
			rover.RotateRight();

			Assert.AreEqual(Direction.West, rover.Heading);
		}

		[TestMethod]
		public void Valid_SetPlateau_SuccessTest()
		{
			var xCoordinate = 1;
			var yCoordinate = 1;
			var heading = Direction.West;

			var rover = new Rover(xCoordinate, yCoordinate, heading);
			rover.SetPlateau(new Plateau(1, 1));
			
			Assert.AreEqual(xCoordinate, rover.XCoordinate);
			Assert.AreEqual(yCoordinate, rover.YCoordinate);
			Assert.AreEqual(heading, rover.Heading);
		}

		[TestMethod]
		[ExpectedException(typeof(RoverOutsidePlateauException))]
		public void Invalid_SetPlateau_FailTest()
		{
			var rover = new Rover(1, 1, Direction.North);
			rover.SetPlateau(new Plateau(2, 3, 4, 5));
		}

		[TestMethod]
		[ExpectedException(typeof(RoverOutsidePlateauException))]
		public void Invalid_Move_FailTest1()
		{
			var rover = new Rover(0, 0, Direction.North);
			rover.SetPlateau(new Plateau(0, 0));
			rover.Move();
		}

		[TestMethod]
		[ExpectedException(typeof(RoverOutsidePlateauException))]
		public void Invalid_Move_FailTest2()
		{
			var rover = new Rover(0, 0, Direction.South);
			rover.SetPlateau(new Plateau(0, 0));
			rover.Move();
		}

		[TestMethod]
		[ExpectedException(typeof(RoverOutsidePlateauException))]
		public void Invalid_Move_FailTest3()
		{
			var rover = new Rover(0, 0, Direction.West);
			rover.SetPlateau(new Plateau(0, 0));
			rover.Move();
		}

		[TestMethod]
		[ExpectedException(typeof(RoverOutsidePlateauException))]
		public void Invalid_Move_FailTest4()
		{
			var rover = new Rover(0, 0, Direction.East);
			rover.SetPlateau(new Plateau(0, 0));
			rover.Move();
		}
	}
}
