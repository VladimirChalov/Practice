using System;
using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace task11
{
    public interface ICalculator
    {
        int Add(int a, int b);
        int Minus(int a, int b);
        int Mul(int a, int b);
        int Div(int a, int b);
    }

    public static class CalculatorFactory
    {
        public static ICalculator CreateInstance(string sourceCode)
        {
            var references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location)
            };

            var compilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);

            var compilation = CSharpCompilation.Create("TempAssembly", new[] {syntaxTree},references,compilationOptions);

            using var stream = new MemoryStream();
            var emitResult = compilation.Emit(stream);

            if (!emitResult.Success)
            {
                throw new InvalidOperationException("Ошибка компиляциq");
            }

            stream.Position = 0;
            var assembly = Assembly.Load(stream.ToArray());
            var calculatorType = assembly.GetType("Calculator");

            if (calculatorType == null)
            {
                throw new TypeLoadException("Класс Calculator не найден");
            }

            var instance = Activator.CreateInstance(calculatorType);
            return (ICalculator)instance;
        }
    }
}
