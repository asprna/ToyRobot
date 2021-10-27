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
			_controller = new Controller(new Surface(6, 6));
		}
		/// <summary>
		/// Representation the secondary placement.
		/// </summary>
		public class Placement
		{
			public Point Point { get; set; }
		}

		/// <summary>
		/// Representation of the initial placement.
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

		public InitialPlacement InitialPosition { get; private set; }
		public bool SetInitialPosition(Match match)
		{
			if (!IsInitialPositionSuccess)
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
						return false;
					}
					return true;
				}
				return false;
			}
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Error.WriteLine("Unable to move the robots, try using command PLACE X,Y");
			Console.ForegroundColor = ConsoleColor.Yellow;
			return false;
		}

		public void TrigerAction(Actions action)
		{
			_controller.Action(action);
		}

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

		public bool IsInitialPositionSuccess => InitialPosition != null;
	}
}
