using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCompressor {
    class TextFile {

        private string filepath;

        private const int BYTE_LENGTH = 8;

        //Constructor
        public TextFile(string filepath) {
            if (isValidFilepath(filepath)) {
                this.filepath = filepath;
            } else {
                throw new ArgumentException("Not a valid filepath on this machine.");
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
                } else {
                    throw new ArgumentException("Not a valid filepath on this machine.");
                }
            }
        }

        //Returns true if the given string is a valid filepath on the current machine
        //Returns false otherwise
        private bool isValidFilepath(string filepath) {
            return File.Exists(filepath);
        }

        //Writes a set of lines to a text file.
        public void writeLines(string[] lines) {
            System.IO.File.WriteAllLines(this.filepath, lines);
        }

        //Reads the text file into a string.
        public string readFile() {
            try {
                StreamReader reader = new StreamReader(this.filepath);
                return reader.ReadToEnd();
            } catch (Exception) {
                return null;
            }
        }

        //gets an array of every character type present in the document
        public char[] getCharset() {
            string charset = "";
            string file = readFile();
            int len = file.Length;
            for (int i = 0; i < len; i++) {
                if (!charset.Contains(file[i])) {
                    charset += file[i];
                }
            }
            return charset.ToCharArray();
        }

        //Takes a set of characters in char array form
        //A *character frequency* is the number of times in which a given character appears in the associated text file
        //Returns an array of integers where the nth integer in the array is the character frequency of the nth character in the input array
        public int[] getCharFrequencies(char[] charset) {
            int len = charset.Length;
            int[] frequencies = new int[len];
            string file = readFile();
            for (int i = 0; i < len; i++) {
                int charTotal = 0;
                for (int j = 0; j < file.Length; j++) {
                    if (file[j].Equals(charset[i])) {
                        ++charTotal;
                    }
                }
                frequencies[i] = charTotal;
            }
            return frequencies;
        }

        //Encodes the text file with specified character-code lookup tables
        //Returns the encoded file in the form of a binary string
        private string encodeToString(char[] chars, HuffmanCode[] codes) {
            string file = readFile();
            string encodedFile = "";
            for (int i = 0; i < file.Length; i++) {
                if (chars.Contains(file[i])) {
                    int index = Array.IndexOf(chars, file[i]);
                    encodedFile += codes[index];    //CHANGE THIS, SHOULD += BINARY DATA OF codes[index] 
                }
            }
            return encodedFile;
        }

        //Builds an encoded .hct file from the text file
        public EncodedFile encodeFile() {
            char[] charset = getCharset();
            int[] weights = getCharFrequencies(charset);
            HuffmanTree tree = new HuffmanTree(charset, weights);
            HuffmanCode[] codes = tree.getCodes(charset);
            string encoded = encodeToString(charset, codes);
            string huffmanData = tree.getBinaryRepresentation();
            EncodedFile enf = new EncodedFile("C:\\Users\\Owner\\Documents\\encodedTEST.hct", EncodedFile.CREATE_NEW); //get fp from user
            writeEncodedFile(enf, huffmanData, encoded);
            return enf;
        }

        private void writeEncodedFile(EncodedFile enf, string huffmanData, string body) {
            BinaryWriter writer = new BinaryWriter(File.Open(enf.Filepath, FileMode.Create));
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
            }
            else {
                numGroups = length / groupSize + 1;
            }
            string[] groups = new string[numGroups];
            for (int i = 0; i < numGroups; ++i) {
                if (i != numGroups - 1) {
                    groups[i] = st.Substring(groupSize * i, groupSize);
                }
                else {
                    groups[i] = st.Substring(groupSize * i);
                }
            }
            return groups;
        }
    }
}
