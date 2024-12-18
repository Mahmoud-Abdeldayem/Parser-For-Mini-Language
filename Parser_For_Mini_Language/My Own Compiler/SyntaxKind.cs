namespace My_Own_Compiler
{
    public enum SyntaxKind
    {
        NumberToken,
        WhiteSpace,
        PluseToken,
        DivisionToken,
        MultiplicationToken,
        MinusToken,
        OpenParenthesisToken,
        ClosedParenthesisToken,
        BadToken,
        EndOfFile,
        NumberExpression,
        BinaryExpression,
        UnaryExpression
    }
}