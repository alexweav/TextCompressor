using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using PriorityQueue;

namespace TextCompressor {
    class HuffmanTree {

        private PriorityQueue<HuffmanTreeNode> topNodes;

        private HuffmanTreeNode head;

        //constructor
        public HuffmanTree(char[] charset, int[] weights) {
            populateQueue(charset, weights);
            buildHuffmanTree();
        }

        public HuffmanTree(string binary) {
            //TODO: build tree from binary representation
            Queue<char> bitStream = new Queue<char>();
            for (int i = 0; i < binary.Length; ++i) {
                bitStream.Enqueue(binary[i]);
            }
            head = buildTreeFromBinary(bitStream);
            populateSearchStrings(head);
        }

        //Fills the priority queue with the appropriate characters and respective weights
        private void populateQueue(char[] charset, int[] weights) {
            topNodes = new PriorityQueue<HuffmanTreeNode>();
            for (int i = 0; i < charset.Length; i++) {
                HuffmanTreeNode currentNode = new HuffmanTreeNode(Char.ToString(charset[i]));
                topNodes.enqueue(currentNode, weights[i]);
            }
        }

        //constructs a huffman tree from a priority queue of characters
        private void buildHuffmanTree() {
            while (topNodes.length() > 1) {
                int node1Priority = topNodes.lowestPriority();
                HuffmanTreeNode node1 = topNodes.dequeueLowest();
                int node2Priority = topNodes.lowestPriority();
                HuffmanTreeNode node2 = topNodes.dequeueLowest();
                HuffmanTreeNode newNode = new HuffmanTreeNode(node1.Charset + node2.Charset, node1, node2);
                topNodes.enqueue(newNode, node1Priority + node2Priority);
            }
            head = topNodes.dequeueHighest();
        }

        //Returns an array of all chars stored in the huffman tree
        public char[] getCharset() {
            string charString = head.Charset;
            return charString.ToCharArray();
        }

        //Traverses the huffman tree and obtains the huffman code for a given character
        //Returns null if the char does not exist in the tree
        public string getHuffmanCode(char symbol) {
            if(!getCharset().Contains(symbol)) {
                return null;
            }
            HuffmanTreeNode currentNode = head;
            string strSymbol = "" + symbol;
            string code = "";
            while (true) {
                if (currentNode.Charset.Equals(strSymbol)) {
                    return code;
                }
                if (currentNode.Left.Charset.Contains(symbol)) {    //traverse left
                    code += "0";
                    currentNode = currentNode.Left;
                } else {    //traverse right
                    code += "1";
                    currentNode = currentNode.Right;
                }
            }
        }

        //Writes the huffman tree to a binary string using the following algorithm:
        //For each node, starting at root:
        //If leaf node, output 1 + char
        //If not leaf node, output 0, and keep traversing
        //To read:
        //Read bit.  If 1, then read the character, then return a new leaf node
        //           If 0, decode left and right child nodes the same way, then return a new node with those children but no value
        //algorithm in better detail: http://stackoverflow.com/questions/759707/efficient-way-of-storing-huffman-tree
        //After reconstruction, empty nodes must be filled with correct charsets in order to make the tree traversable
        public string getBinaryRepresentation() {
            return buildBinaryString(head);
        }

        //process for getBinaryRepresentation()
        //getBinaryRepresentation() is a public interface method which hides the starting parameters of this recursive process from the user
        private string buildBinaryString(HuffmanTreeNode currentNode) {
            if (currentNode == null) {
                return "";
            }
            if (isLeaf(currentNode)) {
                byte[] bytes = Encoding.ASCII.GetBytes(currentNode.Charset);
                byte charValue = bytes[0];
                string binary = Convert.ToString(charValue, 2).PadLeft(7, '0');
                return "1" + binary;
            } else {
                return "0" + buildBinaryString(currentNode.Left) + buildBinaryString(currentNode.Right);
            }
        }

        //Returns whether a node is a leaf node or not
        private Boolean isLeaf(HuffmanTreeNode node) {
            return node.Left == null && node.Right == null;
        }

        public string[] getCodes(char[] charset) {
            int len = charset.Length;
            string[] codes = new string[len];
            for (int i = 0; i < len; i++) {
                codes[i] = getHuffmanCode(charset[i]);
            }
            return codes;
        }

        private HuffmanTreeNode buildTreeFromBinary(Queue<char> stream) {
            char bit = stream.Dequeue();
            if (bit == '1') {
                return new HuffmanTreeNode(getASCIIChar(stream), null, null);
            } else {
                HuffmanTreeNode leftChild = buildTreeFromBinary(stream);
                HuffmanTreeNode rightChild = buildTreeFromBinary(stream);
                return new HuffmanTreeNode("", leftChild, rightChild);
            }
        }

        private string getASCIIChar(Queue<char> stream) {
            string binary = "0";
            for (int i = 0; i < 7; i++) {
                binary += stream.Dequeue();
            }
            byte[] ch = { Convert.ToByte(binary, 2)};
            return Encoding.ASCII.GetString(ch);
        }

        private void populateSearchStrings(HuffmanTreeNode currentNode) {
            string leftCharset = currentNode.Left.Charset;
            string rightCharset = currentNode.Right.Charset;
            if (leftCharset == "") {
                populateSearchStrings(currentNode.Left);
                leftCharset = currentNode.Left.Charset;
            }
            if (rightCharset == "") {
                populateSearchStrings(currentNode.Right);
                rightCharset = currentNode.Right.Charset;
            }
            currentNode.Charset = leftCharset + rightCharset;
        }


    }
}
