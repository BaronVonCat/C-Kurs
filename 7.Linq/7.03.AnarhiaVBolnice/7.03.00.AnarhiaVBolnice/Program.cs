using System;
using System.Collections.Generic;
using System.Linq;

namespace _7._03.AnarhiaVBolnice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PatientCreator creator = new PatientCreator();
            PatientDatabase database = new PatientDatabase(creator);

            database.Work();
        }
    }

    class PatientDatabase
    {
        private List<Patient> _patients;

        public PatientDatabase(PatientCreator creator)
        {
            _patients = creator.CreatePatients();
        }

        public void Work()
        {
            const string CommandExit = "0";
            const string CommandShowPatientsByDisease = "1";
            const string CommandSortByFullName = "2";
            const string CommandSortByAge = "3";

            bool isExit = false;

            while (isExit == false)
            {
                int cursorPositionX = 0;
                int cursorPositionY = 0;

                Console.Clear();
                Console.WriteLine("БАЗА ДАННЫХ ПАЦИЕНТОВ");
                Console.WriteLine($"\nДоступные команды:" +
                    $"\n{CommandExit}. Выход из программы" +
                    $"\n{CommandShowPatientsByDisease}. Показать пациентов по заболеванию" +
                    $"\n{CommandSortByFullName}. Сортировка по фио" +
                    $"\n{CommandSortByAge}. Сортировка по возрасту");
                Console.WriteLine();
                Console.Write("Введите номер необходимой комманды: ");
                cursorPositionX = Console.CursorLeft;
                cursorPositionY = Console.CursorTop;
                Console.WriteLine("\n");
                ShowPatients(_patients);
                Console.SetCursorPosition(0, 0);
                Console.SetCursorPosition(cursorPositionX, cursorPositionY);

                switch (Console.ReadLine())
                {
                    case CommandExit:
                        isExit = true;
                        break;

                    case CommandShowPatientsByDisease:
                        ShowPatientsByDisease();
                        break;

                    case CommandSortByFullName:
                        _patients = _patients.OrderBy(patient => patient.Surname).
                                    ThenBy(patient => patient.Name).
                                    ThenBy(patient => patient.Patronymic).
                                    ToList();
                        break;

                    case CommandSortByAge:
                        _patients = _patients.OrderByDescending(patient => patient.Age).ToList();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Некорректный запрос!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ShowPatientsByDisease()
        {
            List<Patient> patients = new List<Patient>();
            string disease = null;

            Console.Clear();
            Console.Write("\nВведите название заболевания: ");
            disease = Console.ReadLine();
            Console.Clear();
            patients = _patients.Where(patient => patient.Disease.ToLower() == disease.ToLower()).ToList();
            ShowPatients(patients);
            Console.SetCursorPosition(0, 0);
            Console.ReadKey();
        }

        private void ShowPatients(List<Patient> patients)
        {
            foreach (Patient patient in patients)
            {
                patient.ShowInfo();
                Console.WriteLine();
            }
        }
    }

    class Patient
    {
        public Patient(NamesCreator creator, int age, string disease)
        {
            Name = creator.GenerateNames();
            Surname = creator.GenerateSurnames();
            Patronymic = creator.GeneratePatronymics();
            Disease = disease;
            Age = age;
        }

        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Patronymic { get; private set; }
        public string Disease { get; private set; }
        public int Age { get; private set; }

        public void ShowInfo()
        {
            Console.Write($"{Surname} {Name} {Patronymic} | {Age} | {Disease}");
        }
    }

    class PatientCreator
    {
        private NamesCreator _namesCreator;
        private List<string> _diseases;

        public PatientCreator()
        {
            _namesCreator = new NamesCreator();
            _diseases = CreateDiseases();
        }

        public List<Patient> CreatePatients(int numberPatient = 1000)
        {
            List<Patient> patients = new List<Patient>();

            for (int i = 0; i < numberPatient; i++)
            {
                patients.Add(CreatePatient());
            }

            return patients;
        }

        private Patient CreatePatient()
        {
            int age = CreateAge();
            string disease = _diseases[UserUtils.GenereteRandom(_diseases.Count)];

            return new Patient(_namesCreator, age, disease);
        }

        private int CreateAge()
        {
            int ageMin = 10;
            int ageMax = 100;

            return UserUtils.GenereteRandom(ageMin, ageMax);
        }

        private List<string> CreateDiseases()
        {
            List<string> diseases = new List<string>
            {
                "Рак",
                "Ангина",
                "Туберкулёз",
                "Сифилис",
                "Чума"
            };

            return diseases;
        }
    }

    class NamesCreator
    {
        public string GenerateNames()
        {
            List<string> names = new List<string>
            {
                "Дмитрий",
                "Иван",
                "Сергей",
                "Антон",
                "Роман",
                "Артём",
                "Юрий",
                "Олег",
                "Святослав",
                "Марк"
            };

            return names[UserUtils.GenereteRandom(names.Count)];
        }

        public string GenerateSurnames()
        {
            List<string> surnames = new List<string>
            {
                "Ушаков",
                "Кутузов",
                "Белоцерковский",
                "Барабанов",
                "Лаптев",
                "Горелов",
                "Ковалёв",
                "Козлов",
                "Давлятов",
                "Суворов"
            };

            return surnames[UserUtils.GenereteRandom(surnames.Count)];
        }

        public string GeneratePatronymics()
        {
            List<string> patronymics = new List<string>
            {
                "Сергеевич",
                "Олегович",
                "Валерьевич",
                "Святославович",
                "Антонович",
                "Дмитриевич",
                "Семёнович",
                "Юрьевич",
                "Маркович",
                "Алексеевич"
            };

            return patronymics[UserUtils.GenereteRandom(patronymics.Count)];
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

        public static int GenereteRandom(int maxValue)
        {
            return s_random.Next(maxValue);
        }
    }
}
