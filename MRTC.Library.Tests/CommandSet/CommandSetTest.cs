using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MRTC.Library
{
	[TestClass]
	public class CommandSetTest
	{
		[TestMethod]
		public async Task Add_Character_SuccessTestAsync()
		{
			var commandSet = new CommandSet();
			
			await commandSet.AddAsync('L');
			await commandSet.AddAsync('R');
			await commandSet.AddAsync('M');

			Assert.AreEqual(commandSet.Count, 3);
			Assert.AreEqual(Command.RotateLeft, commandSet[0]);
			Assert.AreEqual(Command.RotateRight, commandSet[1]);
			Assert.AreEqual(Command.Move, commandSet[2]);
		}

		[TestMethod]
		public async Task Add_CharacterArray_SuccessTestAsync()
		{
			var commandSet = new CommandSet();

			await commandSet.AddAsync('L', 'R', 'M');

			Assert.AreEqual(commandSet.Count, 3);
			Assert.AreEqual(Command.RotateLeft, commandSet[0]);
			Assert.AreEqual(Command.RotateRight, commandSet[1]);
			Assert.AreEqual(Command.Move, commandSet[2]);
		}

		[TestMethod]
		public async Task Add_String_SuccessTestAsync()
		{
			var commandSet = new CommandSet();
			await commandSet.AddAsync("MML");

			Assert.AreEqual(commandSet.Count, 3);
			Assert.AreEqual(Command.Move, commandSet[0]);
			Assert.AreEqual(Command.Move, commandSet[1]);
			Assert.AreEqual(Command.RotateLeft, commandSet[2]);
		}

		[TestMethod]
		public async Task Execute_OriginRover_FacingNorth_L_SuccessTestAsync()
		{
			var originRoverFacingNorth = new Rover(0, 0, Direction.North);

			var commandSet = new CommandSet();
			await commandSet.AddAsync('L');

			commandSet.SetRover(originRoverFacingNorth);
			commandSet.Execute();

			Assert.AreEqual(Direction.West, originRoverFacingNorth.Heading);
		}

		[TestMethod]
		public async Task Execute_OriginRover_FacingNorth_R_SuccessTestAsync()
		{
			var originRoverFacingNorth = new Rover(0, 0, Direction.North);

			var commandSet = new CommandSet();
			await commandSet.AddAsync('R');

			commandSet.SetRover(originRoverFacingNorth);
			commandSet.Execute();

			Assert.AreEqual(Direction.East, originRoverFacingNorth.Heading);
		}

		[TestMethod]
		public async Task Execute_OriginRover_FacingNorth_LL_SuccessTestAsync()
		{
			var originRoverFacingNorth = new Rover(0, 0, Direction.North);

			var commandSet = new CommandSet();
			await commandSet.AddAsync('L', 'L');

			commandSet.SetRover(originRoverFacingNorth);
			commandSet.Execute();

			Assert.AreEqual(Direction.South, originRoverFacingNorth.Heading);
		}

		[TestMethod]
		public async Task Execute_OriginRover_FacingNorth_RR_SuccessTestAsync()
		{
			var originRoverFacingNorth = new Rover(0, 0, Direction.North);

			var commandSet = new CommandSet();
			await commandSet.AddAsync('R', 'R');

			commandSet.SetRover(originRoverFacingNorth);
			commandSet.Execute();

			Assert.AreEqual(Direction.South, originRoverFacingNorth.Heading);
		}

		[TestMethod]
		public async Task Execute_OriginRover_FacingNorth_MMRM_SuccessTestAsync()
		{
			var originRoverFacingNorth = new Rover(0, 0, Direction.North);

			var commandSet = new CommandSet();
			await commandSet.AddAsync("MMRM");

			commandSet.SetRover(originRoverFacingNorth);
			commandSet.Execute();

			Assert.AreEqual(2, originRoverFacingNorth.YCoordinate);
			Assert.AreEqual(Direction.East, originRoverFacingNorth.Heading);
			Assert.AreEqual(1, originRoverFacingNorth.XCoordinate);
		}

		[TestMethod]
		public async Task Execute_OriginRover_FacingNorth_MLMM_SuccessTestAsync()
		{
			var originRoverFacingNorth = new Rover(0, 0, Direction.North);

			var commandSet = new CommandSet();
			await commandSet.AddAsync("MLMM");

			commandSet.SetRover(originRoverFacingNorth);
			commandSet.Execute();

			Assert.AreEqual(1, originRoverFacingNorth.YCoordinate);
			Assert.AreEqual(Direction.West, originRoverFacingNorth.Heading);
			Assert.AreEqual(-2, originRoverFacingNorth.XCoordinate);
		}

		[TestMethod]
		public async Task Execute_OriginRover_FacingSouth_MMRM_SuccessTestAsync()
		{
			var originRoverFacingSouth = new Rover(0, 0, Direction.South);

			var commandSet = new CommandSet();
			await commandSet.AddAsync("MMRM");

			commandSet.SetRover(originRoverFacingSouth);
			commandSet.Execute();

			Assert.AreEqual(-2, originRoverFacingSouth.YCoordinate);
			Assert.AreEqual(Direction.West, originRoverFacingSouth.Heading);
			Assert.AreEqual(-1, originRoverFacingSouth.XCoordinate);
		}

		[TestMethod]
		public async Task Execute_OriginRover_FacingSouth_MLMM_SuccessTestAsync()
		{
			var originRoverFacingSouth = new Rover(0, 0, Direction.South);

			var commandSet = new CommandSet();
			await commandSet.AddAsync("MLMM");

			commandSet.SetRover(originRoverFacingSouth);
			commandSet.Execute();

			Assert.AreEqual(-1, originRoverFacingSouth.YCoordinate);
			Assert.AreEqual(Direction.East, originRoverFacingSouth.Heading);
			Assert.AreEqual(2, originRoverFacingSouth.XCoordinate);
		}
	}
}