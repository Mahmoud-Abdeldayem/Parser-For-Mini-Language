using Parser_For_Mini_Language.OOP_Paradigm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Paradigm
{
    public class IfSyntaxExpression : BaseExpressionSyntax
    {
        public IfSyntaxExpression(BaseExpressionSyntax condition, BaseExpressionSyntax trueBranch, BaseExpressionSyntax falseBranch = null)
        {
            Condition = condition;
            TrueBranch = trueBranch;
            FalseBranch = falseBranch;
        }

        public override SyntaxKind Kind => SyntaxKind.IfExpression;
        public BaseExpressionSyntax Condition { get; }
        public BaseExpressionSyntax TrueBranch { get; }
        public BaseExpressionSyntax FalseBranch { get; }
    }

}
