using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace TextCompressor {
    public class HuffmanCode {

        private BigInteger data;
        private byte bitLength;

        public HuffmanCode() {
            this.bitLength = 0;
            data = new BigInteger(0);
        }

        public HuffmanCode(string binary) {
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

        public byte getLength() {
            return bitLength;
        }

        public Boolean isEmpty() {
            return bitLength == 0;
        }

        public int NumericValue() {
            return (int)data;
        }

        public void concatenate(byte value) {
            if (value != 0 && value != 1) {
                throw new ArgumentOutOfRangeException("Binary value must be a 0 or a 1");
            }
            this.data = BigInteger.Multiply(this.data, new BigInteger(2));
            this.data = BigInteger.Add(this.data, new BigInteger(value));
            ++bitLength;
        }

        

        
    }
}
