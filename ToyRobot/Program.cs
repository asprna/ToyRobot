using System;

namespace ToyRobot
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Place the robot first on the 6x6 square table top and then commands in the below format.");
			Console.WriteLine("PLACE X, Y,DIRECTION");
			Console.WriteLine("MOVE");
			Console.WriteLine("LEFT");
			Console.WriteLine("RIGHT");
			Console.WriteLine("REPORT");
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("PLACE will put the toy robot on the table in position X,Y and facing NORTH, SOUTH, EAST or WEST.");
			Console.WriteLine("MOVE will move the toy robot one unit forward in the direction it is currently facing.");
			Console.WriteLine("LEFT and RIGHT will rotate the robot 90 degrees in the specified direction without changing the position of the robot.");
			Console.WriteLine("REPORT will announce the X,Y and orientation of the robot.");
			Console.WriteLine();
			Console.WriteLine("Press EXIT to exit the program");

			Console.ForegroundColor = ConsoleColor.Yellow;
			string command = "";

			UserActionHandler userActionHandler = new UserActionHandler();

			//Exit the loop when user enter EXIT
			while (!command.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
			{
				try
				{
					//Read the user input
					command = Console.ReadLine();

					if(!command.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
					{
						userActionHandler.ParseUserInput(command);
					}
				}
				catch
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("---------- An error occurred!!! ----------");
				}
			}

			Console.ForegroundColor = ConsoleColor.White;

		}
	}
}
