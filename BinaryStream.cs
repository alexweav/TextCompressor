using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCompressor.Source
{
    //BinaryStream data type
    //Constructed from an array of bytes, assumed to be raw binary data
    //Functions like an input stream, starting from the beginning of the data
    //Reads individual bits or entire bytes, advancing along the stream in a *forward only* direction until the stream ends
    //Bit and byte reading ignores byte boundaries in the given data
    class BinaryStream
    {
        private const int BYTE_LENGTH = 8;

        private byte[] data;
        private int byteIndex;
        private int bitIndex;
        private Boolean endOfStream;

        #region constructors
        public BinaryStream() {

        }

        public BinaryStream(byte[] data) {
            this.data = data;
            if (this.data.Length > 0) {
                byteIndex = 0;
                bitIndex = 0;
                endOfStream = false;
            } else {
                endOfStream = true;
            }
        }
        #endregion

        public Boolean isEndOfStream() {
            return endOfStream;
        }

        //Reads a single bit from the data stream, returns a 0 or 1 in byte length reflecting the bit's state
        public byte readBit() {
            if (isEndOfStream()) {
                throw new IndexOutOfRangeException("Stream has ended.");
            }
            byte currentBit = getCurrentBit();
            try {
                changeIndices(1);
            } catch (Exception) {
                endOfStream = true;
            }
            return currentBit;
        }

        //Reads 8 bits from the data stream, and returns the data 
        public byte readByte() {
            int initialByteIndex = byteIndex;
            int initialBitIndex = bitIndex;
            byte output = 0;
            for (byte i = 0; i < BYTE_LENGTH; ++i) {
                byte bit;
                try {
                    bit = readBit();
                } catch (Exception) {
                    byteIndex = initialByteIndex;
                    bitIndex = initialBitIndex;
                    throw new IndexOutOfRangeException("Stream has ended.");
                }
                bit = (byte)(bit << i);
                output = (byte)(output | bit);
            }
            return output;
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

        //Returns a 0 or 1 (byte length, due to C#'s pickiness) reflecting the state of the (bitIndex)th bit of the (byteIndex)th byte
        //of the data array
        //Does not advance the pointers
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
