using Parser_For_Mini_Language.OOP_Paradigm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Paradigm
{
    public class AssignmentSyntaxExpression : BaseExpressionSyntax
    {
        public AssignmentSyntaxExpression(SyntaxToken variableToken, BaseExpressionSyntax right)
        {
            VariableToken = variableToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.AssignmentExpression;
        public SyntaxToken VariableToken { get; }
        public BaseExpressionSyntax Right { get; }
    }

}
