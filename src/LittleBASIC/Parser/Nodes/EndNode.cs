using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBASIC.Parser.Nodes
{
    public class EndNode: AstNode
    {
        public int ExitCode { get; private set; }

        public EndNode()
        {
        }

        public EndNode(int exitCode)
        {
            ExitCode = exitCode;
        }
    }
}
