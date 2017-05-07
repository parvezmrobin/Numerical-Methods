using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_Methods
{
	public class Function : IFunction
	{
		string expression;
		CompilerResults compilerResults;

		public string Expression {
			get { return expression; }
			set {
				expression = value;
				format();
				compile();
			}
		}

		/// <summary>
		/// Initializes a new Function object
		/// </summary>
		/// <param name="expression">Function in c style</param>
		public Function(string expression) {
			this.expression = expression;
			format();
			compile();
		}

		private void format() {
			expression = expression.Replace("sin", "Math.Sin");
			expression = expression.Replace("cos", "Math.Cos");
			expression = expression.Replace("tan", "Math.Tan");
			expression = expression.Replace("pow", "Math.Pow");
			expression = expression.Replace("log", "Math.Log10");
			expression = expression.Replace("e", "Math.E");
		}

		private void compile() {
			CompilerParameters compilerParameters = new CompilerParameters {
				GenerateInMemory = true,
				TreatWarningsAsErrors = false,
				GenerateExecutable = false,
			};

			string[] referencedAssemblies = { "System.dll" };
			compilerParameters.ReferencedAssemblies.AddRange(referencedAssemblies);

			string codeTemplate = "using System;public class Dynamic {{static public double Calculate(double x){{";

			codeTemplate += "return {0};}}}}";
			string code = string.Format(codeTemplate, expression);

			CSharpCodeProvider provider = new CSharpCodeProvider();
			compilerResults = provider.CompileAssemblyFromSource(compilerParameters, new string[] { code });
			if (compilerResults.Errors.HasErrors)
				throw new InvalidOperationException(compilerResults.Errors[0].ErrorText);
		}

		/// <summary>
		/// Derivative at x point
		/// </summary>
		/// <param name="x">Value of x</param>
		/// <param name="dell">A small value to calculate the derivative</param>
		/// <returns>Derivative at x point</returns>
		public double DerivativeAt(double x, double dell = .000001) {
			var x1 = x + dell;
			var x2 = x - dell;
			var y1 = EvalFor(x1);
			var y2 = EvalFor(x2);
			return (y2 - y1) / (x2 - x1);
		}

		/// <summary>
		/// Evaluate the value of function for x
		/// </summary>
		/// <param name="x">Value of x</param>
		/// <returns>Evaluated value of function</returns>
		public double EvalFor(double x) {
			Module module = compilerResults.CompiledAssembly.GetModules()[0];
			Type type = module.GetType("Dynamic");
			MethodInfo method = type.GetMethod("Calculate");

			var result = (double)(method.Invoke(null, new object[] { x }));

			return result;
		}
	}
}
