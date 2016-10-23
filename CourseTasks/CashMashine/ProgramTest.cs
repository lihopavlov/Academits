using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachine
{
    class ProgramTest
    {

        private static List<Bill> RemoveDuplicate(List<Bill> pool)
        {
            List<Bill> result = new List<Bill>(pool);
            for (int i = 0; i < result.Count; i++)
            {
                for (int j = i + 1; j < result.Count; j++)
                {
                    if (result[i].Rating == result[j].Rating)
                    {
                        result[i].Count += result[j].Count;
                        result.RemoveAt(j);
                        j--;
                    }
                }
            }
            return result;
        }


        static void Main(string[] args)
        {
            List<Bill> testList = new List<Bill>
            {
                new Bill(Bills.Ten, 10),
                new Bill(Bills.Fifty, 20),
                new Bill(Bills.Ten, 11),
                new Bill(Bills.Fifty, 25),
                new Bill(Bills.Ten, 13),
                new Bill(Bills.Hundred, 1),
                new Bill(Bills.Hundred, 1),
                new Bill(Bills.Hundred, 1)
            };

            List<Bill> outlist = RemoveDuplicate(testList);

            foreach (var x in outlist)
            {
                Console.Out.Write("{0} ", x);
            }


        }
    }
}
