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

    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(EnsureIsTestCodeFixProvider)), Shared]
    public class EnsureIsTestCodeFixProvider : CodeFixProvider
    {
        private const string UnitTitle = "This is a Unit Test";
        private const string IntegrationTitle = "This is an Integration Test";
        private const string EswTestNamespace = "Esw.UnitTest.Common";

        public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(EnsureIsTestAnalyzer.DiagnosticId);

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

            context.RegisterCodeFix(CodeAction.Create(UnitTitle, c => AddIsTestAttribute(context.Document, declaration, "IsUnit", c), UnitTitle), diagnostic);
            context.RegisterCodeFix(CodeAction.Create(IntegrationTitle, c => AddIsTestAttribute(context.Document, declaration, "IsIntegration", c), IntegrationTitle), diagnostic);
        }

        private static async Task<Document> AddIsTestAttribute(Document document, SyntaxNode node, string attribute, CancellationToken cancellationToken)
        {
            var rewriter = new IsTestRewriter(attribute);
            var newMethod = rewriter.Visit(node);

            var docRoot = await document.GetSyntaxRootAsync(cancellationToken);
            docRoot = docRoot.ReplaceNode(node, newMethod);

            var compilation = docRoot as CompilationUnitSyntax;

            if (compilation == null) return document.WithSyntaxRoot(docRoot);

            if (compilation.Usings.All(u => u.Name.ToString() != EswTestNamespace))
            {
                docRoot = compilation.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName(EswTestNamespace)));
            }

            return document.WithSyntaxRoot(docRoot);
        }
    }
}