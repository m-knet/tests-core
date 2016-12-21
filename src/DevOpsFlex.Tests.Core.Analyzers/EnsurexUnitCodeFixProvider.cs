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

    /// <summary>
    /// Provides a provider to change from MSTest to xUnit as the test framework.
    /// This is the <see cref="CodeFixProvider"/> for the <see cref="EnsureTestFrameworkAnalyzer"/>.
    /// </summary>
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(EnsurexUnitCodeFixProvider)), Shared]
    public class EnsurexUnitCodeFixProvider : CodeFixProvider
    {
        private const string Title = "Use xUnit";
        private const string XunitNamespace = "Xunit";

        /// <summary>
        /// A list of diagnostic IDs that this provider can provider fixes for.
        /// </summary>
        public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(EnsureTestFrameworkAnalyzer.DiagnosticId);

        /// <summary>
        /// Gets an optional <see cref="T:Microsoft.CodeAnalysis.CodeFixes.FixAllProvider" /> that can fix all/multiple occurrences of diagnostics fixed by this code fix provider.
        /// Return null if the provider doesn't support fix all/multiple occurrences.
        /// Otherwise, you can return any of the well known fix all providers from <see cref="T:Microsoft.CodeAnalysis.CodeFixes.WellKnownFixAllProviders" /> or implement your own fix all provider.
        /// </summary>
        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        /// <summary>
        /// Computes one or more fixes for the specified <see cref="T:Microsoft.CodeAnalysis.CodeFixes.CodeFixContext" />.
        /// </summary>
        /// <param name="context">
        /// A <see cref="T:Microsoft.CodeAnalysis.CodeFixes.CodeFixContext" /> containing context information about the diagnostics to fix.
        /// The context must only contain diagnostics with an <see cref="P:Microsoft.CodeAnalysis.Diagnostic.Id" /> included in the <see cref="P:Microsoft.CodeAnalysis.CodeFixes.CodeFixProvider.FixableDiagnosticIds" /> for the current provider.
        /// </param>
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