using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCompressor {
    class TextFile {

        private string filepath;

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
            } catch (Exception e) {
                return null;
            }
        }

        public EncodedFile compress() {
            EncodedFile encoded = null;
            HuffmanManager compressor = new HuffmanManager();
            return encoded;
        }

    }
}
