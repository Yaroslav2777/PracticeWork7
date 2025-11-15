using System;
using System.Collections.Generic;

namespace Task1
{

    class GenericComparer<T> where T : IComparable<T>
    {

        public GenericComparer()
        {

        }

        public T FindSmaller(T a, T b)
        {
            if (a.CompareTo(b) < 0)
            {
                return a;
            }
            else
            {
                return b;
            }
        }
    }
    public class Locality
    {
        public string Name { get; set; }
        public int Population { get; set; }

        public Locality(string name, int population)
        {
            Name = name;
            Population = population;
        }

        public override string ToString()
        {
            return $"Місцевість: {Name}, Населення: {Population}";
        }
    }

    public class GenericList<T> where T : Locality
    {
        private class Node
        {
            public Node Next { get; set; }
            public T Data { get; set; }

            public Node(T t)
            {
                Next = null;
                Data = t;
            }
        }

        private Node head;

        public GenericList()
        {
            head = null;
        }

        public void AddHead(T t)
        {
            Node n = new Node(t);
            n.Next = head;
            head = n;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }


        public T FindFirstOccurrence(string name)
        {
            Node current = head;
            T t = null;

            while (current != null)
            {

                if (current.Data.Name == name)
                {
                    t = current.Data;
                    break;
                }
                else
                {
                    current = current.Next;
                }
            }
            return t;
        }
    }




    class Program
    {

        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }


        static (T min, T max) FindMinMax<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length == 0)
            {
                throw new ArgumentException("Масив не може бути порожнім.");
            }

            T min = array[0];
            T max = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].CompareTo(max) > 0)
                {
                    max = array[i];
                }
                if (array[i].CompareTo(min) < 0)
                {
                    min = array[i];
                }
            }

            return (min, max);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("--- Завдання 1: Універсальний метод Swap ---");


            int a = 1;
            int b = 2;
            Console.WriteLine($"До обміну (int): a={a}, b={b}");
            Swap<int>(ref a, ref b);
            Console.WriteLine($"Після обміну (int): a={a}, b={b}");

            double d1 = 5.75;
            double d2 = 10.2;
            Console.WriteLine($"\nДо обміну (double): d1={d1}, d2={d2}");
            Swap<double>(ref d1, ref d2); 
            Console.WriteLine($"Після обміну (double): d1={d1}, d2={d2}");

            string s1 = "Привіт";
            string s2 = "Світ";
            Console.WriteLine($"\nДо обміну (string): s1=\"{s1}\", s2=\"{s2}\"");
            Swap(ref s1, ref s2);
            Console.WriteLine($"Після обміну (string): s1=\"{s1}\", s2=\"{s2}\"");



            Console.WriteLine("\n--- Завдання 2: Універсальний метод Min/Max ---");
            int[] intArray = { 5, 10, 2, 8, -1, 15 };
            var (intMin, intMax) = FindMinMax(intArray);
            Console.WriteLine($"Масив (int): {string.Join(", ", intArray)}");
            Console.WriteLine($"Мін: {intMin}, Макс: {intMax}");
            double[] doubleArray = { 3.14, 2.71, 0.5, -5.0, 9.8 };
            var (doubleMin, doubleMax) = FindMinMax(doubleArray);
            Console.WriteLine($"\nМасив (double): {string.Join(", ", doubleArray)}");
            Console.WriteLine($"Мін: {doubleMin}, Макс: {doubleMax}");


            Console.WriteLine("\n--- Завдання 3: Універсальний клас (менше з двох) ---");

            GenericComparer<int> intComparer = new GenericComparer<int>();
            int smallerInt = intComparer.FindSmaller(10, 20);
            Console.WriteLine($"Менше з (10, 20): {smallerInt}");
            GenericComparer<double> doubleComparer = new GenericComparer<double>();
            double smallerDouble = doubleComparer.FindSmaller(15.5, 15.1);
            Console.WriteLine($"Менше з (15.5, 15.1): {smallerDouble}");

            Console.WriteLine("\n--- Завдання 4: Універсальний список <Locality> ---");

            GenericList<Locality> localityList = new GenericList<Locality>();
            localityList.AddHead(new Locality("Київ", 2800000));
            localityList.AddHead(new Locality("Львів", 720000));
            localityList.AddHead(new Locality("Одеса", 1010000));

            Console.WriteLine("\nВміст списку місцевостей:");
            foreach (Locality loc in localityList)
            {
                Console.WriteLine(loc.ToString());
            }
            string searchName = "Львів";
            Locality foundLocality = localityList.FindFirstOccurrence(searchName);

            if (foundLocality != null)
            {
                Console.WriteLine($"\nЗнайдено '{searchName}': {foundLocality}");
            }
            else
            {
                Console.WriteLine($"\nМісцевість '{searchName}' не знайдено.");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}
