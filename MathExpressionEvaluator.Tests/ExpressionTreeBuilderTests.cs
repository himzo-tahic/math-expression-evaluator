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
    public class ExpressionTreeBuilderTests
    {
        static readonly ExpressionTreeBuilder ExpressionTreeBuilder = new ExpressionTreeBuilder();

        [Fact]
        public void CanBuildExpressionTree()
        {
            var input = new List<Token>()
            {
                new Token{Type = TokenType.Number, Value = 5},
                new Token{Type = TokenType.Add, Value = null},
                new Token{Type = TokenType.Number, Value = 7},
            };

            var expectedOutput = new NodeAdd
            {
                Left = new NodeNumber{ Value = 5 },
                Right = new NodeNumber{ Value = 7 }
            };

            var root = ExpressionTreeBuilder.Build(input) as NodeAdd;
            Assert.NotNull(root);

            var left = root.Left as NodeNumber;
            Assert.NotNull(left);
            Assert.Equal(5, left.Value);

            var right = root.Right as NodeNumber;
            Assert.NotNull(right);
            Assert.Equal(7, right.Value);   
        }
    }
}
