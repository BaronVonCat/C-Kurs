using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _5._4.KadroviyUchotPro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddDossier = "1";
            const string CommandOutputAllDossiers = "2";
            const string CommandDeleteDossier = "3";
            const string CommandSearchByLastName = "4";
            const string CommandExit = "0";
            const char SeparatorSign = ';';

            Dictionary<int, string> dossiers = new Dictionary<int, string>();
            bool isCommandExit = false;
            string manual = "Введите номер запроса: ";
            string userInput;
            string[] availableСommands =
                {$"{CommandAddDossier}. Добавить досье",
                    $"{CommandOutputAllDossiers}. Вывести все досье",
                    $"{CommandDeleteDossier}. Удалить досье",
                    $"{CommandSearchByLastName}. Поиск по фамилии" ,
                    $"{CommandExit}. Выход из программы"};

            dossiers = InitializationDossier(dossiers);
            OutputWelcomeBanner(manual, availableСommands);

            while (isCommandExit == false)
            {
                MoveCursor(manual);
                userInput = Console.ReadLine();
                OutputWelcomeBanner(manual, availableСommands);

                switch (userInput)
                {
                    case CommandAddDossier:
                        dossiers = AddDossier(dossiers, SeparatorSign);
                        break;

                    case CommandOutputAllDossiers:
                        OutputDossierAll(dossiers, SeparatorSign);
                        break;

                    case CommandDeleteDossier:
                        dossiers = DeleteDossier(dossiers, SeparatorSign);
                        break;

                    case CommandSearchByLastName:
                        SearchByLastName(dossiers, SeparatorSign);
                        break;

                    case CommandExit:
                        isCommandExit = Exit(isCommandExit, manual, availableСommands);
                        break;

                    default:
                        OutputProgramResponse();
                        break;
                }

                if (userInput == CommandAddDossier ||
                    userInput == CommandOutputAllDossiers ||
                    userInput == CommandDeleteDossier ||
                    userInput == CommandSearchByLastName)
                {
                    OutputWelcomeBanner(manual, availableСommands);
                }
            }
        }

        static Dictionary<int, string> AddDossier(Dictionary<int, string> dossiers, char separatorSign)
        {
            const string CommandReturnsToMenu = "0";
            const string CommandAddDossier = "1";

            string manual = "Введите номер комманды: ";
            string userInput;
            bool isCommandReturnsToMenu = false;
            string[] availableСommands =
                { $"{CommandAddDossier}. Добавить досье",
                $"{CommandReturnsToMenu}. Вернуться в меню" };

            OutputWelcomeBanner(manual, availableСommands);

            while (isCommandReturnsToMenu == false)
            {
                MoveCursor(manual);
                userInput = Console.ReadLine();
                OutputWelcomeBanner(manual, availableСommands);

                if (userInput == CommandAddDossier)
                {
                    string manualNewDossierUsername = "Введите фамилию гражданина: ";
                    string manualNewDossierWorkongPosition = "Введите должность гражданина: ";
                    string programResponse = "-ДОБАВЛЕННОЕ ДОСЬЕ-";
                    int newDossierKey = dossiers.Count + 1;
                    string newSurname = GetDataFromUser(manualNewDossierUsername);
                    string newWorkingPosition = GetDataFromUser(manualNewDossierWorkongPosition);
                    string newDossierValue = $"{newSurname}{separatorSign}{newWorkingPosition}";

                    dossiers.Add(newDossierKey, newDossierValue);
                    OutputWelcomeBanner(manual, availableСommands, programResponse);
                    Console.WriteLine($"{newDossierKey}. {newSurname} - {newWorkingPosition}");
                }
                else if (userInput == CommandReturnsToMenu)
                {
                    isCommandReturnsToMenu = true;
                }
                else
                {
                    OutputProgramResponse();
                }
            }

            return dossiers;
        }

        static string GetDataFromUser(string manualNewDossier)
        {
            string dataDossier;

            OutputWelcomeBanner(manualNewDossier);
            MoveCursor(manualNewDossier);
            dataDossier = Console.ReadLine();
            return dataDossier;
        }

        static void OutputDossierAll(Dictionary<int, string> dossiers, char separatorSign)
        {
            string manual = "Нажмите 'ВВОД', чтобы вернутся в меню: ";
            string programResponse = "-ВЫВЕДЕН СПИСОК ВСЕХ ДОСЬЕ-";
            string[] availableСommands = null;

            OutputWelcomeBanner(manual, availableСommands, programResponse);

            foreach (var dossier in dossiers)
            {
                string[] surnameAndWorkingPosition = dossier.Value.Split(separatorSign);
                string surname = surnameAndWorkingPosition[0];
                string workingPosition = surnameAndWorkingPosition[1];
                Console.WriteLine($"{dossier.Key}. {surname} - {workingPosition}");
            }

            MoveCursor(manual);
            Console.ReadKey();
        }

        static Dictionary<int, string> DeleteDossier(Dictionary<int, string> dossiers, char separatorSign)
        {
            const string CommandReturnsToMenu = "0";

            bool isCommandReturnsToMenu = false;
            string manual = "Введите номер досье подлежащее утилизации: ";
            string[] availableСommands = { $"{CommandReturnsToMenu}. Вернуться в меню" };

            OutputWelcomeBanner(manual, availableСommands);

            while (isCommandReturnsToMenu == false)
            {
                string userInput;
                int numberDeletedDossier;
                bool isNumberDeletedDossierInitialized;

                MoveCursor(manual);
                userInput = Console.ReadLine();
                isNumberDeletedDossierInitialized = int.TryParse(userInput, out numberDeletedDossier);
                OutputWelcomeBanner(manual, availableСommands);

                if (isNumberDeletedDossierInitialized == true && dossiers.ContainsKey(numberDeletedDossier))
                {
                    string programResponse = "-УДАЛЁННОЕ ДОСЬЕ-";
                    string[] deletedDossierSurnameAndWorkingPosition = dossiers[numberDeletedDossier].Split(separatorSign);
                    string deletedDossierSurname = deletedDossierSurnameAndWorkingPosition[0];
                    string deletedDossierWorkingPosition = deletedDossierSurnameAndWorkingPosition[1];

                    OutputWelcomeBanner(manual, availableСommands, programResponse);
                    Console.WriteLine($"{numberDeletedDossier}. {deletedDossierSurname} - {deletedDossierWorkingPosition}");
                    dossiers = ProcessDelete(dossiers, numberDeletedDossier);
                }
                else if (userInput == CommandReturnsToMenu)
                {
                    isCommandReturnsToMenu = true;
                }
                else
                {
                    OutputProgramResponse();
                }
            }

            return dossiers;
        }

        static Dictionary<int, string> ProcessDelete(Dictionary<int, string> dossiers, int numberDeleteDossier)
        {
            Dictionary<int, string> newDossiers = new Dictionary<int, string>();

            dossiers.Remove(numberDeleteDossier);

            foreach (var dossier in dossiers)
            {
                if (dossier.Key < numberDeleteDossier)
                {
                    newDossiers.Add(dossier.Key, dossier.Value);
                }
                else
                {
                    newDossiers.Add(dossier.Key - 1, dossier.Value);
                }
            }

            dossiers = newDossiers;
            return dossiers;
        }

        static void SearchByLastName(Dictionary<int, string> dossiers, char separatorSign)
        {
            const string CommandReturnsToMenu = "0";

            bool isCommandReturnsToMenu = false;
            string manual = "Введите искомую фамилию: ";
            string[] availableСommands = { $"{CommandReturnsToMenu}. Вернуться в меню" };

            OutputWelcomeBanner(manual, availableСommands);

            while (isCommandReturnsToMenu == false)
            {
                string userInput;

                MoveCursor(manual);
                userInput = Console.ReadLine();

                if (userInput != CommandReturnsToMenu)
                {
                    int numberSurnamesFound = 0;
                    string programResponse = "-ЗАПРОШЕННЫЕ ДАННЫЕ-";

                    OutputWelcomeBanner(manual, availableСommands, programResponse);

                    foreach (var dossier in dossiers)
                    {
                        string[] surnameAndWorkingPosition = dossier.Value.Split(separatorSign); 
                        string surname = surnameAndWorkingPosition[0];
                        string workingPosition = surnameAndWorkingPosition[1];

                        if (surname.ToLower() == userInput.ToLower())
                        {
                            numberSurnamesFound++;
                            Console.WriteLine($"{dossier.Key}. {surname} - {workingPosition}");
                        }
                    }

                    if (numberSurnamesFound == 0)
                    {
                        OutputWelcomeBanner(manual, availableСommands);
                        OutputProgramResponse();
                    }
                }
                else
                {
                    isCommandReturnsToMenu = true;
                }
            }
        }

        static bool Exit(bool haveUserExit, string manual, string[] availableСommands)
        {
            string valueProgramResponse = "запрос о выходе одобрен";

            haveUserExit = true;
            OutputWelcomeBanner(manual, availableСommands);
            OutputProgramResponse(valueProgramResponse);
            return haveUserExit;
        }

        static Dictionary<int, string> InitializationDossier(Dictionary<int, string> dossiers)
        {
            const string NameFileSurnames = "Surnames.txt";
            const string NameFileWorkingPositions = "WorkingPositions.txt";

            Random random = new Random();
            int dossiersNumbersMin = 0;
            int dossiersNumbersMax = 100;
            int dossiersNumbers = random.Next(dossiersNumbersMin, dossiersNumbersMax);

            for (int i = 0; i < dossiersNumbers; i++)
            {
                string surname = FillValueDossier(NameFileSurnames, random);
                string workingPosition = FillValueDossier(NameFileWorkingPositions, random);
                string dossier = $"{surname};{workingPosition}";

                dossiers.Add(i + 1, dossier);
            }

            return dossiers;
        }

        static string FillValueDossier(string nameFile, Random random)
        {
            string[] fileStrings = File.ReadAllLines($"Data/{nameFile}");
            int stringNumberMin = 0;
            int stringNumberMax = fileStrings.Length - 1;
            int stringNumber = random.Next(stringNumberMin, stringNumberMax);
            string dataString = fileStrings[stringNumber];

            return dataString;
        }

        static void OutputWelcomeBanner(string manual, string[] availableСommands = null, string programResponse = null)
        {
            string bannerTitle = "-ИНФОРМИУМ ЮСТИС МАЙОРИС-";
            string bannerSubtitle = "-База данных о гражданах Империуама в мире-улье Юстис Майорис-";

            Console.Clear();
            OutputСenteredString(bannerTitle);
            OutputСenteredString(bannerSubtitle);
            Console.WriteLine();
            Console.WriteLine(manual);

            if (availableСommands != null)
            {
                Console.WriteLine();

                foreach (string availableСommand in availableСommands)
                {
                    Console.WriteLine(availableСommand);
                }
            }

            if (programResponse != null)
            {
                OutputСenteredString(programResponse);
            }
        }

        static void OutputProgramResponse(string valueProgramResponse = "некорректный запрос")
        {
            string programResponse = "-ЗАПРОС-";

            Console.WriteLine();
            OutputСenteredString(programResponse);
            OutputСenteredString(valueProgramResponse);
        }

        static void OutputСenteredString(string text)
        {
            int valueHalf = 2;
            int locationTextX = (Console.WindowWidth / valueHalf) - (text.Length / valueHalf);
            int locationTextY = Console.CursorTop;

            Console.SetCursorPosition(locationTextX, locationTextY);
            Console.WriteLine(text);
        }

        static void MoveCursor(string text)
        {
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(text.Length, 3);
        }
    }
}
