using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCompressor.Source {
    class TextFile {

        private String filepath;

        //Constructor
        public TextFile(String filepath) {
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
        private bool isValidFilepath(String filepath) {
            return File.Exists(filepath);
        }


    }
}
