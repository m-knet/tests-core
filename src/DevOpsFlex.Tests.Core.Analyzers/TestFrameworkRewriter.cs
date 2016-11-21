namespace DevOpsFlex.Tests.Core.Analyzers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class TestFrameworkRewriter : CSharpSyntaxRewriter
    {
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