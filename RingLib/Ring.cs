using System;

namespace RingLib
{

    public class Ring<T>
    {
        private class Node
        {
            public T Data {get; set;}
            public Node Next {get; set;}
            public Node Prev {get; set;}
        }
        private Node Begin {get; set;}

        public Ring()
        {
        }

        public Ring(Ring<T> copy)
        {
            if (copy.Begin == null)
                return;
            Node t = new Node();
            Node c = copy.Begin;
            t.Data = c.Data;
            t.Next = t;
            t.Prev = t;
            Begin = t;
            while (c.Next != copy.Begin) {
                c = c.Next;
                t = new Node();
                t.Data = c.Data;
                t.Next = Begin;
                t.Prev = Begin.Prev;
                Begin.Prev.Next = t;
                Begin.Prev = t;
            }
        }

        public Ring(T[] a)
        {
            if (a.Length == 0)
                return;
            Node t = new Node();
            t.Data = a[0];
            t.Next = t;
            t.Prev = t;
            Begin = t;
            for (int i = 1; i < a.Length; i++) {
                t = new Node();
                t.Data = a[i];
                t.Next = Begin;
                t.Prev = Begin.Prev;
                Begin.Prev.Next = t;
                Begin.Prev = t;
            }
        }

        public static explicit operator Ring<T>(T[] a)
        {
            Ring<T> r = new Ring<T>(a);
            return r;
        }

        public static explicit operator T[](Ring<T> r)
        {
            int N = r.Size();
            T[] a = new T[N];
            Node t = r.Begin;
            for (int i = 0; i < N; i++) {
                a[i] = t.Data;
                t = t.Next;
            }
            return a;
        }

        public int Size()
        {
            int size = 0;
            if (Begin == null)
                return size;
            Node t = Begin;
            do {
                size++;
                t = t.Next;
            } while (t != Begin);
            return size;
        }

        public T Peek()
        {
            if (Begin == null)
                throw new NullReferenceException();
            return Begin.Data;
        }

        public static T operator<(Ring<T> r, T n)
        {
            Node t = new Node();
            t.Data = n;
            if (r.Begin == null) {
                t.Next = t;
                t.Prev = t;
                r.Begin = t;
            } else {
                t.Next = r.Begin;
                t.Prev = r.Begin.Prev;
                r.Begin.Prev.Next = t;
                r.Begin.Prev = t;
            }
            return n;
        }

        public static T operator>(Ring<T> r, T n)
        {
            if (r.Begin == null)
                throw new NullReferenceException();
            T res = r.Begin.Data;
            if (r.Begin == r.Begin.Next) {
                r.Begin = null;
            } else {
                r.Begin.Prev.Next = r.Begin.Next;
                r.Begin.Next.Prev = r.Begin.Prev;
                r.Begin = r.Begin.Next;
            }
            return res;
        }

        public static Ring<T> operator++(Ring<T> r)
        {
            if (r.Begin == null)
                throw new NullReferenceException();
            r.Begin = r.Begin.Next;
            return r;
        }

        public static Ring<T> operator--(Ring<T> r)
        {
            if (r.Begin == null)
                throw new NullReferenceException();
            r.Begin = r.Begin.Prev;
            return r;
        }

        public void Input()
        {
            int N = int.Parse(Console.ReadLine());
            T n;
            for (int i = 0; i < N; i++) {
                n = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
                n = this < n;
            }
        }

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            Node t = Begin;
            sb.Append("{ ");
            do {
                sb.Append(t.Data.ToString());
                sb.Append(", ");
                t = t.Next;
            } while (t != Begin);
            sb.Length -= 2;
            sb.Append(" }");
            return sb.ToString();
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Ring<T> r = (Ring<T>)obj;
            Node tnode = Begin;
            Node rnode = r.Begin;
            if (tnode == null || rnode == null)
                return false;
            do {
                if (!tnode.Data.Equals(rnode.Data))
                    return false;
                tnode = tnode.Next;
                rnode = rnode.Next;
            } while (tnode != Begin && rnode != r.Begin);
            if (tnode != Begin || rnode != r.Begin)
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public T this[int i]
        {
            get
            {
                if (Begin == null)
                    throw new NullReferenceException();
                Node t = Begin;
                for (int j = 0; j < i; j++)
                    t = t.Next;
                return t.Data;
            }
            set
            {
                if (Begin == null)
                    throw new NullReferenceException();
                Node t = Begin;
                for (int j = 0; j < i; j++)
                    t = t.Next;
                t.Data = value;
            }
        }

        public static bool operator==(Ring<T> a, Ring<T> b)
        {
            return Equals(a, b);
        }

        public static bool operator!=(Ring<T> a, Ring<T> b)
        {
            return !Equals(a, b);
        }

        public static bool operator true(Ring<T> r)
        {
            return r.Begin != null;
        }

        public static bool operator false(Ring<T> r)
        {
            return r.Begin == null;
        }

    }

} // namespace RingLib
