using System;
using System.Collections.Generic;
using System.IO;
using ToyRobot;
using Xunit;

namespace ToyRobotTest
{
	public class OrderofActionTest
	{
		public static IEnumerable<object[]> Data =>
			new List<object[]>
			{
				new object[] { new List<string> { "MOVE" }, "Please place the robot first" },
				new object[] { new List<string> { "PLACE 0,0" }, "Please set the initial position first" },
				new object[] { new List<string> { "LEFT" }, "Please place the robot first" },
				new object[] { new List<string> { "RIGHT"}, "Please place the robot first" },
				new object[] { new List<string> { "REPORT" }, "Please place the robot first" },
				new object[] { new List<string> { "PLACE 0,0,North", "MOVE", "PLACE 0,0,North" }, "Incorrect command, try using command PLACE X,Y" },
			};

		/// <summary>
		/// Validate if the controller reject invalid order of the actions
		/// </summary>
		/// <param name="scenario"></param>
		/// <param name="expected"></param>
		[Theory]
		[MemberData(nameof(Data))]
		public void InvalidUserInput_RobotMove_ErrorReturn(List<string> scenario, string expected)
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
