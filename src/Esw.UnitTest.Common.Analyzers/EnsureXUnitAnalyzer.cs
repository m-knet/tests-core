namespace Esw.UnitTest.Common.Analyzers
{
    using System.Collections.Immutable;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Diagnostics;

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class EnsurexUnitAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "ESWU001";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));

        private const bool EnabledByDefault = true;
        private const string Category = "Frameworks";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, EnabledByDefault, Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(EnsureXUnit, SyntaxKind.MethodDeclaration);
        }

        private static void EnsureXUnit(SyntaxNodeAnalysisContext context)
        {
            var visitor = new WrongTestFrameworkVisitor();

            if (visitor.Visit(context.Node))
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation(), ((MethodDeclarationSyntax)context.Node).Identifier.Text));
            }
        }
    }
}
