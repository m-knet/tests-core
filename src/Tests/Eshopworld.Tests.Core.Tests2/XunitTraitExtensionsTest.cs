namespace Eshopworld.Tests.Core.Tests
{
    using Xunit;

    public class XunitTraitExtensionsTest
    {
        [Fact, IsUnit]
        public void Test_IsUnit() { }

        [Fact, IsIntegration]
        public void Test_IsIntegration() { }

        [Fact, IsFakes]
        public void Test_IsFakes() { }

        [Fact, IsCodeContract]
        public void Test_IsCodeContract() { }

        [Fact, IsRoslyn]
        public void Test_IsRoslyn() { }

        [Fact, IsDev]
        public void Test_IsIsDev() { }

        [Fact, IsProfilerCpu]
        public void Test_IsProfilerCpu() { }

        [Fact, IsProfilerMemory]
        public void Test_IsProfilerMemory() { }

        [Fact, IsLayer0]
        public void Test_IsLayer0() { }

        [Fact, IsLayer1]
        public void Test_IsLayer1() { }

        [Fact, IsLayer2]
        public void Test_IsLayer2() { }

        [Fact, Trait("Category", "Unit")]
        public void Foo() { }

        [Fact, FooTest]
        public void Foo2() { }
    }
}
