using task11;
using Xunit;

namespace Tests
{
    public class Tests
    {
        private ICalculator CreateTestCalculator()
        {
            string code = @"
                public class TestCalculator : ICalculator
                {
                    public int Add(int a, int b) => a + b;
                    public int Minus(int a, int b) => a - b;
                    public int Mul(int a, int b) => a * b;
                    public int Div(int a, int b) => b != 0 ? a / b : throw new System.DivideByZeroException();
                }";

            return CalculatorBuilder.CreateCalculator(code);
        }

        [Fact]
        public void Should_Add_Numbers_Correctly()
        {
            var calculator = CreateTestCalculator();
            var result = calculator.Add(7, 3);
            Assert.Equal(10, result);
        }

        [Fact]
        public void Should_Subtract_Numbers_Correctly()
        {
            var calculator = CreateTestCalculator();
            var result = calculator.Minus(15, 4);
            Assert.Equal(11, result);
        }

        [Fact]
        public void Should_Multiply_Numbers_Correctly() 
        {
            var calculator = CreateTestCalculator();
            var result = calculator.Mul(5, 6);
            Assert.Equal(30, result);
        }

        [Fact]
        public void Should_Divide_Numbers_Correctly()
        {
            var calculator = CreateTestCalculator();
            var result = calculator.Div(20, 5);
            Assert.Equal(4, result);
        }

        [Fact]
        public void Should_Throw_When_Dividing_By_Zero()
        {
            var calculator = CreateTestCalculator();
            Assert.Throws<DivideByZeroException>(() => calculator.Div(10, 0));
        }
    }
}
