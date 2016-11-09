using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArrayList;

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

            try
            {
                foreach (string item in strArray)
                {
                    Console.Write("{0}, ", item);
                    strArray[3] = "ffff";
                }
            }
            catch (InvalidOperationException e)
            {
                Console.Out.WriteLine("{0}", e.Message);
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
            Console.WriteLine("----------------------COPYTO-----------------");

            int[] arr = new int[9];
            try
            {
                testList.CopyTo(arr, 1);
            }
            catch (ArgumentException e)
            {
                Console.Out.WriteLine("{0}", e.Message);
            }
            Console.WriteLine("----------------------COPYTO-RESULT-ARRAY------");
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
            Console.Out.WriteLine();
            Console.Out.WriteLine();

            try
            {
                foreach (var item in testList)
                {
                    Console.Out.Write("{0}, ", item);
                    testList.Add(199);
                }
            }
            catch (InvalidOperationException e)
            {
                Console.Out.WriteLine("{0}", e.Message);
            }
            Console.Out.WriteLine();
            Console.Out.WriteLine("testList.Capacity = {0}", testList.Capacity);
            Console.Out.WriteLine("testList.Count = {0}", testList.Count);
            Console.Out.WriteLine();
            testList.TrimExcess();
            Console.Out.WriteLine("-------------TRIM_EXCESS---------------");
            Console.Out.WriteLine("testList.Capacity = {0}", testList.Capacity);
            Console.Out.WriteLine("testList.Count = {0}", testList.Count);
            Console.Out.WriteLine();
            Console.Out.WriteLine("testList = {0}", testList);
            Console.Out.WriteLine();
            testList.Capacity = 19;
            testList.TrimExcess();
            Console.Out.WriteLine("-------------TRIM_EXCESS---------------");
            Console.Out.WriteLine("testList.Capacity = {0}", testList.Capacity);
            Console.Out.WriteLine("testList.Count = {0}", testList.Count);
            Console.Out.WriteLine();
            Console.Out.WriteLine();

//-------------------------------------------------------------------------------------------------------
//------------------------------------------------Linked_List--------------------------------------------
//---------------------------------------------------------------------------------------------------------

            Console.Out.WriteLine("----------------LINKED-LIST-------------------------");
            Console.Out.WriteLine("*****************************************************");
            Console.Out.WriteLine("*****************************************************");
            Console.Out.WriteLine("*****************************************************");
            Console.Out.WriteLine();
            Console.Out.WriteLine();
            MyLinkedList<int> linkedList = new MyLinkedList<int>();
            for (int i = 40; i < 51; i++)
            {
                linkedList.AddLast(i);
            }
            Console.Out.WriteLine("linkedList = {0}", linkedList);
            Console.Out.WriteLine("linkedList.Count = {0}", linkedList.Count);
            Console.Out.WriteLine("Remove 50");
            linkedList.Remove(50);
            Console.Out.WriteLine("linkedList = {0}", linkedList);
            Console.Out.WriteLine("linkedList.Count = {0}", linkedList.Count);
            //Console.Out.WriteLine("----Clear----");
            //linkedList.Clear();
            //Console.Out.WriteLine("linkedList = {0}", linkedList);
            Console.Out.WriteLine();
            Console.Out.WriteLine("-----Contains-------");
            Console.Out.WriteLine("linkedList.Contains(40) = {0}", linkedList.Contains(45));
            Console.Out.WriteLine();
            Console.WriteLine("----------------------COPYTO-----------------");

            int[] arr1 = new int[12];
            try
            {
                linkedList.CopyTo(arr1, 2);
            }
            catch (ArgumentException e)
            {
                Console.Out.WriteLine("{0}", e.Message);
            }
            Console.WriteLine("----------------------COPYTO-RESULT-ARRAY------");
            foreach (int x in arr1)
            {
                Console.Out.Write("{0} ", x);
            }
            Console.WriteLine();
            //Console.Out.WriteLine();
            //Console.WriteLine("----------------------ENUMERATOR-----------------");
            //try
            //{
            //    foreach (var item in linkedList)
            //    {
            //        Console.Out.Write("{0}, ", item);
            //        //linkedList.Add(199);
            //    }
            //}
            //catch (InvalidOperationException e)
            //{
            //    Console.Out.WriteLine("{0}", e.Message);
            //}
            //Console.Out.WriteLine();
            //Console.WriteLine("----------------------AddAfter-----------------");
            //linkedList.AddAfter(linkedList.AddAfter(linkedList.First, 121), new ListNode<int>(122, null));
            //Console.Out.WriteLine();
            //Console.Out.WriteLine("linkedList = {0}", linkedList);

            //Console.WriteLine("----------------------AddBefore-----------------");
            //linkedList.AddBefore(linkedList.AddBefore(linkedList.Last, 123), new ListNode<int>(124, null));
            //linkedList.AddAfter(linkedList.AddAfter(linkedList.Last, 125), new ListNode<int>(126, null));
            //Console.Out.WriteLine();
            //Console.Out.WriteLine("linkedList = {0}", linkedList);
            //Console.Out.WriteLine("linkedList.Count = {0}", linkedList.Count);
            //Console.WriteLine("----------------------CLEAR-----------------");
            //linkedList.Clear();
            //Console.Out.WriteLine("linkedList.Count = {0}", linkedList.Count);
            //linkedList.AddFirst(160);
            //Console.Out.WriteLine("linkedList = {0}", linkedList);
            //Console.Out.WriteLine("linkedList.Count = {0}", linkedList.Count);
            //linkedList.AddFirst(new ListNode<int>(161, null));
            //Console.Out.WriteLine();
            //linkedList.AddLast(162);
            //linkedList.AddLast(new ListNode<int>(163, null));
            //Console.Out.WriteLine();
            //Console.Out.WriteLine("linkedList = {0}", linkedList);
            //Console.Out.WriteLine("linkedList.Count = {0}", linkedList.Count);
            //Console.WriteLine("----------------------Remove node-----------------");
            //Console.Out.WriteLine();
            //linkedList.Remove(linkedList.First);
            //Console.Out.WriteLine("linkedList = {0}", linkedList);
            //Console.Out.WriteLine("linkedList.Count = {0}", linkedList.Count);
            //linkedList.RemoveFirst();
            //linkedList.RemoveLast();
            //Console.Out.WriteLine("linkedList = {0}", linkedList);
            //Console.Out.WriteLine("linkedList.Count = {0}", linkedList.Count);


            ////----------------------------------------------------------------------------------
            ////----------------------------------------------------------------------------------
            ////----------------------------------------------------------------------------------
            //Console.Out.WriteLine("----------------LINKED-LIST-------------------------");
            //Console.Out.WriteLine("*****************************************************");
            //Console.Out.WriteLine("*****************************************************");
            //Console.Out.WriteLine("*****************************************************");
            //Console.Out.WriteLine();
            //Console.Out.WriteLine();
            //MyLinkedList<string> stringList = new MyLinkedList<string>();
            //for (int i = 40; i < 51; i++)
            //{
            //    stringList.AddFirst("str" + i);
            //}
            //Console.Out.WriteLine("stringList = {0}", stringList);
            //Console.Out.WriteLine("stringList.Count = {0}", stringList.Count);
            //stringList.AddAfter(stringList.First, (string)null);
            //stringList.AddAfter(stringList.First, (string)null);
            //stringList.AddAfter(stringList.First, (string)null);
            //Console.Out.WriteLine("stringList = {0}", stringList);
            //Console.Out.WriteLine("stringList.Count = {0}", stringList.Count);
            //Console.Out.WriteLine("stringList.First = {0}", stringList.First);
            //Console.Out.WriteLine("stringList.Last = {0}", stringList.Last);
            //Console.Out.WriteLine();
            //Console.Out.WriteLine("             REMOVE            ");
            //Console.Out.WriteLine();

            //stringList.Remove((string)null);
            //Console.Out.WriteLine("stringList.Contains(null) = {0}", stringList.Contains(null));
            //stringList.Remove((string)null);
            //stringList.Remove((string)null);
            //stringList.Remove((string)null);
            //stringList.Remove((string)null);
            //Console.Out.WriteLine("stringList.Contains(null) = {0}", stringList.Contains(null));

            //stringList.RemoveFirst();
            //stringList.RemoveFirst();
            //stringList.RemoveFirst();
            //stringList.RemoveFirst();
            //stringList.RemoveFirst();
            //stringList.RemoveFirst();
            //stringList.RemoveFirst();
            //stringList.RemoveFirst();
            //stringList.RemoveFirst();
            //stringList.RemoveFirst();
            //stringList.RemoveFirst();

            ////stringList.AddAfter(stringList.First, "string1");
            ////stringList.AddAfter(null, "string1");


            //Console.Out.WriteLine("stringList = {0}", stringList);
            //Console.Out.WriteLine("stringList.Count = {0}", stringList.Count);
            //Console.Out.WriteLine("stringList.First = {0}", stringList.First);
            //Console.Out.WriteLine("stringList.Last = {0}", stringList.Last);

            ////stringList.Remove(stringList.First.NextItem);
            ////stringList.Remove(stringList.First.NextItem);
            ////stringList.Remove(stringList.First.NextItem);
            ////stringList.Remove(stringList.First.NextItem);
            ////stringList.Remove(stringList.First.NextItem);
            ////stringList.Remove(stringList.First.NextItem);
            ////stringList.Remove(stringList.First.NextItem);
            ////Console.Out.WriteLine("stringList = {0}", stringList);
            ////Console.Out.WriteLine("stringList.Count = {0}", stringList.Count);
            ////stringList.Clear();
            ////stringList.Remove("ddd");
            ////stringList.RemoveFirst();
            ////stringList.RemoveLast();
            ////Console.Out.WriteLine("stringList contains sss = {0}", stringList.Contains("ssss"));
            ////Console.Out.WriteLine("stringList = {0}", stringList);
            ////Console.Out.WriteLine("stringList.Count = {0}", stringList.Count);
            ////stringList.AddAfter(stringList.First, "string1");
            ////stringList.AddAfter(stringList.First, "string2");
            ////stringList.AddAfter(stringList.First, "string3");
            ////stringList.AddAfter(stringList.First, "string4");
            ////stringList.AddAfter(stringList.First, "string5");
            ////stringList.AddAfter(stringList.First, "string6");
            ////stringList.AddFirst("begin");
            ////stringList.AddLast("end");
            ////stringList.Remove("string3");
            ////Console.Out.WriteLine("stringList = {0}", stringList);
            ////Console.Out.WriteLine("stringList.Count = {0}", stringList.Count);
            Console.Out.WriteLine("---------------------------------------------------------------------------");
            Console.Out.WriteLine("---------------------------------------------------------------------------");
            Console.Out.WriteLine("-------------------------------HASHTABLE--------------------------------");
            HashTable<string> htTest = new HashTable<string>();

            for (int i = 0; i < 15; i++)
            {
                htTest.Add("d" + i);
            }

            Console.Out.WriteLine();
            Console.Out.WriteLine("htTest = {0}", htTest);
            Console.Out.WriteLine("htTest.Count = {0}", htTest.Count);

            htTest.Remove("d0");
            htTest.Remove("d1");
            htTest.Remove("d2");
            htTest.Remove("d3");
            htTest.Remove("d4");

            Console.Out.WriteLine();
            Console.Out.WriteLine("htTest = {0}", htTest);
            Console.Out.WriteLine("htTest.Count = {0}", htTest.Count);

            htTest.Add("d0");
            htTest.Add("d1");
            htTest.Add("d2");
            htTest.Add("d3");
            htTest.Add("d4");
            htTest.Add("d4");

            htTest.Add(null);
            htTest.Add(null);

            Console.Out.WriteLine();
            Console.Out.WriteLine("htTest = {0}", htTest);
            Console.Out.WriteLine("htTest.Count = {0}", htTest.Count);
            Console.Out.WriteLine();

            Console.Out.WriteLine("htTest.Contains(d4) = {0}", htTest.Contains("d4"));
            Console.Out.WriteLine("htTest.Contains(null) = {0}", htTest.Contains(null));
            Console.Out.WriteLine("htTest.Contains(d44) = {0}", htTest.Contains("d44"));

            Console.Out.WriteLine("htTest.Count = {0}", htTest.Count);

            string[] arr25 = new string[25];
            htTest.CopyTo(arr25, 5);

            foreach (string x in arr25)
            {
                Console.Out.Write("x = {0} ", x);
            }

            Console.Out.WriteLine("htTest = {0}", htTest);

        }
    }
}
