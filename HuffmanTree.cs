using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using DataStructures;

namespace TextCompressor {
    public class HuffmanTree {

        private PriorityQueue<HuffmanTreeNode> topNodes;

        private HuffmanTreeNode head;

        //constructor
        public HuffmanTree(char[] charset, int[] weights) {
            if (charset.Length != weights.Length) {
                throw new ArgumentException("Must be an equal number of characters and frequencies.");
            }
            populateQueue(charset, weights);
            buildHuffmanTree();
        }

        public HuffmanTree(BinaryStream binary) {
            head = buildTreeFromBinary(binary);
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
            try {
                while (topNodes.length() > 1) {
                    int node1Priority = topNodes.lowestPriority();
                    HuffmanTreeNode node1 = topNodes.dequeueLowest();
                    int node2Priority = topNodes.lowestPriority();
                    HuffmanTreeNode node2 = topNodes.dequeueLowest();
                    HuffmanTreeNode newNode = new HuffmanTreeNode(node1.Charset + node2.Charset, node1, node2);
                    topNodes.enqueue(newNode, node1Priority + node2Priority);
                }
                head = topNodes.dequeueHighest();
            } catch (InvalidOperationException) {
                head = null;
            }
        }

        //Returns an array of all chars stored in the huffman tree
        public char[] getCharset() {
            if (head == null) {
                return new char[0];
            }
            string charString = head.Charset;
            return charString.ToCharArray();
        }

        //Traverses the huffman tree and obtains the huffman code for a given character
        public BinarySequence getHuffmanCode(char symbol) {
            if (!getCharset().Contains(symbol)) {
                throw new ArgumentException("That symbol is not a member of the given charset of this Huffman tree.");
            }
            if (getCharset().Length == 1) {
                BinarySequence c = new BinarySequence();
                c.concatenate(1);
                return c;
            }
            HuffmanTreeNode currentNode = head;
            string strSymbol = "" + symbol;
            BinarySequence code = new BinarySequence();
            while (true) {
                if (currentNode.Charset.Equals(strSymbol)) {
                    return code;
                }
                if (currentNode.Left.Charset.Contains(symbol)) {
                    code.concatenate(0);
                    currentNode = currentNode.Left;
                } else {
                    code.concatenate(1);
                    currentNode = currentNode.Right;
                }
            }
        }

        //Writes the huffman tree to a binary string using the following algorithm:
        //For each node, starting at root:
        //If leaf node, output 1 + char
        //If not leaf node, output 0, and keep traversing in postorder
        //To read:
        //Read bit.  If 1, then read the character, then return a new leaf node
        //           If 0, decode left and right child nodes the same way, then return a new node with those children but no value
        //algorithm in better detail: http://stackoverflow.com/questions/759707/efficient-way-of-storing-huffman-tree
        //After reconstruction, empty nodes must be filled with correct charsets in order to make the tree traversable
        public string getBinaryRepresentation_s() {
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

        //Similar to getBinaryRepresentation_s(), but returns the binary in the form of a BinarySequence rather than a string
        public BinarySequence getBinaryRepresentation() {
            return buildBinarySequence(head);
        }

        //BinarySequence based parallel to buildBinaryString(node)
        private BinarySequence buildBinarySequence(HuffmanTreeNode currentNode) {
            if (currentNode == null) {
                return new BinarySequence();
            }
            if (isLeaf(currentNode)) {
                byte[] bytes = Encoding.ASCII.GetBytes(currentNode.Charset);
                byte charValue = bytes[0];
                BinarySequence charBinary = new BinarySequence(charValue);
                BinarySequence b = new BinarySequence("1");
                b.Append(charBinary);
                return b;
            } else {
                BinarySequence b = new BinarySequence("0");
                BinarySequence s1 = buildBinarySequence(currentNode.Left);
                BinarySequence s2 = buildBinarySequence(currentNode.Right);
                b.Append(s1);
                b.Append(s2);
                return b;
            }
        }

        //Returns whether a node is a leaf node or not
        private Boolean isLeaf(HuffmanTreeNode node) {
            return node.Left == null && node.Right == null;
        }

        //Given an array of characters, retrieves the corresponding encoded binary strings in an array
        //The nth string of the output is the encoded form of the nth character of the input
        public BinarySequence[] getCodes(char[] charset) {
            int len = charset.Length;
            BinarySequence[] codes = new BinarySequence[len];
            for (int i = 0; i < len; i++) {
                try {
                    codes[i] = getHuffmanCode(charset[i]);
                } catch (ArgumentException) {
                    throw new ArgumentException("A character was found in the given charset that is not in this Huffman tree.");
                }
            }
            return codes;
        }

        //Takes the encoded huffman tree data from the beginning of an encoded document as a binary stream
        //Populates the huffman tree from given data
        private HuffmanTreeNode buildTreeFromBinary(BinaryStream stream) {
            byte bit = stream.readBit();
            if (bit == 1) {
                return new HuffmanTreeNode(getASCIIChar(stream), null, null);
            } else {
                HuffmanTreeNode leftChild = buildTreeFromBinary(stream);
                HuffmanTreeNode rightChild = buildTreeFromBinary(stream);
                return new HuffmanTreeNode("", leftChild, rightChild);
            }
        }

        //Reads a single ASCII character from the given binary stream and returns it in a string
        private string getASCIIChar(BinaryStream stream) {
            byte bCh = 0;
            for (int i = 1; i < 8; ++i) {
                byte bit = stream.readBit();
                bit = (byte)(bit << 7-i);
                bCh = (byte)(bCh | bit);
            }
            byte[] ch = { bCh };
            return Encoding.ASCII.GetString(ch);
        }

        //When a tree is constructed from binary string data, the charsets of any non-leaf node are empty string rather than 
        //the collected charsets of all its descendants. 
        //Non-leaf nodes need to have charsets so that the tree is quickly searchable
        //This populates all such nodes with their proper charsets and fixes the problem
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

        //Returns a dictionary which maps every char in the tree's charset to its corresponding code in the form of a BinarySequence
        public Dictionary<char, BinarySequence> getEncodingTable() {
            char[] charset = getCharset();
            Dictionary<char, BinarySequence> table = new Dictionary<char, BinarySequence>();
            for (int i = 0; i < charset.Length; ++i) {
                table.Add(charset[i], getHuffmanCode(charset[i]));
            }
            return table;
        }


    }
}
