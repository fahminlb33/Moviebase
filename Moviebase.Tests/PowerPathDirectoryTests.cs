using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Moviebase.Tests
{
    [TestClass]
    public class PowerPathDirectoryTests
    {
        private const string TestFilePath = "E:\\untitled.flp";
        private const string TestDirPath = "E:\\Setups\\NES";
        private const string TestDirPathUpOneLevel = "E:\\Setups";
        private const string TestDirRenamedPath = "E:\\Setups\\" + TestDirName;
        private const string TestDirName = "test";

        [TestMethod]
        public void TestPathCreation()
        {
            var path = new PowerPath(TestDirPath);
            Assert.AreEqual(TestDirPath, path);
        }

        [TestMethod]
        public void TestRenamePath()
        {
            var path = new PowerPath(TestDirPath);
            path.RenameLastDirectory(TestDirName);
            Assert.AreEqual(TestDirRenamedPath, path);
        }
        
        [TestMethod]
        public void TestIsDirectoryFromPath()
        {
            var path = new PowerPath(TestDirPath);
            Assert.IsTrue(path.IsDirectory);
        }

        [TestMethod]
        public void TestIsDirectoryFromFilePath()
        {
            var path = new PowerPath(TestFilePath);
            Assert.IsFalse(path.IsDirectory);
        }

        [TestMethod]
        public void TestDirUp()
        {
            var path = new PowerPath(TestDirPath).UpOneLevel();
            Assert.AreEqual(TestDirPathUpOneLevel, path);
        }
    }
}
