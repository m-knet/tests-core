namespace Esw.UnitTest.Common.Analyzers
{
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class IsTestRewriter : CSharpSyntaxRewriter
    {
        private readonly string _attribute;

        public IsTestRewriter(string attribute)
        {
            _attribute = attribute;
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var factList = node.AttributeLists.First(l => l.Attributes.Any(a => a.Name.ToString() == "Fact"));
            factList = factList.AddAttributes(SyntaxFactory.Attribute(SyntaxFactory.ParseName(_attribute)));

            return node.WithAttributeLists(SyntaxFactory.List(new[] {factList})).WithTriviaFrom(node);
        }
    }
}