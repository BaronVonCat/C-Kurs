using System;
using System.Collections.Generic;

namespace _6._9.Supermapket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Supermarket supermapket = new Supermarket();

            supermapket.Work();
        }
    }

    class Supermarket
    {
        private List<Product> _assortment;
        private Cashier _cashier;

        public Supermarket()
        {
            _assortment = CreateAssortment();
            _cashier = new Cashier();
        }

        public void Work()
        {
            Queue<Buyer> buyers = CreateQueueBuyers();

            while (buyers.Count > 0)
            {
                Buyer buyer = buyers.Dequeue();
                List<Product> products;
                bool isPaymentSuccessful = false;
                int totalCost;

                products = buyer.GetCopyProducts();
                totalCost = _cashier.CalculateCost(products);

                while (isPaymentSuccessful == false)
                {
                    Console.Clear();
                    Console.WriteLine($"Выручка: {_cashier.Money}");
                    Console.WriteLine($"Покупателей в очереди: {buyers.Count}");
                    Console.WriteLine("Товары текущего покупателя:");
                    Console.WriteLine();
                    ShowProducts(products);
                    Console.WriteLine();
                    Console.WriteLine($"Общая стоимость товаров: {totalCost}");
                    Console.WriteLine();

                    if (buyer.Money >= totalCost)
                    {
                        _cashier.AcceptPayment(buyer.Pay(totalCost));
                        isPaymentSuccessful = true;
                        Console.WriteLine("Оплата прошла успешно");
                    }
                    else
                    {
                        Product product = products[UserUtils.GenereteRandom(0, products.Count)];

                        products.Remove(product);
                        totalCost = _cashier.CalculateCost(products);
                        Console.WriteLine("Не хватило средств");
                        Console.WriteLine($"Покупатель убрал {product.Name}");
                    }

                    Console.ReadKey();
                }

                buyer.SetProducts(products);
            }

            Console.Clear();
            Console.WriteLine($"Покупателей больше нет, общая выручка за день: {_cashier.Money}");
            Console.ReadKey();
        }

        private void ShowProducts(List<Product> products)
        {
            foreach (Product product in products)
            {
                product.ShowInfo();
                Console.WriteLine();
            }
        }

        private List<Product> CreateAssortment()
        {
            List<Product> assortment = new List<Product>
            {
                new Product("Молоко", 99),
                new Product("Мыло", 79),
                new Product("Яйца", 89),
                new Product("Досирак", 49),
                new Product("Гречка", 39)
            };

            return assortment;
        }

        private Queue<Buyer> CreateQueueBuyers()
        {
            Queue<Buyer> buyers = new Queue<Buyer>();
            int minBuyers = 5;
            int maxBuyers = 10;
            int numberBuyers = UserUtils.GenereteRandom(minBuyers, maxBuyers);

            for (int i = 0; i < numberBuyers; i++)
            {
                List<Product> products = CreateProducts();
                Buyer buyer = new Buyer(products);
                buyers.Enqueue(buyer);
            }

            return buyers;
        }

        private List<Product> CreateProducts()
        {
            List<Product> products = new List<Product>();
            int minProducts = 1;
            int maxProducts = 10;
            int numberProducts = UserUtils.GenereteRandom(minProducts, maxProducts);

            for (int i = 0; i < numberProducts; i++)
            {
                Product product = _assortment[UserUtils.GenereteRandom(0, _assortment.Count)].GetClone();
                products.Add(product);
            }

            return products;
        }
    }

    class Person
    {
        public Person()
        {
            Money = 0;
        }

        public int Money { get; protected set; }
    }

    class Buyer : Person
    {
        private List<Product> _products;

        public Buyer(List<Product> products)
        {
            _products = products;
            Money = GenereteMoney();
        }

        public List<Product> GetCopyProducts()
        {
            List<Product> products = new List<Product>();

            foreach (Product product in _products)
            {
                products.Add(product);
            }

            return products;
        }

        public void SetProducts(List<Product> products)
        {
            _products.Clear();

            foreach (Product product in products)
            {
                _products.Add(product);
            }
        }

        public int Pay(int cost)
        {
            Money -= cost;
            return cost;
        }

        private int GenereteMoney()
        {
            int minMoney = 0;
            int maxMoney = 1000;
            int money = UserUtils.GenereteRandom(minMoney, maxMoney);

            return money;
        }
    }

    class Cashier : Person
    {
        public int CalculateCost(List<Product> products)
        {
            int cost = 0;

            foreach (Product product in products)
            {
                cost += product.Price;
            }

            return cost;
        }

        public void AcceptPayment(int money)
        {
            Money += money;
        }
    }

    class Product
    {
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }
        public int Price { get; private set; }

        public Product GetClone()
        {
            Product product = new Product(Name, Price);

            return product;
        }

        public void ShowInfo()
        {
            Console.Write($"{Name} - {Price}р.");
        }
    }

    class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenereteRandom(int minValue, int maxValue)
        {
            int randomNumber = s_random.Next(minValue, maxValue);
            return randomNumber;
        }
    }
}
