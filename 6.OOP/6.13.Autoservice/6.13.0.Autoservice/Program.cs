using System;
using System.Collections.Generic;

namespace _6._13.Autoservice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CarStation carStation = new CarStation();
            int amountFine = 1000;

            while (carStation.HasBankrupt == false)
            {
                Client client = new Client();
                bool isChoiceMade = false;

                Console.Clear();

                while (isChoiceMade == false)
                {
                    const string CommandAcceptOrder = "1";
                    const string CommandRejectOrder = "2";

                    carStation.ShowInfo();
                    Console.WriteLine();
                    client.ShowCar();
                    Console.WriteLine();
                    Console.WriteLine($"Команды:" +
                        $"\n{CommandAcceptOrder}. Принять заказ" +
                        $"\n{CommandRejectOrder}. Отклонить заказ(-{amountFine})");
                    Console.WriteLine();
                    Console.Write("Введите номер необходимой команды: ");

                    switch (Console.ReadLine())
                    {
                        case CommandAcceptOrder:
                            carStation.AcceptClient(client); isChoiceMade = true;
                            break;

                        case CommandRejectOrder:
                            carStation.GiveMoney(amountFine); isChoiceMade = true;
                            break;

                        default:
                            Console.Clear();
                            Console.WriteLine("Некорректный запрос!");
                            Console.ReadKey();
                            break;
                    }
                }
            }

            Console.WriteLine("Вы обанкротились!");
            Console.ReadKey();
        }
    }

    class CarStation
    {
        private Warehouse _warehouse;
        private int _money;

        public CarStation()
        {
            _warehouse = CreateWarehouse();
            _money = CreateMoney();
        }

        public bool HasBankrupt => _money < 0;

        public void AcceptClient(Client client)
        {
            Car car = client.GiveCar();
            int paymentInvoise = 0;
            bool isRepairCompleted = false;

            while (isRepairCompleted == false)
            {
                const string CommandFinishRepair = "1";
                const string CommandCarryRepairs = "2";

                Console.Clear();
                ShowInfo();
                Console.WriteLine();
                car.ShowDetails();
                Console.WriteLine();
                Console.WriteLine($"Комманды:" +
                    $"\n{CommandFinishRepair}. Закончить ремонт" +
                    $"\n{CommandCarryRepairs}. Провести ремонт");
                Console.WriteLine();
                Console.Write("Укажите номер необходимой комманды: ");

                switch (Console.ReadLine())
                {
                    case CommandFinishRepair:
                        isRepairCompleted = true;
                        break;

                    case CommandCarryRepairs:
                        paymentInvoise = CarryRepairs(car);
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Некорректный запрос!");
                        Console.ReadKey();
                        break;
                }
            }

            if (car.IsDamaged == false)
            {
                TakeMoney(client.GiveMoney(paymentInvoise));
                Console.WriteLine($"Получена оплата за ремонт: +{paymentInvoise}");
            }
            else
            {
                int priceDamages = 5000;

                client.TakeMoney(GiveMoney(priceDamages));
                Console.WriteLine($"Клиент недоволен работой и требует компенсации: -{priceDamages}");
            }

            client.TakeCar(car);
            Console.ReadKey();
        }

        public int CarryRepairs(Car car)
        {
            int paymentInvoise = 0;
            int priceRepairWork = 1000;
            Detail detail = null;

            paymentInvoise = 0;

            if (_warehouse.TryGetDetail(out detail))
            {
                if (car.TryReplaceDetail(detail))
                {
                    paymentInvoise = detail.Price + priceRepairWork;
                }
            }

            return paymentInvoise;
        }

        public void TakeMoney(int money)
        {
            _money += money;
        }

        public int GiveMoney(int paymentInvoice)
        {
            _money -= paymentInvoice;
            return paymentInvoice;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Деньги: {_money}");
            Console.WriteLine();
            _warehouse.ShowInfo();
        }

        private Warehouse CreateWarehouse()
        {
            Warehouse warehouse = new Warehouse();
            int detailsMin = 10;
            int detailsMax = 50;

            warehouse.AddDetails(new Wheel(), UserUtils.GenereteRandom(detailsMin, detailsMax));
            warehouse.AddDetails(new Engine(), UserUtils.GenereteRandom(detailsMin, detailsMax));
            return warehouse;
        }

        private int CreateMoney()
        {
            int moneyMin = 10000;
            int moneyMax = 20000;

            return UserUtils.GenereteRandom(moneyMin, moneyMax);
        }
    }

    class Warehouse
    {
        private List<Detail> _detailsType;
        private List<int> _detailsQuantity;

        public Warehouse()
        {
            _detailsType = new List<Detail>();
            _detailsQuantity = new List<int>();
        }

        public void ShowInfo()
        {
            Console.WriteLine("Склад:");

            for (int i = 0; i < _detailsType.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_detailsType[i].Name}" +
                    $" {_detailsQuantity[i]}");
            }
        }

        public void AddDetails(Detail detail, int quantity)
        {
            _detailsType.Add(detail);
            _detailsQuantity.Add(quantity);
        }

        public bool TryGetDetail(out Detail detail)
        {
            bool hasDetailReceived = false;
            string userInput = null;
            int userNumber = 0;

            detail = null;
            Console.Write("Укажите номер детали со склада: ");
            userInput = Console.ReadLine();

            if (int.TryParse(userInput, out userNumber))
            {
                userNumber--;

                if (userNumber >= 0 && userNumber < _detailsType.Count)
                {
                    if (_detailsQuantity[userNumber] > 0)
                    {
                        detail = _detailsType[userNumber].GetClone();
                        _detailsQuantity[userNumber]--;
                        hasDetailReceived = true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Таких деталей больше нет!");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Такой детали нет!");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Некорректный запрос!");
                Console.ReadKey();
            }

            return hasDetailReceived;
        }
    }

    class Client
    {
        private Car _car;

        public Client()
        {
            _car = new Car();
        }

        public void ShowCar()
        {
            _car.ShowDetails();
        }

        public Car GiveCar()
        {
            Car car = _car;

            _car = null;
            return car;
        }

        public void TakeCar(Car car)
        {
            _car = car;
        }

        public int GiveMoney(int paymentInvoice)
        {
            return paymentInvoice;
        }

        public void TakeMoney(int money) { }
    }

    class Car
    {
        private List<Detail> _detailsBase;
        private List<Detail> _details;

        public Car()
        {
            _detailsBase = CreateDetails();
            _details = CreateDetails();
            _details[UserUtils.GenereteRandom(0, _details.Count)].BecomeBroken();
        }

        public bool IsDamaged => GetDamageStatus();

        public void ShowDetails()
        {
            int sequenceNumber = 0;

            foreach (Detail detail in _details)
            {
                sequenceNumber++;
                Console.Write($"{sequenceNumber}. ");
                detail.ShowInfo();
                Console.WriteLine();
            }
        }

        public bool TryReplaceDetail(Detail detail)
        {
            bool isReplaceSuccessful = false;
            string userInput = null;
            int userNumber = 0;
            bool isNumber = false;

            Console.Write("Введите номер детали, которую хотите заменить: ");
            userInput = Console.ReadLine();
            isNumber = int.TryParse(userInput, out userNumber);

            if (isNumber == true)
            {
                userNumber--;

                if (userNumber >= 0 && userNumber < _details.Count)
                {
                    _details[userNumber] = detail;
                    isReplaceSuccessful = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Такой детали нет в автомобиле!");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Некорректный запрос!");
                Console.ReadKey();
            }

            return isReplaceSuccessful;
        }

        private List<Detail> CreateDetails()
        {
            List<Detail> details = new List<Detail>
            {
                new Engine(),
                new Wheel(),
                new Wheel(),
                new Wheel(),
                new Wheel()
            };

            return details;
        }

        private bool GetDamageStatus()
        {
            bool hasDamage = false;

            for(int i = 0; i < _details.Count; i++)
            {
                if (_details[i].Name != _detailsBase[i].Name)
                {
                    hasDamage = true;
                }

                if (_details[i].IsDamaged == true)
                {
                    hasDamage = true;
                }
            }

            return hasDamage;
        }
    }

    class Detail
    {
        public Detail(string name, int price)
        {
            Name = name;
            Price = price;
            IsDamaged = false;
        }

        public string Name { get; private set; }
        public bool IsDamaged {  get; private set; }
        public int Price { get; private set; }

        public void ShowInfo()
        {
            Console.Write($"{Name} | ");

            if (IsDamaged == true)
            {
                Console.Write("Повреждёно");
            }
        }

        public void BecomeBroken()
        {
            IsDamaged = true;
        }

        public virtual Detail GetClone()
        {
            Detail detail = new Detail(Name, 0);

            return detail;
        }
    }

    class Wheel : Detail
    {
        public Wheel() : base("Колесо", 1000) { }

        public override Detail GetClone()
        {
            return new Wheel();
        }
    }

    class Engine : Detail
    {
        public Engine() : base("Двигатель", 10000) { }

        public override Detail GetClone()
        {
            return new Engine();
        }
    }

    class UserUtils
    {
        private static Random s_random;

        static UserUtils()
        {
            s_random = new Random();
        }

        public static int GenereteRandom(int minValue, int maxValue)
        {
            int randomNumber = s_random.Next(minValue, maxValue);
            return randomNumber;
        }

        public static bool GenereteRandomBool()
        {
            int valueTrue = 1;

            return s_random.Next(valueTrue + 1) == 1;
        }
    }
}
