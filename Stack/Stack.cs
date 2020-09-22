using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My {

    public class Stack {

        private class Node {
            public float data;
            public Node next;
        }
        private Node begin;

        public Stack() { }
        public Stack(float[] arr) { }
        public Stack(Stack s) { }

        public void Push(float value) { throw new Exception(); }
        public float Pop() { throw new Exception(); }
        public float Back() { throw new Exception(); }

        public override bool Equals(object obj) { throw new Exception(); }

        public void Print() { throw new Exception(); }
        public void Input() { throw new Exception(); }

        static void Main(string[] args)
        {
            Stack s = new Stack();
        }

    }
}
