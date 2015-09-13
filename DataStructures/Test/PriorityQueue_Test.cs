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

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ReadHighest_EmptyQueue_ThrowsInvalidOperationException() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.readHighest();
        }

        [TestMethod]
        public void ReadHighest_QueueLengthOne_DoesNotRemoveElement() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            p.readHighest();
            Assert.AreEqual(1, p.length());
        }

        [TestMethod]
        public void ReadHighest_QueueLengthOne_ReturnsElement() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            Assert.AreEqual(4, p.readHighest());
        }

        [TestMethod]
        public void ReadHighest_ValidQueue_ReturnsCorrectElement1() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            p.enqueue(57, 41);
            p.enqueue(14, -80);
            p.enqueue(16, 12);
            Assert.AreEqual(57, p.readHighest());
        }

        [TestMethod]
        public void ReadHighest_ValidQueue_ReturnsCorrectElement2() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 0);
            p.enqueue(57, 1);
            p.enqueue(14, 2);
            p.enqueue(16, 3);
            p.enqueue(19, 4);
            Assert.AreEqual(19, p.readHighest());
        }

        [TestMethod]
        public void ReadHighest_QueueWithCollisions_ReturnsCollidedElements() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 4);
            p.enqueue(57, 3);
            p.enqueue(14, 2);
            p.enqueue(16, 5);
            p.enqueue(19, 5);
            int element = p.readHighest();
            Assert.IsTrue(element == 16 || element == 19);
        }
#endregion

#region readLowest_test

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ReadLowest_EmptyQueue_ThrowsInvalidOperationException() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.readLowest();
        }

        [TestMethod]
        public void ReadLowest_QueueLengthOne_DoesNotRemoveElement() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            p.readLowest();
            Assert.AreEqual(1, p.length());
        }

        [TestMethod]
        public void ReadLowest_QueueLengthOne_ReturnsElement() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            Assert.AreEqual(4, p.readLowest());
        }

        [TestMethod]
        public void ReadLowest_ValidQueue_ReturnsCorrectElement1() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            p.enqueue(57, 41);
            p.enqueue(14, -80);
            p.enqueue(16, 12);
            Assert.AreEqual(14, p.readLowest());
        }

        [TestMethod]
        public void ReadLowest_ValidQueue_ReturnsCorrectElement2() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 4);
            p.enqueue(57, 3);
            p.enqueue(14, 2);
            p.enqueue(16, 1);
            p.enqueue(19, 0);
            Assert.AreEqual(19, p.readLowest());
        }

        [TestMethod]
        public void ReadLowest_QueueWithCollisions_ReturnsCollidedElements() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 4);
            p.enqueue(57, 3);
            p.enqueue(14, 2);
            p.enqueue(16, 0);
            p.enqueue(19, 0);
            int element = p.readLowest();
            Assert.IsTrue(element == 16 || element == 19);
        }

#endregion

#region highestPriority_test

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void HighestPriority_EmptyQueue_ThrowsInvalidOperationException() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.highestPriority();
        }

        [TestMethod]
        public void HighestPriority_QueueLengthOne_DoesNotRemoveElement() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            p.highestPriority();
            Assert.AreEqual(1, p.length());
        }

        [TestMethod]
        public void HighestPriority_QueueLengthOne_ReturnsHighestPriority() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            Assert.AreEqual(1, p.highestPriority());
        }

        [TestMethod]
        public void HighestPriority_ValidQueue_ReturnsCorrectPriority1() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            p.enqueue(57, 41);
            p.enqueue(14, -80);
            p.enqueue(16, 12);
            Assert.AreEqual(41, p.highestPriority());
        }

        [TestMethod]
        public void HighestPriority_ValidQueue_ReturnsCorrectPriority2() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 0);
            p.enqueue(57, 1);
            p.enqueue(14, 2);
            p.enqueue(16, 3);
            p.enqueue(19, 4);
            Assert.AreEqual(4, p.highestPriority());
        }

        [TestMethod]
        public void HighestPriority_QueueWithCollisions_ReturnsCorrectPriority() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 4);
            p.enqueue(57, 3);
            p.enqueue(14, 2);
            p.enqueue(16, 5);
            p.enqueue(19, 5);
            Assert.AreEqual(5, p.highestPriority());
        }

#endregion

#region lowestPriority_test

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LowestPriority_EmptyQueue_ThrowsInvalidOperationException() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.lowestPriority();
        }

        [TestMethod]
        public void LowestPriority_QueueLengthOne_DoesNotRemoveElement() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            p.lowestPriority();
            Assert.AreEqual(1, p.length());
        }

        [TestMethod]
        public void LowestPriority_QueueLengthOne_ReturnsLowestPriority() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            Assert.AreEqual(1, p.lowestPriority());
        }

        [TestMethod]
        public void LowestPriority_ValidQueue_ReturnsCorrectPriority1() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 1);
            p.enqueue(57, 41);
            p.enqueue(14, -80);
            p.enqueue(16, 12);
            Assert.AreEqual(-80, p.lowestPriority());
        }

        [TestMethod]
        public void LowestPriority_ValidQueue_ReturnsCorrectPriority2() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 4);
            p.enqueue(57, 3);
            p.enqueue(14, 2);
            p.enqueue(16, 1);
            p.enqueue(19, 0);
            Assert.AreEqual(0, p.lowestPriority());
        }

        [TestMethod]
        public void LowestPriority_QueueWithCollisions_ReturnsCorrectPriority() {
            PriorityQueue<int> p = new PriorityQueue<int>();
            p.enqueue(4, 4);
            p.enqueue(57, 3);
            p.enqueue(14, 2);
            p.enqueue(16, 0);
            p.enqueue(19, 0);
            Assert.AreEqual(0, p.lowestPriority());
        }

#endregion


    }
}
