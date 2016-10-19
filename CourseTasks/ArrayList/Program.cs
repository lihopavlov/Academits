using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            MyArrayList<int> testList = new MyArrayList<int>(false);

            for (int i = 0; i < 148; i++)
            {
                testList.Add(i);
            }

            Console.WriteLine(testList.ToString());

            for (int i = 20; i < 35; i++)
            {
                testList.Remove(i);
            }

            for (int i = 5; i < 145; i++)
            {
                testList.Remove(i);
            }
            Console.WriteLine("---------------------------");
            foreach (int item in testList)
            {
                Console.Write("{0}, ", item);
            }
            Console.WriteLine();

            Console.WriteLine(testList.Contains(2));
            Console.WriteLine("---------------------------");

            int[] arr = new int[10];
            testList.CopyTo(arr, 2);
            foreach (int x in arr)
            {
                Console.Out.Write("{0} ", x);
            }
            Console.WriteLine();

            Console.Out.WriteLine("testList.Count = {0}", testList.Count);

            testList.Clear();
            Console.Out.WriteLine("testList = {0}", testList);
        }
    }
}
