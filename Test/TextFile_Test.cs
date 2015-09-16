using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

using TextCompressor;

namespace TextCompressor_Test {
    [TestClass]
    public class TextFile_Test {
        public TextFile_Test() {

        }

        #region constructor_get_set_test

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TextFile_NonExistingFile_ThrowsArgumentException() {
            TextFile file = new TextFile("C:/Users/ayylmao.txt");   //provided this does not exist
        }

        [TestMethod]
        public void TextFile_ExistingFile_Success() {
            var validPathStart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TextCompressor");
            string filepath = Path.Combine(validPathStart, "TextCompressor_TextFile_Test.txt");
            if (!File.Exists(filepath)) {
                System.IO.File.Create(filepath).Close();
            }
            TextFile file = new TextFile(filepath);
            Assert.AreEqual(filepath, file.Filepath);
            System.IO.File.Delete(filepath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetFilepath_NotExistingFile_ThrowsArgumentException() {
            var validPathStart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TextCompressor");
            string filepath = Path.Combine(validPathStart, "TextCompressor_TextFile_Test.txt");
            if (!File.Exists(filepath)) {
                System.IO.File.Create(filepath).Close();
            }
            TextFile file = new TextFile(filepath);
            file.Filepath = "C:/Users/ayylmao.txt";     //provided this does not exist
        }

        [TestMethod]
        public void SetFilepath_ExistingFile_Success() {
            var validPathStart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TextCompressor");
            string filepath = Path.Combine(validPathStart, "TextCompressor_TextFile_Test.txt");
            if (!File.Exists(filepath)) {
                System.IO.File.Create(filepath).Close();
            }
            TextFile file = new TextFile(filepath);
            string newFilepath = Path.Combine(validPathStart, "TextCompressor_TextFile_Test2.txt");
            if (!File.Exists(newFilepath)) {
                System.IO.File.Create(newFilepath).Close();
            }
            file.Filepath = newFilepath;
            Assert.AreEqual(newFilepath, file.Filepath);
        }

        #endregion

        #region read_write_test

        #endregion

    }
}
