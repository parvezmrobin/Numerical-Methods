using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_Methods
{
	public static class Regression
	{
		/// <summary>
		/// Determines the linear regression for given series of (x,y)
		/// </summary>
		/// <param name="x">Array of x</param>
		/// <param name="y">Array of y</param>
		/// <param name="a0">Constant</param>
		/// <param name="a1">Slop</param>
		public static void LinearRegression(double[] x, double[] y, out double a0, out double a1) {
			if (x.Length != y.Length) {
				throw new ArgumentException("Lenght of x and y are not same");
			}
			int n = x.Length;
			double sumx, sumy, sumxy, sumx2, st, sr;
			sumx = sumy = sumxy = sumx2 = st = sr = 0;
			for (int i = 0; i < n; i++) {
				sumx += x[i];
				sumy += y[i];
				sumx2 += (x[i] * x[i]);
				sumxy += (x[i] * y[i]);
			}

			var avgx = sumx / n;
			var avgy = sumy / n;

			a1 = (n * sumxy - sumx * sumy) / (n * sumx2 - sumx * sumx);
			a0 = (avgy - a1 * avgx);
		}

		/// <summary>
		/// Determines the polinomial regression for given series of (x,y)
		/// </summary>
		/// <param name="x">Array of x</param>
		/// <param name="y">Array of y</param>
		/// <param name="order">Order of regression</param>
		/// <returns>Array containing the constants</returns>
		public static double[] PolinomialRegression(double[] x, double[] y, int order) {
			if (x.Length != y.Length) {
				throw new ArgumentException("Lenght of x and y are not same");
			}
			int n = x.Length;

			if (n <= order) {
				throw new ArgumentException("Number of (x,y) must be greater than order");
			}
			double[][] matrix = new double[order + 1][];
			for (int i = 0; i <= order; i++) {
				matrix[i] = new double[order + 1];
			}
			double[] b = new double[order + 1];
			double sumx, sumy;

			for (int i = 0; i <= order * 2; i++) {
				sumx = sumy = 0;
				int j, k;
				for (j = 0; j < n; j++) {
					var px = Math.Pow(x[j], i);
					sumx += px;
					if (i <= order)
						sumy += (y[j] * px);
				}
				if (i <= order)
					b[i] = sumy;

				if (i <= order) {
					j = 0;
					k = i;
				} else {
					j = i - order;
					k = order;
				}
				for (; j <= order && k >= 0; k--, j++) {
					matrix[k][j] = sumx;
				}
			}
			var a = LinearAlgebraicEquation.NaiveGauss(matrix, b);
			sumy = 0;
			return a;
		}

		
	}
}
