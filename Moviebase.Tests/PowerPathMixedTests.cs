using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Moviebase.Tests
{
    [TestClass]
    public class PowerPathMixedTests
    {
        [TestMethod]
        public void TestSwapFileName()
        {
            var name = new PowerPath("E:\\fahmi\\The Fah.jpg");
            name.SwapFileName("The");

            Assert.AreEqual("E:\\fahmi\\Fah, The.jpg", name);
        }

        [TestMethod]
        public void TestSwapDirName()
        {
            var name = new PowerPath("E:\\Nya fahmi\\The Fah.jpg");
            name.SwapLastDirectoryName("Nya");

            Assert.AreEqual("E:\\fahmi, Nya\\The Fah.jpg", name);
        }

        [TestMethod]
        public void TestMultiRename()
        {
            string org = "E:\\[Inbox]\\1.png";

            var fullPathObj = new PowerPath(org);
            fullPathObj.RenameFile("DD").RenameLastDirectory("DD");

            Assert.AreEqual("E:\\DD\\DD.png", fullPathObj);
        }

        [TestMethod]
        public void TestDirDetermination()
        {
            var isFile = !string.IsNullOrWhiteSpace(Path.GetExtension("C:"));
            Debug.Print(isFile.ToString());

            isFile = !string.IsNullOrWhiteSpace(Path.GetExtension("C:\\"));
            Debug.Print(isFile.ToString());

            isFile = !string.IsNullOrWhiteSpace(Path.GetExtension("C:\\ff"));
            Debug.Print(isFile.ToString());

            isFile = !string.IsNullOrWhiteSpace(Path.GetExtension("C:\\fff.jpeg"));
            Debug.Print(isFile.ToString());

            Assert.IsTrue(isFile);
        }


    }
}
