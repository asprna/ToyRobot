using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobot
{
	/// <summary>
	/// The direction of the robot facing.
	/// </summary>
	public enum Direction
	{
		North = 0,
		South = 1,
		East = 2,
		West = 3
	}

	public class Robot
	{
		/// <summary>
		/// Current location of the Robot.
		/// </summary>
		private Point _currentLocation;

		/// <summary>
		/// Current direction of the Robot.
		/// </summary>
		private Direction _currentDirection;

		/// <summary>
		/// Represent characteristic of robot's move.
		/// </summary>
		private Dictionary<Direction, System.Action> _move;

		/// <summary>
		/// Represent characteristic of robot's turn left.
		/// </summary>
		private Dictionary<Direction, Direction> _turnLeft = new()
		{
			{ Direction.North, Direction.West },
			{ Direction.West, Direction.South },
			{ Direction.South, Direction.East },
			{ Direction.East, Direction.North }
		};

		/// <summary>
		/// Represent characteristic of robot's turn right.
		/// </summary>
		private Dictionary<Direction, Direction> _turnRight = new()
		{
			{ Direction.North, Direction.East },
			{ Direction.East, Direction.South },
			{ Direction.South, Direction.West },
			{ Direction.West, Direction.North }
		};

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="point">Initial position of the Robot</param>
		/// <param name="direction">Initial direction of the Robot</param>
		public Robot(Point point, Direction direction)
		{
			_currentLocation = point;
			_currentDirection = direction;

			_move = new Dictionary<Direction, Action>()
			{
				{ Direction.North,  MoveNorth },
				{ Direction.South,  MoveSouth },
				{ Direction.East,  MoveEast },
				{ Direction.West,  MoveWest }
			};
		}

		/// <summary>
		/// Move the robot one position in the current direction.
		/// </summary>
		public void Move()
		{
			_move[_currentDirection].Invoke();
		}

		/// <summary>
		/// Turn the robot left.
		/// </summary>
		public void TurnLeft()
		{
			_currentDirection = _turnLeft[_currentDirection];
		}

		/// <summary>
		/// Turn the robot right.
		/// </summary>
		public void TurnRight()
		{
			_currentDirection = _turnRight[_currentDirection];
		}

		public string Report() => $"{_currentLocation.X},{_currentLocation.Y},{_currentDirection.ToString().ToUpper()}";

		/// <summary>
		/// Move robot one position to North.
		/// </summary>
		private void MoveNorth() => _currentLocation = Point.Add(_currentLocation, new Size(1, 0));
		/// <summary>
		/// Move robot one position to South.
		/// </summary>
		private void MoveSouth() => _currentLocation = Point.Subtract(_currentLocation, new Size(1, 0));
		/// <summary>
		/// Move robot one position to East.
		/// </summary>
		private void MoveEast() => _currentLocation = Point.Add(_currentLocation, new Size(0, 1));
		/// <summary>
		/// Move robot one position to West.
		/// </summary>
		private void MoveWest() => _currentLocation = Point.Subtract(_currentLocation, new Size(0, 1));

	}
}
