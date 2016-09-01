namespace Esw.UnitTest.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    /// <summary>
    /// Decorates a test as an Unit Test, so that it runs in Continuous Integration builds.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class IsUnitAttribute : EswCategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IsUnitAttribute"/>
        /// </summary>
        public IsUnitAttribute() : base("Unit")
        {
        }
    }

    /// <summary>
    /// Decorates a test as an Integration Test, so that it runs in Continuous Integration builds.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class IsIntegrationAttribute : EswCategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IsIntegrationAttribute"/>
        /// </summary>
        public IsIntegrationAttribute() : base("Integration")
        {
        }
    }

    /// <summary>
    /// Decorates a test as an Unit Test and as requiring Fakes to be present, so that it runs in Continuous Integration builds.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class IsFakesAttribute : EswCategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IsFakesAttribute"/>
        /// </summary>
        public IsFakesAttribute() : base("Unit", "Fakes")
        {
        }
    }

    /// <summary>
    /// Decorates a test as an Unit Test and as requiring Code Contracts to be present, so that it runs in Continuous Integration builds.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class IsCodeContractAttribute : EswCategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IsCodeContractAttribute"/>
        /// </summary>
        public IsCodeContractAttribute() : base("Unit", "CodeContract")
        {
        }
    }

    /// <summary>
    /// Decorates a test as an Unit Test and as requiring Roslyn to be present, so that it runs in Continuous Integration builds.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class IsRoslynAttribute : EswCategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IsRoslynAttribute"/>
        /// </summary>
        public IsRoslynAttribute() : base("Unit", "Roslyn")
        {
        }
    }

    /// <summary>
    /// Base class for all the category model attributes. Contains a list of all the attribute categories
    /// and supports multiple categories.
    /// </summary>
    [TraitDiscoverer(EswCategoryDiscoverer.FullyQualifiedName, EswCategoryDiscoverer.Namespace)]
    public abstract class EswCategoryAttribute : Attribute, ITraitAttribute
    {
        protected EswCategoryAttribute(params string[] categories)
        {
            Categories = categories;
        }

        /// <summary>
        /// Gets the list of categores in the test that the attribute is decorating.
        /// Exposed as public because xUnit needs to reflect the getter to get to it.
        /// </summary>
        public IEnumerable<string> Categories { get; }
    }

    /// <summary>
    /// Discoverer that provides eShopWorld trait values to xUnit.net v2 tests.
    /// </summary>
    public class EswCategoryDiscoverer : ITraitDiscoverer
    {
        internal const string Namespace = nameof(Esw) + "." + nameof(UnitTest) + "." + nameof(Common);

        internal const string FullyQualifiedName = Namespace + "." + nameof(EswCategoryDiscoverer);

        /// <summary>Gets the trait values from the trait attribute.</summary>
        /// <param name="traitAttribute">The trait attribute containing the trait values.</param>
        /// <returns>The trait values.</returns>
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            var categories = traitAttribute.GetNamedArgument<IEnumerable<string>>(nameof(EswCategoryAttribute.Categories));
            return categories.Select(c => new KeyValuePair<string, string>("Category", c));
        }
    }
}