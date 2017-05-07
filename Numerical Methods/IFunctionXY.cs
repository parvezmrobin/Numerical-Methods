namespace Numerical_Methods
{
	public interface IFunctionXY
	{
		/// <summary>
		/// Evaluate the value of function for x and y
		/// </summary>
		/// <param name="x">Value of x</param>
		/// <param name="y">Value of y</param>
		/// <returns>Evaluated value of function</returns>
		double EvalFor(double x, double y);
	}
}
