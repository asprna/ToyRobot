using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
				new object[] { new List<string> { "REPORT" }, "Please place the robot first" }
			};

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
