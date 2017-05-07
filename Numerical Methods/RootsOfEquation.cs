using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_Methods
{
	public static class RootsOfEquation
	{
		public static double MaxError = .0001;
		public static int MaxIteration = 100;

		/// <summary>
		/// Finds the root of an equation using Bi-Section method
		/// </summary>
		/// <param name="expression">Function of which root is to find</param>
		/// <param name="xl">Lower limit</param>
		/// <param name="xu">Upper limit</param>
		/// <returns>The root</returns>
		public static double BiSection(IFunction expression, double xl = short.MinValue, double xu = short.MaxValue) {
			Console.WriteLine("Iteration\t\tXi\t\tXu\t\tXr\t\tEa");
			double xr = double.NaN;
			double prev_xr = double.NaN;
			var fl = expression.EvalFor(xl);
			double fr;
			for (int i = 0; i <= MaxIteration; i++) {
				xr = (xl + xu) / 2;
				double error = Math.Abs((xr - prev_xr) / xr);
				Console.WriteLine("{0}\t\t\t{1:e2}\t{2:e2}\t{3:e2}\t{4:p5}", i + 1, xl, xu, xr, error);
				if (error < MaxError)
					break;

				fr = expression.EvalFor(xr);

				var mul = fl * fr;
				if (mul < 0) {
					xu = xr;
				} else if (mul > 0) {
					xl = xr;
					fl = fr;
				} else
					break;

				prev_xr = xr;

			}
			return xr;
		}

		/// <summary>
		/// Finds the root of an equation using False Position method
		/// </summary>
		/// <param name="expression">Function of which root is to find</param>
		/// <param name="xl">Lower limit</param>
		/// <param name="xu">Upper limit</param>
		/// <returns>The root</returns>
		public static double FalsePosition(IFunction expression, double xl = short.MinValue, double xu = short.MaxValue) {
			Console.WriteLine("Iteration\t\tXi\t\tXu\t\tXr\t\tEa");

			var fl = expression.EvalFor(xl);
			var fu = expression.EvalFor(xu);
			var iu = 0;
			var il = 0;
			var xr = double.NaN;
			double error = 1;
			for (int i = 0; i < MaxIteration; i++) {
				var xold = xr;
				xr = xu - fu * (xl - xu) / (fl - fu);
				if (xr == 0)
					error = double.PositiveInfinity;
				else
					error = Math.Abs((xr - xold) / xr);

				Console.WriteLine("{0}\t\t\t{1:e2}\t{2:e2}\t{3:e2}\t{4:p5}", i + 1, xl, xu, xr, error);
				if (error < MaxError)
					break;

				var fr = expression.EvalFor(xr);
				var mul = fl * fr;
				if (mul < 0) {
					xu = xr;
					fu = fr;
					iu = 0;
					++il;
					if (il > 1)
						fl /= 2;
				} else if (mul > 0) {
					xl = xr;
					fl = fr;
					il = 0;
					++iu;
					if (iu > 1)
						fu /= 2;
				} else {
					break;
				}
			}
			return xr;
		}

		/// <summary>
		/// Finds the root of an equation using Fixed Point method
		/// </summary>
		/// <param name="expression">Function of which root is to find</param>
		/// <param name="x">Initial value of x</param>
		/// <returns>The root</returns>
		public static double FixedPoint(IFunction expression, double x = 0) {
			var xr = x;
			Console.WriteLine("Iteration\t\tXr\t\tEa");

			for (int i = 0; i < MaxIteration; i++) {
				var xold = xr;
				xr = expression.EvalFor(xr);
				var error = Math.Abs((xr - xold) / xr);

				Console.WriteLine("{0}\t\t\t{1:e2}\t{2:p5}", i + 1, xr, error);

				if (error < MaxError)
					break;
			}
			return xr;
		}

		/// <summary>
		/// Finds the root of an equation using Newton Raphson method
		/// </summary>
		/// <param name="expression">Function of which root is to find</param>
		/// <param name="x">Initial value of x</param>
		/// <returns>The root</returns>
		public static double NewtonRaphson(IFunction expression, double x = 0) {
			var xr = x;
			Console.WriteLine("Iteration\t\tXr\t\tEa");

			for (int i = 0; i < MaxIteration; i++) {
				var xold = xr;
				var fr = expression.EvalFor(xr);
				var dfr = expression.DerivativeAt(xr);
				xr = (xr - (fr / dfr));
				var error = Math.Abs((xr - xold) / xr);

				Console.WriteLine("{0}\t\t\t{1:e2}\t{2:p5}", i + 1, xr, error);

				if (error < MaxError)
					break;
			}
			return xr;
		}

		/// <summary>
		/// Finds the root of an equation using Secant method
		/// </summary>
		/// <param name="expression">Function of which root is to find</param>
		/// <param name="x">Initial value of x</param>
		/// <returns>The root</returns>
		public static double Secant(IFunction function, double prevx, double x) {
			var nextx = double.NaN;
			Console.WriteLine("Iteration\t\tXi-1\t\tXi\t\tXi+1\t\tEa");

			for (int i = 0; i < MaxIteration; i++) {
				var fx = function.EvalFor(x);
				var fxprev = function.EvalFor(prevx);
				nextx = x - ((fx * (prevx - x)) / (fxprev - fx));
				var error = Math.Abs((nextx - x) / nextx);

				Console.WriteLine("{0}\t\t\t{1:e2}\t{2:e2}\t{3:e2}\t{4:p5}", i + 1, prevx, x, nextx, error);
				prevx = x;
				x = nextx;
				if (error < MaxError)
					break;
			}
			return nextx;
		}
	}
}
