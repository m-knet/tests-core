namespace DevOpsFlex.Tests.Core.Analyzers
{
    using System.Collections.Immutable;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Diagnostics;

    /// <summary>
    /// Analyzes that there are no tests using MSTest as the test framework.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class EnsureTestFrameworkAnalyzer : DiagnosticAnalyzer
    {
        /// <summary>
        /// The ID of this diagnostic analyzer.
        /// </summary>
        public const string DiagnosticId = "ESWU001";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.TestFrameworkTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.TestFrameworkMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.TestFrameworkDescription), Resources.ResourceManager, typeof(Resources));

        private const bool EnabledByDefault = true;
        private const string Category = "Frameworks";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, EnabledByDefault, Description);

        /// <summary>
        /// Returns a set of descriptors for the diagnostics that this analyzer is capable of producing.
        /// </summary>
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        /// <summary>
        /// Called once at session start to register actions in the analysis context.
        /// </summary>
        /// <param name="context">The <see cref="AnalysisContext"/> that this analyzer starts from.</param>
        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(EnsureXUnit, SyntaxKind.MethodDeclaration);
        }

        private static void EnsureXUnit(SyntaxNodeAnalysisContext context)
        {
            var visitor = new TestFrameworkVisitor();

            if (visitor.Visit(context.Node))
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation(), ((MethodDeclarationSyntax)context.Node).Identifier.Text));
            }
        }
    }
}
