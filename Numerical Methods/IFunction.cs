namespace Numerical_Methods
{
	public interface IFunction
	{
		double EvalFor(double x);

		double DerivativeAt(double x, double dell = .00001);
	}
}
