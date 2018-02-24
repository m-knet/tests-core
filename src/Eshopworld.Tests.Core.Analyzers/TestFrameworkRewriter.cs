namespace Eshopworld.Tests.Core.Analyzers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Provides a Re-Writer to replace the MSTest <see cref="System.Attribute"/> with the xUnit 'Fact' <see cref="System.Attribute"/> to a <see cref="MethodDeclarationSyntax"/> node.
    /// </summary>
    public class TestFrameworkRewriter : CSharpSyntaxRewriter
    {
        /// <summary>
        /// Visits a <see cref="MethodDeclarationSyntax"/> node, clones it and replaces the MSTest <see cref="System.Attribute"/> with the xUnit 'Fact' <see cref="System.Attribute"/>.
        /// </summary>
        /// <param name="node">The <see cref="MethodDeclarationSyntax"/> node that we are visiting.</param>
        /// <returns>The new node with the Is* decorator attribute</returns>
        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var msTestList = node.AttributeLists.Where(l => l.Attributes.Any(a => a.Name.ToString() == "TestMethod")); 
            var resultList = new List<AttributeListSyntax>();

            foreach (var list in msTestList)
            {
                var aList = new List<AttributeSyntax>();
                aList.AddRange(list.Attributes.Where(a => a.Name.ToString() != "TestMethod"));
                aList.Add(SyntaxFactory.Attribute(SyntaxFactory.ParseName("Fact")));

                resultList.Add(SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(aList)).WithTriviaFrom(list));
            }

            return node.WithAttributeLists(SyntaxFactory.List(resultList)).WithTriviaFrom(node);
        }
    }
}