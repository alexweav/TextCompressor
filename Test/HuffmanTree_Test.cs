using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextCompressor;

namespace TextCompressor_Test {
    [TestClass]
    public class HuffmanTree_Test {

#region constructor_test

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HuffmanTree_DifferentSizeInputArrays_ThrowsArgumentException() {
            char[] charset = { 'A', 'B', 'C' };
            int[] weights = { 1, 2 };
            HuffmanTree t = new HuffmanTree(charset, weights); 
        }

        [TestMethod]
        public void HuffmanTree_HeadContainsCharset_AfterInitialization() {
            char[] charset = { 'A', 'B', 'C' };
            int[] weights = { 3, 1, 2 };
            HuffmanTree t = new HuffmanTree(charset, weights);
            Assert.IsTrue(t.getCharset().Contains('A'));
            Assert.IsTrue(t.getCharset().Contains('B'));
            Assert.IsTrue(t.getCharset().Contains('C'));
            Assert.AreEqual(3, t.getCharset().Length);
        }

        [TestMethod]
        public void HuffmanTree_EmptyInput_EmptyTree() {
            char[] charset = { };
            int[] weights = { };
            HuffmanTree t = new HuffmanTree(charset, weights);
            Assert.AreEqual(0, t.getCharset().Length);
        }
#endregion

#region getHuffmanCode_test

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetHuffmanCode_InvalidCharacter_ThrowsArgumentException() {
            char[] charset = { 'A', 'B', 'C' };
            int[] weights = { 3, 1, 2 };
            HuffmanTree t = new HuffmanTree(charset, weights);
            t.getHuffmanCode('D');
        }

        [TestMethod]
        public void GetHuffmanCode_SingleCharacterTree_GivesSingleDigitCode() {
            char[] charset = { 'A' };
            int[] weights = { 47 };
            HuffmanTree t = new HuffmanTree(charset, weights);
            Assert.AreEqual(1, t.getHuffmanCode('A').getLength());
        }

        [TestMethod]
        public void GetHuffmanCode_CodeOfCharacter_IsNotEmpty() {
            char[] charset = { 'A', 'B', 'C' };
            int[] weights = { 3, 1, 2 };
            HuffmanTree t = new HuffmanTree(charset, weights);
            Assert.IsTrue(t.getHuffmanCode('A').getLength() > 0);
            Assert.IsTrue(t.getHuffmanCode('B').getLength() > 0);
            Assert.IsTrue(t.getHuffmanCode('C').getLength() > 0);
        }

        [TestMethod]
        public void GetHuffmanCode_MostFrequentChar_HasShortestCode() {
            char[] charset = { 'A', 'B', 'C' };
            int[] weights = { 3, 1, 2 };
            HuffmanTree t = new HuffmanTree(charset, weights);
            Assert.IsTrue(t.getHuffmanCode('A').getLength() <= t.getHuffmanCode('B').getLength());
            Assert.IsTrue(t.getHuffmanCode('A').getLength() <= t.getHuffmanCode('C').getLength());
        }

        [TestMethod]
        public void GetHuffmanCode_LeastFrequentChar_HasLongestCode() {
            char[] charset = { 'A', 'B', 'C' };
            int[] weights = { 3, 1, 2 };
            HuffmanTree t = new HuffmanTree(charset, weights);
            Assert.IsTrue(t.getHuffmanCode('B').getLength() >= t.getHuffmanCode('A').getLength());
            Assert.IsTrue(t.getHuffmanCode('B').getLength() >= t.getHuffmanCode('C').getLength());
        }

        [TestMethod]
        public void GetHuffmanCode_ValidChar_ValidCode1() {
            char[] charset = { 'A', 'B', 'C' , 'D', 'E'};
            int[] weights = { 3, 1, 2, 7, 5};
            HuffmanTree t = new HuffmanTree(charset, weights);
            //shown to be correct
            Assert.AreEqual("0", t.getHuffmanCode('D').ToString());
            Assert.AreEqual("10", t.getHuffmanCode('E').ToString());
            Assert.AreEqual("111", t.getHuffmanCode('A').ToString());
            Assert.AreEqual("1101", t.getHuffmanCode('C').ToString());
            Assert.AreEqual("1100", t.getHuffmanCode('B').ToString());
        }

        [TestMethod]
        public void GetHuffmanCode_ValidChar_ValidCode2() {
            char[] charset = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            int[] weights = { 27, 16, 8, 1, 190, 53, 54, 6 };
            HuffmanTree t = new HuffmanTree(charset, weights);
            //shown to be correct
            Assert.AreEqual("1", t.getHuffmanCode('E').ToString());
            Assert.AreEqual("000", t.getHuffmanCode('A').ToString());
            Assert.AreEqual("010", t.getHuffmanCode('F').ToString());
            Assert.AreEqual("011", t.getHuffmanCode('G').ToString());
            Assert.AreEqual("0011", t.getHuffmanCode('B').ToString());
            Assert.AreEqual("00101", t.getHuffmanCode('C').ToString());
            Assert.AreEqual("001001", t.getHuffmanCode('H').ToString());
            Assert.AreEqual("001000", t.getHuffmanCode('D').ToString());
        }

#endregion

#region getBinaryRepresentation_test

        [TestMethod]
        public void GetBinaryRepresentation_EmptyTree_EmptyString() {
            char[] charset = { };
            int[] weights = { };
            HuffmanTree t = new HuffmanTree(charset, weights);
            Assert.IsTrue(t.getBinaryRepresentation().isEmpty());
        }

        [TestMethod]
        public void GetBinaryRepresentation_SingleChar_CorrectOutput() {
            char[] charset = { 'A' };
            int[] weights = { 1 };
            HuffmanTree t = new HuffmanTree(charset, weights);
            Assert.AreEqual("101000001", t.getBinaryRepresentation().ToString());
        }

        [TestMethod]
        public void GetBinaryRepresentation_ValidChars_CorrectCode1() {
            char[] charset = { 'A', 'B', 'C', 'D', 'E' };
            int[] weights = { 3, 1, 2, 7, 5 };
            HuffmanTree t = new HuffmanTree(charset, weights);
            Assert.AreEqual("0101000100010100010100101000010101000011101000001", t.getBinaryRepresentation().ToString());   //01D01E001B1C1A all char values in ascii
        }

        [TestMethod]
        public void GetBinaryRepresentation_ValidChars_CorrectCode2() {
            char[] charset = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            int[] weights = { 27, 16, 8, 1, 190, 53, 54, 6 };
            HuffmanTree t = new HuffmanTree(charset, weights);
            Assert.AreEqual("0001010000010001010001001010010001010000111010000100101000110101000111101000101", t.getBinaryRepresentation().ToString());    //0001A0001D1H1C1B01F1G1E
        }

#endregion

#region getCodes_test

#endregion

#region getEncodingTable_test

#endregion
    }
}
