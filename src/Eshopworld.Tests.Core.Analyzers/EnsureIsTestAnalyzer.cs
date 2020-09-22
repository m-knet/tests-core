namespace Eshopworld.Tests.Core.Analyzers
{
    using System.Collections.Immutable;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Diagnostics;

    /// <summary>
    /// Analyzes anything marked with 'Fact' or 'Theory' and checks for the presence of any Is* decorator.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class EnsureIsTestAnalyzer : DiagnosticAnalyzer
    {
        /// <summary>
        /// The ID of this diagnostic analyzer.
        /// </summary>
        public const string DiagnosticId = "ESWU002";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.IsTestTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.IsTestMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.IsTestDescription), Resources.ResourceManager, typeof(Resources));

        private const bool EnabledByDefault = true;
        private const string Category = "Frameworks";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, EnabledByDefault, Description);

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
            context.RegisterSyntaxNodeAction(EnsureIsTest, SyntaxKind.MethodDeclaration);
        }

        private static void EnsureIsTest(SyntaxNodeAnalysisContext context)
        {
            var visitor = new IsTestVisitor();

            if (visitor.Visit(context.Node))
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation(), ((MethodDeclarationSyntax)context.Node).Identifier.Text));
            }
        }
    }
}
