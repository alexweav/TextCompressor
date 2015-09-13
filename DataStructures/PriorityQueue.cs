using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures {
    public class PriorityQueue<T> {

        private List<PriorityQueueNode<T>> data;

        //constructor
        public PriorityQueue() {
            data = new List<PriorityQueueNode<T>>();
        }

        //Adds an element to the priority queue with a specified integer priority
        public void enqueue(T element, int priority) {
            if (isEmpty()) {
                data.Add(new PriorityQueueNode<T>(element, priority));
            } else {
                int len = this.data.Count;
                for (int i = 0; i < len; i++) {
                    if (priority <= data[i].Priority) {
                        data.Insert(i, new PriorityQueueNode<T>(element, priority));
                        return;
                    }
                }
                data.Add(new PriorityQueueNode<T>(element, priority));
            }
        }

        //Removes the element with the highest priority in the queue, returns that element
        public T dequeueLowest() {
            if (isEmpty()) {
                throw new InvalidOperationException("Cannot dequeue from an empty queue.");
            }
            T output = data[0].Data;
            data.RemoveAt(0);
            return output;
        }

        //Removes the element with the lowest priority in the queue, returns that element
        public T dequeueHighest() {
            if (isEmpty()) {
                throw new InvalidOperationException("Cannot dequeue from an empty queue.");
            }
            int len = this.data.Count;
            T output = data[len - 1].Data;
            data.RemoveAt(len - 1);
            return output;
        }

        //Returns the element with the highest priority in the queue, without removing it
        public T readHighest() {
            if (!isEmpty()) {
                int len = this.data.Count;
                return data[len - 1].Data;
            }
            return default(T);
        }

        //Returns the element with the lowest priority in the queue, without removing it
        public T readLowest() {
            if (!isEmpty()) {
                return data[0].Data;
            }
            return default(T);
        }

        //Returns the priority of the highest priority element in the queue
        public int highestPriority() {
            if (!isEmpty()) {
                int len = this.data.Count;
                return data[len - 1].Priority;
            }
            return 0;
        }

        //Returns the priority of the lowest priority element in the queue
        public int lowestPriority() {
            if (!isEmpty()) {
                return data[0].Priority;
            }
            return 0;
        }
        
        //Returns whether the queue is empty or not
        public Boolean isEmpty() {
            return !this.data.Any();
        }

        //Returns the length of the priority queue
        public int length() {
            return this.data.Count;
        }
    }
}
