namespace Eshopworld.Tests.Core.Analyzers
{
    using System.Linq;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Represents a <see cref="bool" /> visitor that visits only the single <see cref="MethodDeclarationSyntax"/> node.
    /// Returns true if the method is decorated with MSTest attributes, false otherwise.
    /// </summary>
    public class TestFrameworkVisitor : CSharpSyntaxVisitor<bool>
    {
        /// <summary>
        /// Called when the visitor visits a <see cref="MethodDeclarationSyntax"/> node.
        /// </summary>
        public override bool VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            return node.AttributeLists.SelectMany(l => l.Attributes).Any(a => a.Name.ToString() == "TestMethod");
        }
    }
}