using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using My;

namespace MyTest {

    [TestClass]
    public class StackTest {

        [TestMethod]
        public void Push_Number_Equals_Pop () {
            // Arrange
            float number = 1.5f;
            float expected = 1.5f;
            Stack s = new Stack();
            // Act
            s.Push(number);
            float actual = s.Pop();
            // Assert
            Assert.AreEqual(expected, actual, 0.001, "Push Pop values doesn't match");
        }

        [TestMethod]
        public void Push_Number_Equals_Peek () {
            // Arrange
            float number = 1.5f;
            float expected = 1.5f;
            Stack s = new Stack();
            // Act
            s.Push(number);
            float actual = s.Peek();
            // Assert
            Assert.AreEqual(expected, actual, 0.0001, "Push Peek values doesn't match");
        }

        [TestMethod]
        public void Pop_Empty_Stack_Should_Throw_NullReferenceException() {
            // Arrange
            Stack s = new Stack();

            //Act and Assert
            Assert.ThrowsException<System.NullReferenceException>(() => s.Pop());
        }

        [TestMethod]
        public void Peek_Empty_Stack_Should_Throw_NullReferenceException() {
            // Arrange
            Stack s = new Stack();

            //Act and Assert
            Assert.ThrowsException<System.NullReferenceException>(() => s.Peek());
        }

        [TestMethod]
        public void Pop_Full_Stack_Should_Remove_Element() {
            // Arrange
            float expected = 1.5f;
            Stack s = new Stack();
            s.Push(3.4f);
            s.Push(1.5f);
            s.Push(1.2f);
            // Act
            s.Pop();
            float actual = s.Pop();
            // Assert
            Assert.AreEqual(expected, actual, 0.0001, "Pop doesn't remove element");
        }

        [TestMethod]
        public void Peek_Full_Stack_Should_Not_Remove_Element() {
            // Arrange
            float expected = 1.5f;
            Stack s = new Stack();
            s.Push(3.4f);
            s.Push(1.2f);
            s.Push(1.5f);
            // Act
            s.Peek();
            float actual = s.Peek();
            // Assert
            Assert.AreEqual(expected, actual, 0.0001, "Peek removes element");
        }

        [TestMethod]
        public void Equals_Empty_Stacks_Return_True () {
            // Arrange
            bool expected = true;
            Stack a = new Stack();
            Stack b = new Stack();
            // Act
            bool actual = a.Equals(b);
            // Assert
            Assert.AreEqual(expected, actual, "Stacks are not equal");
    }

        [TestMethod]
        public void Equals_Stacks_With_Same_Data_Return_True () {
            // Arrange
            bool expected = true;
            Stack a = new Stack();
            a.Push(1.4f);
            a.Push(2.4f);
            a.Push(3.4f);
            Stack b = new Stack();
            b.Push(1.4f);
            b.Push(2.4f);
            b.Push(3.4f);
            // Act
            bool actual = a.Equals(b);
            // Assert
            Assert.AreEqual(expected, actual, "Stacks are not equal");
        }

        [TestMethod]
        public void Equals_Stacks_With_Different_Data_Same_Size_Return_False () {
            // Arrange
            bool expected = false;
            Stack a = new Stack();
            a.Push(1.4f);
            a.Push(2.4f);
            a.Push(4.4f);
            Stack b = new Stack();
            b.Push(1.4f);
            b.Push(2.4f);
            b.Push(3.4f);
            // Act
            bool actual = a.Equals(b);
            // Assert
            Assert.AreEqual(expected, actual, "Stacks are equal");
        }

        [TestMethod]
        public void Equals_Stacks_With_Different_Data_Different_Size_Return_False () {
            // Arrange
            bool expected = false;
            Stack a = new Stack();
            a.Push(1.4f);
            a.Push(2.4f);
            Stack b = new Stack();
            b.Push(1.4f);
            b.Push(2.4f);
            b.Push(3.4f);
            // Act
            bool actual = a.Equals(b);
            // Assert
            Assert.AreEqual(expected, actual, "Stacks are equal");
        }

        [TestMethod]
        public void Stack_Full_Array_Init_Data_Should_Equal_Array_Data () {
            // Arrange
            bool expected = true;
            float[] arr = { 1, 2, 3, 4, 5 };
            Stack s = new Stack(arr);
            // Act
            bool actual = true;
            for (int i = 0; i < arr.Length; ++i)
                if (arr[i] != s.Pop())
                    actual = false;
            // Assert
            Assert.AreEqual(expected, actual, "Stack data not equals array data");
        }

        [TestMethod]
        public void Stack_Empty_Array_Init_Should_Equal_Empty_Stack () {
            // Arrange
            bool expected = true;
            float[] arr = {};
            Stack a = new Stack(arr);
            Stack b = new Stack();
            // Act
            bool actual = a.Equals(b);
            // Assert
            Assert.AreEqual(expected, actual, "Stacks are not equal");
        }

    }
}
