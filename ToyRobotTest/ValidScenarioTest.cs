using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRobot;
using Xunit;

namespace ToyRobotTest
{
	public class ValidScenarioTest
	{
		public static IEnumerable<object[]> Data =>
			new List<object[]>
			{
				new object[] { new List<string> { "PLACE 0,0,NORTH", "MOVE", "REPORT" }, "Output: 0,1,NORTH" },
				new object[] { new List<string> { "PLACE 0,0,NORTH", "LEFT", "REPORT" }, "Output: 0,0,WEST" },
				new object[] { new List<string> { "PLACE 1,2,EAST", "MOVE", "MOVE", "LEFT", "MOVE", "REPORT" }, "Output: 3,3,NORTH" },
				new object[] { new List<string> { "PLACE 1,2,EAST", "MOVE", "LEFT", "MOVE", "PLACE 3,1", "MOVE", "REPORT" }, "Output: 3,2,NORTH" }
			};

		public static IEnumerable<object[]> DataCaseInsensitive =>
			new List<object[]>
			{
				new object[] { new List<string> { "Place 0,0,NORTH", "MOVE", "Report" }, "Output: 0,1,NORTH" },
				new object[] { new List<string> { "PLACE 0,0,North", "LEFT", "REPORT" }, "Output: 0,0,WEST" },
				new object[] { new List<string> { "PLACE 1,2,EAST", "Move", "MOVE", "LEFT", "MOVE", "REPORT" }, "Output: 3,3,NORTH" },
				new object[] { new List<string> { "PLACE 1,2,EAST", "MOVE", "Left", "MOVE", "Place 3,1", "MOVE", "REPORT" }, "Output: 3,2,NORTH" }
			};

		public static IEnumerable<object[]> RobotOntheEdgeOfTheTable =>
			new List<object[]>
			{
				new object[] { new List<string> { "PLACE 0,4,NORTH", "MOVE", "MOVE" }, "I cannot move forward anymore, please turn me Left or Right!!!" },
				new object[] { new List<string> { "PLACE 4,5,EAST", "MOVE", "MOVE" }, "I cannot move forward anymore, please turn me Left or Right!!!" },
				new object[] { new List<string> { "PLACE 5,1,SOUTH", "MOVE", "MOVE" }, "I cannot move forward anymore, please turn me Left or Right!!!" },
				new object[] { new List<string> { "PLACE 1,0,WEST", "MOVE", "MOVE" }, "I cannot move forward anymore, please turn me Left or Right!!!" }
			};

		[Theory]
		[MemberData(nameof(Data))]
		public void ValidScenario_RobotMove_Success(List<string> scenario, string expected)
		{
			//Arrange
			var stringWriter = new StringWriter();
			Console.SetOut(stringWriter);

			UserActionHandler userActionHandler = new UserActionHandler();

			//Action
			scenario.ForEach(a => userActionHandler.ParseUserInput(a));
			
			//Assert
			var output = stringWriter.ToString().Replace("\r\n", string.Empty);
			Assert.Equal(expected, output);
		}

		[Theory]
		[MemberData(nameof(DataCaseInsensitive))]
		public void ValidScenario_CaseInsensitiveAction_Success(List<string> scenario, string expected)
		{
			//Arrange
			var stringWriter = new StringWriter();
			Console.SetOut(stringWriter);

			UserActionHandler userActionHandler = new UserActionHandler();

			//Action
			scenario.ForEach(a => userActionHandler.ParseUserInput(a));

			//Assert
			var output = stringWriter.ToString().Replace("\r\n", string.Empty);
			Assert.Equal(expected, output);
		}

		[Fact]
		public void ValidScenario_PlaceingRobotOutsideTable_ErrorReturn()
		{
			//Arrange
			var stringWriter = new StringWriter();
			Console.SetError(stringWriter);

			UserActionHandler userActionHandler = new UserActionHandler();

			//Action
			userActionHandler.ParseUserInput("PLACE 7,7,NORTH");

			//Assert
			var output = stringWriter.ToString().Replace("\r\n", string.Empty);
			Assert.Equal("The given position is not valid on a 6x6 surface!!!", output);
		}

		[Theory]
		[MemberData(nameof(RobotOntheEdgeOfTheTable))]
		public void ValidScenario_RobotOnTheEdgeOfTheTable_RobotStopMove(List<string> scenario, string expected)
		{
			//Arrange
			var stringWriter = new StringWriter();
			Console.SetError(stringWriter);

			UserActionHandler userActionHandler = new UserActionHandler();

			//Action
			scenario.ForEach(a => userActionHandler.ParseUserInput(a));

			//Assert
			var output = stringWriter.ToString().Replace("\r\n", string.Empty);
			Assert.Equal(expected, output);
		}
	}
}
