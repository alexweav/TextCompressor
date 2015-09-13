using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextCompressor;
using DataStructures;

namespace DataStructures_Test {
    [TestClass]
    public class PriorityQueue_Test {

#region enqueue_test

        [TestMethod]
        public void Enqueue_EnqueueToEmpty_GivesQueueOfLengthOne() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            Assert.AreEqual(1, p.length());
        }

        [TestMethod]
        public void Enqueue_EnqueueToNonEmpty_IncrementsLength() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            p.enqueue(67, 2);
            Assert.AreEqual(2, p.length());
        }

        [TestMethod]
        public void Enqueue_MultipleWithSamePriority_DoesNotCollide() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            p.enqueue(67, 1);
            Assert.AreEqual(2, p.length());
        }

#endregion

#region dequeueLowest_test

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DequeueLowest_EmptyQueue_ThrowsInvalidOperationException() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.dequeueLowest();
        }

        [TestMethod]
        public void DequeueLowest_QueueLengthOne_RemovesElement() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            p.dequeueLowest();
            Assert.AreEqual(0, p.length());
        }

        [TestMethod]
        public void DequeueLowest_QueueLengthOne_ReturnsElement() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            Assert.AreEqual(4, p.dequeueLowest());
        }

        [TestMethod]
        public void DequeueLowest_ValidQueue_ReturnsCorrectElement1() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            p.enqueue(57, 41);
            p.enqueue(14, -80);
            p.enqueue(16, 12);
            Assert.AreEqual(14, p.dequeueLowest());
        }

        [TestMethod]
        public void DequeueLowest_ValidQueue_ReturnsCorrectElement2() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 4);
            p.enqueue(57, 3);
            p.enqueue(14, 2);
            p.enqueue(16, 1);
            p.enqueue(19, 0);
            Assert.AreEqual(19, p.dequeueLowest());
        }

        [TestMethod]
        public void DequeueLowest_QueueWithCollisions_ReturnsCollidedElements() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 4);
            p.enqueue(57, 3);
            p.enqueue(14, 2);
            p.enqueue(16, 0);
            p.enqueue(19, 0);
            int element1 = p.dequeueLowest();
            int element2 = p.dequeueLowest();
            Assert.IsTrue(element1 == 16 || element1 == 19);
            Assert.IsTrue(element2 == 16 || element2 == 19);
            Assert.AreNotEqual(element1, element2);
        }

#endregion

#region dequeueHighest_test

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DequeueHighest_EmptyQueue_ThrowsInvalidOperationException() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.dequeueHighest();
        }

        [TestMethod]
        public void DequeueHighest_QueueLengthOne_RemovesElement() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            p.dequeueHighest();
            Assert.AreEqual(0, p.length());
        }

        [TestMethod]
        public void DequeueHighest_QueueLengthOne_ReturnsElement() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            Assert.AreEqual(4, p.dequeueHighest());
        }

        [TestMethod]
        public void DequeueHighest_ValidQueue_ReturnsCorrectElement1() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            p.enqueue(57, 41);
            p.enqueue(14, -80);
            p.enqueue(16, 12);
            Assert.AreEqual(57, p.dequeueHighest());
        }

        [TestMethod]
        public void DequeueHighest_ValidQueue_ReturnsCorrectElement2() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 0);
            p.enqueue(57, 1);
            p.enqueue(14, 2);
            p.enqueue(16, 3);
            p.enqueue(19, 4);
            Assert.AreEqual(19, p.dequeueHighest());
        }

        [TestMethod]
        public void DequeueHighest_QueueWithCollisions_ReturnsCollidedElements() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 4);
            p.enqueue(57, 3);
            p.enqueue(14, 2);
            p.enqueue(16, 5);
            p.enqueue(19, 5);
            int element1 = p.dequeueHighest();
            int element2 = p.dequeueHighest();
            Assert.IsTrue(element1 == 16 || element1 == 19);
            Assert.IsTrue(element2 == 16 || element2 == 19);
            Assert.AreNotEqual(element1, element2);
        }

#endregion

#region readHighest_test

#endregion

#region readLowest_test

#endregion

#region highestPriority_test

#endregion

#region lowestPriority_test

#endregion


    }
}
