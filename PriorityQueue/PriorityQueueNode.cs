using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueue {
    class PriorityQueueNode<T> {

        private T data;
        private int priority;

        //constructor
        public PriorityQueueNode(T obj, int priority) {
            this.data = obj;
            this.priority = priority;
        }

        public T Data {
            get { return this.data; }
            set { this.data = value; }
        }

        public int Priority {
            get { return this.priority; }
            set { this.priority = value; }
        }
    }
}
