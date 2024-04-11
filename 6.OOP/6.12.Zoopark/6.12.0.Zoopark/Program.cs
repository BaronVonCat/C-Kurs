using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace _6._12.Zoopark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();

            zoo.Work();
        }
    }

    class Zoo
    {
        private List<Aviary> _aviaries;

        public Zoo()
        {
            _aviaries = CreateAviaries();
        }

        public void Work()
        {
            bool isExit = false;

            while (isExit == false)
            {
                const string CommandExit = "0";

                string userInput = null;

                Console.Clear();
                Console.WriteLine("Введите номер комманды или номер вольера к которому хотите подойти.");
                ShowAviaries();
                Console.WriteLine();
                Console.WriteLine($"Комманды:" +
                    $"\n{CommandExit}. Выйти из программы.");
                Console.WriteLine();
                Console.Write("Ввод: ");
                userInput = Console.ReadLine();

                if (userInput == CommandExit)
                {
                    isExit = true;
                }
                else
                {
                    bool isNumber = false;
                    int userNumber = 0;

                    isNumber = int.TryParse(userInput, out userNumber);
                    userNumber--;

                    if (isNumber == true && userNumber >= 0 && userNumber < _aviaries.Count)
                    {
                        Console.Clear();
                        _aviaries[userNumber].ShowInfo();
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Некорректный запрос!");
                        Console.ReadKey();
                    }
                }
            }
        }

        private void ShowAviaries()
        {
            int sequenceNumber = 0;

            Console.WriteLine("Вольеры:\n");

            foreach (Aviary aviary in _aviaries)
            {
                sequenceNumber++;
                Console.WriteLine($"{sequenceNumber}. {aviary.Name}");
            }
        }

        private List<Aviary> CreateAviaries()
        {
            List<Aviary> aviaries = new List<Aviary>
            {
                new Aviary(new Lion(), "Львы"),
                new Aviary(new Panda(), "Панды"),
                new Aviary(new Hyena(), "Гиены"),
                new Aviary(new Jaguar(), "Ягуары")
            };

            return aviaries;
        }
    }

    class Aviary
    {
        private List<Animal> _animals;
        private int _capacity;

        public Aviary(Animal animal, string name)
        {
            _capacity = 10;
            _animals = CreateAnimals(animal);
            Name = name;
        }

        public string Name { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name}" +
                $"\nОсобоей в вольере: {_animals.Count}");

            Console.WriteLine();

            foreach (Animal animal in _animals)
            {
                animal.Show();
                Console.WriteLine();
            }
        }

        private List<Animal> CreateAnimals(Animal animal)
        {
            List<Animal> animals = new List<Animal>();
            int numberAnimals = UserUtils.GenereteRandom(1, _capacity + 1);

            for (int i = 0; i < numberAnimals; i++)
            {
                animals.Add(animal.GetClone());
            }

            return animals;
        }
    }

    class Animal
    {
        private string _name;
        private string _nameType;
        private string _sound;
        private bool _isMale;

        public Animal(string nameType, string sound, string nameMale = null, string nameFemale = null)
        {
            _nameType = nameType;
            _sound = sound;
            _isMale = UserUtils.GenereteRandomBool();
            _name = CreateName(nameType, nameMale, nameFemale);
        }

        public void Show()
        {
            Console.Write($"{_name} | ");
            TryMakeSound();
        }
        
        public virtual Animal GetClone()
        {
            Animal clone = new Animal(_nameType, _sound);

            return clone;
        }

        private void TryMakeSound()
        {
            if (UserUtils.GenereteRandomBool())
            {
                Console.Write(_sound);
            }
            else
            {
                Console.Write("Спокоен");
            }
        }

        private string CreateName(string nameType, string nameMale = null, string nameFemale = null)
        {
            string name = null;

            if (nameFemale != null && nameMale != null)
            {
                if (_isMale == true)
                {
                    name = nameMale;
                }
                else
                {
                    name = nameFemale;
                }
            }
            else
            {
                if (_isMale == true)
                {
                    name = $"{nameType} самец";
                }
                else
                {
                    name = $"{nameType} самка";
                }
            }

            return name;
        }
    }

    class Lion : Animal 
    {
        public Lion() : base("Лев", "Рычит", "Лев", "Львица") { }

        public override Animal GetClone()
        {
            Animal clone = new Lion();

            return clone;
        }
    }

    class Panda : Animal 
    {
        public Panda() : base("Панда", "Угарает") { }

        public override Animal GetClone()
        {
            Animal clone = new Panda();

            return clone;
        }
    } 

    class Hyena : Animal 
    {
        public Hyena() : base("Гиена", "Лает") { }

        public override Animal GetClone()
        {
            Animal clone = new Hyena();

            return clone;
        }
    }

    class Jaguar : Animal 
    {
        public Jaguar() : base("Ягуар", "Рычит") { }

        public override Animal GetClone()
        {
            Animal clone = new Jaguar();

            return clone;
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
