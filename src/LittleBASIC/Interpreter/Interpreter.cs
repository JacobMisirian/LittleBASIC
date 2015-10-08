using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using LittleBASIC.Parser;
using LittleBASIC.Parser.Nodes;

namespace LittleBASIC.Interpreter
{
    public class Interpreter
    {
        public Dictionary<string, object> Variables = new Dictionary<string, object>
        {
            {"TRUE", true },
            {"FALSE", false },
            {"NULL", null }
        };

        public Dictionary<string, int> Labels = new Dictionary<string, int>();

        private AstNode code { get; set; }
        private int position { get; set; }

        public Interpreter()
        {
        }

        public Interpreter(AstNode code)
        {
            this.code = code;
            foreach (Dictionary<string, InternalFunction> entries in getFunctions())
                foreach (KeyValuePair<string, InternalFunction> entry in entries)
                    Variables.Add(entry.Key, entry.Value);
        }

        public void Interpret()
        {
            for (position = 0; position < code.Children.Count; position++)
                executeStatement(code.Children[position]);
        }

        public void Interpret(AstNode code)
        {
            this.code = code;
            Interpret();
        }

        private void executeStatement(AstNode node)
        {
            if (node is CodeBlockNode)
                foreach (AstNode cnode in node.Children)
                    executeStatement(cnode);
            else if (node is ConditionalNode)
            {
                ConditionalNode cnode = (ConditionalNode)node;
                bool evaluates = (bool)evaluateNode(cnode.Predicate);
                if (evaluates)
                    executeStatement(cnode.Body);
                else if (!evaluates && cnode.Children.Count > 2)
                    executeStatement(cnode.ElseBody);
            }
            else if (node is NumberNode)
            {
                string name = ((NumberNode)node).Value.ToString();
                if (Labels.ContainsKey(name))
                    Labels.Remove(name);
                Labels.Add(name, position);
            }
            else if (node is WhileNode)
            {
                WhileNode wnode = (WhileNode)node;
                while ((bool)evaluateNode(wnode.Predicate))
                    executeStatement(wnode.Body);
            }
            else if (node is LabelNode)
            {
                string label = ((LabelNode)node).Label;
                if (Labels.ContainsKey(label))
                    Labels.Remove(label);

                Labels.Add(label, position);
            }
            else if (node is GotoNode)
                position = Labels[((GotoNode)node).Position];
            else if (node is PrintNode)
                Console.WriteLine(evaluateNode(((PrintNode)node).Value));
            else if (node is InputNode)
            {
                string variable = ((IdentifierNode)((InputNode)node).Variable).Identifier;
                if (Variables.ContainsKey(variable))
                    Variables.Remove(variable);

                Variables.Add(variable, Console.ReadLine());
            }
            else if (node is EndNode)
            {
                EndNode enode = (EndNode)node;
                if (enode.Children.Count <= 0)
                    Environment.Exit(0);

                Environment.Exit(enode.ExitCode);
            }
            else if (node is ClsNode)
                Console.Clear();
            else if (node is PauseNode)
            {
                Console.WriteLine(((PauseNode)node).Message);
                Console.ReadKey(true);
            }
            else
                evaluateNode(node);
        }

        private object evaluateNode(AstNode node)
        {
            if (node is IdentifierNode)
            {
                IdentifierNode idNode = (IdentifierNode)node;
                if (Variables.ContainsKey(idNode.Identifier.ToUpper()))
                    return Variables[idNode.Identifier.ToUpper()];
                throw new Exception("Variable " + idNode.Identifier.ToUpper() + " does not exist in dictionary!");
            }
            else if (node is FunctionCallNode)
            {
                FunctionCallNode fnode = (FunctionCallNode)node;
                IFunction target = evaluateNode(fnode.Target) as IFunction;
                if (target == null)
                    throw new Exception("Attempt to run a non-valid function!");
                object[] arguments = new object[fnode.Arguments.Children.Count];
                for (int x = 0; x < fnode.Arguments.Children.Count; x++)
                    arguments[x] = evaluateNode(fnode.Arguments.Children[x]);
                return target.Invoke(arguments);
            }
            else if (node is NumberNode)
                return ((NumberNode)node).Value;
            else if (node is StringNode)
                return ((StringNode)node).Value;
            else if (node is BinaryOpNode)
                return interpretBinaryOperation((BinaryOpNode)node);
            else if (node is LetNode)
            {
                LetNode lnode = (LetNode)node;
                string variable = ((IdentifierNode)lnode.Variable).Identifier;
                object data = evaluateNode(lnode.Data);
                if (Variables.ContainsKey(variable.ToUpper()))
                    throw new Exception("Variable " + variable.ToUpper() + " already has been declared!");
                Variables.Add(variable.ToUpper(), data);

                return data;
            }
            else
                throw new Exception("Unknown node " + node.ToString() + "  " + node.GetType());
        }

        private object interpretBinaryOperation(BinaryOpNode node)
        {
            switch (node.BinaryOp)
            {
                case BinaryOperation.Assignment:
                    if (!(node.Left is IdentifierNode))
                        throw new Exception("Must be an identifier!");
                    object right = evaluateNode(node.Right);
                    string left = ((IdentifierNode)node.Left).Identifier.ToUpper();

                    if (!Variables.ContainsKey(left))
                        throw new Exception("Variable " + left + " is being used before it is declared!");

                    Variables.Remove(left);
                    Variables.Add(left, right);

                    return right;
                case BinaryOperation.Addition:
                    object addLeft = evaluateNode(node.Left);
                    object addRight = evaluateNode(node.Right);
                    if (addLeft is string || addRight is string)
                        return (string)addLeft + (string)addRight;
                    else
                        return Convert.ToDouble(addLeft) + Convert.ToDouble(addRight);
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
                    return !((bool)interpretBinaryOperation(new BinaryOpNode(node.BinaryOp, node.Left, node.Right)));
                default:
                    throw new NotImplementedException("Unknown binary operation: " + node.Left + " " + node.BinaryOp + " " +node.Right);
            }
        }

        private List<Dictionary<string, InternalFunction>> getFunctions(string path = "")
        {
            List<Dictionary<string, InternalFunction>> result = new List<Dictionary<string, InternalFunction>>();
            Assembly testAss;

            if (path != "")
                testAss = Assembly.LoadFrom(path);
            else
                testAss = Assembly.GetExecutingAssembly();

            foreach (Type type in testAss.GetTypes())
                if (type.GetInterface(typeof(ILibrary).FullName) != null)
                {
                    ILibrary ilib = (ILibrary)Activator.CreateInstance(type);
                    result.Add(ilib.GetFunctions());
                }

            return result;
        }
    }
}
