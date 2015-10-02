using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleBASIC.Lexer;
using LittleBASIC.Parser.Nodes;

namespace LittleBASIC.Parser
{
    public class Parser
    {
        private List<Token> tokens { get; set; }
        private int position = 0;

        public bool EndOfStream { get { return tokens.Count <= position; } }

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public AstNode Parse()
        {
            CodeBlockNode tree = new CodeBlockNode();
            while (!EndOfStream)
                tree.Children.Add(Nodes.StatementNode.Parse(this));

            return tree;
        }

        public bool MatchToken(TokenType clazz)
        {
            return position < tokens.Count && tokens[position].TokenType == clazz;
        }

        public bool MatchToken(TokenType clazz, string value)
        {
            return position < tokens.Count && tokens[position].TokenType == clazz && (string)tokens[position].Value == value;
        }

        public bool AcceptToken(TokenType clazz)
        {
            if (MatchToken(clazz))
            {
                position++;
                return true;
            }

            return false;
        }

        public bool AcceptToken(TokenType clazz, string value)
        {
            if (MatchToken(clazz, value))
            {
                position++;
                return true;
            }

            return false;
        }

        public Token ExpectToken(TokenType clazz)
        {
            if (!MatchToken(clazz))
                throw new Exception("Tokens did not match. Expected " + clazz);

            return tokens[position++];
        }

        public Token ExpectToken(TokenType clazz, string value)
        {
            if (!MatchToken(clazz, value))
                throw new Exception("Tokens did not match. Expected " + clazz + " of value " + value);

            return tokens[position++];
        }

        public Token CurrentToken(int n = 0)
        {
            return tokens[position + n];
        }
    }
}
