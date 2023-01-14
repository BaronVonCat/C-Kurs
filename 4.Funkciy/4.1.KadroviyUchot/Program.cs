using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;

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

            Random random = new Random();
            int dossiersNumbersMin = 0;
            int dossiersNumbersMax = 10000;
            int dossiersNumbers = random.Next(dossiersNumbersMin, dossiersNumbersMax);
            string[] dossiersSurnames = new string[dossiersNumbers];
            string nameFileSurnames = "Surnames.txt";
            string[] dossiersWorkingPositions = new string[dossiersNumbers];
            string nameFileWorkingPositions = "WorkingPositions.txt";
            bool isCommandExit = false;
            string manual = "Введите номер запроса: ";
            string userInput;
            string[] availableСommands =
                {$"{CommandAddDossier}. Добавить досье",
                    $"{CommandOutputAllDossiers}. Вывести все досье",
                    $"{CommandDeleteDossier}. Удалить досье",
                    $"{CommandSearchByLastName}. Поиск по фамилии" ,
                    $"{CommandExit}. Выход из программы"};

            DossierInitializing(dossiersSurnames, nameFileSurnames, random);
            DossierInitializing(dossiersWorkingPositions, nameFileWorkingPositions, random);
            OutputWelcomeBanner(manual, availableСommands);

            while (isCommandExit == false)
            {
                MovingCursor(manual);
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAddDossier:
                        AddDossier(ref dossiersSurnames, ref dossiersWorkingPositions, ref dossiersNumbers);
                        break;

                    case CommandOutputAllDossiers:
                        OutputDossierAll(dossiersSurnames, dossiersWorkingPositions);
                        break;

                    case CommandDeleteDossier:
                        DeletingDossier(ref dossiersSurnames, ref dossiersWorkingPositions, ref dossiersNumbers);
                        break;

                    case CommandSearchByLastName:
                        SearchByLastName(dossiersSurnames, dossiersWorkingPositions, dossiersNumbers);
                        break;

                    case CommandExit:
                        Exit(ref isCommandExit);
                        break;
                }

                OutputWelcomeBanner(manual, availableСommands);

                if (userInput != CommandAddDossier 
                    && userInput != CommandOutputAllDossiers
                    && userInput != CommandDeleteDossier
                    && userInput != CommandSearchByLastName
                    && userInput != CommandExit)
                {
                    OutputProgramResponse();
                }
                else if (isCommandExit == true)
                {
                    string valueProgramResponse = "запрос о выходе одобрен";
                    OutputProgramResponse(valueProgramResponse);
                    Console.ReadKey();
                }
            }
        }

        static void AddDossier(ref string[] dossiersSurnames, ref string[] dossiersWorkingPositions, ref int dossiersNumbers)
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
                MovingCursor(manual);
                userInput = Console.ReadLine();
                OutputWelcomeBanner(manual, availableСommands);


                if (userInput == CommandAddDossier)
                {
                    string manualNewDossierUsername = "Введите фамилию гражданина: ";
                    string manualNewDossierWorkongPosition = "Введите должность гражданина: ";
                    string programResponse = "-ДОБАВЛЕННОЕ ДОСЬЕ-";

                    ProcessAddingDossier(ref dossiersSurnames, manualNewDossierUsername, dossiersNumbers);
                    ProcessAddingDossier(ref dossiersWorkingPositions, manualNewDossierWorkongPosition, dossiersNumbers);
                    dossiersNumbers++;
                    OutputWelcomeBanner(manual, availableСommands, programResponse);
                    Console.WriteLine($"{dossiersNumbers}. {dossiersSurnames[dossiersNumbers - 1]} - " +
                        $"{dossiersWorkingPositions[dossiersNumbers - 1]}");
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

        static void ProcessAddingDossier(ref string[] dossiers, string manualNewDossier, int dossiersNumbers)
        {
            string[] nextDossiers = new string[dossiersNumbers + 1];
            string newDossier;

            OutputWelcomeBanner(manualNewDossier);
            MovingCursor(manualNewDossier);
            newDossier = Console.ReadLine();

            for (int i = 0; i < dossiersNumbers; i++)
            {
                nextDossiers[i] = dossiers[i];
            }

            nextDossiers[nextDossiers.Length - 1] = newDossier;
            dossiers = nextDossiers;
        }

        static void OutputDossierAll(string[] surnames, string[] workingPositions)
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

            MovingCursor(manual);
            Console.ReadKey();
        }

        static void DeletingDossier(ref string[] surnames, ref string[] workingPosition, ref int dossiersNumbers)
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

                MovingCursor(manual);
                userInput = Console.ReadLine();
                isNumberDeletedDossierInitialized = int.TryParse(userInput, out numberDeletedDossier);
                OutputWelcomeBanner(manual, availableСommands);

                if (isNumberDeletedDossierInitialized == true 
                    && numberDeletedDossier != 0 
                    && numberDeletedDossier <=dossiersNumbers)
                {
                    int newDossiersNumbers = dossiersNumbers - 1;
                    string[] sortableSurnames = new string[newDossiersNumbers];
                    string[] sortableWorkingPositions = new string[newDossiersNumbers];
                    string programResponse = "-УДАЛЁННОЕ ДОСЬЕ-";

                    numberDeletedDossier--;
                    OutputWelcomeBanner(manual, availableСommands, programResponse);
                    Console.WriteLine($"{userInput}. {surnames[numberDeletedDossier]}" + $" - {workingPosition[numberDeletedDossier]}");

                    for (int i = 0; i < newDossiersNumbers; i++)
                    {
                        if (i < numberDeletedDossier)
                        {
                            sortableSurnames[i] = surnames[i];
                            sortableWorkingPositions[i] = workingPosition[i];
                        }
                        else
                        {
                            sortableSurnames[i] = surnames[i + 1];
                            sortableWorkingPositions[i] = workingPosition[i + 1];
                        }
                    }

                    surnames = sortableSurnames;
                    workingPosition = sortableWorkingPositions;
                    dossiersNumbers = newDossiersNumbers;
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

        static void SearchByLastName(string[] surnames, string[] workingPositions, int dossiersNumbers)
        {
            const string CommandReturnsToMenu = "0";

            bool isCommandReturnsToMenu = false;
            string manual = "Введите запрашиваемую фамилию: ";
            string[] availableСommands = { $"{CommandReturnsToMenu}. Вернуться в меню" };

            OutputWelcomeBanner(manual, availableСommands);

            while (isCommandReturnsToMenu == false)
            {
                string userInput;

                MovingCursor(manual);
                userInput = Console.ReadLine();

                if (userInput != CommandReturnsToMenu)
                {
                    int sequenceNumber;
                    int numberSurnamesFound = 0;
                    string programResponse = "-ЗАПРОШЕННЫЕ ДАННЫЕ-";

                    OutputWelcomeBanner(manual, availableСommands, programResponse);

                    for (int i = 0; i < dossiersNumbers; i++)
                    {
                        if (surnames[i].ToLower() == userInput.ToLower())
                        {
                            numberSurnamesFound++;
                            sequenceNumber = i + 1;
                            Console.WriteLine($"{sequenceNumber}. {surnames[i]} - {workingPositions[i]}");
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

        static void Exit(ref bool haveUserExit)
        {
            string programResponse = "-ЗАПРОС-";
            int locationProgramResponseX = (Console.WindowWidth / 2) - (programResponse.Length / 2);
            int locationProgramResponseY = Console.CursorTop + 1;
            string valueProgramResponse = "запрос о выходе одобрен";
            int locationValueProgramResponseX = (Console.WindowWidth / 2) - (valueProgramResponse.Length / 2) - 1;
            int locationValueProgramResponseY;

            haveUserExit = true;
            Console.SetCursorPosition(locationProgramResponseX, locationProgramResponseY);
            Console.WriteLine(programResponse);
            locationValueProgramResponseY = Console.CursorTop;
            Console.SetCursorPosition(locationValueProgramResponseX, locationValueProgramResponseY);
            Console.WriteLine(valueProgramResponse);
        }

        static void DossierInitializing(string[] dossiers, string nameFile, Random random)
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
            int locationBannerTitleX = Console.WindowWidth / 2 - bannerTitle.Length / 2;
            int locationBannerTitleY = 0;
            string bannerSubtitle = "-База данных о гражданах Империуама в мире-улье Юстис Майорис-";
            int locationBannerSubtitleX = (Console.WindowWidth / 2 - bannerSubtitle.Length / 2) + 1;
            int locationBannerSubtitleY = locationBannerTitleY + 1;

            
            Console.Clear();
            Console.SetCursorPosition(locationBannerTitleX, locationBannerTitleY);
            Console.WriteLine(bannerTitle);
            Console.SetCursorPosition(locationBannerSubtitleX, locationBannerSubtitleY);
            Console.WriteLine(bannerSubtitle);
            Console.WriteLine($"\n{manual}");

            if (availableСommands != null)
            {
                Console.WriteLine();

                foreach (string availableСommand in availableСommands)
                {
                    Console.WriteLine(availableСommand);
                }
            }

            if(programResponse != null)
            {
                int locationProgramResponseX = (Console.WindowWidth / 2) - (programResponse.Length / 2);
                int locationProgramResponseY = Console.CursorTop + 1;

                Console.SetCursorPosition(locationProgramResponseX, locationProgramResponseY);
                Console.WriteLine(programResponse);
            }
        }

        static void OutputProgramResponse(string valueProgramResponse = "некорректный запрос")
        {
            string programResponse = "-ЗАПРОС-";
            int locationProgramResponseX = (Console.WindowWidth / 2) - (programResponse.Length / 2);
            int locationProgramResponseY = Console.CursorTop + 1;
            int locationValueProgramResponseX = (Console.WindowWidth / 2) - (valueProgramResponse.Length / 2) - 1;
            int locationValueProgramResponseY;

            Console.SetCursorPosition(locationProgramResponseX, locationProgramResponseY);
            Console.WriteLine(programResponse);
            locationValueProgramResponseY = Console.CursorTop;
            Console.SetCursorPosition(locationValueProgramResponseX, locationValueProgramResponseY);
            Console.WriteLine(valueProgramResponse);
        }

        static void MovingCursor(string text)
        {
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(text.Length, 3);
        }
    }
}
