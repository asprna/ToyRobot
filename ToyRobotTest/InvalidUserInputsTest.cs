using System;
using System.Collections.Generic;
using System.IO;
using ToyRobot;
using Xunit;

namespace ToyRobotTest
{
	public class InvalidUserInputsTest
	{
		public static IEnumerable<object[]> Data =>
			new List<object[]>
			{
				new object[] { new List<string> { "PLAC 0,0,NORTH" }, "Invalid command" },
				new object[] { new List<string> { "PLACE ,NORTH"  }, "Invalid command" },
				new object[] { new List<string> { "PLACE 1,2,EAST", "GO" }, "Invalid command" },
				new object[] { new List<string> { "PLACE 1,2,EAST", "LEFTT" }, "Invalid command" },
				new object[] { new List<string> { "PLACE 1,2,EAST", "RIGHTRIGHT" }, "Invalid command" },
				new object[] { new List<string> { "PLACE 1,2,EAST", "Print" }, "Invalid command" },
			};

		/// <summary>
		/// Validate if we are rejecting any invalid commands.
		/// </summary>
		/// <param name="scenario">Scenarios with invalid command.</param>
		/// <param name="expected">Expected error.</param>
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
