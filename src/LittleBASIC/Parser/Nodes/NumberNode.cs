using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LittleBASIC.Parser.Nodes
{
    public class NumberNode : AstNode
    {
        public double Value { get; private set; }

        public NumberNode(double value)
        {
            Value = value;
        }
    }
}