using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCompressor {
    class EncodedFile {

        private string filepath;

        private int BYTE_LENGTH = 8;

        //constructor
        public EncodedFile(string filepath) {
            if (isValidFilepath(filepath)) {
                this.filepath = filepath;
            }
            else {
                File.Create(filepath);
                this.filepath = filepath;
            }
        }

        //Get, set
        public string Filepath {
            get {
                return filepath;
            }
            set {
                if (isValidFilepath(value)) {
                    this.filepath = value;
                }
                else {
                    File.Create(value);
                    this.filepath = value;
                }
            }
        }

        //Returns true if the given string is a valid filepath on the current machine
        //Returns false otherwise
        private bool isValidFilepath(string filepath) {
            return File.Exists(filepath);
        }

        public void writeFile(string huffmanData, string body) {
            BinaryWriter writer = new BinaryWriter(File.Open(filepath, FileMode.Create));
            writeBinaryString(huffmanData, writer, true);
            writeBinaryString(body, writer, false);
        }

        //Given a huffman data string, writes the length of the string followed by the string itself to the encoded file
        //If the string's length is not an even multiple of 8 bytes, zeros are appended to the end appropriately
        private void writeBinaryString(string data, BinaryWriter writer, Boolean writeLength) {
            string[] groupedData = groupString(data, BYTE_LENGTH);
            byte length = (byte)groupedData.Length;
            if (writeLength) {
                writer.Write(length);
            }
            int lastLength = groupedData[length - 1].Length;
            for (int i = 0; i < 8 - lastLength; i++) {
                groupedData[length - 1] += "0";
            }
            byte[] bytes = new byte[groupedData.Length];
            for (int i = 0; i < groupedData.Length; ++i) {
                bytes[i] = Convert.ToByte(groupedData[i], 2);
            }
            writer.Write(bytes);
        }

        //Splits a string st into a string array, each element of at most size groupSize
        //If the length of st is not divisible by groupSize, the final element of the string array is sized appropriately
        private string[] groupString(string st, int groupSize) {
            int length = st.Length;
            int numGroups;
            if (length % groupSize == 0) {
                numGroups = length / groupSize;
            } else {
                numGroups = length / groupSize + 1;
            }
            string[] groups = new string[numGroups];
            for (int i = 0; i < numGroups; ++i) {
                if (i != numGroups - 1) {
                    groups[i] = st.Substring(groupSize * i, groupSize);
                } else {
                    groups[i] = st.Substring(groupSize * i);
                }
            }
            return groups;
        }
    }
}
