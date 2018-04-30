using System;
using System.Collections.Generic;
using System.Linq;
using MathExpressionEvaluator.Tokenization;
using MathExpressionEvaluator.Nodes;

namespace MathExpressionEvaluator
{
    public class ExpressionTreeBuilder 
    {
        /*
         * Builds an expression tree by recursively evaluating slices of tokens.
         * To build a proper expression tree, we have to split up the input bottom-up
         * we start by creating nodes with the lowest-binding tokens (single numbers, +,-)
         * and continue up to the highest-binding tokens (brackets, sqrt)
         */
        public Node Build(List<Token> tokens)
        {
            EvaluateBrackets(tokens);
            
            if (tokens.IsLeaf())
                return new NodeNumber() { Value = tokens[0].Value.Value };

            //Slice up by + and -
            for (var i = 0; i < tokens.Count; i++)
            {
                var current = tokens[i];
                if (current.Type == TokenType.Add)
                {
                    return new NodeAdd
                    {
                        Left = Build(tokens.Take(i).ToList()),
                        Right = Build(tokens.Skip(i + 1).ToList())
                    };
                }
                if (current.Type == TokenType.Subtract)
                {
                    return new NodeSubstract
                    {
                        Left = Build(tokens.Take(i).ToList()),
                        Right = Build(tokens.Skip(i + 1).ToList())
                    };
                }
            }

            //Slice up my * and /
            for (var i = 0; i < tokens.Count; i++)
            {
                var current = tokens[i];
                if (current.Type == TokenType.Multiply)
                {
                    return new NodeMultiply
                    {
                        Left = Build(tokens.Take(i).ToList()),
                        Right = Build(tokens.Skip(i + 1).ToList())
                    };
                }

                if (current.Type == TokenType.Divide)
                {
                    return new NodeDivide
                    {
                        Left = Build(tokens.Take(i).ToList()),
                        Right = Build(tokens.Skip(i + 1).ToList())
                    };
                }
            }

            //Resolve SQRT
            for (var i = 0; i < tokens.Count; i++)
            {
                var current = tokens[i];
                if (current.Type == TokenType.Sqrt)
                {
                    return new NodeSqrt
                    {
                        Value = Build(tokens.Skip(i + 1).ToList())
                    };
                }
            }

            return null;
        }

        private void EvaluateBrackets(List<Token> tokens)
        {
            //Evaluate Brackets
            for (var i = 0; i < tokens.Count; i++)
            {
                var current = tokens[i];
                var next = tokens.Count <= i ? tokens[i] : null;
                if (current.Type == TokenType.ParenOpen)
                {
                    var slice = tokens.Skip(i + 1).ToList();
                    for (int j = 0, z = 0; j < slice.Count; j++)
                    {
                        if (slice[j].Type == TokenType.ParenClose)
                        {
                            if (z == 0)
                            {
                                slice = slice.Take(j).ToList();
                                break;
                            }
                            else z--;
                        }

                        if (slice[j].Type == TokenType.ParenOpen)
                            z++;
                    }
                    var node = new NodeBracket
                    {
                        Value = Build(slice.ToList())
                    };
                    tokens[i] = new Token { Type = TokenType.Number, Value = node.Evaluate() };
                    tokens.RemoveRange(i + 1, slice.Count() + 1);
                }
            }
        }
    }

    public static class TokenListExtensions 
    {
        public static bool IsLeaf(this List<Token> tokens)
        {
            return tokens.Count == 1 && tokens[0].Type == TokenType.Number;
        }
    }
}