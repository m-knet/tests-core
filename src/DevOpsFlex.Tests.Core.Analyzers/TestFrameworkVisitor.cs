namespace DevOpsFlex.Tests.Core.Analyzers
{
    using System.Linq;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class TestFrameworkVisitor : CSharpSyntaxVisitor<bool>
    {
        public override bool VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            return node.AttributeLists.SelectMany(l => l.Attributes).Any(a => a.Name.ToString() == "TestMethod");
        }
    }
}