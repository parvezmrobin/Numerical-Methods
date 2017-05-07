using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_Methods
{
	public static class LinearAlgebraicEquation
	{
		public static double MaxError = .00001;
		public static double MaxIteration = 100;
		public static double Lambda = .5;

		/// <summary>
		/// Finds roots of n linear equations using Naive Gauss Formula		
		/// </summary>
		/// <param name="a">2D double array containing the co-efficients</param>
		/// <param name="b">Double array containing the constants</param>
		/// <returns>Double array containing the roots</returns>
		public static double[] NaiveGauss(double[][] a, double[] b) {
			int n = a.Length;
			
			//Forward Elimination
			for (int k = 0; k < n - 1; k++) {
				for (int i = k + 1; i < n; i++) {
					double factor = a[i][k] / a[k][k];
					for (int j = k + 1; j < n; j++) {
						a[i][j] = a[i][j] - factor * a[k][j];
					}
					b[i] = b[i] - factor * b[k];
				}
			}

			//Backward Substitution
			double[] x = new double[n];
			x[n - 1] = b[n - 1] / a[n - 1][n - 1];
			for (int i = n - 2; i >= 0; i--) {
				double sum = b[i];
				for (int j = i + 1; j < n; j++) {
					sum -= a[i][j] * x[j];
				}
				x[i] = sum / a[i][i];
			}
			return x;
		}

		/// <summary>
		/// Finds roots of n linear equations using Gauss Seidel Formula		
		/// </summary>
		/// <param name="a">2D double array containing the co-efficients</param>
		/// <param name="b">Double array containing the constants</param>
		/// <param name="r">Double array containing the initial guess of roots</param>
		/// <returns>Double array containing the roots</returns>
		public static double[] GaussSeidel(double[][] a, double[] b, double[] r) {
			int n = a.Length;

			//Division by the diagonal element to reduce calculation
			for (int i = 0; i < n; i++) {
				double d = a[i][i];
				for (int j = 0; j < n; j++) {
					a[i][j] = a[i][j] / d;
				}
				b[i] = b[i] / d;
			}

			//Generation of initial values for roots
			for (int i = 0; i < n; i++) {
				double sum = b[i];
				for (int j = 0; j < n; j++) {
					if (i != j) {
						sum -= (a[i][j] * r[j]);
					}
				}
				r[i] = sum;
			}

			//Iterations for converging to the real roots
			for (int itr = 1; itr < MaxIteration; itr++) {

				for (int i = 0; i < n; i++) {
					double old = r[i];
					double sum = b[i];

					for (int j = 0; j < n; j++) {
						if (i != j)
							sum -= (a[i][j] * r[j]);
					}

					r[i] = Lambda * sum + (1 - Lambda) * old;
					if (r[i] != 0) {
						double ea = Math.Abs((r[i] - old) / r[i]) * 100;
						if (ea < MaxError)
							return r;
					}
				}
			}

			return r;
		}

		/// <summary>
		/// Finds roots of n linear equations using LU Decomposition Formula with Gauss Elimination		
		/// </summary>
		/// <param name="a">2D double array containing the co-efficients</param>
		/// <param name="b">Double array containing the constants</param>
		/// <returns>Double array containing the roots</returns>
		public static double[] LUDecomposition(double[][] a, double[] b) {
			double[] m;
			int[] o;

			decompose(a, out o, out m);

			return substitute(a, b, o);
		}

		/// <summary>
		/// Finds roots of n linear equations using Gauss Jordan method
		/// </summary>
		/// <param name="a">2D double array containing the co-efficients</param>
		/// <param name="b">Double array containing the constants</param>
		/// <returns>Double array containing the roots</returns>
		public static double[] GaussJordan(double[][] a, double[] b) {
			int n = a.Length;
			double[][] c = new double[n][];

			for (int i = 0; i < n; i++) {
				c[i] = new double[n + 1];

				for (int j = 0; j < n; j++) {
					c[i][j] = a[i][j];
				}
				c[i][n] = b[i];
			}

			return GaussJordan(c);
		}

		/// <summary>
		/// Finds roots of n linear equations using Gauss Jordan method
		/// </summary>
		/// <param name="a">2D double array containing the equations</param>
		/// <returns>Double array containing the roots</returns>
		public static double[] GaussJordan(double[][] a) {
			int n = a.Length;

			for (int k = 0; k < n; k++) {
				for (int i = k+1; i < n+1; i++) {
					a[k][i] = a[k][i] / a[k][k];
				}

				a[k][k] = 1;

				for (int i = 0; i < n; i++) {
					if (i != k) {
						for (int j = k+1; j < n+1; j++) {
							a[i][j] = a[i][j] - (a[k][j] * a[i][k]);
						}
					}
				}		
								
			}
			double[] x = new double[n];
			for (int m = 0; m < n; m++) {
				x[m] = a[m][n];
			}

			return x;
		}

		/// <summary>
		/// Finds the Matrix Inverse using LU Decomposition
		/// </summary>
		/// <param name="a">2D double array representing the matrix</param>
		/// <returns>The array inverse</returns>
		public static double[][] MatrixInverse(double[][] a) {
			int n = a.Length;
			double[] b = new double[n];
			int[] o = new int[n];
			double[][] ainv = new double[n][];
			for (int i = 0; i < n; i++) {
				ainv[i] = new double[n];
			}

			for (int i = 0; i < n; i++) {
				o[i] = i;
			}

			for (int i = 0; i < n; i++) {
				for (int j = 0; j < n; j++) {
					if (i == j) {
						b[j] = 1;
					} else {
						b[j] = 0;
					}
				}
				double[] x = substitute(a, b, o);
				for (int j = 0; j < n; j++) {
					ainv[j][i] = x[j];
				}
			}

			return ainv;
		}
		

		#region private functions

		static void pivot(double[][] a, int[] o, double[] m, int k) {
			int p = k;
			int n = a.Length;
			double big = Math.Abs(a[o[k]][k] / m[o[k]]);
			double dummy;

			for (int ii = k + 1; ii < n; ii++) {
				dummy = Math.Abs(a[o[ii]][k] / m[o[ii]]);

				if (dummy > big) {
					big = dummy;
					p = ii;
				}
			}

			dummy = o[p];
			o[p] = o[k];
			o[k] = (int)dummy;
		}

		static void decompose(double[][] a, out int[] o, out double[] m) {
			int n = a.Length;
			m = new double[n];
			o = new int[n];

			for (int i = 0; i < n; i++) {
				o[i] = i;
				m[i] = Math.Abs(a[i][0]);
				for (int j = 1; j < n; j++) {
					if (Math.Abs(a[i][j]) > m[i]) {
						m[i] = Math.Abs(a[i][j]);
					}
				}
			}

			for (int k = 0; k < n - 1; k++) {
				pivot(a, o, m, k);


				Debug.Assert(Math.Abs(a[o[k]][k] / m[o[k]]) > float.Epsilon, (a[o[k]][k] / m[o[k]]).ToString());


				for (int i = k + 1; i < n; i++) {
					double factor = a[o[i]][k] / a[o[k]][k];
					a[o[i]][k] = factor;

					for (int j = k + 1; j < n; j++) {
						a[o[i]][j] = a[o[i]][j] - factor * a[o[k]][j];
					}
				}
			}

			Debug.Assert(Math.Abs(a[o[n - 1]][n - 1] / m[o[n - 1]]) > float.Epsilon, (Math.Abs(a[o[n - 1]][n - 1] / m[o[n - 1]])).ToString());
		}

		static double[] substitute(double[][] a, double[] b, int[] o) {
			int n = a.Length;
			double[] r = new double[n];

			for (int i = 1; i < n; i++) {
				double sum = b[o[i]];
				for (int j = 0; j < i - 1; j++) {
					sum -= (a[o[i]][j] * b[o[j]]);
				}
				b[o[i]] = sum;
			}

			r[n - 1] = b[o[n - 1]] / a[o[n - 1]][n - 1];

			for (int i = n - 2; i >= 0; i--) {
				double sum = 0;
				for (int j = i + 1; j < n; j++) {
					sum += (a[o[i]][j] * r[j]);
				}
				r[i] = (b[o[i]] - sum) / a[o[i]][i];
			}

			return r;
		}

		#endregion
	}
}
