using System;
using System.Collections.Generic;

namespace _6._13.Autoservice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DetailsCreator detailsCreator = new DetailsCreator();
            CarStation carStation = new CarStation(detailsCreator);

            while (carStation.HasBankrupt == false)
            {
                Car car = new Car(detailsCreator);

                carStation.TryAcceptCar(car);
            }

            Console.WriteLine("Вы обанкротились!");
            Console.ReadKey();
        }
    }

    class CarStation
    {
        private Warehouse _warehouse;
        private int _money;

        public CarStation(DetailsCreator detailsCreator)
        {
            _warehouse = CreateWarehouse(detailsCreator);
            _money = CreateMoney();
        }

        public bool HasBankrupt => _money < 0;

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

        public void TryAcceptCar(Car car)
        {
            bool isChoiceMade = false;
            int amountFine = 1000;

            while (isChoiceMade == false)
            {
                const int CommandAcceptOrder = 1;
                const int CommandRejectOrder = 2;

                int userNumber = 0;

                Console.Clear();
                ShowInfo();
                Console.WriteLine();
                car.ShowDetails();
                Console.WriteLine();
                Console.WriteLine($"Команды:" +
                    $"\n{CommandAcceptOrder}. Принять заказ" +
                    $"\n{CommandRejectOrder}. Отклонить заказ(-{amountFine})");
                Console.WriteLine();
                Console.Write("Введите номер необходимой команды: ");

                if (isChoiceMade = UserUtils.TryEnterNumberFromRange(CommandAcceptOrder, CommandRejectOrder, out userNumber))
                {
                    switch (userNumber)
                    {
                        case CommandAcceptOrder:
                            AcceptCar(car);
                            break;

                        case CommandRejectOrder:
                            GiveMoney(amountFine);
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Такой комманды не существует!");
                    Console.ReadKey();
                }
            }
        }

        private void AcceptCar(Car car)
        {
            int priceRepairWork = 1000;
            int paymentInvoise = car.GetCostDamagedDetails() + priceRepairWork;

            CarryRepair(car);

            if (car.IsDamaged == false)
            {
                TakeMoney(paymentInvoise);
                Console.WriteLine($"Получена оплата за ремонт: +{paymentInvoise}");
            }
            else
            {
                int priceDamages = 5000;

                GiveMoney(priceDamages);
                Console.WriteLine($"Клиент недоволен работой и требует компенсации: -{priceDamages}");
            }

            Console.ReadKey();
        }

        private void CarryRepair(Car car)
        {
            const string CommandFinishRepair = "1";
            const string CommandCarryRepairs = "2";

            bool isRepairCompleted = false;

            while (isRepairCompleted == false)
            {
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
                        TryReplaceDetail(car);
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Некорректный запрос!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void TryReplaceDetail(Car car)
        {
            Detail detail = null;

            if (_warehouse.TryGetDetail(out detail))
            {
                car.TryReplaceDetail(detail);
            }
        }

        private Warehouse CreateWarehouse(DetailsCreator detailsCreator)
        {
            Warehouse warehouse = new Warehouse();
            int detailsMin = 10;
            int detailsMax = 50;

            foreach (Detail detail in detailsCreator.GetTypesDetails())
            {
                warehouse.AddDetails(detail, UserUtils.GenereteRandom(detailsMin, detailsMax));
            }

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
        private List<Cell> _cellsDetails;

        public Warehouse()
        {
            _cellsDetails = new List<Cell>();
        }

        public void ShowInfo()
        {
            Console.WriteLine("Склад:");

            for (int i = 0; i < _cellsDetails.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_cellsDetails[i].Name}" +
                    $" {_cellsDetails[i].Quantity}");
            }
        }

        public void AddDetails(Detail detail, int quantity)
        {
            bool isSimilarCell = false;

            foreach (Cell cell in _cellsDetails)
            {
                if (detail.Name == cell.Name)
                {
                    cell.ReplenishDetails(quantity);
                    isSimilarCell = true;
                }
            }

            if (isSimilarCell == false)
            {
                _cellsDetails.Add(new Cell(detail, quantity));
            }
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

                if (userNumber >= 0 && userNumber < _cellsDetails.Count)
                {
                    if (_cellsDetails[userNumber].Quantity > 0)
                    {
                        detail = _cellsDetails[userNumber].GetDetail();
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
                    Console.WriteLine("Такой детали нет в списке!");
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

    class Car
    {
        private List<Detail> _detailsBase;
        private List<Detail> _details;

        public Car(DetailsCreator detailsCreator)
        {
            _detailsBase = detailsCreator.CreateCarDetails();
            _details = detailsCreator.CreateCarDetails();
            _details[UserUtils.GenereteRandom(0, _details.Count)].Breake();
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

        public void TryReplaceDetail(Detail detail)
        {
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
        }

        public int GetCostDamagedDetails()
        {
            int costDamagedDetails = 0;

            foreach (Detail detail in _details)
            {
                if (detail.IsDamaged == true)
                {
                    costDamagedDetails += detail.Price;
                }
            }

            return costDamagedDetails;
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

    class Cell
    {
        private Detail _detail;

        public Cell(Detail detail, int quantity)
        {
            _detail = detail;
            Quantity = quantity;
            Name = detail.Name;
        }

        public string Name { get; private set; }
        public int Quantity { get; private set; }

        public void ReplenishDetails(int quantity)
        {
            Quantity += quantity;  
        }

        public Detail GetDetail()
        {
            Detail detail = null;

            if (Quantity > 0)
            {
                Quantity--;
                detail = _detail.Clone();
            }
            
            return detail;
        }
    }

    class DetailsCreator
    {
        public List<Detail> GetTypesDetails()
        {
            List<Detail> typesDetails = new List<Detail>();

            foreach (Detail detail in CreateCarDetails())
            {
                if (TryFindMatches(detail, typesDetails) == false)
                {
                    typesDetails.Add(detail);
                }
            }

            return typesDetails;
        }

        public List<Detail> CreateCarDetails()
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

        private bool TryFindMatches(Detail desiredDetail, List<Detail> details)
        {
            bool hasMatchFound = false;

            foreach (Detail detail in details)
            {
                if (detail.Name == desiredDetail.Name)
                {
                    hasMatchFound = true;
                }
            }

            return hasMatchFound;
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

        public void Breake()
        {
            IsDamaged = true;
        }

        public virtual Detail Clone()
        {
            Detail detail = new Detail(Name, 0);

            return detail;
        }
    }

    class Wheel : Detail
    {
        public Wheel() : base("Колесо", 1000) { }

        public override Detail Clone()
        {
            return new Wheel();
        }
    }

    class Engine : Detail
    {
        public Engine() : base("Двигатель", 10000) { }

        public override Detail Clone()
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

        public static bool TryEnterNumberFromRange(int numberMin, int numberMax, out int userNumber)
        {
            bool isCorrectNumberEntered = false;

            userNumber = 0;

            if (int.TryParse(Console.ReadLine(), out userNumber))
            {
                if (userNumber >= numberMin && userNumber <= numberMax)
                {
                    isCorrectNumberEntered = true;
                }
            }

            return isCorrectNumberEntered;
        }
    }
}
