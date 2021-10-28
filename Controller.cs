using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ToyRobot.Scenario;

namespace ToyRobot
{
	/// <summary>
	/// Controller of the robot.
	/// </summary>
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

		/// <summary>
		/// Initial placement of the robots.
		/// </summary>
		/// <param name="placement">The position of the robots.</param>
		/// <returns></returns>
		public bool Place(InitialPlacement placement)
		{
			if (_surface.IsValidLocation(placement.Point)) //Check if the given position is within the surface.
			{
				_robot = new Robot(placement.Point, placement.Direction); //Place the robots

				//assign action delegates
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

		/// <summary>
		/// Change the position of the robots.
		/// </summary>
		/// <param name="placement"></param>
		public void ChangePosition(Placement placement)
		{
			if (_surface.IsValidLocation(placement.Point)) //Check if the given position is within the surface.
			{
				_robot.ChangePosition(placement.Point);
				return;
			}
			Extension.ErrorOutput("The given position is not valid on a 6x6 surface!!!");
		}

		/// <summary>
		/// Trigger the user action.
		/// </summary>
		/// <param name="action"></param>
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
