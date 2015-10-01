using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleBASIC.Lexer;

namespace LittleBASIC.Parser.Nodes
{
    public class CodeBlockNode : AstNode
    {
        public static AstNode Parse(Parser parser)
        {
            CodeBlockNode block = new CodeBlockNode();

            while (!parser.EndOfStream)
                block.Children.Add(StatementNode.Parse(parser));

            return block;
        }
    }
}