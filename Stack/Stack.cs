using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace My {

    public class Stack {

        private class Node {
            public float data;
            public Node next;
        }
        private Node begin;
        Node Begin { get; set; }

        public Stack() { }
        
        public Stack(float[] arr) {
            foreach (var item in arr) {
                Node temp = new Node();
                temp.data = item;
                temp.next = begin;
                begin = temp;
            }
        }

        public Stack(Stack s) {
            Node snode = s.begin;
            while (snode != null) {
                Node temp = new Node();
                temp.data = snode.data;
                temp.next = begin;
                begin = temp;
                snode = snode.next;
            } 
        }

        public void Push(float value) {
            Node temp = new Node();
            temp.data = value;
            temp.next = begin;
            begin = temp;
        }

        public float Pop() {
            if (begin == null)
                throw new NullReferenceException();
            float res = begin.data;
            begin = begin.next;
            return res;
        }

        public float Peek() {
            if (begin == null)
                throw new NullReferenceException();
            return begin.data;
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Stack s = (Stack)obj;
            Node tnode = begin;
            Node snode = s.begin;
            while (tnode != null && snode != null) {
                if (tnode.data != snode.data)
                    return false;
                tnode = tnode.next;
                snode = snode.next;
            }
            if (tnode != null || snode != null)
                return false;
            return true;
        }

        public void Print() {
            Node tnode = begin;
            while (tnode != null) {
                Console.WriteLine("{0} ", tnode.data);
                tnode = tnode.next;
            }
        }

        public void Input() {
            begin = null;
            int N = int.Parse(Console.ReadLine());
            for (int i = 0; i < N; ++i) {
                Node temp = new Node();
                temp.data = float.Parse(Console.ReadLine());
                temp.next = begin;
                begin = temp;
            }
        }

        static void Main(string[] args)
        {
            float[] arr = { 1, 2, 3 };
            Stack s = new Stack(arr);
            s.Print();
            s.Input();
            s.Print();
            Console.Read();
        }

    }
}
