using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobot
{
	/// <summary>
	/// Extension methods.
	/// </summary>
	public static class Extension
	{
		/// <summary>
		/// Report errors to console.
		/// </summary>
		/// <param name="error"></param>
		public static void ErrorOutput(string error)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Error.WriteLine(error);
			Console.ForegroundColor = ConsoleColor.Yellow;
		}
	}
}
