using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextCompressor;

using DataStructures;

namespace TextCompressor_Test {
    [TestClass]
    public class BinarySequence_Test {

#region constructor_test

        [TestMethod]
        public void HuffmanCode_WithoutArguments_CreatesEmptyCode() {
            BinarySequence code = new BinarySequence();

            int length = code.getLength();

            Assert.AreEqual(0, length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HuffmanCode_WithEmptyString_ThrowsArgumentException() {
            BinarySequence code = new BinarySequence("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HuffmanCode_WithNonbinaryString_ThrowsArgumentException() {
            BinarySequence code = new BinarySequence("101011 10");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HuffmanCode_WithNonBinaryValues_ThrowsArgumentException() {
            BinarySequence code = new BinarySequence("101002001");
        }

        [TestMethod]
        public void HuffmanCode_WithValidString_DataIsCorrect() {
            BinarySequence code = new BinarySequence("1010");
            Assert.AreEqual(10, code.NumericValue());
        }

        [TestMethod]
        public void HuffmanCode_WithValidString_LengthIsCorrect() {
            BinarySequence code = new BinarySequence("1010");
            Assert.AreEqual(4, code.getLength());
        }

        [TestMethod]
        public void HuffmanCode_WithZerosString_DataIsZero() {
            BinarySequence code = new BinarySequence("0000");
            Assert.AreEqual(0, code.NumericValue());
        }

        [TestMethod]
        public void HuffmanCode_WithZerosString_LengthIsCorrect() {
            BinarySequence code = new BinarySequence("0000");
            Assert.AreEqual(4, code.getLength());
        }

        [TestMethod]
        public void BinarySequence_WithZeroByte_Is8Zeros() {
            byte b = 0;
            BinarySequence code = new BinarySequence(b);
            Assert.AreEqual("00000000", code.ToString());
        }

        [TestMethod]
        public void BinarySequence_ValidByte_IsCorrectValue() {
            byte b = 218;
            BinarySequence code = new BinarySequence(b);
            Assert.AreEqual("11011010", code.ToString());
        }

#endregion

#region concatenate_test

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void concatenate_LargeNumber_ThrowsArgumentOutOfRange() {
            BinarySequence code = new BinarySequence();
            code.concatenate(2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void concatenate_LargeNumberChar_ThrowsArgumentException() {
            BinarySequence code = new BinarySequence();
            code.concatenate('2');
        }

        [TestMethod]
        public void concatenate_ValidBit_IncreasesBitLengthByOne() {
            BinarySequence code = new BinarySequence();
            int prevLength = code.getLength();
            code.concatenate(0);
            int newLength = code.getLength();
            Assert.AreEqual(prevLength + 1, newLength);
        }

        [TestMethod]
        public void concatenate_ZeroChar_ConcatenatesZero() {
            BinarySequence code = new BinarySequence("101");
            code.concatenate('0');
            BinarySequence re = new BinarySequence("1010");
            Assert.IsTrue(code.isEqual(re));
        }

        [TestMethod]
        public void concatenate_OneChar_ConcatenatesOne() {
            BinarySequence code = new BinarySequence("101");
            code.concatenate('1');
            BinarySequence re = new BinarySequence("1011");
            Assert.IsTrue(code.isEqual(re));
        }

        [TestMethod]
        public void concatenate_ZeroFromEmpty_CodeEqualsZero() {
            BinarySequence code = new BinarySequence();
            code.concatenate(0);
            Assert.AreEqual(0, code.NumericValue());
        }

        [TestMethod]
        public void concatenate_OneFromEmpty_CodeEqualsOne() {
            BinarySequence code = new BinarySequence();
            code.concatenate(1);
            Assert.AreEqual(1, code.NumericValue());
        }

        [TestMethod]
        public void concatenate_ValidSequence_DataIsCorrect() {
            BinarySequence code = new BinarySequence();
            code.concatenate(1);
            code.concatenate(0);
            code.concatenate(1);
            code.concatenate(0);
            Assert.AreEqual(10, code.NumericValue());
        }

        [TestMethod]
        public void concatenate_MultipleZeros_DataIsCorrect() {
            BinarySequence code = new BinarySequence();
            code.concatenate(0);
            code.concatenate(0);
            code.concatenate(0);
            code.concatenate(0);
            Assert.AreEqual(0, code.NumericValue());
        }

        [TestMethod]
        public void concatenate_MultipleZeros_LengthIsCorrect() {
            BinarySequence code = new BinarySequence();
            code.concatenate(0);
            code.concatenate(0);
            code.concatenate(0);
            code.concatenate(0);
            Assert.AreEqual(4, code.getLength());
        }

        [TestMethod]
        public void concatenate_ValidSequence_LengthIsCorrect() {
            BinarySequence code = new BinarySequence();
            code.concatenate(1);
            code.concatenate(0);
            code.concatenate(1);
            code.concatenate(0);
            Assert.AreEqual(4, code.getLength());
        }

#endregion

#region isEqual_test

        [TestMethod]
        public void isEqual_BothEmpty_ReturnsTrue() {
            BinarySequence thisCode = new BinarySequence();
            BinarySequence argCode = new BinarySequence();
            Assert.AreEqual(true, thisCode.isEqual(argCode));
        }

        [TestMethod]
        public void isEqual_ThisEmptyArgumentNot_ReturnsFalse() {
            BinarySequence thisCode = new BinarySequence();
            BinarySequence argCode = new BinarySequence("101");
            Assert.AreEqual(false, thisCode.isEqual(argCode));
        }

        [TestMethod]
        public void isEqual_ArgumentEmptyThisNot_ReturnsFalse() {
            BinarySequence thisCode = new BinarySequence("101");
            BinarySequence argCode = new BinarySequence();
            Assert.AreEqual(false, thisCode.isEqual(argCode));
        }

        [TestMethod]
        public void isEqual_DiffCodesDiffLengths_ReturnsFalse() {
            BinarySequence thisCode = new BinarySequence("101");
            BinarySequence argCode = new BinarySequence("0010");
            Assert.AreEqual(false, thisCode.isEqual(argCode));
        }

        [TestMethod]
        public void isEqual_SameCodesDiffLengths_ReturnsFalse() {
            BinarySequence thisCode = new BinarySequence("101");
            BinarySequence argCode = new BinarySequence("0101");
            Assert.AreEqual(false, thisCode.isEqual(argCode));
        }

        [TestMethod]
        public void isEqual_DiffCodesSameLength_ReturnsFalse() {
            BinarySequence thisCode = new BinarySequence("101");
            BinarySequence argCode = new BinarySequence("110");
            Assert.AreEqual(false, thisCode.isEqual(argCode));
        }

        [TestMethod]
        public void isEqual_SameCodesSameLength_ReturnsFalse() {
            BinarySequence thisCode = new BinarySequence("101");
            BinarySequence argCode = new BinarySequence("101");
            Assert.AreEqual(true, thisCode.isEqual(argCode));
        }

        [TestMethod]
        public void isEqual_ZeroEmpty_ReturnsFalse() {
            BinarySequence thisCode = new BinarySequence("0");
            BinarySequence argCode = new BinarySequence();
            Assert.AreEqual(false, thisCode.isEqual(argCode));
        }

#endregion

#region ToString_test

        [TestMethod]
        public void ToString_EmptyStructure_ReturnsEmptyString() {
            BinarySequence seq = new BinarySequence();
            string str = seq.ToString();
            Assert.AreEqual("", str);
        }

        [TestMethod]
        public void ToString_One_ReturnsOne() {
            BinarySequence seq = new BinarySequence("1");
            string str = seq.ToString();
            Assert.AreEqual("1", str);
        }

        [TestMethod]
        public void ToString_Zero_ReturnsZero() {
            BinarySequence seq = new BinarySequence("0");
            string str = seq.ToString();
            Assert.AreEqual("0", str);
        }

        [TestMethod]
        public void ToString_ValidString_CorrectOutput() {
            BinarySequence seq = new BinarySequence("1010");
            string str = seq.ToString();
            Assert.AreEqual("1010", str);
        }

        [TestMethod]
        public void ToString_ValidString2_CorrectOutput() {
            BinarySequence seq = new BinarySequence("0101");
            string str = seq.ToString();
            Assert.AreEqual("0101", str);
        }

        [TestMethod]
        public void ToString_PrecedingZeros_CorrectOutput() {
            BinarySequence seq = new BinarySequence("000101");
            string str = seq.ToString();
            Assert.AreEqual("000101", str);
        }

        [TestMethod]
        public void ToString_AllZeros_CorrectOutput() {
            BinarySequence seq = new BinarySequence("000000");
            string str = seq.ToString();
            Assert.AreEqual("000000", str);
        }

        [TestMethod]
        public void ToString_HugeNumber_CorrectOutput() {
            string v = "01111101010010101011011010101011011010101011010010000111";
            BinarySequence seq = new BinarySequence(v);
            string str = seq.ToString();
            Assert.AreEqual(v, str);
        }
#endregion

#region Append_test
        [TestMethod]
        public void Append_Zero_AppendsZero() {
            BinarySequence seq = new BinarySequence("10");
            seq.Append(new BinarySequence("0"));
            Assert.AreEqual("100", seq.ToString());
        }

        [TestMethod]
        public void Append_One_AppendsOne() {
            BinarySequence seq = new BinarySequence("10");
            seq.Append(new BinarySequence("1"));
            Assert.AreEqual("101", seq.ToString());
        }

        [TestMethod]
        public void Append_ZeroToEmpty_GivesZeroSequence() {
            BinarySequence seq = new BinarySequence();
            seq.Append(new BinarySequence("0"));
            Assert.AreEqual("0", seq.ToString());
        }

        [TestMethod]
        public void Append_OneToEmpty_GivesOneSequence() {
            BinarySequence seq = new BinarySequence();
            seq.Append(new BinarySequence("1"));
            Assert.AreEqual("1", seq.ToString());
        }

        [TestMethod]
        public void Append_ValidSequences_CorrectResult() {
            BinarySequence seq = new BinarySequence("101");
            seq.Append(new BinarySequence("0010"));
            Assert.AreEqual("1010010", seq.ToString());
        }

        [TestMethod]
        public void Append_ValidSequences2_CorrectResult() {
            BinarySequence seq = new BinarySequence("0010");
            seq.Append(new BinarySequence("111"));
            Assert.AreEqual("0010111", seq.ToString());
        }

        [TestMethod]
        public void Append_LongSequences_CorrectResult() {
            string s1 = "001110101110101101001";
            string s2 = "00010101100101001001100100110";
            BinarySequence seq = new BinarySequence(s1);
            seq.Append(new BinarySequence(s2));
            Assert.AreEqual(s1 + s2, seq.ToString());
        }

        public void Append_EmptySequence_GivesOriginal() {
            BinarySequence seq = new BinarySequence("0011");
            BinarySequence re = new BinarySequence("0011");
            seq.Append(new BinarySequence());
            Assert.IsTrue(re.Equals(seq));
        }
#endregion

#region ToByteArray_test

        [TestMethod]
        public void ToByteArray_EmptySequence_ReturnsEmptyArray() {
            BinarySequence seq = new BinarySequence();
            byte[] b = seq.ToByteArray();
            Assert.AreEqual(0, b.Length);
        }

        [TestMethod]
        public void ToByteArray_SeqLengthLessThan8_ReturnsLength1Array() {
            BinarySequence seq = new BinarySequence("110101");
            byte[] b = seq.ToByteArray();
            Assert.AreEqual(1, b.Length);
        }

        [TestMethod]
        public void ToByteArray_SeqLength8_ReturnsLength1Array() {
            BinarySequence seq = new BinarySequence("00101101");
            byte[] b = seq.ToByteArray();
            Assert.AreEqual(1, b.Length);
        }

        [TestMethod]
        public void ToByteArray_LongSeq_ReturnsCorrectLengthArray() {
            string s = "000001111011011110111010110101000000001010110101";
            int len = s.Length;
            BinarySequence seq = new BinarySequence(s);
            byte[] b = seq.ToByteArray();
            Assert.AreEqual(len/8, b.Length);
        }

        [TestMethod]
        public void ToByteArray_SeqOne_ReturnsArrayWithOnlyOne_LEN() {
            BinarySequence seq = new BinarySequence("1");
            byte[] b = seq.ToByteArray();
            Assert.AreEqual(1, b.Length);
        }

        [TestMethod]
        public void ToByteArray_SeqOne_ReturnsArrayWithOnlyOne_VAL() {
            BinarySequence seq = new BinarySequence("1");
            byte[] b = seq.ToByteArray();
            Assert.AreEqual(1, b[0]);
        }

        [TestMethod]
        public void ToByteArray_SeqZero_ReturnsArrayWithOnlyZero_LEN() {
            BinarySequence seq = new BinarySequence("0");
            byte[] b = seq.ToByteArray();
            Assert.AreEqual(1, b.Length);
        }

        [TestMethod]
        public void ToByteArray_SeqZero_ReturnsArrayWithOnlyZero_VAL() {
            BinarySequence seq = new BinarySequence("0");
            byte[] b = seq.ToByteArray();
            Assert.AreEqual(0, b[0]);
        }

        [TestMethod]
        public void ToByteArray_MultiZeros_ReturnsCorrectLengthArray() {
            BinarySequence seq = new BinarySequence("0000000000");
            byte[] b = seq.ToByteArray();
            Assert.AreEqual(2, b.Length);
        }

        [TestMethod]
        public void ToByteArray_MultiZeros_ReturnsCorrectValsArray() {
            BinarySequence seq = new BinarySequence("0000000000");
            byte[] b = seq.ToByteArray();
            Assert.IsTrue(b[0] == 0 && b[1] == 0);
        }

        [TestMethod]
        public void ToByteArray_ValidSequence1_ReturnsCorrectArrayLen() {
            string s = "0001110111011001010";
            BinarySequence seq = new BinarySequence(s);
            byte[] b = seq.ToByteArray();
            Assert.AreEqual(3, b.Length);
        }

        [TestMethod]
        public void ToByteArray_ValidSequence1_FirstByteCorrect() {
            string s = "0001110111011001010";
            BinarySequence seq = new BinarySequence(s);
            byte[] b = seq.ToByteArray();
            Assert.AreEqual(0, b[0]);
        }

        public void ToByteArray_ValidSequence1_SecondByteCorrect() {
            string s = "0001110111011001010";
            BinarySequence seq = new BinarySequence(s);
            byte[] b = seq.ToByteArray();
            Assert.AreEqual(238, b[1]);
        }

        public void ToByteArray_ValidSequence1_ThirdByteCorrect() {
            string s = "0001110111011001010";
            BinarySequence seq = new BinarySequence(s);
            byte[] b = seq.ToByteArray();
            Assert.AreEqual(202, b[2]);
        }

        [TestMethod]
        public void ToByteArray_ValidSequence2_ReturnsCorrectArrayLen() {
            string s = "11001011010";
            BinarySequence seq = new BinarySequence(s);
            byte[] b = seq.ToByteArray();
            Assert.AreEqual(2, b.Length);
        }

        [TestMethod]
        public void ToByteArray_ValidSequence2_FirstByteCorrect() {
            string s = "11001011010";
            BinarySequence seq = new BinarySequence(s);
            byte[] b = seq.ToByteArray();
            Assert.AreEqual(6, b[0]);
        }

        [TestMethod]
        public void ToByteArray_ValidSequence2_SecondByteCorrect() {
            string s = "11001011010";
            BinarySequence seq = new BinarySequence(s);
            byte[] b = seq.ToByteArray();
            Assert.AreEqual(90, b[1]);
        }

#endregion


    }


}
