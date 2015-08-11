using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace DataStructures {

    //General use, arbitrary-precision string of binary data
    //Does not have an inherent byte-wise division as is prevalent in C#
    //Does not have any groupings or outward-visible divisions of any kind
    //Does not provide support for mathematical operations (e.g. BigNum)
    public class BinarySequence {

        private BigInteger data;
        private byte bitLength;

        //Builds an empty BinarySequence
        public BinarySequence() {
            this.bitLength = 0;
            data = new BigInteger(0);
        }

        //Builds a BinarySequence from a given string of 0 and 1 characters
        //Any other characters in the string will throw an exception
        public BinarySequence(string binary) {
            if (binary == "") {
                throw new ArgumentException("Empty binary string cannot be constructed.");
            }
            this.bitLength = (byte)binary.Length;
            data = new BigInteger(0);
            bitLength = 0;
            for (int i = 0; i < binary.Length; ++i) {
                if (binary[i] == 48) {
                    concatenate(0);
                } else if (binary[i] == 49) {
                    concatenate(1);
                } else {
                    throw new ArgumentException("Binary string must consist of strictly 0s and 1s");
                }
            }
        }

        //Takes a byte b
        //Constructs a BinarySequence of length 8 containing the binary data from the given byte
        public BinarySequence(byte b) {
            string str = "";
            for (int i = 0; i < 8; ++i) {
                if (isEven(b)) {
                    str = "0" + str;
                    b = (byte)(b / 2);
                } else {
                    str = "1" + str;
                    b = (byte)(--b / 2);
                }
            }
            for (int i = 0; i < 8; ++i) {
                if (str[i] == 48) {
                    concatenate(0);
                } else if (str[i] == 49) {
                    concatenate(1);
                }
            }
        }

        private Boolean isEven(byte b) {
            return b % 2 == 0;
        }

        //Returns the bit-length of the BinarySequence
        public byte getLength() {
            return bitLength;
        }

        //Returns a boolean reflecting whether or not the BinarySequence is empty
        public Boolean isEmpty() {
            return bitLength == 0;
        }

        //Converts the BinaryString to an integer value and returns the value
        //If the binary string is longer than 32 bits, data loss will occur
        public int NumericValue() {
            return (int)data;
        }

        //Takes a 0 or 1 in the form of a byte
        //Appends the binary digit to the end of the BinarySequence
        public void concatenate(byte value) {
            if (value != 0 && value != 1) {
                throw new ArgumentOutOfRangeException("Binary value must be a 0 or a 1");
            }
            this.data = BigInteger.Multiply(this.data, new BigInteger(2));
            this.data = BigInteger.Add(this.data, new BigInteger(value));
            ++bitLength;
        }

        //Takes a 0 or 1 in the form of a char
        //Appends the binary digit to the end of the BinarySequence
        public void concatenate(char value) {
            if(value == 48) {
                concatenate(0);
            } else if (value == 49) {
                concatenate(1);
            } else {
                throw new ArgumentException("Character must be a 0 or a 1.");
            }
        }

        //Takes another BinarySequence
        //Returns whether or not this BinarySequence is equivalent to the provided sequence
        public Boolean isEqual(BinarySequence code) {
            if (this.bitLength == 0) {
                return code.getLength() == 0;
            } else {
                if (this.getLength() == code.getLength()) {
                    return this.NumericValue() == code.NumericValue();
                } else {
                    return false;
                }
            }
        }

        //Returns the BinarySequence in the form of a string of 1 and 0 characters
        public override string ToString() {
            string output = "";
            for (int i = 0; i < bitLength; ++i) {
                if (data.IsEven) {
                    data = BigInteger.Divide(data, new BigInteger(2));
                    output = "0" + output;
                } else {
                    data = BigInteger.Subtract(data, new BigInteger(1));
                    data = BigInteger.Divide(data, new BigInteger(2));
                    output = "1" + output;
                }
            }
            return output;
        }

        //Appends the given BinarySequence to the end of the current BinarySequence
        public void Append(BinarySequence seq) {
            string seqStr = seq.ToString();
            for (int i = 0; i < seqStr.Length; ++i) {
                if (seqStr[i] == 48) {
                    concatenate(0);
                } else if (seqStr[i] == 49) {
                    concatenate(1);
                }
            }
        }

        //Returns the binary sequence in the form of a big-endian ordered byte array
        //Leading zeros are added as needed to fill the largest byte completely.
        public byte[] ToByteArray() {
            if (bitLength == 0) {
                return new byte[0];
            }
            int intendedLength = ByteLength();
            byte[] raw = data.ToByteArray();
            byte[] output = new byte[intendedLength];
            int numZeroBytes = intendedLength - raw.Length;
            for (int i = 0; i < raw.Length; ++i) {
                output[i+numZeroBytes] = raw[raw.Length - 1 - i];
            }

            return output;
        }

        //Returns the length in bytes if this sequence were to be converted into a byte array
        public int ByteLength() {
            int len = bitLength / 8;
            if (!(bitLength % 8 == 0)) {
                ++len;
            }
            return len;
        }

        

        
    }
}
