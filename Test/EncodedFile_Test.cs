using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TextCompressor;

namespace TextCompressor_Test {

    [TestClass]
    public class EncodedFile_Test {
        public EncodedFile_Test() {

        }

        #region constructor_test

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EncodedFile_NonExistingFileNotCreatingNew_ThrowsArgumentException() {
            EncodedFile file = new EncodedFile("C:/Users/ayylmao.txt", false);   //provided this does not exist
        }

        [TestMethod]
        public void EncodedFile_ExistingFileNotCreatingNew_Success() {
            var validPathStart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TextCompressor");
            string filepath = Path.Combine(validPathStart, "TextCompressor_EncodedFile_Test.hct");
            if (!File.Exists(filepath)) {
                System.IO.File.Create(filepath).Close();
            }
            EncodedFile file = new EncodedFile(filepath, false);
            Assert.AreEqual(filepath, file.Filepath);
            System.IO.File.Delete(filepath);
        }

        [TestMethod]
        public void EncodedFile_NonExistingFileCreatingNew_Success() {
            var validPathStart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TextCompressor");
            string filepath = Path.Combine(validPathStart, "TextCompressor_EncodedFile_Test.hct");
            EncodedFile file = new EncodedFile(filepath, true);
            Assert.AreEqual(filepath, file.Filepath);
            Assert.IsTrue(File.Exists(file.Filepath));
            System.IO.File.Delete(filepath);
        }

        [TestMethod]
        public void EncodedFile_ExistingFileCreatingNew_Success() {
            var validPathStart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TextCompressor");
            string filepath = Path.Combine(validPathStart, "TextCompressor_EncodedFile_Test.hct");
            if (!File.Exists(filepath)) {
                System.IO.File.Create(filepath).Close();
            }
            EncodedFile file = new EncodedFile(filepath, true);
            Assert.AreEqual(filepath, file.Filepath);
            System.IO.File.Delete(filepath);
        }

        #endregion

        #region ReadFile_test
        [TestMethod]
        public void ReadFile_EmptyFile_EmptyResult() {
            var validPathStart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TextCompressor");
            string filepath = Path.Combine(validPathStart, "TextCompressor_EncodedFile_Test.hct");
            EncodedFile file = new EncodedFile(filepath, true);
            Assert.AreEqual(0, file.readFile().Length);
            System.IO.File.Delete(filepath);
        }
        #endregion

        #region DecodeFile_test
        [TestMethod]
        public void DecodeFile_EmptyFile_ResultsInEmptyString() {
            var validPathStart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TextCompressor");
            string filepath = Path.Combine(validPathStart, "TextCompressor_TextFile_Test.txt");
            if (!File.Exists(filepath)) {
                System.IO.File.Create(filepath).Close();
            }
            TextFile file = new TextFile(filepath);
            string encodedFilepath = Path.Combine(validPathStart, "TextCompressor_EncodedFile_Test.hct");
            EncodedFile encoded = file.encodeFile(encodedFilepath);
            Assert.AreEqual("", encoded.decodeFile());
            System.IO.File.Delete(filepath);
            System.IO.File.Delete(encodedFilepath);
        }

        #endregion



    }
}
