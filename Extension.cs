using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobot
{
	public static class Extension
	{
		public static void ErrorOutput(string error)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Error.WriteLine(error);
			Console.ForegroundColor = ConsoleColor.Yellow;
		}
	}
}
