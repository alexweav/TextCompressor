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

        public const Boolean CREATE_NEW = true;
        public const Boolean USE_EXISTING = false;

        //constructor
        public EncodedFile(string filepath, Boolean creatingNew) {
            if (isValidFilepath(filepath)) {
                this.filepath = filepath;
            }
            else {
                if (creatingNew) {
                    File.Create(filepath);
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

        public byte[] readFile() {
            return File.ReadAllBytes(filepath);
        }

        public void decodeFile() {
            byte[] file = readFile();
            byte huffmanDataLength = file[0];
            int fileStartIndex = huffmanDataLength + 1;
            HuffmanTree tree = getTreeFromFile(file, huffmanDataLength);
            Console.WriteLine(tree.getHuffmanCode(','));           //TEST TEST TEST
            Dictionary<string, char> codeTable = getCodeDictionary(tree);
            byte[] textData = new byte[file.Length - huffmanDataLength - 1];
            Array.Copy(file, fileStartIndex, textData, 0, textData.Length);
            string textDataStr = "";
            for (int i = 0; i < textData.Length; ++i) {
                textDataStr += Convert.ToString(textData[i], 2).PadLeft(8, '0');
            }
            string text = getText(textDataStr, codeTable);
            Console.WriteLine(text);
        }

        private HuffmanTree getTreeFromFile(byte[] file, byte huffmanDataLength) {
            byte[] hData = new byte[huffmanDataLength];
            Array.Copy(file, 1, hData, 0, huffmanDataLength);
            string huffmanData = "";
            for (int i = 0; i < hData.Length; ++i) {
                huffmanData += Convert.ToString(hData[i], 2).PadLeft(8, '0');
            }
            HuffmanTree tree = new HuffmanTree(huffmanData);
            return tree;
        }

        private Dictionary<string, char> getCodeDictionary(HuffmanTree tree) {
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
        }

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





    }
}
