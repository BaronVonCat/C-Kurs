using System;
using System.Collections.Generic;

namespace _6._7.KonfiguratorPoezdov
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TrainFlightPlanner planner = new TrainFlightPlanner();

            planner.Work();
        }
    }

    class TrainFlightPlanner
    {
        private List<Station> _stations;
        private List<RailwayFlight> _flights;

        public TrainFlightPlanner()
        {
            _stations = CreateStations();
            _flights = CreateFligths();
        }

        public void Work()
        {
            const string CommandExit = "0";
            const string CommandSkipDay = "1";
            const string CommandLaunchFlight = "2";

            bool isExit = false;

            while (isExit == false)
            {
                Console.Clear();
                Console.WriteLine("Дейстующие рейсы:");
                Console.WriteLine("\nОткуда | Куда | Количество пассажиров | Дней до прибытия\n");

                foreach (RailwayFlight flight in _flights)
                {
                    flight.Show();
                    Console.WriteLine();
                }

                Console.WriteLine($"\nКомманды:\n" +
                    $"\n{CommandExit}. Выход из программы." +
                    $"\n{CommandSkipDay}. Пропустить день." +
                    $"\n{CommandLaunchFlight}. Запустить рейс.");
                Console.Write("\nВвод: ");

                switch (Console.ReadLine())
                {
                    case CommandExit:
                        isExit = true;
                        break;

                    case CommandSkipDay:
                        SkipDay();
                        break;

                    case CommandLaunchFlight:
                        LaunchFlight();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Некорректный запрос!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void SkipDay()
        {
            foreach (RailwayFlight flight in _flights)
            {
                flight.SkipDay();
            }
        }

        private void LaunchFlight()
        {
            RailwayFlight flight;

            if (ChooseFlight(out flight))
            {
                flight.Launch();
            }
        }

        private bool ChooseFlight(out RailwayFlight flight)
        {
            bool isFlightSelected = false;
            string userInput;
            bool isNumber;
            int userNumber;

            flight = null;
            Console.Clear();
            Console.WriteLine("Выберите номер направления, чтобы создать состав:");
            Console.WriteLine();

            for (int i = 0; i < _flights.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                _flights[i].Show();
                Console.WriteLine();
            }

            Console.Write("\nВвод: ");
            userInput = Console.ReadLine();
            isNumber = int.TryParse(userInput, out userNumber);

            if (isNumber == true && userNumber > 0 && userNumber <= _flights.Count)
            {
                flight = _flights[userNumber - 1];
                isFlightSelected = true;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Некорректный запрос!");
                Console.ReadKey();
            }

            return isFlightSelected;
        }

        private List<Station> CreateStations()
        {
            List<Station> stations = new List<Station>()
            {
                new Station("Париж", 1, 5),
                new Station("Москва", 5, 9),
                new Station("Пекин", 9, 3)
            };

            return stations;
        }

        private List<RailwayFlight> CreateFligths()
        {
            List<RailwayFlight> flights = new List<RailwayFlight>();

            foreach (Station stationDeparture in _stations)
            {
                foreach (Station stationDestination in _stations)
                {
                    if (stationDeparture != stationDestination)
                    {
                        RailwayFlight flight = new RailwayFlight(stationDeparture, stationDestination);

                        flights.Add(flight);
                    }
                }
            }

            return flights;
        }
    }

    class RailwayFlight
    {
        private Station _stationDeparture;
        private Station _stationDestination;
        private Train _train;
        private int _remainingFlightDays;

        public RailwayFlight(Station stationDeparture, Station stationDestination)
        {
            _stationDeparture = stationDeparture;
            _stationDestination = stationDestination;
            _train = null;
            _remainingFlightDays = 0;
        }

        public void Launch()
        {
            int numbersPassengers = GeneretePassengers();

            _train = CreateTrain(numbersPassengers);
            _remainingFlightDays = CountDurationFlightDays(_stationDeparture, _stationDestination);
        }

        public void Show()
        {
            Console.Write($"{_stationDeparture.Name} | {_stationDestination.Name} | ");

            if (_remainingFlightDays > 0)
            {
                Console.Write($"{_train.NumberPassengers} | {_remainingFlightDays}");
            }
            else
            {
                Console.Write($"Не активен | Не активен");
            }
        }

        public void SkipDay()
        {
            if (_remainingFlightDays > 0)
            {
                _remainingFlightDays--;
            }
        }

        private int GeneretePassengers()
        {
            Random random = new Random();
            int maxGeneretePassengers = 200;
            int numbersPassengers = random.Next(0, maxGeneretePassengers);

            return numbersPassengers;
        }

        private Train CreateTrain(int numbersPassengers)
        {
            List<Wagon> wagonTemplates = new List<Wagon>()
            {
                new Wagon(20, "Вагон А"),
                new Wagon(50, "Вагон Б"),
                new Wagon(100, "Вагон С")
            };

            List<Wagon> wagons = new List<Wagon>();
            Train train = null;
            int passengerSeats = 0;
            bool hasTrainCreated = false;

            while (hasTrainCreated == false)
            {
                const string CommandCompleteTrain = "0";
                const string CommandAddWagon = "1";
                const string CommandRemoveWagon = "2";

                Console.Clear();
                passengerSeats = CountPassengerSeats(wagons);
                Console.WriteLine($"Пассажиры/Места - {numbersPassengers}/{passengerSeats}");
                ShowWagons(wagonTemplates, wagons);
                Console.WriteLine("\nКоманды:\n" +
                    $"\n{CommandCompleteTrain}. Завершить поезд." +
                    $"\n{CommandAddWagon}. Добавить вагон." +
                    $"\n{CommandRemoveWagon}. Удалить вагон.\n");
                Console.Write("Ввод: ");

                switch (Console.ReadLine())
                {
                    case CommandCompleteTrain:
                        hasTrainCreated = TryCompleteTrain(wagons, numbersPassengers, out train);
                        break;

                    case CommandAddWagon:
                        AddWagon(wagonTemplates, wagons);
                        break;

                    case CommandRemoveWagon:
                        RemoveWagon(wagonTemplates, wagons);
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Некорректный запрос!");
                        Console.ReadKey();
                        break;
                }
            }

            return train;
        }

        private bool TryCompleteTrain(List<Wagon> wagons, int numberPassengers, out Train train)
        {
            bool isTrainFormedCorrectly = false;
            int passengerSeats = CountPassengerSeats(wagons);

            train = null;

            if (passengerSeats >= numberPassengers)
            {
                train = new Train(numberPassengers, wagons);
                isTrainFormedCorrectly = true;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Поезд не удовлетворяет спрос пассажиров!");
                Console.ReadKey();
            }

            return isTrainFormedCorrectly;
        }

        private void AddWagon(List<Wagon> wagonTemplates, List<Wagon> wagons)
        {
            string userInput;
            int userNumber;
            bool isNumber;

            Console.Clear();
            Console.WriteLine("Укажите тип вагона, чтобы добавить его в состав.\n");
            ShowWagons(wagonTemplates, wagons);
            Console.Write("\nВвод: ");
            userInput = Console.ReadLine();
            isNumber = int.TryParse(userInput, out userNumber);

            if (isNumber == true)
            {
                if (userNumber > 0 && userNumber <= wagonTemplates.Count)
                {
                    wagons.Add(wagonTemplates[userNumber - 1]);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Некорректный запрос!");
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

        private void RemoveWagon(List<Wagon> wagonTemplates, List<Wagon> wagons)
        {
            if (wagons.Count > 0)
            {
                string userInput;
                int userNumber;
                bool isNumber;

                Console.Clear();
                Console.WriteLine("Укажите тип вагона, чтобы вывести его из состава.\n");
                ShowWagons(wagonTemplates, wagons);
                userInput= Console.ReadLine();
                isNumber = int.TryParse(userInput, out userNumber);

                if (isNumber == true)
                {
                    if (userNumber > 0 && userNumber <= wagonTemplates.Count)
                    {
                        bool hasWagonFound = false;

                        Wagon removeWagon = null;

                        foreach (Wagon wagon in wagons)
                        {
                            if (wagon.Name == wagonTemplates[userNumber - 1].Name)
                            {
                                removeWagon = wagon;
                                hasWagonFound = true;
                            }
                        }

                        if (hasWagonFound == true)
                        {
                            wagons.Remove(removeWagon);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Таких вагонов нет в составе!");
                            Console.ReadKey();
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Некорректный запрос!");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("В составе нет вагонов!");
                Console.ReadKey();
            }
        }

        private void ShowWagons(List<Wagon> wagonTemplates, List<Wagon> wagons)
        {
            for (int i = 0; i < wagonTemplates.Count; i++)
            {
                int sequenсeNumber = i + 1;
                int numberWagons = 0;

                Console.Write(sequenсeNumber + ". ");
                wagonTemplates[i].Show();
                Console.Write(": ");

                foreach (Wagon wagon in wagons)
                {
                    if (wagon.Name == wagonTemplates[i].Name)
                    {
                        numberWagons++;
                    }
                }

                Console.WriteLine(numberWagons);
            }
        }

        private int CountPassengerSeats(List<Wagon> wagons)
        {
            int passengerSeats = 0;

            foreach (Wagon wagon in wagons)
            {
                passengerSeats += wagon.Capacity;
            }

            return passengerSeats;
        }

        private int CountDurationFlightDays(Station stationDeparture, Station stationDestination)
        {
            int durationFlightDays;

            durationFlightDays = 
                Math.Abs(stationDeparture.PositionY - stationDestination.PositionY) +
                Math.Abs(stationDeparture.PositionX - stationDestination.PositionX);

            return durationFlightDays;
        }
    }

    class Station
    {
        public Station(string name, int positionX, int positionY)
        {
            Name = name;
            PositionX = positionX;
            PositionY = positionY;
        }

        public string Name { get; private set; }
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
    }

    class Train
    {
        private List<Wagon> _wagons;

        public Train(int numberPassengers, List<Wagon> wagons)
        {
            NumberPassengers = numberPassengers;
            _wagons = wagons;
        }

        public int NumberPassengers { get; private set; }

        public void AddWagon(Wagon wagon)
        {
            _wagons.Add(wagon);
        }
    }

    class Wagon
    {
        public Wagon(int capacity, string name)
        {
            Capacity = capacity;
            Name = name;
        }

        public int Capacity { get; private set; }
        public string Name { get; private set; }

        public void Show()
        {
            Console.Write($"{Name}({Capacity})");
        }
    }
}
