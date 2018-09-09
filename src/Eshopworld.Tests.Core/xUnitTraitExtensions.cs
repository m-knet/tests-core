namespace Eshopworld.Tests.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    /// <summary>
    /// Decorates a test as a Unit Test, so that it runs in Continuous Integration builds.
    /// </summary>
    public sealed class IsUnitAttribute : EswCategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IsUnitAttribute"/>
        /// </summary>
        public IsUnitAttribute() : base("Unit") { }
    }

    /// <summary>
    /// Decorates a test as an Integration Test, so that it runs in Continuous Integration builds.
    /// </summary>
    public sealed class IsIntegrationAttribute : EswCategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IsIntegrationAttribute"/>
        /// </summary>
        public IsIntegrationAttribute() : base("Integration") { }
    }

    /// <summary>
    /// Decorates a test as a Read Only Integration Test, so that it runs in Continuous Integration builds and can also run in Production.
    /// </summary>
    public sealed class IsIntegrationReadOnlyAttribute : EswCategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IsIntegrationReadOnlyAttribute"/>
        /// </summary>
        public IsIntegrationReadOnlyAttribute() : base("Integration", "ReadOnly") { }
    }

    /// <summary>
    /// Decorates a test as a WarmUp Test, so that it runs in Continuous Integration builds ahead of Integration tests
    ///     to warm up the deployment and kick off things like EF migrations.
    /// </summary>
    public sealed class IsWarmUpAttribute : EswCategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IsWarmUpAttribute"/>
        /// </summary>
        public IsWarmUpAttribute() : base("WarmUp") { }
    }

    /// <summary>
    /// Decorates a test as a Coded UI Test, so that it runs in Continuous Integration builds as a Coded UI test.
    /// </summary>
    public sealed class IsAutomatedUiAttribute : EswCategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IsAutomatedUiAttribute"/>
        /// </summary>
        public IsAutomatedUiAttribute() : base("AutomatedUi") { }
    }

    /// <summary>
    /// Decorates a test as a Unit Test and as requiring Fakes to be present, so that it runs in Continuous Integration builds.
    /// </summary>
    public sealed class IsFakesAttribute : EswCategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IsFakesAttribute"/>
        /// </summary>
        public IsFakesAttribute() : base("Unit", "Fakes") { }
    }

    /// <summary>
    /// Decorates a test as a Unit Test and as requiring Code Contracts to be present, so that it runs in Continuous Integration builds.
    /// </summary>
    public sealed class IsCodeContractAttribute : EswCategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IsCodeContractAttribute"/>
        /// </summary>
        public IsCodeContractAttribute() : base("Unit", "CodeContract") { }
    }

    /// <summary>
    /// Decorates a test as a Unit Test and as requiring Roslyn to be present, so that it runs in Continuous Integration builds.
    /// </summary>
    public sealed class IsRoslynAttribute : EswCategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IsRoslynAttribute"/>
        /// </summary>
        public IsRoslynAttribute() : base("Unit", "Roslyn") { }
    }

    /// <summary>
    /// Decorates a test as a development entry point.
    /// This test never runs on any automated build, is purely a development facility.
    /// </summary>
    public sealed class IsDevAttribute : EswCategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IsDevAttribute"/>
        /// </summary>
        public IsDevAttribute() : base("Dev") { }
    }

    /// <summary>
    /// Decorates a test as profiler entry point for profiling CPU.
    /// This test never runs on any automated build, is purely a development facility.
    /// </summary>
    public sealed class IsProfilerCpuAttribute : EswCategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IsProfilerCpuAttribute"/>
        /// </summary>
        public IsProfilerCpuAttribute() : base("Profiler CPU") { }
    }

    /// <summary>
    /// Decorates a test as profiler entry point for profiling memory.
    /// This test never runs on any automated build, is purely a development facility.
    /// </summary>
    public sealed class IsProfilerMemoryAttribute : EswCategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IsProfilerMemoryAttribute"/>
        /// </summary>
        public IsProfilerMemoryAttribute() : base("Profiler Memory") { }
    }

    /// <summary>
    /// decorates a test as L0 test
    /// this was added to facilitate lowest level of L(x) test categorization, logically equivalent to basic unit test
    /// </summary>
    public sealed class IsLayer0Attribute : EswCategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IsProfilerMemoryAttribute"/>
        /// </summary>
        public IsLayer0Attribute() : base("Layer0") { }
    }

    /// <summary>
    /// L1 test aimed at being executed during build stage - effectively CI attribute
    /// </summary>
    public sealed class IsLayer1Attribute : EswCategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IsProfilerMemoryAttribute"/>
        /// </summary>
        public IsLayer1Attribute() : base("Layer1") { }
    }

    /// <summary>
    /// L1 test aimed at being executed during deploy stage - effectively CD attribute
    /// </summary>
    public sealed class IsLayer2Attribute : EswCategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IsProfilerMemoryAttribute"/>
        /// </summary>
        public IsLayer2Attribute() : base("Layer2") { }
    }

    /// <summary>
    /// Base class for all the category model attributes. Contains a list of all the attribute categories
    /// and supports multiple categories.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    [TraitDiscoverer(EswCategoryDiscoverer.FullyQualifiedName, EswCategoryDiscoverer.Namespace)]
    public abstract class EswCategoryAttribute : Attribute, ITraitAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EswCategoryAttribute"/>.
        /// </summary>
        /// <param name="categories">The trait categories present in the attribute.</param>
        protected EswCategoryAttribute(params string[] categories)
        {
            Categories = categories.ToList();
        }

        /// <summary>
        /// Gets the list of categories in the test that the attribute is decorating.
        /// Exposed as public because xUnit needs to reflect the getter to get to it.
        /// </summary>
        public IEnumerable<string> Categories { get; }
    }

    /// <summary>
    /// Discoverer that provides eShopWorld trait values to xUnit.net tests.
    /// </summary>
    public class EswCategoryDiscoverer : ITraitDiscoverer
    {
        internal const string Namespace = nameof(Eshopworld) + "." + nameof(Tests) + "." + nameof(Core);
        internal const string FullyQualifiedName = Namespace + "." + nameof(EswCategoryDiscoverer);

        /// <summary>Gets the trait values from the trait attribute.</summary>
        /// <param name="traitAttribute">The trait attribute containing the trait values.</param>
        /// <returns>The trait values.</returns>
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            var categories = ((traitAttribute as ReflectionAttributeInfo)?.Attribute as EswCategoryAttribute)?.Categories;
            return categories?.Select(c => new KeyValuePair<string, string>("Category", c)) ?? new[] { new KeyValuePair<string, string>("Category", "ERROR") };
        }
    }
}