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

		public void Place(Placement placement)
		{
			if (_surface.IsValidLocation(placement.InitialPoint))
			{
				_robot = new Robot(placement.InitialPoint, placement.Direction);

				_action = new()
				{
					{ Actions.MOVE, _robot.Move },
					{ Actions.RIGHT, _robot.TurnRight },
					{ Actions.LEFT, _robot.TurnLeft },
					{ Actions.REPORT, _robot.Report }
				};
			}
		}

		public void Action(Actions action)
		{
			_action[action].DynamicInvoke();
		}
	}
}
