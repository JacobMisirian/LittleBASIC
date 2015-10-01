using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBASIC.Parser
{
    public abstract class AstNode
    {
        public List<AstNode> Children { get; private set; }

        public AstNode()
        {
            this.Children = new List<AstNode>();
        }
    }
}