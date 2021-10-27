using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ToyRobot
{
	public class UserActionHandler
	{
		private const string initialPlacement = @"(PLACE)\s(\d+),(\d+),(NORTH|SOUTH|EAST|WEST)$";
		private const string action = @"(MOVE|LEFT|RIGHT|REPORT)$";
		private const string secondaryPlacement = @"PLACE\s\d+,\d+$";

		private Regex initialPlacementRegex = new Regex(initialPlacement, RegexOptions.Compiled | RegexOptions.IgnoreCase);
		private Regex actionRegex = new Regex(action, RegexOptions.Compiled | RegexOptions.IgnoreCase);
		private Regex secondaryPlacementRegex = new Regex(secondaryPlacement, RegexOptions.Compiled | RegexOptions.IgnoreCase);

		private Scenario _scenario;

		public UserActionHandler()
		{
			_scenario = new Scenario();
		}

		public void ParseUserInput(string userInput)
		{
			try
			{
				var initialPlacementMatch = initialPlacementRegex.Match(userInput);
				if (initialPlacementMatch.Success)
				{
					_scenario.SetInitialPosition(initialPlacementMatch);
				}
			}
			catch
			{

			}
		}
	}
}
