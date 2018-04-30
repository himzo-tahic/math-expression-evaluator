using System;
using System.Collections.Generic;
using System.Linq;
using MathExpressionEvaluator;
using MathExpressionEvaluator.Nodes;
using MathExpressionEvaluator.Tokenization;
using Xunit;

namespace MathExpressionEvaluator.Tests
{
    //TODO: Tests for more complicated inputs
    public class MathExpressionEvaluatorTests
    {
        static readonly MathExpressionEvaluator MathExpressionEvaluator = new MathExpressionEvaluator();

        [Theory]
        [InlineData("5 + 7", 12)]
        [InlineData("5 + 3 * 7", 26)]
        [InlineData("5 * (2 + 3)", 25)]
        [InlineData("(5 + 7) * (8 + 2)", 120)]
        [InlineData("sqrt(169)", 13)]
        [InlineData("(7 + 8) * 3 + 2 * (4 + sqrt(9))", 59)]
        public void CanEvaluate(string input, int output)
        {
            Assert.Equal(MathExpressionEvaluator.Evaluate(input), output);
        }
    }
}
