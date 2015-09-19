using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataStructures;

namespace TextCompressor {
    public class EncodedFile {

        private string filepath;

        private int BYTE_LENGTH = 8;

        public const Boolean CREATE_NEW = true;
        public const Boolean USE_EXISTING = false;

        //constructor
        public EncodedFile(string filepath, Boolean creatingNew) {
            if (isValidFilepath(filepath)) {
                this.filepath = filepath;
            }
            else {
                if (creatingNew) {
                    File.Create(filepath).Close();
                    this.filepath = filepath;
                } else {
                    throw new ArgumentException("Filepath is not valid.");
                }
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
                    File.Create(value).Close();
                    this.filepath = value;
                }
            }
        }

        //Returns true if the given string is a valid filepath on the current machine
        //Returns false otherwise
        private bool isValidFilepath(string filepath) {
            return File.Exists(filepath);
        }

        //Returns the entire file as a byte array
        public byte[] readFile() {
            return File.ReadAllBytes(filepath);
        }

        //Decodes the file and returns the results
        //Desperately in need of refactorization
        public string decodeFile() {
            byte[] file = readFile();
            byte huffmanDataLength = file[0];
            int fileStartIndex = huffmanDataLength + 1;
            HuffmanTree tree = getTreeFromFile(file, huffmanDataLength);
            Dictionary<BinarySequence, char> codeTable = getCodeDictionary(tree);
            byte[] textData = new byte[file.Length - huffmanDataLength - 1];
            Array.Copy(file, fileStartIndex, textData, 0, textData.Length);
            string text = getText(new BinaryStream(textData), codeTable);
            return text;
        }

        //Reads the huffman data at the beginning of the file, and returns the corresponding huffman tree
        private HuffmanTree getTreeFromFile(byte[] file, byte huffmanDataLength) {
            byte[] hData = new byte[huffmanDataLength];
            Array.Copy(file, 1, hData, 0, huffmanDataLength);
            BinaryStream huffmanData = new BinaryStream(hData);
            HuffmanTree tree = new HuffmanTree(huffmanData);
            return tree;
        }

        //Produces a lookup dictionary which maps a given encoded binary symbol, in string format to the symbol's corresponding ASCII character
        /*private Dictionary<string, char> getCodeDictionary(HuffmanTree tree) {
            Dictionary<string, char> table = new Dictionary<string, char>();
            char[] charset = tree.getCharset();
            for (int i = 0; i < charset.Length; ++i) {
                try {
                    table.Add(tree.getHuffmanCode(charset[i]), charset[i]);
                } catch {
                    continue;   //...
                }
            }
            return table;
        }*/
        //TODO: Complete. Requires a new tree.getHuffmanCode() function
        private Dictionary<BinarySequence, char> getCodeDictionary(HuffmanTree tree) {
            Dictionary<BinarySequence, char> table = new Dictionary<BinarySequence, char>();
            char[] charset = tree.getCharset();
            for (int i = 0; i < charset.Length; ++i) {
                try {
                    table.Add(tree.getHuffmanCode(charset[i]), charset[i]);
                } catch {
                    continue; //...
                }
            }
            return table;
        }

        //Takes a binary string of the main data portion of an encoded file
        //Takes a lookup table
        //Returns the decoded string with respect to the given lookup table
        private string getText(string textData, Dictionary<string, char> table) {
            int captureSize = 1;
            int index = 0;
            string text = "";
            char converted = '0';
            while (true) {
                try {
                    string sub = textData.Substring(index, captureSize);
                    if (table.TryGetValue(sub, out converted)) {
                        text += converted;
                        index = index + captureSize;
                        captureSize = 1;
                    } else {
                        ++captureSize;
                    }
                } catch {
                    break;
                }
            }
            return text;
        }

        //TODO: Complete. Requires a new code dictionary item in this.getCodeDictionary()
        //and this.decodeFile()
        private string getText(BinaryStream textData, Dictionary<BinarySequence, char> table) {
            string text = "";
            BinarySequence code = new BinarySequence();
            char resultChar;
            while (!textData.isEndOfStream()) {
                if (table.TryGetValue(code, out resultChar)) {
                    text += resultChar;
                    code = new BinarySequence();
                } else {
                    code.concatenate(textData.readBit());
                }
            }
            return text;
        }






    }
}
