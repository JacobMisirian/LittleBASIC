using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleBASIC.Parser;
using LittleBASIC.Parser.Nodes;

namespace LittleBASIC.Interpreter
{
    public class Interpreter
    {
        public Dictionary<string, object> Variables = new Dictionary<string, object>();

        private AstNode code { get; set; }

        public Interpreter(AstNode code)
        {
            this.code = code;
        }

        public void Interpret()
        {
            for (int x = 0; x < code.Children.Count; x++)
                executeStatement(code.Children[x]);
        }

        private void executeStatement(AstNode node)
        {
            if (node is CodeBlockNode)
            {
                foreach (AstNode cnode in node.Children)
                    executeStatement(cnode);
            }
            else if (node is ConditionalNode)
            {
                ConditionalNode cnode = (ConditionalNode)node;
                bool evaluates = (bool)evaluateNode(cnode.Predicate);
                if (evaluates)
                    executeStatement(cnode.Body);
                else if (!evaluates && cnode.Children.Count > 2)
                    executeStatement(cnode.ElseBody);
            }
            else if (node is WhileNode)
            {
                WhileNode wnode = (WhileNode)node;
                while ((bool)evaluateNode(wnode.Predicate))
                    executeStatement(wnode.Body);
            }
            else if (node is PrintNode)
                Console.WriteLine(evaluateNode(((PrintNode)node).Value));
            else
                evaluateNode(node);
        }

        private object evaluateNode(AstNode node)
        {
            if (node is IdentifierNode)
            {
                IdentifierNode idNode = (IdentifierNode)node;
                if (Variables.ContainsKey(idNode.Identifier))
                    return Variables[idNode.Identifier];
                throw new Exception("Variable " + idNode.Identifier + " does not exist in dictionary!");
            }
            else if (node is NumberNode)
                return ((NumberNode)node).Value;
            else if (node is StringNode)
                return ((StringNode)node).Value;
            else if (node is BinaryOpNode)
                return interpretBinaryOperation((BinaryOpNode)node);
            else
                throw new Exception("Unknown node " + node.ToString() + "  " + node.GetType());
        }

        private object interpretBinaryOperation(BinaryOpNode node)
        {
            switch (node.BinaryOp)
            {
                case BinaryOperation.Addition:
                    return Convert.ToDouble(evaluateNode(node.Left)) + Convert.ToDouble(evaluateNode(node.Right));
                case BinaryOperation.Subtraction:
                    return Convert.ToDouble(evaluateNode(node.Left)) - Convert.ToDouble(evaluateNode(node.Right));
                case BinaryOperation.Division:
                    return Convert.ToDouble(evaluateNode(node.Left)) / Convert.ToDouble(evaluateNode(node.Right));
                case BinaryOperation.Multiplication:
                    return Convert.ToDouble(evaluateNode(node.Left)) * Convert.ToDouble(evaluateNode(node.Right));
                case BinaryOperation.Modulus:
                    return Convert.ToDouble(evaluateNode(node.Left)) % Convert.ToDouble(evaluateNode(node.Right));
                case BinaryOperation.Equals:
                    return evaluateNode(node.Left).GetHashCode() == evaluateNode(node.Right).GetHashCode();
                case BinaryOperation.LessThan:
                    return Convert.ToDouble(evaluateNode(node.Left)) < Convert.ToDouble(evaluateNode(node.Right));
                case BinaryOperation.GreaterThan:
                    return Convert.ToDouble(evaluateNode(node.Left)) > Convert.ToDouble(evaluateNode(node.Right));
                case BinaryOperation.Not:
                    return evaluateNode(node.Left).GetHashCode() != evaluateNode(node.Right).GetHashCode();
                case BinaryOperation.Assignment:
                    if (!(node.Left is IdentifierNode))
                        throw new Exception("Must be an identifier!");
                    object right = evaluateNode(node.Right);
                    string left = ((IdentifierNode)node.Left).Identifier;
                    if (Variables.ContainsKey(left))
                        Variables.Remove(left);
                    Variables.Add(left, right);
                    return right;
                default:
                    throw new NotImplementedException("Unknown binary operation");
            }
        }
    }
}
