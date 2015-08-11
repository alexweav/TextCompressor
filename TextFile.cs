using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataStructures;

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

        //Takes a dictionary mapping from characters to corresponding BinarySequences
        //For any character, its corresponding BinarySequence is taken to be its Huffman Code
        //Constructs a large BinarySequence which is the encoded version of this text file
        //Returns the encoded BinarySequence
        private BinarySequence encodeToBinary(Dictionary<char, BinarySequence> table) {
            string file = readFile();
            BinarySequence encodedFile = new BinarySequence();
            for (int i = 0; i < file.Length; ++i) {
                BinarySequence encodedChar = new BinarySequence();
                if (table.TryGetValue(file[i], out encodedChar)) {
                    encodedFile.Append(encodedChar);
                } else {
                    throw new ArgumentException("Unrecognized char, something has gone wrong.");
                }
            }
            return encodedFile;
        }

        //Builds an encoded .hct file from the text file
        public EncodedFile encodeFile() {
            char[] charset = getCharset();
            int[] weights = getCharFrequencies(charset);
            HuffmanTree tree = new HuffmanTree(charset, weights);
            Dictionary<char, BinarySequence> codeTable = tree.getEncodingTable(charset);
            BinarySequence encoded = encodeToBinary(codeTable);
            BinarySequence huffmanData = tree.getBinaryRepresentation();
            EncodedFile enf = new EncodedFile("C:\\Users\\Owner\\Documents\\encodedTEST.hct", EncodedFile.CREATE_NEW); //get fp from user
            writeEncodedFile(enf, huffmanData, encoded);
            return enf;
        }

        //Given a particular encoded file, huffman data string, and message body
        //Writes the huffman data and message to the encoded file with the proper formatting
        private void writeEncodedFile(EncodedFile enf, BinarySequence huffmanData, BinarySequence body) {
            BinaryWriter writer = new BinaryWriter(File.Open(enf.Filepath, FileMode.Create));
            writeBinarySequence(huffmanData, writer, true);
            writeBinarySequence(body, writer, false);
        }

        //Given a sequence of binary data in the form of a BinarySequence, a writer, and a writeLength boolean
        //Writes the binary data, padded to the *right* with zeros, to the writer
        //If writeLength is true, the data is preceded by an unsigned byte value which represents the length of the data in bytes
        private void writeBinarySequence(BinarySequence data, BinaryWriter writer, Boolean writeLength) {
            int dataLength = data.getLength();
            int numExtraZeros = dataLength % 8;
            for (int i = 0; i < numExtraZeros; ++i) {
                data.concatenate(0);
            }
            byte[] dataBytes = data.ToByteArray();
            if (writeLength) {
                writer.Write((byte)(dataBytes.Length));
            }
            writer.Write(dataBytes);
        }
    }
}
