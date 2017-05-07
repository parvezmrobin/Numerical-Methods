using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_Methods
{
	public class PartialIntegration
	{
		/// <summary>
		/// Determines integration of a two variable function using Euler method
		/// </summary>
		/// <param name="f">Derivative of function to be integrated</param>
		/// <param name="low">Lower limit</param>
		/// <param name="high">Higher limit</param>
		/// <param name="h">Step size</param>
		/// <param name="y">Initial value of y</param>
		/// <returns>Integral value</returns>
		public static double Euler(IFunctionXY f, double low, double high, double h, double y) {
			Console.WriteLine("X\t\tY\t\tError");

			for (double x = low; x <= high; x += h) {
				var dell = f.EvalFor(x, y);
				var temp = y;
				y += (dell * h);
				Console.WriteLine("{0:f6}\t{1:f6}\t{2:p5}", x, temp, Math.Abs((y - temp) / y));
			}

			return y;
		}

		/// <summary>
		/// Determines integration of a two variable function using Heun method
		/// </summary>
		/// <param name="f">Derivative of function to be integrated</param>
		/// <param name="low">Lower limit</param>
		/// <param name="high">Higher limit</param>
		/// <param name="h">Step size</param>
		/// <param name="y">Initial value of y</param>
		/// <returns>Integral value</returns>
		public static double Heun(IFunctionXY f, double low, double high, double h, double y) {
			Console.WriteLine("X\t\tY\t\tError");
			double y2 = y;
			for (double x = low; x <= high; x += h) {
				var y0 = y + f.EvalFor(x, y) * h;
				var y1 = f.EvalFor(x + h, y0);
				var slope = (f.EvalFor(x, y) + y1) / 2;
				y2 = y + slope * h;
				var e = (y2 - y) / y2;
				Console.WriteLine("{0:f6}\t{1:f6}\t{2:p5}", x, y, e);
				y = y2;
			}

			return y2;
		}

		/// <summary>
		/// Determines integration of a two variable function using Mid Point method
		/// </summary>
		/// <param name="f">Derivative of function to be integrated</param>
		/// <param name="low">Lower limit</param>
		/// <param name="high">Higher limit</param>
		/// <param name="h">Step size</param>
		/// <param name="y">Initial value of y</param>
		/// <returns>Integral value</returns>
		public static double MidPoint(IFunctionXY f, double low, double high, double h, double y) {
			double ynext = y;
			Console.WriteLine("X\t\tY\t\tError");
			for (double x = low; x <= high; x += h) {
				var y0 = y + f.EvalFor(x, y) * (h / 2);
				var y1 = f.EvalFor(x + (h / 2), y0);
				ynext = y + y1 * h;
				var e = (ynext - y) / ynext;
				Console.WriteLine("{0:f6}\t{1:f6}\t{2:p5}", x, y, e);
				y = ynext;
			}
			return ynext;
		}

		/// <summary>
		/// Determines integration of a two variable function using Ralston method
		/// </summary>
		/// <param name="f">Derivative of function to be integrated</param>
		/// <param name="low">Lower limit</param>
		/// <param name="high">Higher limit</param>
		/// <param name="h">Step size</param>
		/// <param name="y">Initial value of y</param>
		/// <returns>Integral value</returns>
		public static double Ralston(IFunctionXY f, double low, double high, double h, double y) {
			Console.WriteLine("X\t\tY\t\tError");
			for (double x = low; x <= high; x += h) {
				var dell1 = f.EvalFor(x, y);
				var dell2 = f.EvalFor(x + (.75 * h), y + (.75 * dell1 * h));
				var y2 = y + ((1 / 3.0) * dell1 + (2 / 3.0) * dell2) * h;
				var e = (y2 - y) / y2;
				Console.WriteLine("{0:f6}\t{1:f6}\t{2:p5}", x, y, e);
				y = y2;
			}

			return y;
		}

		/// <summary>
		/// Determines integration of a two variable function using Third Order Runge Kutta method
		/// </summary>
		/// <param name="f">Derivative of function to be integrated</param>
		/// <param name="low">Lower limit</param>
		/// <param name="high">Higher limit</param>
		/// <param name="h">Step size</param>
		/// <param name="y">Initial value of y</param>
		/// <returns>Integral value</returns>
		public static double RungeKutta3(IFunctionXY f, double low, double high, double h, double y) {
			Console.WriteLine("X\t\tY\t\tError");
			for (double x = low; x <= high; x += h) {
				var k1 = f.EvalFor(x, y);
				var k2 = f.EvalFor(x + .5 * h, y + .5 * k1 * h);
				var k3 = f.EvalFor(x + h, y - k1 * h + 2 * k2 * h);
				var ynext = y + (1.0 / 6) * (k1 + 4 * k2 + k3) * h;
				var e = (ynext - y) / ynext;
				Console.WriteLine("{0:f6}\t{1:f6}\t{2:p5}", x, y, e);
				y = ynext;
			}

			return y;
		}

		/// <summary>
		/// Determines integration of a two variable function using Fourth Order Runge Kutta method
		/// </summary>
		/// <param name="f">Derivative of function to be integrated</param>
		/// <param name="low">Lower limit</param>
		/// <param name="high">Higher limit</param>
		/// <param name="h">Step size</param>
		/// <param name="y">Initial value of y</param>
		/// <returns>Integral value</returns>
		public static double RungeKutta4(IFunctionXY f, double low, double high, double h, double y) {
			Console.WriteLine("X\t\tY\t\tError");
			for (double x = low; x <= high; x += h) {
				var k1 = f.EvalFor(x, y);
				var k2 = f.EvalFor(x + .5 * h, y + .5 * k1 * h);
				var k3 = f.EvalFor(x + .5 * h, y + .5 * k2 * h);
				var k4 = f.EvalFor(x + h, y + k3 * h);
				var ynext = y + (1.0 / 6) * (k1 + 2 * k2 + 2 * k3 + k4) * h;
				var e = (ynext - y) / ynext;
				Console.WriteLine("{0:f6}\t{1:f6}\t{2:p5}", x, y, e);
				y = ynext;
			}

			return y;
		}
	}
}
