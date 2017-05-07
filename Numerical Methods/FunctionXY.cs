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
	public class FunctionXY : IFunctionXY
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
		/// Initializes a new FunctionXY object
		/// </summary>
		/// <param name="expression">Function in c style</param>
		public FunctionXY(string expression) {
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
			expression = expression.Replace("sqrt", "Math.Sqrt");
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

			string codeTemplate = "using System;public class Dynamic {{static public double Calculate(double x, double y){{";

			codeTemplate += "return {0};}}}}";
			string code = string.Format(codeTemplate, expression);

			CSharpCodeProvider provider = new CSharpCodeProvider();
			compilerResults = provider.CompileAssemblyFromSource(compilerParameters, new string[] { code });
			if (compilerResults.Errors.HasErrors)
				throw new InvalidOperationException(compilerResults.Errors[0].ErrorText);
		}

		public double EvalFor(double x, double y) {
			Module module = compilerResults.CompiledAssembly.GetModules()[0];
			Type type = module.GetType("Dynamic");
			MethodInfo method = type.GetMethod("Calculate");

			var result = (double)(method.Invoke(null, new object[] { x, y }));

			return result;
		}
	}
}
