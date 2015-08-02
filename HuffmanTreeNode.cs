using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCompressor {
    class HuffmanTreeNode {

        private string charset = "";
        private HuffmanTreeNode left = null;
        private HuffmanTreeNode right = null;

        //general use constructor
        public HuffmanTreeNode(string charset, HuffmanTreeNode left, HuffmanTreeNode right) {
            this.charset = charset;
            this.left = left;
            this.right = right;
        }

        //constructs leaf node with specified charset
        public HuffmanTreeNode(string charset) {
            this.charset = charset;
            this.left = null;
            this.right = null;
        }

        //get/set
        public string Charset {
            get { return charset; }
            set { this.charset = value; }
        }

        public HuffmanTreeNode Left {
            get { return left; }
            set { this.left = value; }
        }

        public HuffmanTreeNode Right {
            get { return right; }
            set { this.right = value; }
        }
    }
}
