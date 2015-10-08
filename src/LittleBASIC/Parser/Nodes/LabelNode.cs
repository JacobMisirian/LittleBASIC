using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LittleBASIC.Parser.Nodes
{
    public class LabelNode: AstNode
    {
        public string Label { get; private set; }

        public LabelNode(string label)
        {
            Label = label;
        }
    }
}
