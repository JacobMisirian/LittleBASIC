using System;
namespace LittleBASIC.Parser.Nodes
{
    public class PauseNode: AstNode
    {
        public string Message { get; private set; }

        public PauseNode(string message)
        {
            Message = message;
        }

        public static AstNode Parse(Parser parser)
        {
            parser.ExpectToken(Lexer.TokenType.Identifier, "PAUSE");
            if (parser.MatchToken(Lexer.TokenType.String) || parser.MatchToken(Lexer.TokenType.Number))
                return new PauseNode((string)parser.ReadToken().Value);
            else
                return new PauseNode("PRESS ANY KEY TO CONTINUE...");
        }
    }
}

