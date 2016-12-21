namespace DevOpsFlex.Tests.Core.Analyzers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class IsTestVisitor : CSharpSyntaxVisitor<bool>
    {
        private readonly IEnumerable<string> _isTestAttributes =
            new[]
            {
                "IsUnit",
                "IsUnitAttribute",
                "IsIntegration",
                "IsIntegrationAttribute",
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
                "IsProfilerMemoryAttribute"
            };

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