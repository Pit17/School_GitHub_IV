using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LinkedList
{

    class LinkedList
    {
        private class Node
        {
            // Essendo "private" l'intera class, posso usare "public" per i campi
            public int value;
            public Node next;
        }

        private Node head;
        public LinkedList()
        {
            head = null;
        }
        public void PrintList()
        {
            Node curr = head;
            for (int i = 0;curr != null; i++)
            {
                Console.Write(curr.value + ", ");
                curr = curr.next;
            }

        }

        public int Count 
        { 
            get 
            {
                int count = 0;
                for (Node curr = head; curr != null; curr = curr.next)
                    ++count;
                return count;
            }
        }
        public int this[int idx] 
        {
            get
            {

               Node curr = head;
                for (int i = 0; i <= idx && curr != null; ++i)
                    curr = curr.next;

                if (curr == null)
                    throw new IndexOutOfRangeException();

                return curr.value;
            }
            set
            {
                Node curr = head;
                for (int i = 0; i <= idx && curr != null; ++i)
                    curr = curr.next;

                if (curr == null)
                    throw new IndexOutOfRangeException();

                curr.value=value;

            } 
        
        }

        public void Add(int value)
        {
            // Crea il nuovo nodo
            Node n = new Node();
            n.value = value;
            n.next = null;

            if (head == null)  // lista vuota?
            {
                head = n;
            }
            else
            {
                Node prev = head;
                while (prev.next != null)
                    prev = prev.next;

                prev.next = n;
            }
        }
        public void RemoveAt(int idx)
        {

            Node node = head;
            if (idx == 0 ) head = node.next;
            for(int i = 0; i < idx && node != null; ++i)
            {
                if (i != idx-1) node = node.next;

            }
            if (node.next == null) throw new IndexOutOfRangeException();
            Node next = node.next;
            if (next.next == null)
            {
                node.next = null;
            }
            else
            {
                node.next = next.next;
            }            

        }
        public void RemoveValue(int value)
        {
            Node node = head;
            for (int i = 0; node != null; ++i)
            {
                if (node.value == value)RemoveAt(i);
                node = node.next;
            }
        }
        public int Search(int value)
        {
            int i = 0;
            for (Node curr = head; curr != null; curr = curr.next, ++i)
            {
                if (curr.value == value)
                    return i;
            }

            return -1;
        }
    }
    class ArrayList
    {
        private int[] data;
        private int count;
        public ArrayList(int capacity)
        {
            data = new int[capacity];
            count = 0;
        }

        public int Count { get { return count; } }
        public int this[int idx]
        {
            get
            {
                if (idx < 0 || count < idx)
                    throw new IndexOutOfRangeException();
                return data[idx];
            }
            set
            {
                if (idx < 0 || count < idx)
                    throw new IndexOutOfRangeException();
                data[idx] = value;
            }
        }
        public void Add(int value)
        {
            if (count == data.Length)
                Realloc(2 * data.Length);

            data[count++] = value;
        }
        public void RemoveAt(int idx)
        {
            if (idx < 0 || count < idx)
                throw new IndexOutOfRangeException();
            ShiftLeft(idx);
        }
        public void RemoveValue(int value)
        {
            int idx = Search(value);
            if (idx >= 0)
                RemoveAt(idx);
        }
        public int Search(int value)
        {
            for (int i = 0; i < count; ++i)
            {
                if (data[i] == value)
                    return i;
            }

            return -1;
        }
        

        // i metodi che seguono sono stati presi da https://classroom.google.com/c/NjI0MDAwODEyNDMx/m/NjcyOTQ3NjgwMjM5/details
        private void Realloc(int new_capacity)
        {
            int[] new_data = new int[new_capacity];
            int idx_max = Math.Min(data.Length, new_data.Length);
            for (int i = 0; i < idx_max; ++i)
                new_data[i] = data[i];
            data = new_data;
        }
        private void ShiftRight(int idx)
        {
            if (idx < 0 || count < idx)
                throw new IndexOutOfRangeException();
            if (count == data.Length)
                Realloc(2 * data.Length);
            int move_count = count - idx; // numero di elementi da spostare
            for (int k = move_count; k > 0; --k)
                data[idx + k] = data[idx + k - 1];
            ++count;
        }
        private void ShiftLeft(int idx)
        {
            if (idx < 0 || count <= idx)
                throw new IndexOutOfRangeException();
            int move_count = count - idx - 1; // numero di elementi da spostare
            for (int k = 0; k < move_count; ++k)
                data[idx + k] = data[idx + k + 1];
            --count;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList linkedList = new LinkedList();
            linkedList.Add(5);
            linkedList.Add(3);
            linkedList.Add(-1);
            linkedList.Add(12);
            linkedList.PrintList();
            Console.WriteLine();
            linkedList.RemoveAt(2);
            linkedList.RemoveValue(5);
            linkedList.PrintList();


        }
    }
}
