using System;
using System.Text.RegularExpressions;
using static ToyRobot.Scenario;

namespace ToyRobot
{
	/// <summary>
	/// This service trigger appropriate user actions
	/// </summary>
	public class UserActionHandler
	{
		/// <summary>
		/// Regex to validate initial placement.
		/// </summary>
		private const string initialPlacement = @"(PLACE)\s(\d+),(\d+),(NORTH|SOUTH|EAST|WEST)$";
		/// <summary>
		/// Regex to validate user moments.
		/// </summary>
		private const string action = @"\b(MOVE|LEFT|RIGHT|REPORT)\b$";
		/// <summary>
		/// Regex to validate position changes.
		/// </summary>
		private const string changePosition = @"(PLACE)\s(\d+),(\d+)$";

		//Regex builders
		private Regex initialPlacementRegex = new Regex(initialPlacement, RegexOptions.Compiled | RegexOptions.IgnoreCase);
		private Regex actionRegex = new Regex(action, RegexOptions.Compiled | RegexOptions.IgnoreCase);
		private Regex changePositionRegex = new Regex(changePosition, RegexOptions.Compiled | RegexOptions.IgnoreCase);

		/// <summary>
		/// Facilitate each scenarios.
		/// </summary>
		private Scenario _scenario;

		public UserActionHandler()
		{
			_scenario = new Scenario();
		}

		/// <summary>
		/// Validate user actions.
		/// </summary>
		/// <param name="userInput"></param>
		public void ParseUserInput(string userInput)
		{
			try
			{
				//Validation for the initial placement.
				var initialPlacementMatch = initialPlacementRegex.Match(userInput);
				if (initialPlacementMatch.Success)
				{
					_scenario.SetInitialPosition(initialPlacementMatch);
					return;
				}

				//Validation for the moments.
				var actionRegexMatch = actionRegex.Match(userInput);
				if (actionRegexMatch.Success)
				{
					Actions action;
					if(Enum.TryParse(actionRegexMatch.Value, true, out action))
					{
						_scenario.TrigerAction(action);
						return;
					}
					Extension.ErrorOutput("The action cannot be supported!!!");
					return;
				}

				//Validation for the position change.
				var changePositionRegexMatch = changePositionRegex.Match(userInput);
				if (changePositionRegexMatch.Success)
				{
					_scenario.ChangePosition(changePositionRegexMatch);
					return;
				}

				Extension.ErrorOutput("Invalid command");
			}
			catch
			{
				Extension.ErrorOutput("Unexpected Error");
			}
		}
	}
}
