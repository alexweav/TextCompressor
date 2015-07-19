﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueue {
    class PriorityQueue<T> {

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
        public T dequeueHighest() {
            if (!isEmpty()) {
                T output = data[0].Data;
                data.RemoveAt(0);
                return output;
            }
            return default(T);
        }

        //Removes the element with the lowest priority in the queue, returns that element
        public T dequeueLowest() {
            if (!isEmpty()) {
                int len = this.data.Count;
                T output = data[len - 1].Data;
                data.RemoveAt(len - 1);
                return output;
            }
            return default(T);
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

        //Returns whether the queue is empty or not
        public Boolean isEmpty() {
            return !this.data.Any();
        }
    }
}