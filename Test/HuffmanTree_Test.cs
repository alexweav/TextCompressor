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
#endregion

#region getHuffmanCode_test

#endregion

#region getBinaryRepresentation_test

#endregion

#region getCodes_test

#endregion

#region getEncodingTable_test

#endregion
    }
}
