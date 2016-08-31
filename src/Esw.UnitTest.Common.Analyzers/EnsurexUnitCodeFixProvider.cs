namespace Esw.UnitTest.Common.Analyzers
{
    using System.Collections.Immutable;
    using System.Composition;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeActions;
    using Microsoft.CodeAnalysis.CodeFixes;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(EnsurexUnitCodeFixProvider)), Shared]
    public class EnsurexUnitCodeFixProvider : CodeFixProvider
    {
        private const string Title = "Use xUnit";

        public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(EnsurexUnitAnalyzer.DiagnosticId);

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            var declaration = root.FindNode(diagnosticSpan);

            context.RegisterCodeFix(CodeAction.Create(Title, c => ReplaceTestClassWithFact(context.Document, declaration, c), Title), diagnostic);
        }

        private static async Task<Document> ReplaceTestClassWithFact(Document document, SyntaxNode node, CancellationToken cancellationToken)
        {
            var rewriter = new WrongTestFrameworkRewriter();
            var newMethod = rewriter.Visit(node);

            var docRoot = await document.GetSyntaxRootAsync(cancellationToken);

            return document.WithSyntaxRoot(docRoot.ReplaceNode(node, newMethod));
        }
    }
    public class WrongTestFrameworkRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var msTestList = node.AttributeLists.Where(l => l.Attributes.Any(a => a.Name.ToString() == "TestClass")); // TODO: SWAP TO CLIB INSTEAD OF PCL AND MOVE TO NAMEOF!
            var resultList = new SyntaxList<AttributeListSyntax>();

            foreach (var list in msTestList)
            {
                var aList = new SeparatedSyntaxList<AttributeSyntax>();
                aList.AddRange(list.Attributes.Where(a => a.Name.ToString() == "TestClass")); // TODO: SWAP TO CLIB INSTEAD OF PCL AND MOVE TO NAMEOF!
                resultList.Add(list.WithAttributes(aList).WithTriviaFrom(list));
            }

            return node.WithAttributeLists(resultList).WithTriviaFrom(node);
        }
    }
}