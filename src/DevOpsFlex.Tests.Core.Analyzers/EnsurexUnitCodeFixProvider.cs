namespace DevOpsFlex.Tests.Core.Analyzers
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

    // TODO: SWAP TO CLIB INSTEAD OF PCL AND MOVE TO NAMEOF!

    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(EnsurexUnitCodeFixProvider)), Shared]
    public class EnsurexUnitCodeFixProvider : CodeFixProvider
    {
        private const string Title = "Use xUnit";
        private const string XunitNamespace = "Xunit";

        public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(EnsureTestFrameworkAnalyzer.DiagnosticId);

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
            var rewriter = new TestFrameworkRewriter();
            var newMethod = rewriter.Visit(node);

            var docRoot = await document.GetSyntaxRootAsync(cancellationToken);
            docRoot = docRoot.ReplaceNode(node, newMethod);

            var compilation = docRoot as CompilationUnitSyntax;

            if (compilation == null) return document.WithSyntaxRoot(docRoot);

            if (compilation.Usings.All(u => u.Name.ToString() != XunitNamespace))
            {
                docRoot = compilation.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName(XunitNamespace)));
            }

            return document.WithSyntaxRoot(docRoot);
        }
    }
}