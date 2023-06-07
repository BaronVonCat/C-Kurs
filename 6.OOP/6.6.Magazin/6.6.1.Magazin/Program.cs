using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace _6._6.Magazin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Shop shop = new Shop();

            shop.Work();
        }
    }

    class Shop
    {
        private Seller _seller;
        private Player _player;

        public Shop()
        {
            _seller = CreateSeller();
            _player = CreatePlayer();
        }

        public void Work()
        {
            const string CommandExitShop = "0";
            const string CommandBuyProduct = "1";
            const string CommandViewProducts = "2";
            const string CommandOpenInventory = "3";

            bool isWork = true;

            while (isWork == true)
            {
                int inputPositionX;

                Console.Clear();
                Console.Write("Введите номер команды: ");
                inputPositionX = Console.CursorLeft;
                Console.WriteLine("");
                Console.WriteLine($"\nКоманды:" +
                    $"\n\n{CommandExitShop}. Выйти из магазина" +
                    $"\n{CommandBuyProduct}. Купить товар" +
                    $"\n{CommandViewProducts}. Посмотреть товары продовца" +
                    $"\n{CommandOpenInventory}. Открыть инвентарь");
                Console.SetCursorPosition( inputPositionX, 0 );

                switch (Console.ReadLine())
                {
                    case CommandExitShop:
                        isWork = false;
                        break;

                    case CommandBuyProduct:
                        Trade();
                        break;

                    case CommandOpenInventory:
                        _player.OpenInventory();
                        break;

                    case CommandViewProducts: 
                        _seller.OpenInventory();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Некорректный запрос!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void Trade()
        {
            Product product;
            bool isProductInSeller = TryGetProduct(out product);

            if (isProductInSeller == true)
            {
                if (_player.CanPay(product.Price))
                {
                    _seller.AddMoney(_player.GiveMoney(product.Price));
                    _player.AddProduct(_seller.GiveProduct(product.Name));
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("У вас не хватит денег на этот товар!");
                    Console.ReadKey();
                }
            }
        }

        private bool TryGetProduct(out Product product)
        {
            bool isProductInSeller = false;
            string userInput;

            product = null;
            Console.Clear();
            Console.Write("Введите название продукта: ");
            userInput = Console.ReadLine();

            if (_seller.TryGetCopyProductByName(userInput, out product))
            {
                isProductInSeller = true;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Такого товара нет у продовца!");
                Console.ReadKey();
            }

            return isProductInSeller;
        }

        private Seller CreateSeller()
        {
            List<Product> products = new List<Product>
            {
                new Product("Веер", 99),
                new Product("Ложка", 499),
                new Product("Кепка Джотаро", 100000),
                new Product("Удочка", 999),
                new Product("Мармелад", 59),
                new Product("Мармелад", 59)
            };

            Seller seller = new Seller(products, 0);

            return seller;
        }

        private Player CreatePlayer()
        {
            Player player = new Player(new List<Product>(), 100000);

            return player;
        }
    }

    class Human
    {
        protected List<Product> Products;

        public Human(List<Product> products, int money)
        {
            Products = products;
            Money = money;
        }

        public int Money { get; protected set; }

        public void OpenInventory()
        {
            Console.Clear();
            Console.WriteLine($"Деньги: {Money}");
            Console.WriteLine($"Инвентарь: ");
            Console.WriteLine();

            foreach (Product product in Products)
            {
                product.Show();
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }

    class Seller : Human
    {
        public Seller(List<Product> products, int money) : base (products, money) { }

        public Product GiveProduct(string productName)
        {
            Product foundProduct = null;

            foreach (Product product in Products)
            {
                if (product.Name == productName)
                {
                    foundProduct = product;
                    break;
                }
            }

            Products.Remove(foundProduct);
            return foundProduct;
        }

        public bool TryGetCopyProductByName(string desiredProductName, out Product productCopy)
        {
            bool hasProductFound = false;

            productCopy = null;

            foreach (Product product in Products)
            {
                if (product.Name.ToUpper() == desiredProductName.ToUpper())
                {
                    productCopy = new Product(product.Name, product.Price);
                    hasProductFound = true;
                    break;
                }
            }

            return hasProductFound;
        }

        public void AddMoney(int money)
        {
            Money += money;
        }
    }

    class Player : Human
    {
        public Player(List<Product> products, int money) : base (products, money) { }

        public int GiveMoney(int productPrice)
        {
            Money -= productPrice;

            return productPrice;
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public bool CanPay(int price)
        {
            bool isPlayerEnoughMoney = false;

            if (Money >= price)
            {
                isPlayerEnoughMoney = true;
            }

            return isPlayerEnoughMoney; 
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

        public void Show()
        {
            Console.Write($"{Name}|{Price}");
        }
    }
}
