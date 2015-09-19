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
            System.IO.File.Delete(filepath);
            System.IO.File.Delete(newFilepath);

        }

        #endregion

        #region Read_Write_test

        [TestMethod]
        public void ReadWrite_BlankFile_ReadEmpty() {
            var validPathStart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TextCompressor");
            string filepath = Path.Combine(validPathStart, "TextCompressor_TextFile_Test.txt");
            if (!File.Exists(filepath)) {
                System.IO.File.Create(filepath).Close();
            }
            TextFile file = new TextFile(filepath);
            Assert.AreEqual("", file.readFile());
            System.IO.File.Delete(filepath);
        }

        [TestMethod]
        public void ReadWrite_ValidText_ReadsText1() {
            var validPathStart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TextCompressor");
            string filepath = Path.Combine(validPathStart, "TextCompressor_TextFile_Test.txt");
            if (!File.Exists(filepath)) {
                System.IO.File.Create(filepath).Close();
            }
            TextFile file = new TextFile(filepath);
            string text = "This is a test file.\nThis is a new line.";
            file.writeText(text);
            Assert.AreEqual(text, file.readFile());
            System.IO.File.Delete(filepath);
        }

        [TestMethod]
        public void ReadWrite_ValidText_ReadsText2() {
            var validPathStart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TextCompressor");
            string filepath = Path.Combine(validPathStart, "TextCompressor_TextFile_Test.txt");
            if (!File.Exists(filepath)) {
                System.IO.File.Create(filepath).Close();
            }
            TextFile file = new TextFile(filepath);
            string text = "\n\n\n\n\n";
            file.writeText(text);
            Assert.AreEqual(text, file.readFile());
            System.IO.File.Delete(filepath);
        }

        #endregion

        #region GetCharset_test

        [TestMethod]
        public void GetCharset_EmptyFile_EmptyCharset() {
            var validPathStart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TextCompressor");
            string filepath = Path.Combine(validPathStart, "TextCompressor_TextFile_Test.txt");
            if (!File.Exists(filepath)) {
                System.IO.File.Create(filepath).Close();
            }
            TextFile file = new TextFile(filepath);
            Assert.AreEqual(0, file.getCharset().Length);
            System.IO.File.Delete(filepath);
        }

        [TestMethod]
        public void GetCharset_ValidFile_CorrectCharset() {
            var validPathStart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TextCompressor");
            string filepath = Path.Combine(validPathStart, "TextCompressor_TextFile_Test.txt");
            if (!File.Exists(filepath)) {
                System.IO.File.Create(filepath).Close();
            }
            TextFile file = new TextFile(filepath);
            string text = "Test charset.\nNewline.";
            file.writeText(text);
            char[] expected = { 'T', 'e', 's', 't', ' ', 'c', 'h', 'a', 'r', '.', '\n', 'N', 'w', 'l', 'i', 'n' };
            Assert.AreEqual(expected.Length, file.getCharset().Length);
            for (int i = 0; i < file.getCharset().Length; ++i) {
                Assert.AreEqual(expected[i], file.getCharset()[i]);
            }
            System.IO.File.Delete(filepath);
        }

        #endregion

        #region GetFrequencies_test
        [TestMethod]
        public void GetFrequencies_EmptyFile_ZeroFrequencies() {
            var validPathStart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TextCompressor");
            string filepath = Path.Combine(validPathStart, "TextCompressor_TextFile_Test.txt");
            if (!File.Exists(filepath)) {
                System.IO.File.Create(filepath).Close();
            }
            TextFile file = new TextFile(filepath);
            char[] chars = { 'a', 'b', 'c', 'd', 'e' };
            int[] frequencies = file.getCharFrequencies(chars);
            for (int i = 0; i < frequencies.Length; ++i) {
                Assert.AreEqual(0, frequencies[i]);
            }
            System.IO.File.Delete(filepath);
        }

        [TestMethod]
        public void GetFrequencies_ValidFile_NonexistantCharsZeroFrequencies() {
            var validPathStart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TextCompressor");
            string filepath = Path.Combine(validPathStart, "TextCompressor_TextFile_Test.txt");
            if (!File.Exists(filepath)) {
                System.IO.File.Create(filepath).Close();
            }
            TextFile file = new TextFile(filepath);
            file.writeText("This DoEsn't hAvE Any lowErCAsE A, B, C, D, or Es in it.");
            char[] chars = { 'a', 'b', 'c', 'd', 'e' };
            int[] frequencies = file.getCharFrequencies(chars);
            for (int i = 0; i < frequencies.Length; ++i) {
                Assert.AreEqual(0, frequencies[i]);
            }
            System.IO.File.Delete(filepath);
        }

        [TestMethod]
        public void GetFrequencies_ValidFile_CorrectFrequencies() {
            var validPathStart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TextCompressor");
            string filepath = Path.Combine(validPathStart, "TextCompressor_TextFile_Test.txt");
            if (!File.Exists(filepath)) {
                System.IO.File.Create(filepath).Close();
            }
            TextFile file = new TextFile(filepath);
            file.writeText("I'm bad at inventing teststrings.");
            char[] chars = { 'I', '\'', 'm', 'b', 'a', 'd', 't', 'i', 'n', 'v', 'e', 'g', 's', 'r' };
            int[] expectedFrequencies = { 1, 1, 1, 1, 2, 1, 5, 3, 4, 1, 2, 2, 3, 1 };
            int[] frequencies = file.getCharFrequencies(chars);
            Assert.AreEqual(expectedFrequencies.Length, frequencies.Length);
            for (int i = 0; i < frequencies.Length; ++i) {
                Assert.AreEqual(expectedFrequencies[i], frequencies[i]);
            }
            System.IO.File.Delete(filepath);
        }
        #endregion

        #region EncodeFile_test
        [TestMethod]
        public void EncodeFile_EmptyFile_EmptyEncodedFile() {
            var validPathStart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TextCompressor");
            string filepath = Path.Combine(validPathStart, "TextCompressor_TextFile_Test.txt");
            if (!File.Exists(filepath)) {
                System.IO.File.Create(filepath).Close();
            }
            TextFile file = new TextFile(filepath);
            string encodedFilepath = Path.Combine(validPathStart, "TextCompressor_EncodedFile_Test.hct");
            EncodedFile encoded = file.encodeFile(encodedFilepath);
            Assert.AreEqual(0, encoded.readFile()[0]);
            System.IO.File.Delete(filepath);
            System.IO.File.Delete(encodedFilepath);
        }

        [TestMethod]
        public void EncodeFile_ValidFile_CompleteCycle() {
            var validPathStart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TextCompressor");
            string filepath = Path.Combine(validPathStart, "TextCompressor_TextFile_Test.txt");
            if (!File.Exists(filepath)) {
                System.IO.File.Create(filepath).Close();
            }
            TextFile file = new TextFile(filepath);
            file.writeText("Mississippi river");
            string encodedFilepath = Path.Combine(validPathStart, "TextCompressor_EncodedFile_Test.hct");
            EncodedFile encoded = file.encodeFile(encodedFilepath);
            Assert.AreEqual("Mississippi river", encoded.decodeFile());
            System.IO.File.Delete(filepath);
            System.IO.File.Delete(encodedFilepath);
        }
        #endregion

    }
}
