using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCompressor.Source {
    class HuffmanTreeNode {

        private string charset = "";
        private HuffmanTreeNode left = null;
        private HuffmanTreeNode right = null;

        //constructor
        public HuffmanTreeNode(string charset, HuffmanTreeNode left, HuffmanTreeNode right) {
            this.charset = charset;
            this.left = left;
            this.right = right;
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
