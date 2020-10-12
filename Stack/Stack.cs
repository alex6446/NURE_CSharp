using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace My {

    public class Stack {

        public class Node {
            public float Data { get; set; }
            public Node Next { get; set; }
        }
        
        public Node Begin { get; set; }

        public Stack() { }
        
        public Stack(float[] arr) {
            foreach (var item in arr) {
                Node temp = new Node();
                temp.Data = item;
                temp.Next = Begin;
                Begin = temp;
            }
        }

        public Stack(Stack s) {
            Node snode = s.Begin;
            while (snode != null) {
                Node temp = new Node();
                temp.Data = snode.Data;
                temp.Next = Begin;
                Begin = temp;
                snode = snode.Next;
            } 
        }

        public void Push(float value) {
            Node temp = new Node();
            temp.Data = value;
            temp.Next = Begin;
            Begin = temp;
        }

        public float Pop() {
            if (Begin == null)
                throw new NullReferenceException();
            float res = Begin.Data;
            Begin = Begin.Next;
            return res;
        }

        public float Peek() {
            if (Begin == null)
                throw new NullReferenceException();
            return Begin.Data;
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Stack s = (Stack)obj;
            Node tnode = Begin;
            Node snode = s.Begin;
            while (tnode != null && snode != null) {
                if (tnode.Data != snode.Data)
                    return false;
                tnode = tnode.Next;
                snode = snode.Next;
            }
            if (tnode != null || snode != null)
                return false;
            return true;
        }

        public void Print() {
            Node tnode = Begin;
            while (tnode != null) {
                Console.WriteLine("{0} ", tnode.Data);
                tnode = tnode.Next;
            }
        }

        public void Input() {
            Begin = null;
            int N = int.Parse(Console.ReadLine());
            for (int i = 0; i < N; ++i) {
                Node temp = new Node();
                temp.Data = float.Parse(Console.ReadLine());
                temp.Next = Begin;
                Begin = temp;
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
