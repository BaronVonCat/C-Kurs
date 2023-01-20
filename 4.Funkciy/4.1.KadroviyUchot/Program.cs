using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

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

            int dossiersNumbers = 0;
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

            ToInitializationDossier(ref dossiersNumbers, ref dossiersSurnames, ref dossiersWorkingPositions);
            ToOutputWelcomeBanner(manual, availableСommands);

            while (isCommandExit == false)
            {
                ToMovingCursor(manual);
                userInput = Console.ReadLine();
                ToOutputWelcomeBanner(manual, availableСommands);

                switch (userInput)
                {
                    case CommandAddDossier:
                        ToAddDossier(ref dossiersSurnames, ref dossiersWorkingPositions, ref dossiersNumbers);
                        ToOutputWelcomeBanner(manual, availableСommands);
                        break;

                    case CommandOutputAllDossiers:
                        ToOutputDossierAll(dossiersSurnames, dossiersWorkingPositions);
                        ToOutputWelcomeBanner(manual, availableСommands);
                        break;

                    case CommandDeleteDossier:
                        ToDeleteDossier(ref dossiersSurnames, ref dossiersWorkingPositions, ref dossiersNumbers);
                        ToOutputWelcomeBanner(manual, availableСommands);
                        break;

                    case CommandSearchByLastName:
                        ToSearchByLastName(dossiersSurnames, dossiersWorkingPositions, dossiersNumbers);
                        ToOutputWelcomeBanner(manual, availableСommands);
                        break;

                    case CommandExit:
                        ToExit(ref isCommandExit, manual, availableСommands);
                        break;

                    default:
                        ToOutputProgramResponse();
                        break;
                }
            }
        }

        static void ToAddDossier(ref string[] dossiersSurnames, ref string[] dossiersWorkingPositions, ref int dossiersNumbers)
        {
            const string CommandReturnsToMenu = "0";
            const string CommandAddDossier = "1";

            string manual = "Введите номер комманды: ";
            string userInput;
            bool isCommandReturnsToMenu = false;
            string[] availableСommands = 
                { $"{CommandAddDossier}. Добавить досье",
                $"{CommandReturnsToMenu}. Вернуться в меню" };

            ToOutputWelcomeBanner(manual, availableСommands);

            while (isCommandReturnsToMenu == false)
            {
                ToMovingCursor(manual);
                userInput = Console.ReadLine();
                ToOutputWelcomeBanner(manual, availableСommands);

                if (userInput == CommandAddDossier)
                {
                    string manualNewDossierUsername = "Введите фамилию гражданина: ";
                    string manualNewDossierWorkongPosition = "Введите должность гражданина: ";
                    string programResponse = "-ДОБАВЛЕННОЕ ДОСЬЕ-";

                    ToProcessAddingDossier(ref dossiersSurnames, manualNewDossierUsername, dossiersNumbers);
                    ToProcessAddingDossier(ref dossiersWorkingPositions, manualNewDossierWorkongPosition, dossiersNumbers);
                    dossiersNumbers++;
                    ToOutputWelcomeBanner(manual, availableСommands, programResponse);
                    Console.WriteLine($"{dossiersNumbers}. {dossiersSurnames[dossiersNumbers - 1]} - " +
                        $"{dossiersWorkingPositions[dossiersNumbers - 1]}");
                }
                else if (userInput == CommandReturnsToMenu)
                {
                    isCommandReturnsToMenu = true;
                }
                else 
                {
                    ToOutputProgramResponse();
                }
            }
        }

        static void ToProcessAddingDossier(ref string[] dossiers, string manualNewDossier, int dossiersNumbers)
        {
            string[] nextDossiers = new string[dossiersNumbers + 1];
            string newDossier;

            ToOutputWelcomeBanner(manualNewDossier);
            ToMovingCursor(manualNewDossier);
            newDossier = Console.ReadLine();

            for (int i = 0; i < dossiersNumbers; i++)
            {
                nextDossiers[i] = dossiers[i];
            }

            nextDossiers[nextDossiers.Length - 1] = newDossier;
            dossiers = nextDossiers;
        }

        static void ToOutputDossierAll(string[] surnames, string[] workingPositions)
        {
            int sequenceNumber;
            string manual = "Нажмите 'ВВОД', чтобы вернутся в меню: ";
            string programResponse = "-ВЫВЕДЕН СПИСОК ВСЕХ ДОСЬЕ-";
            string[] availableСommands = null;

            ToOutputWelcomeBanner(manual, availableСommands, programResponse);

            for (int i = 0; i < surnames.Length; i++)
            {
                sequenceNumber = i + 1;
                Console.WriteLine($"{sequenceNumber}. {surnames[i]} - {workingPositions[i]}");
            }

            ToMovingCursor(manual);
            Console.ReadKey();
        }

        static void ToDeleteDossier(ref string[] surnames, ref string[] workingPosition, ref int dossiersNumbers)
        {
            const string CommandReturnsToMenu = "0";

            bool isCommandReturnsToMenu = false;
            string manual = "Введите номер досье подлежащее утилизации: ";
            string[] availableСommands = { $"{CommandReturnsToMenu}. Вернуться в меню" };

            ToOutputWelcomeBanner(manual, availableСommands);

            while (isCommandReturnsToMenu == false)
            {
                string userInput;
                int numberDeletedDossier;
                bool isNumberDeletedDossierInitialized;

                ToMovingCursor(manual);
                userInput = Console.ReadLine();
                isNumberDeletedDossierInitialized = int.TryParse(userInput, out numberDeletedDossier);
                ToOutputWelcomeBanner(manual, availableСommands);

                if (isNumberDeletedDossierInitialized == true 
                    && numberDeletedDossier != 0 
                    && numberDeletedDossier <=dossiersNumbers)
                {
                    int newDossiersNumbers = dossiersNumbers - 1;
                    string[] sortableSurnames = new string[newDossiersNumbers];
                    string[] sortableWorkingPositions = new string[newDossiersNumbers];
                    string programResponse = "-УДАЛЁННОЕ ДОСЬЕ-";

                    numberDeletedDossier--;
                    ToOutputWelcomeBanner(manual, availableСommands, programResponse);
                    Console.WriteLine($"{userInput}. {surnames[numberDeletedDossier]}" + $" - {workingPosition[numberDeletedDossier]}");
                    ToProcessDelete(ref surnames, newDossiersNumbers, numberDeletedDossier);
                    ToProcessDelete(ref workingPosition, newDossiersNumbers, numberDeletedDossier);
                    dossiersNumbers = newDossiersNumbers;
                }
                else if (userInput == CommandReturnsToMenu)
                {
                    isCommandReturnsToMenu = true;
                }
                else
                {
                    ToOutputProgramResponse();
                }
            }
        }

        static void ToProcessDelete(ref string[] dossiers, int newDossiersNumbers, int numberDeletedDossier)
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
        }

        static void ToSearchByLastName(string[] surnames, string[] workingPositions, int dossiersNumbers)
        {
            const string CommandReturnsToMenu = "0";

            bool isCommandReturnsToMenu = false;
            string manual = "Введите запрашиваемую фамилию: ";
            string[] availableСommands = { $"{CommandReturnsToMenu}. Вернуться в меню" };

            ToOutputWelcomeBanner(manual, availableСommands);

            while (isCommandReturnsToMenu == false)
            {
                string userInput;

                ToMovingCursor(manual);
                userInput = Console.ReadLine();

                if (userInput != CommandReturnsToMenu)
                {
                    int sequenceNumber;
                    int numberSurnamesFound = 0;
                    string programResponse = "-ЗАПРОШЕННЫЕ ДАННЫЕ-";

                    ToOutputWelcomeBanner(manual, availableСommands, programResponse);

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
                        ToOutputWelcomeBanner(manual, availableСommands);
                        ToOutputProgramResponse();
                    }
                }
                else
                {
                    isCommandReturnsToMenu = true;
                }
            }
        }

        static void ToExit(ref bool haveUserExit, string manual, string[] availableСommands)
        {
            int valueHalf = 2;
            string programResponse = "-ЗАПРОС-";
            int locationProgramResponseX = (Console.WindowWidth / valueHalf) - (programResponse.Length / valueHalf);
            int locationProgramResponseY = Console.CursorTop + 1;
            string valueProgramResponse = "запрос о выходе одобрен";
            int locationValueProgramResponseX = (Console.WindowWidth / valueHalf) - (valueProgramResponse.Length / valueHalf) - 1;
            int locationValueProgramResponseY;

            haveUserExit = true;
            ToOutputWelcomeBanner(manual, availableСommands);
            Console.SetCursorPosition(locationProgramResponseX, locationProgramResponseY);
            Console.WriteLine(programResponse);
            locationValueProgramResponseY = Console.CursorTop;
            Console.SetCursorPosition(locationValueProgramResponseX, locationValueProgramResponseY);
            Console.WriteLine(valueProgramResponse);
        }

        static void ToInitializationDossier
            (
            ref int dossiersNumbers,
            ref string[] dossiersSurnames,
            ref string[] dossiersWorkingPositions
            )
        {
            Random random = new Random();
            int dossiersNumbersMin = 0;
            int dossiersNumbersMax = 10000;
            string nameFileSurnames = "Surnames.txt";
            string nameFileWorkingPositions = "WorkingPositions.txt";

            dossiersNumbers = random.Next(dossiersNumbersMin, dossiersNumbersMax);
            dossiersSurnames = new string[dossiersNumbers];
            dossiersWorkingPositions = new string[dossiersNumbers];
            ToFillDossier(dossiersSurnames, nameFileSurnames, random);
            ToFillDossier(dossiersWorkingPositions, nameFileWorkingPositions, random);
        }

        static void ToFillDossier(string[] dossiers, string nameFile, Random random)
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

        static void ToOutputWelcomeBanner(string manual, string[] availableСommands = null, string programResponse = null)
        {
            int valueHalf = 2;
            string bannerTitle = "-ИНФОРМИУМ ЮСТИС МАЙОРИС-";
            int locationBannerTitleX = Console.WindowWidth / valueHalf - bannerTitle.Length / valueHalf;
            int locationBannerTitleY = 0;
            string bannerSubtitle = "-База данных о гражданах Империуама в мире-улье Юстис Майорис-";
            int locationBannerSubtitleX = (Console.WindowWidth / valueHalf - bannerSubtitle.Length / valueHalf) + 1;
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
                int locationProgramResponseX = (Console.WindowWidth / valueHalf) - (programResponse.Length / valueHalf);
                int locationProgramResponseY = Console.CursorTop + 1;

                Console.SetCursorPosition(locationProgramResponseX, locationProgramResponseY);
                Console.WriteLine(programResponse);
            }
        }

        static void ToOutputProgramResponse(string valueProgramResponse = "некорректный запрос")
        {
            int valueHalf = 2;
            string programResponse = "-ЗАПРОС-";
            int locationProgramResponseX = (Console.WindowWidth / valueHalf) - (programResponse.Length / valueHalf);
            int locationProgramResponseY = Console.CursorTop + 1;
            int locationValueProgramResponseX = (Console.WindowWidth / valueHalf) - (valueProgramResponse.Length / valueHalf) - 1;
            int locationValueProgramResponseY;

            Console.SetCursorPosition(locationProgramResponseX, locationProgramResponseY);
            Console.WriteLine(programResponse);
            locationValueProgramResponseY = Console.CursorTop;
            Console.SetCursorPosition(locationValueProgramResponseX, locationValueProgramResponseY);
            Console.WriteLine(valueProgramResponse);
        }

        static void ToMovingCursor(string text)
        {
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(text.Length, 3);
        }
    }
}
