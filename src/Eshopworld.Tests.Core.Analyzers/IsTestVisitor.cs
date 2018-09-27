namespace Eshopworld.Tests.Core.Analyzers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Represents a <see cref="bool" /> visitor that visits only the single <see cref="MethodDeclarationSyntax"/> node.
    /// Returns true if the method is an xUnit test method and isn't decorated with an Is* decorator, false otherwise.
    /// </summary>
    public class IsTestVisitor : CSharpSyntaxVisitor<bool>
    {
        private readonly IEnumerable<string> _isTestAttributes =
            new[]
            {
                "IsUnit",
                "IsUnitAttribute",
                "IsIntegration",
                "IsIntegrationAttribute",
                "IsIntegrationReadOnly",
                "IsIntegrationReadOnlyAttribute",
                "IsFakes",
                "IsFakesAttribute",
                "IsCodeContract",
                "IsCodeContractAttribute",
                "IsRoslyn",
                "IsRoslynAttribute",
                "IsDev",
                "IsDevAttribute",
                "IsProfilerCpu",
                "IsProfilerCpuAttribute",
                "IsProfilerMemory",
                "IsProfilerMemoryAttribute",
                "IsLayer0Attribute",
                "IsLayer0",
                "IsLayer1Attribute",
                "IsLayer1",
                "IsLayer2Attribute",
                "IsLayer2",
                "IsLayer3Attribute",
                "IsLayer3"
            };

        /// <summary>
        /// Called when the visitor visits a <see cref="MethodDeclarationSyntax"/> node.
        /// </summary>
        public override bool VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node.AttributeLists.SelectMany(l => l.Attributes).Any(a => a.Name.ToString() == "Fact" || a.Name.ToString() == "Theory"))
            {
                return !node.AttributeLists.SelectMany(l => l.Attributes).Any(a => _isTestAttributes.Contains(a.Name.ToString()));
            }

            return false;
        }
    }
}