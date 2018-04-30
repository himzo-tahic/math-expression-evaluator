namespace MathExpressionEvaluator.Nodes
{
    public class NodeNumber : Node
    {
        public double Value {get;set;}

        public override double Evaluate() => Value;
    }
}