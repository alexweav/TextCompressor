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
    }
}
