using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachine
{
    class Program
    {
        private static void CashMachineOperaton(Operation operation, CashMachine cashMashine)
        {
            bool isBillEnterSuccess = false;
            int count = 0;
            do
            {
                string line;
                Bills rating = Bills.Blank;
                if (!isBillEnterSuccess)
                {
                    Console.Out.Write("Введите номинал купюры (10, 50, 100, 500, 1000, 5000, 0 - выход): ");
                    line = Console.In.ReadLine();
                    if (line == "0")
                    {
                        break;
                    }
                    if (Enum.TryParse(line, out rating))
                    {
                        if (Enum.IsDefined(typeof(Bills), rating) && rating != Bills.Blank)
                        {
                            isBillEnterSuccess = true;
                        }
                        else
                        {
                            Console.Out.WriteLine("Ошибка.Такой купюры не существует.");
                            Console.Out.WriteLine();
                            continue;
                        }
                    }
                    else
                    {
                        Console.Out.WriteLine("Ошибка. Неверный ввод.");
                        Console.Out.WriteLine();
                        continue;
                    }
                }
                Console.Out.Write("Введите количество купюр (0 - выход): ");
                line = Console.In.ReadLine();
                if (line == "0")
                {
                    break;
                }
                try
                {
                    count = Convert.ToInt32(line);
                    isBillEnterSuccess = false;
                    try
                    {
                        if (operation == Operation.Pay)
                        {
                            Console.Out.WriteLine();
                            Console.Out.WriteLine("Выдано средств {0}", cashMashine.PayBill(rating, count));
                        }
                        else if (operation == Operation.Add)
                        {
                            cashMashine.AddBill(rating, count);
                            Console.Out.WriteLine();
                            Console.Out.WriteLine("Средства успешно добавлены.");
                            Console.Out.WriteLine();
                            Console.Out.WriteLine("Статус банкомата = {0}", cashMashine);
                        }
                    }
                    catch (ArgumentException e)
                    {
                        Console.Out.WriteLine("{0}", e.Message);
                        break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка. Неверный ввод.");
                    Console.Out.WriteLine();
                }
                catch (OverflowException)
                {
                    Console.Out.WriteLine("Слишком большое число");
                    Console.Out.WriteLine();
                }

            } while (true);

        }

        static void Main(string[] args)
        {
            CashMachine cashMashine1;
            CashMachine cashMashine2 = new CashMachine(100, "fff");
            try
            {
                cashMashine1 = new CashMachine(new List<Bill>
                {
                    new Bill(Bills.Ten, 10),
                    new Bill(Bills.Fifty, 20),
                    new Bill(Bills.Ten, 11),
                    new Bill(Bills.Fifty, 25),
                    new Bill(Bills.Ten, 13)
                }, 100, "улица1 строение1");
            }
            catch (ArgumentException e)
            {
                Console.Out.WriteLine("{0}", e);
                return;
            }
            Console.Out.WriteLine("Статус банкомата = {0}", cashMashine1);
            
            do
            {
                Console.Out.WriteLine();
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
                    Console.Out.WriteLine();
                    Console.WriteLine("Ошибка. Неверный ввод.");
                }
                if (choiceCode == 4)
                {
                    break;
                }
                if (choiceCode == 1)
                {
                    CashMachineOperaton(Operation.Pay, cashMashine1);
                }
                else if (choiceCode == 2)
                {
                    CashMachineOperaton(Operation.Add, cashMashine1);
                }
                else if (choiceCode == 3)
                {
                    Console.Out.WriteLine();
                    Console.Out.WriteLine("Состояние банкомата = {0}", cashMashine1);
                }
            } while (true);
        }
    }
}
