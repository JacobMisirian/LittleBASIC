using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LittleBASIC.Parser.Nodes
{
    public class StatementNode: AstNode
    {
        public static AstNode Parse(Parser parser)
        {
            if (parser.MatchToken(Lexer.TokenType.Identifier, "IF"))
                return ConditionalNode.Parse(parser);
            else if (parser.MatchToken(Lexer.TokenType.Identifier, "WHILE"))
                return WhileNode.Parse(parser);
            else if (parser.MatchToken(Lexer.TokenType.Identifier, "PRINT"))
                return PrintNode.Parse(parser);
            else if (parser.MatchToken(Lexer.TokenType.Identifier, "INPUT"))
                return InputNode.Parse(parser);
            else if (parser.MatchToken(Lexer.TokenType.Identifier, "LET"))
                return LetNode.Parse(parser);
            else if (parser.MatchToken(Lexer.TokenType.Identifier, "GOTO"))
                return GotoNode.Parse(parser);
            else if (parser.MatchToken(Lexer.TokenType.Identifier, "CLS"))
                return ClsNode.Parse(parser);
            else if (parser.MatchToken(Lexer.TokenType.Identifier, "PAUSE"))
                return PauseNode.Parse(parser);
            else
                return ExpressionNode.Parse(parser);
        }
    }
}
