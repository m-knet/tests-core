namespace Debug
{
    using Xunit;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DevOpsFlex.Tests.Core;

    [TestClass]
    public class AnalyzerDebugTest
    {
        [TestMethod]
        public void DebugMethod1()
        {
        }

        [TestMethod, TestCategory("Foo")] // WAZZA!
        public void DebugMethod2()
        {
        }

        [Fact, IsDev]
        public void Attribute_Test()
        {

        }
    }
}
