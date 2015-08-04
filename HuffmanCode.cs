using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCompressor {
    class HuffmanCode {

        private List<byte> data;
        private byte bitLength;

        public HuffmanCode(string binary) {
            this.bitLength = (byte)binary.Length;
            data = new List<byte>();
        }

        public byte getLength() {
            return bitLength;
        }

        public void concatenate(byte value) {
            if (value != 0 && value != 1) {
                throw new ArgumentException("Binary value must be a 0 or a 1");
            }
            if (bitLength == 0) {
                data.Add(0);
                data[0] = value;
            } else if (bitLength % 8 == 0 && bitLength != 0) {
                data.Insert(0, firstBit(data[0]));
                for (int i = 1; i < data.Count; ++i) {
                    data[i] = (byte)(data[i] << 1);
                    if (i < data.Count - 1) {
                        data[i] = (byte)(data[i] | firstBit(data[i+1]));
                    } else {
                        data[i] = (byte)(data[i] | value);
                    }
                }
            } else {
                for (int i = 0; i < data.Count; ++i) {
                    data[i] = (byte)(data[i] << 1);
                    if (i < data.Count - 1) {
                        data[i] = (byte)(data[i] | firstBit(data[i+1]));
                    } else {
                        data[i] = (byte)(data[i] | value);
                    }
                }
            }
            ++bitLength;

        }

        private byte firstBit(byte b) {
            if ((b & 128) == 0) {
                return 0;
            } else {
                return 1;
            }
        }
    }
}
