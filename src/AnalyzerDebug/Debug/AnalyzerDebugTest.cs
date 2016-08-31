namespace Debug
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
