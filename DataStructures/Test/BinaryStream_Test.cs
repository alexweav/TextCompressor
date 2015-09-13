using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextCompressor;
using DataStructures;

namespace DataStructures_Test {
    [TestClass]
    public class BinaryStream_Test {

#region constructor_test

        [TestMethod]
        public void BinaryStream_EmptyConstructor_CreatesEmptyStream() {
            BinaryStream s = new BinaryStream();
            Assert.IsTrue(s.isEndOfStream());
        }

        [TestMethod]
        public void BinaryStream_EmptyArray_CreatesEmptyStream() {
            byte[] b = new byte[0];
            BinaryStream s = new BinaryStream(b);
            Assert.IsTrue(s.isEndOfStream());
        }

        [TestMethod]
        public void BinaryStream_NonEmptyArray_CreatesNonemptyStream() {
            byte[] b = { 57 };
            BinaryStream s = new BinaryStream(b);
            Assert.IsFalse(s.isEndOfStream());
        }

#endregion

#region ReadBit_test

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ReadBit_EmptyStream_ThrowsIndexOutOfRangeException() {
            byte[] b = new byte[0];
            BinaryStream s = new BinaryStream(b);
            s.readBit();
        }

        [TestMethod]
        public void ReadBit_ValidStream_BitsReadCorrectly1() {
            byte[] b = { 85 };  //0x01010101b
            BinaryStream s = new BinaryStream(b);
            string returnedBinary = "";
            for (int i = 0; i < 8; ++i) {
                returnedBinary += s.readBit();
            }
            Assert.AreEqual("01010101", returnedBinary);
        }

        [TestMethod]
        public void ReadBit_ValidStream_BitsReadCorrectly2() {
            byte[] b = { 43 }; //0x00101011b
            BinaryStream s = new BinaryStream(b);
            string returnedBinary = "";
            for (int i = 0; i < 8; ++i) {
                returnedBinary += s.readBit();
            }
            Assert.AreEqual("00101011", returnedBinary);
        }

        [TestMethod]
        public void ReadBit_LongStream_BitsReadCorrectly() {
            byte[] b = { 231, 24 }; //0x11100111b 0x00011000b
            BinaryStream s = new BinaryStream(b);
            string returnedBinary = "";
            for (int i = 0; i < 16; ++i) {
                returnedBinary += s.readBit();
            }
            Assert.AreEqual("1110011100011000", returnedBinary);
        }

        [TestMethod]
        public void ReadBit_ValidStream_SetsEndOfStreamCorrectly() {
            byte[] b = { 231 };
            BinaryStream s = new BinaryStream(b);
            for(int i = 0; i < 8; ++i) {
                s.readBit();
            }
            Assert.IsTrue(s.isEndOfStream());
        }

#endregion

#region ReadByte_test

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ReadByte_EmptyStream_ThrowsIndexOutOfRangeException() {
            byte[] b = new byte[0];
            BinaryStream s = new BinaryStream(b);
            s.readByte();
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ReadByte_StreamLengthLessThan8_ThrowsIndexOutOfRangeException() {
            byte[] b = { 23 };
            BinaryStream s = new BinaryStream(b);
            s.readBit();
            s.readByte();
        }

        [TestMethod]
        public void ReadByte_OneByteStream_ReturnsCorrectValue() {
            byte[] b = { 231 };
            BinaryStream s = new BinaryStream(b);
            Assert.AreEqual(231, s.readByte());
        }

        [TestMethod]
        public void ReadByte_MultiByteStream_ReturnsCorrectValues() {
            byte[] b = { 231, 24 };
            BinaryStream s = new BinaryStream(b);
            byte[] output = new byte[2];
            output[0] = s.readByte();
            output[1] = s.readByte();
            Assert.AreEqual(231, output[0]);
            Assert.AreEqual(24, output[1]);
        }

        [TestMethod]
        public void ReadByte_StaggeredByte_ReturnsCorrectValues() {
            byte[] b = { 231, 24 }; //0x11100111b 0x00011000b
            BinaryStream s = new BinaryStream(b);
            s.readBit();
            s.readBit();
            Assert.AreEqual(156, s.readByte()); //0x10011100b
        }

        [TestMethod]
        public void ReadByte_StaggeredByte_UpdatesIndicesCorrectly() {
            byte[] b = { 231, 24 }; //0x11100111b 0x00011000b
            BinaryStream s = new BinaryStream(b);
            s.readBit();
            s.readBit();
            s.readByte();
            Assert.AreEqual(0, s.readBit());
            Assert.AreEqual(1, s.readBit());
        }

#endregion


    }



    

}
