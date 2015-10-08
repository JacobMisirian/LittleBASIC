using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LittleBASIC.Parser.Nodes
{
    public enum BinaryOperation
    {
        Assignment,
        Addition,
        Subtraction,
        Division,
        Multiplication,
        Modulus,
        Equals,
        Not,
        LessThan,
        GreaterThan
    }

    class BinaryOpNode : AstNode
    {
        public BinaryOperation BinaryOp { get; set; }
        public AstNode Left { get { return Children[0]; } }
        public AstNode Right { get { return Children[1]; } }

        public BinaryOpNode(BinaryOperation op, AstNode left, AstNode right)
        {
            BinaryOp = op;
            Children.Add(left);
            Children.Add(right);
        }
    }
}
