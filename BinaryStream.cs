using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCompressor.Source
{
    class BinaryStream
    {
        private const int BYTE_LENGTH = 8;

        private byte[] data;
        private int byteIndex;
        private int bitIndex;

        #region constructors
        public BinaryStream() {

        }

        public BinaryStream(byte[] data) {
            this.data = data;
            if (this.data.Length > 0) {
                byteIndex = 0;
                bitIndex = 0;
            }
        }
        #endregion

        public byte readBit() {
            byte currentBit = getCurrentBit();
            changeIndices(1);
            return currentBit;
        }
        
        //Advances the "current bit" indices by the specified number of bits
        private void changeIndices(int delta) {
            if (byteIndex + ((bitIndex + delta) / BYTE_LENGTH) > this.data.Length - 1) {
                throw new IndexOutOfRangeException();
            }
            try {
                this.byteIndex += (bitIndex + delta) / BYTE_LENGTH;
                this.bitIndex = (bitIndex + delta) % BYTE_LENGTH;
            } catch (Exception) {
                throw new IndexOutOfRangeException("Binary stream indices out of range.");
            }
        }

        private byte getCurrentBit() {
            byte andBuffer = (byte)(1 << bitIndex);
            byte res = (byte)(data[byteIndex] & andBuffer);
            if (res == 0) {
                return (byte)0;
            } else {
                return (byte)1;
            }
        }
    }
}
