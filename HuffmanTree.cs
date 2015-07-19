using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PriorityQueue;

namespace TextCompressor {
    class HuffmanTree {

        private PriorityQueue<HuffmanTreeNode> topNodes;

        private HuffmanTreeNode head;

        public HuffmanTree(char[] charset, int[] weights) {
            populateQueue(charset, weights);
            buildHuffmanTree();
        }

        private void populateQueue(char[] charset, int[] weights) {
            topNodes = new PriorityQueue<HuffmanTreeNode>();
            for (int i = 0; i < charset.Length; i++) {
                HuffmanTreeNode currentNode = new HuffmanTreeNode(Char.ToString(charset[i]));
                topNodes.enqueue(currentNode, weights[i]);
            }
        }

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

        public char[] getCharset() {
            string charString = head.Charset;
            return charString.ToCharArray();
        }

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

    }
}
