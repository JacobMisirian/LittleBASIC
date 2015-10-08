using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LittleBASIC.Parser.Nodes
{
    public class IdentifierNode : AstNode
    {
        public string Identifier { get; private set; }

        public IdentifierNode(string value)
        {
            Identifier = value;
        }

        public override string ToString()
        {
            return Identifier;
        }
    }
}