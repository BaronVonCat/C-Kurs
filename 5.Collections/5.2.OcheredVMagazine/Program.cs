using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace _5._2.OcheredVMagazine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Queue<string> buyers = CreateBuyers();
            int totalRevenue = 0;

            while (buyers.Count > 0)
            {
                Console.Write("Имя покупателя:");
                Console.SetCursorPosition(30, Console.CursorTop);
                Console.WriteLine("Покупателей в очереди:");
                Console.Write(buyers.Dequeue());
                Console.SetCursorPosition(30, Console.CursorTop);
                Console.WriteLine(buyers.Count);
                totalRevenue += ServeBuyer();
                Console.Write("Общая выручка:");
                Console.SetCursorPosition(30, Console.CursorTop);
                Console.WriteLine(totalRevenue);
                Console.Write("\nНажмите, чтобы обслужить следующего клиента: ");
                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("Покупателей больше нет. Общая выручка за день: " + totalRevenue);
            Console.ReadKey();
        }

        static Queue<string> CreateBuyers()
        {
            Random random = new Random();
            Queue<string> buyers = new Queue<string>();
            string[] fileSurnames = File.ReadAllLines("Data/surnames.txt");
            int queueLengthMin = 1;
            int queueLengthMax = 11;
            int queueLength = random.Next(queueLengthMin, queueLengthMax);

            for (int i = 0; i < queueLength; i++)
            {
                string buyerSurname = fileSurnames[random.Next(0, fileSurnames.Length)];
                buyers.Enqueue(buyerSurname);
            }

            return buyers;
        }

        static int ServeBuyer()
        {
            Dictionary<string, int> assortment = CreateAssortment();
            List<string> buyerProducts = CollectProducts(assortment);
            int totalToPaid = 0;

            Console.Write("\nНаименование товара:");
            Console.SetCursorPosition(30, Console.CursorTop);
            Console.WriteLine("Цена:");
            Console.WriteLine();

            foreach (var buyerProduct in buyerProducts)
            {
                Console.Write(buyerProduct);
                Console.SetCursorPosition(30, Console.CursorTop);
                Console.WriteLine(assortment[buyerProduct]);
                totalToPaid += assortment[buyerProduct];
            }

            Console.WriteLine();
            Console.Write("Всего к оплате:");
            Console.SetCursorPosition(30, Console.CursorTop);
            Console.WriteLine(totalToPaid);

            return totalToPaid;
        }

        static Dictionary<string, int> CreateAssortment()
        {
            Dictionary<string, int> assortment = new Dictionary<string, int>();
            string[] fileAssortment = File.ReadAllLines("Data/assortment.txt");

            for (int i = 0; i < fileAssortment.Length; i++)
            {
                string[] productAndPrice = fileAssortment[i].Split('=');
                string product = productAndPrice[0];
                int price = int.Parse(productAndPrice[1]);
                assortment.Add(product, price);
            }

            return assortment;
        }

        static List<string> CollectProducts(Dictionary<string, int> assortment)
        {
            Random random = new Random();
            List<string> productsBuyer = new List<string>();
            int numberProductsBuyerMin = 1;
            int numberProductsBuyerMax = 10;
            int numberProductsBuyer = random.Next(numberProductsBuyerMin, numberProductsBuyerMax);
            string[] namesProducts = new string[assortment.Count];
            int sequenceNumberProduct = 0;
            
            foreach (var product in assortment)
            {
                namesProducts[sequenceNumberProduct] = product.Key;
                sequenceNumberProduct++;
            }

            for (int i = 0; i < numberProductsBuyer; i++)
            {
                productsBuyer.Add(namesProducts[random.Next(0, namesProducts.Length)]);
            }

            return productsBuyer;
        }
    }
}
