using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_Methods
{
	public class Interpolation
	{
		/// <summary>
		/// Determines the interpolation using Newton's Interpolation
		/// </summary>
		/// <param name="x">Arrya of x</param>
		/// <param name="y">Array of y</param>
		/// <param name="xi">Point at which to interpolate</param>
		/// <returns>Array of the interpolation of different order</returns>
		public static double[] NewtonInterpolation(double[] x, double[] y, double xi) {
			if (x.Length != y.Length) {
				throw new ArgumentException("Length of x and y are not same");
			}
			int n = x.Length;
			var yint = new double[n];

			var fdd = new double[n][];
			for (int i = 0; i < n; i++) {
				fdd[i] = new double[n];
				fdd[i][0] = y[i];
			}
			for (int j = 1; j < n; j++) {
				for (int i = 0; i < n - j; i++) {
					fdd[i][j] = (fdd[i + 1][j - 1] - fdd[i][j - 1]) / (x[i + j] - x[i]);

				}
			}
			double xterm = 1;
			yint[0] = fdd[0][0];
			var ea = new double[n];

			for (int order = 1; order < n; order++) {
				xterm = xterm * (xi - x[order - 1]);
				var yint2 = yint[order - 1] + fdd[0][order] * xterm;
				ea[order - 1] = yint2 - yint[order - 1];
				yint[order] = yint2;
			}

			return yint;
		}

		/// <summary>
		/// Determines the interpolation using Newton's Interpolation
		/// </summary>
		/// <param name="x">Arrya of x</param>
		/// <param name="y">Array of y</param>
		/// <param name="xi">Point at which to interpolate</param>
		/// <param name="order">Order of interpolation</param>
		/// <returns>Interpolation</returns>
		public static double NewtonInterpolation(double[] x, double[] y, double xi, int order) {
			return NewtonInterpolation(x, y, xi)[order - 1];
		}

		/// <summary>
		/// Determines the interpolation using Lagrange's Interpolation
		/// </summary>
		/// <param name="x">Arrya of x</param>
		/// <param name="y">Array of y</param>
		/// <param name="xi">Point at which to interpolate</param>
		/// <returns>Interpolation</returns>
		public static double LagrangeInterpolation(double[] x, double[] y, double xi) {
			if (x.Length != y.Length) {
				throw new ArgumentException("Length of x and y are not same");
			}
			int n = x.Length;

			double sum = 0;
			for (int i = 0; i < n; i++) {
				var product = y[i];
				for (int j = 0; j < n; j++) {
					if (i != j) {
						product *= ((xi - x[j]) / (x[i] - x[j]));
					}
				}
				sum += product;
			}
			return sum;
		}

		/// <summary>
		/// Determines the interpolation using Cubic Splines Interpolation
		/// </summary>
		/// <param name="x">Arrya of x</param>
		/// <param name="y">Array of y</param>
		/// <param name="xi">Point at which to interpolate</param>
		/// <returns>Interpolation</returns>
		public static double CubicSplinesInterpolation(double[] x, double[] y, double xi) {
			if (x.Length != y.Length) {
				throw new ArgumentException("Length of x and y must be equal");
			}

			int n = x.Length;

			double A = 2 * (x[2] - x[0]);
			double B = (x[2] - x[1]);
			double C = 6 / (x[2] - x[1]) * (y[2] - y[1]) + 6 / (x[1] - x[0]) * (y[0] - y[1]);

			double D = x[2] - x[1];
			double E = 2 * (x[3] - x[1]);
			double F = 6 / (x[3] - x[2]) * (y[3] - y[2]) + 6 / (x[2] - x[1]) * (y[1] - y[2]);

			var fdd = new double[4];
			fdd[0] = 0;
			fdd[1] = (B * F - C * E) / (B * D - A * E);
			fdd[2] = (C - A * fdd[1]) / B;
			fdd[3] = 0;
			int i;

			if (xi > x[2] && xi < x[3]) {
				i = 3;
			} else if (xi > x[1] && xi < x[2]) {
				i = 2;
			} else if (xi > x[0] && xi < x[1]) {
				i = 1;
			} else {
				throw new ArgumentException("xi is not inside any interval");
			}

			var var1 = fdd[i - 1] / (6 * (x[i] - x[i - 1]));
			var var2 = fdd[i] / (6 * (x[i] - x[i - 1]));
			var var3 = ((y[i - 1] / (x[i] - x[i - 1]) - fdd[i - 1] * (x[i] - x[i - 1]) / 6));
			var var4 = ((y[i] / (x[i] - x[i - 1]) - fdd[i] * (x[i] - x[i - 1]) / 6));
			var res = var1 * Math.Pow(x[i] - xi, 3)
				+ var2 * Math.Pow(xi - x[i - 1], 3)
				+ (var3 * (x[i] - xi))
				+ (var4 * (xi - x[i - 1]));
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("\ny = {0}({1} - x)^3 ", var1, x[i]);
			Console.Write("{0} {1}(x {2} {3})^3 ", var2 < 0 ? '-' : '+', 
				Math.Abs(var2), x[i - 1] > 0 ? '-' : '+', Math.Abs(x[i - 1]));
			Console.Write("{0} {1}({2} - x)", var3 < 0 ? '-' : '+', Math.Abs(var3), x[i]);
			Console.WriteLine("{0} {1}(x {2} {3})", var4 < 0 ? '-' : '+', 
				Math.Abs(var4), x[i - 1] > 0 ? '-' : '+', Math.Abs(x[i - 1]));
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			return res;
		}
	}
}
