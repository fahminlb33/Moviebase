using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Moviebase.Tests
{
    [TestClass]
    public class PowerPathFileTests
    {
        private const string TestFilePath = "E:\\untitled.flp";
        private const string TestDirPath = "E:\\Setups\\NES";
        private const string TestDirRenamedPath = "E:\\Setups\\test";

        [TestMethod]
        public void TestFilePathCreation()
        {
            var path = new PowerPath(TestFilePath);
            Assert.AreEqual(TestFilePath, path);
        }

        [TestMethod]
        public void TestFileExist()
        {
            var path = new PowerPath(TestFilePath);
            Assert.IsTrue(path.IsFile);
        }

        [TestMethod]
        public void TestPatternRename()
        {
            string org = "E:\\coba1\\coba.html";

            var fullPathObj = new PowerPath(org);
            var values = ValueObject.CreateMock();
            var result = fullPathObj.RenameFileByPattern("{Name}{Value}", values);

            Assert.AreEqual("Fahmit.html", result.GetFileName());
        }
    }
}
