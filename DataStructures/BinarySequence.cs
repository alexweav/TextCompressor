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

        }

        

        
    }
}
