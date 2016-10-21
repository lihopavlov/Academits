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
            MyArrayList<string> strArray = new MyArrayList<string>();
            strArray.Add("str1");
            strArray.Add(null);
            strArray.Add(null);
            strArray.Add("str4");

            //strArray.RemoveAt(1);

            foreach (string item in strArray)
            {
                Console.Write("{0}, ", item);
            }
            Console.WriteLine();
            try
            {
                Console.Out.WriteLine("strArray.Contains(null) = {0}", strArray.Contains(null));
            }
            catch (ArgumentException e)
            {
                Console.Out.WriteLine("{0}", e.Message);
            }

            try
            {
                Console.Out.WriteLine("strArray.Remove(null) = {0}", strArray.Remove(null));
            }
            catch (ArgumentException e)
            {
                Console.Out.WriteLine("{0}", e.Message);
            }


            strArray.Insert(0, null);
            foreach (string item in strArray)
            {
                Console.Write("{0}, ", item);
            }
            Console.WriteLine();

            

            Console.WriteLine("---------------------------");

            //-----------------------------------------------------------------------
            MyArrayList<int> testList = new MyArrayList<int>();

            for (int i = 0; i < 15; i++)
            {
                testList.Add(i);
            }
            testList.Add(10);
            Console.Out.WriteLine("testList.Capacity = {0}", testList.Capacity);
            Console.WriteLine(testList.ToString());

            for (int i = 5; i < 8; i++)
            {
                testList.Remove(i);
            }
            Console.Out.WriteLine("testList.Capacity = {0}", testList.Capacity);
            for (int i = 8; i < 9; i++)
            {
                testList.Remove(i);
            }
            Console.Out.WriteLine("testList.Capacity = {0}", testList.Capacity);

            //for (int i = 5; i < 145; i++)
            //{
            //    
            //}
            testList.Remove(14);
            Console.Out.WriteLine("testList.Capacity = {0}", testList.Capacity);
            Console.WriteLine("---------------------------");
            foreach (int item in testList)
            {
                Console.Write("{0}, ", item);
            }

            Console.WriteLine();

            Console.Out.WriteLine("testList.IndexOf(3) = {0}", testList.IndexOf(3));
            testList.Insert(0, 99);
            foreach (int item in testList)
            {
                Console.Write("{0}, ", item);
            }
            Console.WriteLine();
            Console.WriteLine("--------Remove------At--------");
            testList.RemoveAt(1);
            foreach (int item in testList)
            {
                Console.Write("{0}, ", item);
            }
            Console.WriteLine();

            Console.WriteLine("---------------------------");
            testList[1] = 98;
            Console.WriteLine(testList[9]);

            foreach (int item in testList)
            {
                Console.Write("{0}, ", item);
            }


            Console.WriteLine(testList.Contains(2));
            Console.WriteLine("---------------------------");

            testList.RemoveAt(8);

            foreach (int item in testList)
            {
                Console.Write("{0}, ", item);
            }
            Console.WriteLine();

            int[] arr = new int[10];
            testList.CopyTo(arr, 2);
            foreach (int x in arr)
            {
                Console.Out.Write("{0} ", x);
            }
            Console.WriteLine();

            Console.Out.WriteLine("testList.Count = {0}", testList.Count);
            Console.Out.WriteLine("***************************************************************");
            Console.Out.WriteLine("testList.Capacity = {0}", testList.Capacity);
            Console.Out.WriteLine("testList.Count = {0}", testList.Count);
            //testList.Clear();
            Console.Out.WriteLine("testList = {0}", testList);
            try
            {
                testList.Capacity = 30;
            }
            catch (ArgumentException e)
            {
                Console.Out.WriteLine("{0}", e.Message);
            }
            Console.Out.WriteLine("testList.Capacity = {0}", testList.Capacity);
            Console.Out.WriteLine("testList.Count = {0}", testList.Count);
            Console.Out.WriteLine("Add 1 element 45");
            testList.Add(45);
            Console.Out.WriteLine("testList.Capacity = {0}", testList.Capacity);
            Console.Out.WriteLine("testList.Count = {0}", testList.Count);
            MyArrayList<int> addRng = new MyArrayList<int>();
            for (int i = 100; i < 106; i++)
            {
                addRng.Add(i);
            }
            Console.Out.WriteLine("Add Range!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            testList.AddRange(addRng);
            Console.Out.WriteLine("testList = {0}", testList);
            Console.Out.WriteLine("testList.Capacity = {0}", testList.Capacity);
            Console.Out.WriteLine("testList.Count = {0}", testList.Count);
        }
    }
}
