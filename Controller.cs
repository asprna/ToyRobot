using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ToyRobot.Scenario;

namespace ToyRobot
{
	public class Controller
	{
		private Robot _robot;
		private Surface _surface;

		/// <summary>
		/// Represent controller's action.
		/// </summary>
		private Dictionary<Actions, System.Action> _action;

		public Controller(Surface surface)
		{
			_surface = surface;
		}

		public bool Place(InitialPlacement placement)
		{
			if (_surface.IsValidLocation(placement.Point))
			{
				_robot = new Robot(placement.Point, placement.Direction);

				_action = new()
				{
					{ Actions.MOVE, _robot.Move },
					{ Actions.RIGHT, _robot.TurnRight },
					{ Actions.LEFT, _robot.TurnLeft },
					{ Actions.REPORT, _robot.Report }
				};

				return true;
			}
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Error.WriteLine("The given position is not valid on a 6x6 surface!!!");
			Console.ForegroundColor = ConsoleColor.Yellow;
			return false;
		}

		public void Action(Actions action)
		{
			_action[action].DynamicInvoke();
		}
	}
}
