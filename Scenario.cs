using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobot
{
	public class Scenario
	{
		public class Placement
		{
			public Point InitialPoint { get; set; }
			public Direction Direction { get; set; }
		}

		public enum Actions
		{
			MOVE = 0,
			LEFT = 1,
			RIGHT = 2,
			REPORT = 3
		}

		public List<Actions> RobotActions { get; set; }
	}
}
