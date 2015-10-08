using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LittleBASIC.Parser.Nodes
{
    public class StringNode : AstNode
    {
        public string Value { get; private set; }

        public StringNode(string value)
        {
            Value = value;
        }
    }
}
