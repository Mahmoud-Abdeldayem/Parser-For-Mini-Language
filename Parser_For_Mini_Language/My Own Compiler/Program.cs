namespace My_Own_Compiler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Go> ");

                var text = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(text))
                {
                    return;
                }

                // Step 1: Tokenize input using the lexer
                var lexer = new Lexer(text);
                var tokens = new List<SyntaxToken>();
                while (true)
                {
                    var token = lexer.NextToken();
                    if (token.SyntaxKind == SyntaxKind.EndOfFile)
                    {
                        break;
                    }

                    tokens.Add(token);
                    Console.Write($"{token.SyntaxKind} : {token.Text} : ");
                    if (token.Value != null)
                        Console.Write($" ' {token.Value} '");

                    Console.WriteLine();
                }

                // Step 2: Pass the tokens to the parser to generate the abstract syntax tree (AST)
                var parser = new Parser(text); // Create the parser and pass the input text
                var syntaxTree = parser.Parse(); // Parse the expression

                // Step 3: Display the result of the parsing (AST)
                Console.WriteLine("\nAbstract Syntax Tree (AST):");
                PrintSyntaxTree(syntaxTree, 0);

                // Step 4: Evaluate the expression represented by the AST
                Console.WriteLine("\nEvaluating the expression:");
                var result = EvaluateExpression(syntaxTree);
                Console.WriteLine($"Result: {result}");
            }
        }

        // Modified method to print the syntax tree in a tree-like structure
        static void PrintSyntaxTree(BaseExpressionSyntax syntaxNode, int indent)
        {
            if (syntaxNode is NumberSyntaxExpression number)
            {
                Console.WriteLine(new string(' ', indent) + $"{number.NumberToken.Text}");
            }
            else if (syntaxNode is BinarySyntaxExpression binary)
            {
                Console.WriteLine(new string(' ', indent) + binary.OperatorToken.Text);
                PrintSyntaxTree(binary.Left, indent + 2); // Print the left operand
                PrintSyntaxTree(binary.Right, indent + 2); // Print the right operand
            }
            else if (syntaxNode is UnarySyntaxExpression unary)
            {
                Console.WriteLine(new string(' ', indent) + unary.OperatorToken.Text);
                PrintSyntaxTree(unary.Operand, indent + 2); // Print the operand
            }
            else
            {
                Console.WriteLine(new string(' ', indent) + "Unknown node");
            }
        }

        // Method to evaluate the expression based on the AST
        static int EvaluateExpression(BaseExpressionSyntax syntaxNode)
        {
            if (syntaxNode is NumberSyntaxExpression number)
            {
                // If it's a number node, return its value
                return (int)number.NumberToken.Value;
            }
            else if (syntaxNode is BinarySyntaxExpression binary)
            {
                // If it's a binary expression, evaluate left and right operands
                var leftValue = EvaluateExpression(binary.Left);
                var rightValue = EvaluateExpression(binary.Right);

                // Apply the operator
                return binary.OperatorToken.SyntaxKind switch
                {
                    SyntaxKind.PluseToken => leftValue + rightValue,
                    SyntaxKind.MinusToken => leftValue - rightValue,
                    SyntaxKind.MultiplicationToken => leftValue * rightValue,
                    SyntaxKind.DivisionToken => leftValue / rightValue,
                    _ => throw new Exception($"Unexpected operator: {binary.OperatorToken.SyntaxKind}")
                };
            }
            else if (syntaxNode is UnarySyntaxExpression unary)
            {
                // If it's a unary expression, evaluate the operand
                var operandValue = EvaluateExpression(unary.Operand);

                // Apply the unary operator
                return unary.OperatorToken.SyntaxKind switch
                {
                    SyntaxKind.PluseToken => operandValue, // + is a no-op for positive numbers
                    SyntaxKind.MinusToken => -operandValue,
                    _ => throw new Exception($"Unexpected unary operator: {unary.OperatorToken.SyntaxKind}")
                };
            }
            else
            {
                throw new Exception("Unknown expression type");
            }
        }
    }
}
