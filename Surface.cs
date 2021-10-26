using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobot
{
	public class Surface
	{
		/// <summary>
		/// Surface Width.
		/// </summary>
		public readonly int Width;
		
		/// <summary>
		/// Surface Length.
		/// </summary>
		public readonly int Length;

		/// <summary>
		/// Check if the new location is within the surface.
		/// </summary>
		/// <param name="point">(X,Y) coordination of the new location.</param>
		/// <returns></returns>
		public bool IsValidLocation(Point point) => point.X >= 0 && point.X <= Width && point.Y >= 0 && point.Y <= Length;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="width">Width of the surface.</param>
		/// <param name="length">Length of the surface.</param>
		public Surface(int width, int length)
		{
			Width = width;
			Length = length;
		}
	}
}
