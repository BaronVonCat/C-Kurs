using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace _4._1.KadroviyUchot
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

            string[] dossiersSurnames = new string[0];
            string[] dossiersWorkingPositions = new string[0];
            bool isCommandExit = false;
            string manual = "Введите номер запроса: ";
            string userInput;
            string[] availableСommands =
                {$"{CommandAddDossier}. Добавить досье",
                    $"{CommandOutputAllDossiers}. Вывести все досье",
                    $"{CommandDeleteDossier}. Удалить досье",
                    $"{CommandSearchByLastName}. Поиск по фамилии" ,
                    $"{CommandExit}. Выход из программы"};

            InitializationDossier(ref dossiersSurnames, ref dossiersWorkingPositions);
            OutputWelcomeBanner(manual, availableСommands);

            while (isCommandExit == false)
            {
                MoveCursor(manual);
                userInput = Console.ReadLine();
                OutputWelcomeBanner(manual, availableСommands);

                switch (userInput)
                {
                    case CommandAddDossier:
                        AddDossier(ref dossiersSurnames, ref dossiersWorkingPositions);
                        break;

                    case CommandOutputAllDossiers:
                        OutputDossierAll(dossiersSurnames, dossiersWorkingPositions);
                        break;

                    case CommandDeleteDossier:
                        DeleteDossier(ref dossiersSurnames, ref dossiersWorkingPositions);
                        break;

                    case CommandSearchByLastName:
                        SearchByLastName(dossiersSurnames, dossiersWorkingPositions);
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

        static void AddDossier( ref string[] dossiersSurnames, ref string[] dossiersWorkingPositions)
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

                    dossiersSurnames = ProcessAddingDossier(dossiersSurnames, manualNewDossierUsername);
                    dossiersWorkingPositions = ProcessAddingDossier(dossiersWorkingPositions, manualNewDossierWorkongPosition);
                    OutputWelcomeBanner(manual, availableСommands, programResponse);
                    Console.WriteLine($"{dossiersSurnames.Length}. {dossiersSurnames[dossiersSurnames.Length - 1]} - " +
                        $"{dossiersWorkingPositions[dossiersSurnames.Length - 1]}");
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
        }

        static string[] ProcessAddingDossier(string[] dossiers, string manualNewDossier)
        {
            string[] nextDossiers = new string[dossiers.Length + 1];
            string newDossier;

            OutputWelcomeBanner(manualNewDossier);
            MoveCursor(manualNewDossier);
            newDossier = Console.ReadLine();

            for (int i = 0; i < dossiers.Length; i++)
            {
                nextDossiers[i] = dossiers[i];
            }

            nextDossiers[nextDossiers.Length - 1] = newDossier;
            dossiers = nextDossiers;
            return dossiers;
        }

        static void OutputDossierAll( string[] surnames, string[] workingPositions)
        {
            int sequenceNumber;
            string manual = "Нажмите 'ВВОД', чтобы вернутся в меню: ";
            string programResponse = "-ВЫВЕДЕН СПИСОК ВСЕХ ДОСЬЕ-";
            string[] availableСommands = null;

            OutputWelcomeBanner(manual, availableСommands, programResponse);

            for (int i = 0; i < surnames.Length; i++)
            {
                sequenceNumber = i + 1;
                Console.WriteLine($"{sequenceNumber}. {surnames[i]} - {workingPositions[i]}");
            }

            MoveCursor(manual);
            Console.ReadKey();
        }

        static void DeleteDossier( ref string[] dossiersSurnames, ref string[] dossiersWorkingPosition)
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

                if (isNumberDeletedDossierInitialized == true 
                    && numberDeletedDossier != 0 
                    && numberDeletedDossier <= dossiersSurnames.Length)
                {
                    int newDossiersNumbers = dossiersSurnames.Length - 1;
                    string programResponse = "-УДАЛЁННОЕ ДОСЬЕ-";

                    numberDeletedDossier--;
                    OutputWelcomeBanner(manual, availableСommands, programResponse);
                    Console.WriteLine($"{userInput}. {dossiersSurnames[numberDeletedDossier]}" + 
                        $" - {dossiersWorkingPosition[numberDeletedDossier]}");
                    dossiersSurnames = ProcessDelete(dossiersSurnames, newDossiersNumbers, numberDeletedDossier);
                    dossiersWorkingPosition = ProcessDelete(dossiersWorkingPosition, newDossiersNumbers, numberDeletedDossier);
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
        }

        static string[] ProcessDelete(string[] dossiers, int newDossiersNumbers, int numberDeletedDossier)
        {
            string[] sortableDossiers = new string[newDossiersNumbers];

            for (int i = 0; i < newDossiersNumbers; i++)
            {
                if (i < numberDeletedDossier)
                {
                    sortableDossiers[i] = dossiers[i];
                }
                else
                {
                    sortableDossiers[i] = dossiers[i + 1];
                }
            }

            dossiers = sortableDossiers;
            return dossiers;
        }

        static void SearchByLastName( string[] dossiersSurnames, string[] dossiersWorkingPositions)
        {
            const string CommandReturnsToMenu = "0";

            bool isCommandReturnsToMenu = false;
            string manual = "Введите запрашиваемую фамилию: ";
            string[] availableСommands = { $"{CommandReturnsToMenu}. Вернуться в меню" };

            OutputWelcomeBanner(manual, availableСommands);

            while (isCommandReturnsToMenu == false)
            {
                string userInput;

                MoveCursor(manual);
                userInput = Console.ReadLine();

                if (userInput != CommandReturnsToMenu)
                {
                    int sequenceNumber;
                    int numberSurnamesFound = 0;
                    string programResponse = "-ЗАПРОШЕННЫЕ ДАННЫЕ-";

                    OutputWelcomeBanner(manual, availableСommands, programResponse);

                    for (int i = 0; i < dossiersSurnames.Length; i++)
                    {
                        if (dossiersSurnames[i].ToLower() == userInput.ToLower())
                        {
                            numberSurnamesFound++;
                            sequenceNumber = i + 1;
                            Console.WriteLine($"{sequenceNumber}. {dossiersSurnames[i]} - {dossiersWorkingPositions[i]}");
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

        static void InitializationDossier(ref string[] dossiersSurnames, ref string[] dossiersWorkingPositions)
        {
            Random random = new Random();
            int dossiersNumbersMin = 0;
            int dossiersNumbersMax = 10000;
            int dossiersNumbers = random.Next(dossiersNumbersMin, dossiersNumbersMax);
            string nameFileSurnames = "Surnames.txt";
            string nameFileWorkingPositions = "WorkingPositions.txt";

            dossiersSurnames = new string[dossiersNumbers];
            dossiersWorkingPositions = new string[dossiersNumbers];
            FillDossier(dossiersSurnames, nameFileSurnames, random);
            FillDossier(dossiersWorkingPositions, nameFileWorkingPositions, random);
        }

        static void FillDossier(string[] dossiers, string nameFile, Random random)
        {
            string[] fileStrings = File.ReadAllLines($"Data/{nameFile}");
            int stringNumberMin = 0;
            int stringNumberMax = fileStrings.Length - 1;
            int stringNumber;

            for (int i = 0; i < dossiers.Length; i++)
            {
                stringNumber = random.Next(stringNumberMin, stringNumberMax);
                dossiers[i] = fileStrings[stringNumber];
            }
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
