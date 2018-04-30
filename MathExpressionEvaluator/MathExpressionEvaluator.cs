using System;
using System.Linq;
using MathExpressionEvaluator.Tokenization;

namespace MathExpressionEvaluator
{
    public class MathExpressionEvaluator 
    {
        private Tokenizer _tokenizer;
        private ExpressionTreeBuilder _expressionTreeBuilder;

        public MathExpressionEvaluator()
        {
            _tokenizer = new Tokenizer();
            _expressionTreeBuilder = new ExpressionTreeBuilder();   
        }
        public double Evaluate(string expression) 
        {
            var tokens = _tokenizer.Tokenize(expression).ToList();
            
            // foreach(var token in tokens)
            //     Console.Write($"[{token.Type}({token.Value?.ToString()})] ");

            var expressionTree = _expressionTreeBuilder.Build(tokens);
            var evaluationResult = expressionTree.Evaluate();
            // Console.WriteLine($"{expression} = {evaluationResult}");
            return evaluationResult;
        }
    }
}