using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Test
{
    [TestClass]
    public class AppTest
    {
        [TestMethod]
        public void WhenReadFilesFromFolder_ThenFilesIsRead()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Test");
            string[] files = InputOutput.ReadFilesFromFolder(path);

            Assert.AreEqual(1, files.Length);
        }

        [TestMethod]
        public void WhenReadNonExistedFolder_ThenNoFileIsRead()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "abcde");
            string[] files = InputOutput.ReadFilesFromFolder(path);

            Assert.IsNull(files);
        }

        [TestMethod]
        public void WhenReadFileContent_ThenFileContentIsRead()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Test");
            string[] files = InputOutput.ReadFilesFromFolder(path);

            Assert.AreEqual(1, files.Length);

            var file = files[0];
            var result = InputOutput.ReadFileContent(file);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void WhenReadNonExistedFileContent_ThenNoFileIsRead()
        {
            var file = "c:\abc\abc.xml";
            var result = InputOutput.ReadFileContent(file);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void WhenGetCustomerInfoRequestFromXMLString_ThenReturnCustomerInfoRequestObjectAndImages()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Test");
            string[] files = InputOutput.ReadFilesFromFolder(path);

            Assert.AreEqual(1, files.Length);

            var file = files[0];
            var xmlString = InputOutput.ReadFileContent(file);

            Assert.IsNotNull(xmlString);
            
            CustomerInfoRequest obj = Serializer.GetCustomerInfoRequest(xmlString);

            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.MatchedImage);
            Assert.IsNotNull(obj.capturedImage);
        }

        [TestMethod]
        public void WhenGetCustomerInfoRequestFromInvalidXMLString_ThenReturnNullObject()
        {
            string xmlString = "<test></test>";
            CustomerInfoRequest obj = Serializer.GetCustomerInfoRequest(xmlString);

            Assert.IsNull(obj);
        }
    }
}
