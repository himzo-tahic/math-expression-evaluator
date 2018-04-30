using System;
using System.Collections.Generic;
using System.Linq;
using MathExpressionEvaluator;
using MathExpressionEvaluator.Tokenization;
using Xunit;

namespace MathExpressionEvaluator.Tests
{
    public class TokenizerTests
    {
        static readonly Tokenizer Tokenizer = new Tokenizer();

        [Fact]
        public void CanTokenize()
        {
            var expectedOutput = new List<Token>()
            {
                new Token{Type = TokenType.Number, Value = 5},
                new Token{Type = TokenType.Add, Value = null},
                new Token{Type = TokenType.Number, Value = 7},
            };

            var input = "5 + 7";
            var result = Tokenizer.Tokenize(input).ToList();
            for(var i = 0; i < result.Count; i++){
                var expected = expectedOutput[i];
                var actual = result[i];
                Assert.Equal(expected.Type, actual.Type);
                Assert.Equal(expected.Value, actual.Value);
            }
        }

        [Fact]
        public void CanTokenizeComplicated()
        {
            var expectedOutput = new List<Token>()
            {
                new Token{Type = TokenType.ParenOpen, Value = null},
                new Token{Type = TokenType.Number, Value = 7},
                new Token{Type = TokenType.Add, Value = null},
                new Token{Type = TokenType.Number, Value = 8},
                new Token{Type = TokenType.ParenClose, Value = null},
                new Token{Type = TokenType.Multiply, Value = null},
                new Token{Type = TokenType.Number, Value = 3},
                new Token{Type = TokenType.Add, Value = null},
                new Token{Type = TokenType.Number, Value = 2},
                new Token{Type = TokenType.Multiply, Value = null},
                new Token{Type = TokenType.ParenOpen, Value = null},
                new Token{Type = TokenType.Number, Value = 4},
                new Token{Type = TokenType.Add, Value = null},
                new Token{Type = TokenType.Sqrt, Value = null},
                new Token{Type = TokenType.ParenOpen, Value = null},
                new Token{Type = TokenType.Number, Value = 3},
                new Token{Type = TokenType.ParenClose, Value = null},
                new Token{Type = TokenType.ParenClose, Value = null},
            };

            var input = "(7 + 8) * 3 + 2 * (4 + sqrt(3))";
            var result = Tokenizer.Tokenize(input).ToList();
            for(var i = 0; i < result.Count; i++){
                var expected = expectedOutput[i];
                var actual = result[i];
                Assert.Equal(expected.Type, actual.Type);
                Assert.Equal(expected.Value, actual.Value);
            }
        }
    }
}
