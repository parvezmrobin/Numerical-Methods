using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_Methods
{
	class Program
	{
		static void Main(string[] args) {
			Console.WriteLine("Write function: ");
			FunctionXY f = new FunctionXY("(1 + 2*x) * sqrt(y)");
			double a = 0, b = .5, h = .5, y = 1;
			Console.WriteLine("Eular: ");
			Integration.Euler(f, a, b, h, y);
			Console.WriteLine("Heun: ");
			Integration.Heun(f, a, b, h, y);
			Console.WriteLine("Mid Point: ");
			Integration.MidPoint(f, a, b, h, y);
			Console.WriteLine("Ralston: ");
			Integration.Ralston(f, a, b, h, y);
			Console.WriteLine("RK 4th order: ");
			Integration.RungeKutta4(f, a, b, h, y);

			Console.ReadKey();
		}
	}
}
