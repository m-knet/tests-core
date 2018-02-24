namespace Eshopworld.Tests.Core.Analyzers
{
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Provides a Re-Writer to add an Is* decorator to a <see cref="MethodDeclarationSyntax"/> node.
    /// </summary>
    public class IsTestRewriter : CSharpSyntaxRewriter
    {
        private readonly string _attribute;

        /// <summary>
        /// Initializes a new instace of <see cref="IsTestRewriter"/>.
        /// </summary>
        /// <param name="attribute">The name of the Is* decorator attribute that we want to create the re-writer for.</param>
        public IsTestRewriter(string attribute)
        {
            _attribute = attribute;
        }

        /// <summary>
        /// Visits a <see cref="MethodDeclarationSyntax"/> node, clones it and adds the chosen Is* decorator.
        /// </summary>
        /// <param name="node">The <see cref="MethodDeclarationSyntax"/> node that we are visiting.</param>
        /// <returns>The new node with the Is* decorator attribute</returns>
        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var factList = node.AttributeLists.First(l => l.Attributes.Any(a => a.Name.ToString() == "Fact"));
            factList = factList.AddAttributes(SyntaxFactory.Attribute(SyntaxFactory.ParseName(_attribute)));

            return node.WithAttributeLists(SyntaxFactory.List(new[] {factList})).WithTriviaFrom(node);
        }
    }
}