using System;

namespace Numerical_Methods
{
	public static class Integration
	{
		delegate double F1(double x);

		public enum GaussQuadraturePoint { Two, Three };
		public enum SimpsonMethod { OneThird, ThreeEighth };

		/// <summary>
		/// Determines integration of a single variable function using Trapezoidal rule
		/// </summary>
		/// <param name="f">Function to be integrated</param>
		/// <param name="low">Lower limit</param>
		/// <param name="high">Upper limit</param>
		/// <param name="part">Number of parts</param>
		/// <returns>Integral value</returns>
		public static double Trapezoidal(IFunction f, double low, double high, int part = 1) {
			double step = (high - low) / part;
			double sum = f.EvalFor(low) + f.EvalFor(high);
			for (double i = low + step; i < high; i += step) {
				sum += (2 * f.EvalFor(i));
			}

			var trap = step * sum / 2;
			return trap;
		}

		/// <summary>
		/// Determines integration of a single variable function using Simpson method
		/// </summary>
		/// <param name="f">Function to be integrated</param>
		/// <param name="low">Lower limit</param>
		/// <param name="high">Upper limit</param>
		/// <param name="method">One third of Three eighth</param>
		/// <returns>Integral value</returns>
		public static double Simpson(IFunction f, double low, double high, SimpsonMethod method) {
			switch (method) {
			case SimpsonMethod.OneThird:
				return (high - low) * (f.EvalFor(low) + 4 * f.EvalFor((high - low) / 2) + f.EvalFor(high)) / 6;
			case SimpsonMethod.ThreeEighth:
				var step = (high - low) / 3;
				return (high - low) * (f.EvalFor(low) + 3 * (f.EvalFor(low + step) + f.EvalFor(high - step)) + f.EvalFor(high)) / 8;
			default:
				throw new ArgumentException("Invalid Method");
			}
		}

		/// <summary>
		/// Determines integration of a single variable function using Romberg method
		/// </summary>
		/// <param name="f">Function to be integrated</param>
		/// <param name="low">Lower limit</param>
		/// <param name="high">Upper limit</param>
		/// <param name="level">Level of order</param>
		/// <returns>Integral value</returns>
		public static double Romberg(IFunction f, double low, double high, int level) {
			double[][] I = new double[level][];
			for (int i = 0; i < level; i++) {
				I[i] = new double[level];
			}

			var n = 1;
			I[0][0] = Trapezoidal(f, low, high, n);
			for (int itr = 1; itr < level; itr++) {
				n = (int)Math.Pow(2, itr);
				I[itr][0] = Trapezoidal(f, low, high, n);
				for (int k = 1; k < itr + 1; k++) {
					var j = itr - k;
					I[j][k] = (Math.Pow(4, k) * I[j + 1][k - 1] - I[j][k - 1]) / (Math.Pow(4, k) - 1);

				}
			}
			return I[0][level - 1];
		}

		/// <summary>
		/// Determines integration of a single variable function using three of four point Gauss Quadrature method
		/// </summary>
		/// <param name="f">Function to be integrated</param>
		/// <param name="low">Lower limit</param>
		/// <param name="high">Upper limit</param>
		/// <param name="point">Three of Four</param>
		/// <returns>Integral value</returns>
		public static double GaussQuadrature(IFunction f, double low, double high, GaussQuadraturePoint point) {
			var sub = (high - low) / 2;
			var add = (high + low) / 2;

			F1 f1 = (double x) => { return f.EvalFor(add + x * sub); };

			switch (point) {
			case GaussQuadraturePoint.Two:
				return sub * (f1(-0.577350269) + f1(0.577350269));
			case GaussQuadraturePoint.Three:
				return sub * (0.5555556 * f1(-0.774596669) + 0.8888889 * f1(0) + 0.5555556 * f1(0.774596669));
			default:
				throw new ArgumentException("Invalid Argument");
			}
		}

		
	}
}
