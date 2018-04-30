using System;
using System.Collections.Generic;

namespace MathExpressionEvaluator.Tokenization
{
    public class Tokenizer
    {
        public static class StopChars 
        {
            public const char Add = '+';
            public const char Subtract = '-';
            public const char Multiply = '*';
            public const char Divide = '/';
            public const char Sqrt = 's';
            public const string SqrtLong = "sqrt";
            public const char ParenOpen = '(';
            public const char ParenClose = ')';
        };

        public string Sanitize(string expression)
        {
            return expression
                .Trim()
                .Replace(" ", "");
        }
        public IEnumerable<Token> Tokenize(string expression) 
        {
            var sanitized = Sanitize(expression);
            for(var i = 0; i < sanitized.Length; i++)
            {
                switch(sanitized[i])
                {
                    case StopChars.Add:
                        yield return new Token{Type=TokenType.Add};
                        break;
                    case StopChars.Subtract:
                        yield return new Token{Type=TokenType.Subtract};
                        break;
                    case StopChars.Multiply:
                        yield return new Token{Type=TokenType.Multiply};
                        break;
                    case StopChars.Divide:
                        yield return new Token{Type=TokenType.Divide};
                        break;
                    case StopChars.ParenClose:
                        yield return new Token{Type=TokenType.ParenClose};
                        break;
                    case StopChars.ParenOpen:
                        yield return new Token{Type=TokenType.ParenOpen};
                        break;
                    case StopChars.Sqrt:
                        if(sanitized.Substring(i, StopChars.SqrtLong.Length) == StopChars.SqrtLong)
                        {
                            i += StopChars.SqrtLong.Length - 1;
                            yield return new Token{Type=TokenType.Sqrt};
                        } else throw new Exception($"Unknown Token '{sanitized[i]}'");
                        break;
                    default:
                        if(Char.IsDigit(sanitized[i]))
                        {
                            var numberAsString = string.Empty;
                            for(;i < sanitized.Length && Char.IsDigit(sanitized[i]);)
                                numberAsString += sanitized[i++];
                            i--;
                            yield return new Token{Type=TokenType.Number, Value = double.Parse(numberAsString)};
                            break;
                        }
                        throw new Exception($"Unknown Token '{sanitized[i]}'");
                }
            }
        }
    }
}