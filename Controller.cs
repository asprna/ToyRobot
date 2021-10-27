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
			Extension.ErrorOutput("The given position is not valid on a 6x6 surface!!!");
			return false;
		}

		public void ChangePosition(Placement placement)
		{
			if (_surface.IsValidLocation(placement.Point))
			{
				_robot.ChangePosition(placement.Point);
				return;
			}
			Extension.ErrorOutput("The given position is not valid on a 6x6 surface!!!");
		}

		public void Action(Actions action)
		{
			if (action.Equals(Actions.MOVE) && !_surface.IsValidLocation(_robot.NextPosition()))
			{
				Extension.ErrorOutput("I cannot move forward anymore, please turn me Left or Right!!!");
				return;
			}
			_action[action].DynamicInvoke();
		}
	}
}
