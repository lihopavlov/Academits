using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMashine
{
    enum Operation
    {
        Pay = 0,
        Add = 1
    }

    class Program
    {
        private static void CashMashineOperaton(Operation operation, CashMashine cashMashine)
        {
            bool isCountEnterSuccess = false;
            int count = 0;
            do
            {
                string line;
                if (!isCountEnterSuccess)
                {
                    Console.Out.Write("Введите количество купюр (0 - выход): ");
                    line = Console.In.ReadLine();
                    try
                    {
                        count = Convert.ToInt32(line);
                        isCountEnterSuccess = true;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Ошибка. Неверный ввод.");
                        continue;
                    }
                    if (count == 0)
                    {
                        break;
                    }
                }
                Console.Out.Write("Введите номинал купюры (Ten, Fifty, Hundred, FiveHundred, Thousand, FiveThousand, 0 - выход): ");
                line = Console.In.ReadLine();
                if (line == "0")
                {
                    break;
                }
                try
                {
                    Bills rating = (Bills)Enum.Parse(typeof(Bills), line);
                    try
                    {
                        if (operation == Operation.Pay)
                        {
                            Console.Out.WriteLine("Выдано средств {0}", cashMashine.PayBill(rating, count));
                        }
                        else if (operation == Operation.Add)
                        {
                            cashMashine.AddBill(rating, count);
                            Console.Out.WriteLine("Средства успешно добавлены.");
                            Console.Out.WriteLine("Статус банкомата = {0}", cashMashine);
                        }
                        isCountEnterSuccess = false;
                    }
                    catch (ArgumentException e)
                    {
                        Console.Out.WriteLine("{0}", e.Message);
                        break;
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Ошибка. Неверный ввод.");
                }
            } while (true);

        }

        static void Main(string[] args)
        {
            CashMashine cashMashine1 = new CashMashine(new List<Bill>
            {
                new Bill(Bills.Ten, 10),
                new Bill(Bills.Fifty, 20),
                new Bill(Bills.Ten, 11),
                new Bill(Bills.Fifty, 25),
                new Bill(Bills.Ten, 13)
            }, 100, "улица1 строение1");

            Console.Out.WriteLine("Статус банкомата = {0}", cashMashine1);
            
            do
            {
                Console.Out.WriteLine("Операция:");
                Console.Out.WriteLine("1. Извлечь деньги");
                Console.Out.WriteLine("2. Загрузить деньги");
                Console.Out.WriteLine("3. Состояние");
                Console.Out.WriteLine("4. Выход");

                string line = Console.In.ReadLine();
                int choiceCode = 0;
                try
                {
                    choiceCode = Convert.ToInt32(line);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка. Неверный ввод.");
                }
                if (choiceCode == 4)
                {
                    break;
                }
                if (choiceCode == 1)
                {
                    CashMashineOperaton(Operation.Pay, cashMashine1);
                }
                else if (choiceCode == 2)
                {
                    CashMashineOperaton(Operation.Add, cashMashine1);
                }
                else if (choiceCode == 3)
                {
                    Console.Out.WriteLine("Состояние банкомата = {0}", cashMashine1);
                }
            } while (true);
        }
    }
}
