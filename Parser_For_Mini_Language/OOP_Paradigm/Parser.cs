
using OOP_Paradigm;
using System;
using System.Collections.Generic;

namespace Parser_For_Mini_Language.OOP_Paradigm
{
    public class Parser
    {
        private int Position { get; set; }
        private List<SyntaxToken> tokens = new List<SyntaxToken>();
        private Dictionary<string, int> variables = new Dictionary<string, int>(); // Symbol table for variables


        public Parser(string text)
        {
            var lexer = new Lexer(text);
            SyntaxToken token;
            do
            {
                token = lexer.NextToken();
                if (token.SyntaxKind != SyntaxKind.WhiteSpace && token.SyntaxKind != SyntaxKind.BadToken)
                    tokens.Add(token);
            } while (token.SyntaxKind != SyntaxKind.EndOfFile);
        }

        private SyntaxToken Peek(int offset)
        {
            var index = Position + offset;
            if (index >= tokens.Count)
                return tokens[tokens.Count - 1];

            return tokens[index];
        }

        private SyntaxToken Current => Peek(0);

        public BaseExpressionSyntax Parse()
        {
            return ParseExpression();
        }

        private BaseExpressionSyntax ParseExpression(int parentPrecedence = 0)
        {
            var left = ParsePrimaryExpression();

            while (true)
            {
                var precedence = GetOperatorPrecedence(Current);
                if (precedence == 0 || precedence <= parentPrecedence)
                    break;

                var operatorToken = Current;
                Position++;

                var right = ParseExpression(precedence);
                left = new BinarySyntaxExpression(left, operatorToken, right);
            }

            return left;
        }

        private BaseExpressionSyntax ParsePrimaryExpression()
        {

            if (Current.SyntaxKind == SyntaxKind.IdentifierToken) // Handle variable usage
            {
                var identifier = Current;
                Position++;

                if (Current.SyntaxKind == SyntaxKind.EqualsToken) // Handle assignment
                {
                    Position++; // Skip '='
                    var right = ParseExpression(); // Parse the right-hand side expression

                    var variableToken = new SyntaxToken(identifier.Position, identifier.Text, null, SyntaxKind.IdentifierToken);

                    return new AssignmentSyntaxExpression(variableToken, right);
                }

                // Handle identifier as a variable reference
                if (variables.ContainsKey(identifier.Text))
                    return new NumberSyntaxExpression(new SyntaxToken(0, variables[identifier.Text].ToString(), variables[identifier.Text], SyntaxKind.NumberToken));

                throw new Exception($"Undefined variable: {identifier.Text}");
            }


            // Handle Unary Operators (e.g., -5 or +3)
            if (Current.SyntaxKind == SyntaxKind.MinusToken || Current.SyntaxKind == SyntaxKind.PluseToken)
            {
                var operatorToken = Current;
                Position++; // Move past the unary operator
                var operand = ParsePrimaryExpression(); // Parse the next expression
                return new UnarySyntaxExpression(operatorToken, operand); // Return a unary expression
            }

            // Handle Number Tokens (e.g., 3, 5)
            if (Current.SyntaxKind == SyntaxKind.NumberToken)
            {
                var numberToken = Current;
                Position++; // Move past the number token
                return new NumberSyntaxExpression(numberToken); // Return a number expression
            }

            // Handle Parenthesized Expressions (e.g., (1 + 2))
            if (Current.SyntaxKind == SyntaxKind.OpenParenthesisToken)
            {
                Position++; // Move past '('
                var expression = ParseExpression(); // Parse the expression inside the parentheses
                if (Current.SyntaxKind != SyntaxKind.ClosedParenthesisToken)
                    throw new Exception("Expected ')'");
                Position++; // Move past ')'
                return expression;
            }

            throw new Exception($"Unexpected token: {Current.Text}");
        }

        private int GetOperatorPrecedence(SyntaxToken token)
        {
            return token.SyntaxKind switch
            {
                SyntaxKind.PluseToken => 1,
                SyntaxKind.MinusToken => 1,
                SyntaxKind.MultiplicationToken => 2,
                SyntaxKind.DivisionToken => 2,
                _ => 0
            };
        }
    }

    // Expression base class and derived classes for expressions
    public abstract class BaseExpressionSyntax : SyntaxNode
    {
    }

    // Represents a number token in an expression
    sealed class NumberSyntaxExpression : BaseExpressionSyntax
    {
        public NumberSyntaxExpression(SyntaxToken numberToken)
        {
            NumberToken = numberToken;
        }
        public override SyntaxKind Kind => SyntaxKind.NumberExpression;
        public SyntaxToken NumberToken { get; }
    }

    // Represents a binary operation expression (e.g., 1 + 2, 3 * 4)
    public class BinarySyntaxExpression : BaseExpressionSyntax
    {
        public BinarySyntaxExpression(BaseExpressionSyntax left, SyntaxToken operatorToken, BaseExpressionSyntax right)
        {
            Left = left;
            OperatorToken = operatorToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;
        public BaseExpressionSyntax Left { get; }
        public SyntaxToken OperatorToken { get; }  // Change this to SyntaxToken
        public BaseExpressionSyntax Right { get; }
    }


    // Represents a unary operation expression (e.g., -5, +3)
    sealed class UnarySyntaxExpression : BaseExpressionSyntax
    {
        public UnarySyntaxExpression(SyntaxToken operatorToken, BaseExpressionSyntax operand)
        {
            OperatorToken = operatorToken;
            Operand = operand;
        }

        public override SyntaxKind Kind => SyntaxKind.UnaryExpression;
        public SyntaxToken OperatorToken { get; }
        public BaseExpressionSyntax Operand { get; }
    }
}
