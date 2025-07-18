using task11;
using Xunit;

namespace task11tests
{
    public class CalculatorTests
    {
        private readonly ICalculator _calculator;

        public CalculatorTests()
        {
            string code = @"
                public class Calculator : task11.ICalculator
                {
                    public int Add(int a, int b) => a + b;
                    public int Minus(int a, int b) => a - b;
                    public int Mul(int a, int b) => a * b;
                    public int Div(int a, int b) => b != 0 ? a / b : throw new System.DivideByZeroException();
                }";

            _calculator = CalculatorBuilder.CreateCalculator(code);
        }

        [Fact]
        public void Add_ShouldReturnCorrectSum()
        {
            Assert.Equal(5, _calculator.Add(2, 3));
        }

        [Fact]
        public void Minus_ShouldReturnCorrectDifference()
        {
            Assert.Equal(1, _calculator.Minus(5, 4));
        }

        [Fact]
        public void Mul_ShouldReturnCorrectProduct()
        {
            Assert.Equal(6, _calculator.Mul(2, 3));
        }

        [Fact]
        public void Div_ShouldReturnCorrectQuotient()
        {
            Assert.Equal(2, _calculator.Div(6, 3));
        }

        [Fact]
        public void Div_ByZero_ShouldThrowException()
        {
            Assert.Throws<DivideByZeroException>(() => _calculator.Div(1, 0));
        }
    }
}
