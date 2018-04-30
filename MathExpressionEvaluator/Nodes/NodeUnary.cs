using System;

namespace MathExpressionEvaluator.Nodes
{
    public abstract class NodeUnary : Node
    {
        public Node Value {get;set;}
        protected abstract Func<double,double> Reduce {get;set;}

        public override double Evaluate() => Reduce(Value.Evaluate());
    }

    public class NodeBracket : NodeUnary
    {
        protected override Func<double,double> Reduce {get;set;} = (a) => a;
    }

    public class NodeSqrt : NodeUnary
    {
        protected override Func<double,double> Reduce {get;set;} = (a) => Math.Sqrt(a);
    }
}