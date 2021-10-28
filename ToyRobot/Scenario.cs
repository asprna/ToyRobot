using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ToyRobot
{
	public class Scenario
	{
		private Controller _controller;

		public Scenario()
		{
			//Create a controller with a surface of 6*6.
			//TODO: This can be added as a configuration option.
			_controller = new Controller(new Surface(6, 6));
		}

		/// <summary>
		/// The position of the robots.
		/// The direction is not allowed in subsequent placements
		/// </summary>
		public class Placement
		{
			public Point Point { get; set; }
		}

		/// <summary>
		/// The initial placement with the direction.
		/// Initial placement must contain point and direction.
		/// </summary>
		public class InitialPlacement : Placement
		{
			public Direction Direction { get; set; }
		}

		/// <summary>
		/// All possible action that user can use
		/// </summary>
		public enum Actions
		{
			MOVE = 0,
			LEFT = 1,
			RIGHT = 2,
			REPORT = 3
		}

		/// <summary>
		/// Record the robot's initial position.
		/// </summary>
		public InitialPlacement InitialPosition { get; private set; }

		/// <summary>
		/// Assign the initial position to robot.
		/// </summary>
		/// <param name="match"></param>
		/// <returns></returns>
		public void SetInitialPosition(Match match)
		{
			if (!IsInitialPositionSuccess) //Check if the robot has an initial position 
			{
				int x, y;
				Direction direction;

				if (int.TryParse(match.Groups[2].Value, out x)
					&& int.TryParse(match.Groups[3].Value, out y)
					&& Enum.TryParse(match.Groups[4].Value, true, out direction))
				{
					InitialPosition = new Scenario.InitialPlacement
					{
						Point = new Point(x, y),
						Direction = direction
					};
					
					if(!_controller.Place(InitialPosition))
					{
						InitialPosition = null;
					}
				}
			} 
			else
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Error.WriteLine("Incorrect command, try using command PLACE X,Y");
				Console.ForegroundColor = ConsoleColor.Yellow;
			}
		}

		/// <summary>
		/// Execute action submitted by the user.
		/// </summary>
		/// <param name="action"></param>
		public void TrigerAction(Actions action)
		{
			if (IsInitialPositionSuccess) //Check if the robot has an initial position 
			{
				_controller.Action(action);
				return;
			}
			Extension.ErrorOutput("Please place the robot first");
		}

		/// <summary>
		/// Change the position of the robot.
		/// </summary>
		/// <param name="match"></param>
		public void ChangePosition(Match match)
		{
			if (IsInitialPositionSuccess)
			{
				int x, y;

				if (int.TryParse(match.Groups[2].Value, out x)
					&& int.TryParse(match.Groups[3].Value, out y))
				{
					_controller.ChangePosition(new Placement { Point = new Point(x, y) });
					return;
				}
				Extension.ErrorOutput("Invalid X,Y coordinations");
			}
			Extension.ErrorOutput("Please set the initial position first");
		}

		/// <summary>
		/// If the robots has a initial position.
		/// </summary>
		public bool IsInitialPositionSuccess => InitialPosition != null;
	}
}
