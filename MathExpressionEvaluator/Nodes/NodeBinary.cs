using System;

namespace MathExpressionEvaluator.Nodes
{
    public abstract class NodeBinary : Node
    {
        public Node Left {get;set;}
        public Node Right {get;set;}
        protected abstract Func<double,double,double> Reduce {get;set;}
        public override double Evaluate() => Reduce(Left.Evaluate(), Right.Evaluate());
    }

    public class NodeAdd : NodeBinary 
    {
        protected override Func<double,double,double> Reduce {get;set;} = (a,b) => a + b;
    }

    public class NodeDivide : NodeBinary 
    {
        protected override Func<double,double,double> Reduce {get;set;} = (a,b) => a / b;
    }

    public class NodeMultiply : NodeBinary 
    {
        protected override Func<double,double,double> Reduce {get;set;} = (a,b) => a * b;
    }

    public class NodeSubstract : NodeBinary 
    {
        protected override Func<double,double,double> Reduce {get;set;} = (a,b) => a - b;
    }
}