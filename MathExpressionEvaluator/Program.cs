using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MathExpressionEvaluator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter an expression to be evaluated.");
            Console.WriteLine("Supported operators are: (+), (-), (*), (/) and (sqrt())");
            Console.WriteLine("e.g: (2*3+4)*(8-5)+2*(sqrt(6-2))");

            var input = Console.ReadLine();
            var mathExpressionEvaluator = new MathExpressionEvaluator();
            var result = mathExpressionEvaluator.Evaluate(input);
            Console.WriteLine($"{input} = {result}");
        }
    }
}