using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleBASIC.Lexer;

namespace LittleBASIC.Parser.Nodes
{
    public class ExpressionNode: AstNode 
    {
        public static AstNode Parse(Parser parser)
        {
            return parseAssignment(parser);
        }

        private static AstNode parseAssignment(Parser parser)
        {
            AstNode left = parseAdditive(parser);

            if (parser.AcceptToken(TokenType.Assignment))
                return new BinaryOpNode(BinaryOperation.Assignment, left, parseAssignment(parser));
            else if (parser.AcceptToken(TokenType.Comparison, "=="))
                return new BinaryOpNode(BinaryOperation.Equals, left, parseAssignment(parser));
            else
                return left;
        }

        private static AstNode parseAdditive(Parser parser)
        {
            AstNode left = parseMultiplicitive(parser);
            while (parser.MatchToken(TokenType.Operation))
            {
                switch ((string)parser.CurrentToken().Value)
                {
                    case "+":
                        parser.AcceptToken(TokenType.Operation);
                        left = new BinaryOpNode(BinaryOperation.Addition, left, parseMultiplicitive(parser));
                        continue;
                    case "-":
                        parser.AcceptToken(TokenType.Operation);
                        left = new BinaryOpNode(BinaryOperation.Subtraction, left, parseMultiplicitive(parser));
                        continue;
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private static AstNode parseMultiplicitive(Parser parser)
        {
            AstNode left = parseComparison(parser);
            while (parser.MatchToken(TokenType.Operation))
            {
                switch ((string)parser.CurrentToken().Value)
                {
                    case "*":
                        parser.AcceptToken(TokenType.Operation);
                        left = new BinaryOpNode(BinaryOperation.Multiplication, left, parseComparison(parser));
                        continue;
                    case "/":
                        parser.AcceptToken(TokenType.Operation);
                        left = new BinaryOpNode(BinaryOperation.Division, left, parseComparison(parser));
                        continue;
                    case "%":
                        parser.AcceptToken(TokenType.Operation);
                        left = new BinaryOpNode(BinaryOperation.Modulus, left, parseComparison(parser));
                        continue;
                    case "=":
                        parser.AcceptToken(TokenType.Assignment);
                        left = new BinaryOpNode(BinaryOperation.Assignment, left, parseComparison(parser));
                        continue;
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private static AstNode parseComparison(Parser parser)
        {
            AstNode left = parseTerm(parser);
            while (parser.MatchToken(TokenType.Comparison))
            {
                switch ((string)parser.CurrentToken().Value)
                {
                    case ">":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOpNode(BinaryOperation.GreaterThan, left, parseTerm(parser));
                        continue;
                    case "<":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOpNode(BinaryOperation.LessThan, left, parseTerm(parser));
                        continue;
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private static AstNode parseTerm(Parser parser)
        {
            if ((string)parser.CurrentToken(1).Value == ":")
            {
                LabelNode lnode;
                if (parser.MatchToken(TokenType.Identifier))
                    lnode = new LabelNode((string)parser.ExpectToken(TokenType.Identifier).Value);
                else if (parser.MatchToken(TokenType.Number))
                    lnode = new LabelNode(Convert.ToString(parser.ExpectToken(TokenType.Number).Value));
                else
                    throw new Exception("Unknown label type " + parser.CurrentToken().TokenType + " " + parser.CurrentToken().Value);

                parser.ExpectToken(TokenType.Identifier, ":");
                return lnode;
            }
            else if (parser.MatchToken(TokenType.Number))
                return new NumberNode(Convert.ToDouble(parser.ExpectToken(TokenType.Number).Value));
            else if (parser.AcceptToken(TokenType.Parentheses, "("))
            {
                AstNode statement = ExpressionNode.Parse(parser);
                parser.ExpectToken(TokenType.Parentheses, ")");
                return statement;
            }
            else if (parser.MatchToken(TokenType.Identifier, "THEN"))
            {
                CodeBlockNode block = new CodeBlockNode();
                parser.ExpectToken(TokenType.Identifier, "THEN");

                while (!parser.EndOfStream && !parser.MatchToken(TokenType.Identifier, "ENDIF") && !parser.MatchToken(TokenType.Identifier, "ELSE"))
                    block.Children.Add(StatementNode.Parse(parser));

                if (parser.MatchToken(TokenType.Identifier, "ELSE"))
                    return block;

                parser.ExpectToken(TokenType.Identifier, "ENDIF");

                return block;
            }
            else if (parser.MatchToken(TokenType.Identifier, "ELSE"))
            {
                parser.ExpectToken(TokenType.Identifier, "ELSE");
                return StatementNode.Parse(parser);
            }
            else if (parser.MatchToken(TokenType.Identifier, "DO"))
            {
                CodeBlockNode block = new CodeBlockNode();
                parser.ExpectToken(TokenType.Identifier, "DO");

                while (!parser.EndOfStream && !parser.MatchToken(TokenType.Identifier, "ENDWHILE"))
                    block.Children.Add(StatementNode.Parse(parser));

                parser.ExpectToken(TokenType.Identifier, "ENDWHILE");

                return block;
            }
            else if (parser.MatchToken(TokenType.String))
                return new StringNode((string)parser.ExpectToken(TokenType.String).Value);
            else if (parser.MatchToken(TokenType.Identifier))
                return new IdentifierNode((string)parser.ExpectToken(TokenType.Identifier).Value);
            else
                throw new Exception("Unexpected " + parser.CurrentToken().TokenType + " in Parser: " + parser.CurrentToken().Value + ".");
        }
    }
}