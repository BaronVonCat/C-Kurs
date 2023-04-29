using System;
using System.Collections.Generic;
using System.Linq;

namespace _6._4.KolodKart
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandExit = "0";
            const string CommandTakeCard = "1";
            const string CommandTakeSomeCards = "2";
            const string CommandTakeAllCards = "3";
            const string CommandPutCardsBack = "4";

            Deck deck = new Deck();
            User user = new User();
            bool isWorke = false;
            int inputPositionX;

            while (isWorke == false)
            {
                Console.Write("Введите необходимую комманду: ");
                inputPositionX = Console.CursorLeft;
                Console.WriteLine();
                Console.WriteLine($"Комманды:\n\n" +
                    $"{CommandExit}. Выход\n" +
                    $"{CommandTakeCard}. Взять одну карту\n" +
                    $"{CommandTakeSomeCards}. Взять несколько карт\n" +
                    $"{CommandTakeAllCards}. Взять все карты\n" +
                    $"{CommandPutCardsBack}. Положить карды обратно\n");
                Console.WriteLine($"Карт в колоде: {deck.GetCardsCount()}");
                Console.WriteLine($"Ваши карты:\n");
                user.ShowCards();
                Console.SetCursorPosition(inputPositionX, 0);

                switch (Console.ReadLine())
                {
                    case CommandExit:
                        isWorke = true;
                        break;

                    case CommandTakeCard:
                        deck.TryTransferCard(user);
                        break;
                        
                    case CommandTakeSomeCards:
                        deck.TryTransferSomeCards(user);
                        break;

                    case CommandTakeAllCards:
                        deck.TransferAllCards(user);
                        break;

                    case CommandPutCardsBack:
                        user.TransferCards(deck);
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Некорректный запрос!");
                        Console.ReadKey();
                        break;
                }

                Console.Clear();
            }
        }
    }

    enum CardValue
    {
        six,
        seven, 
        eight, 
        nine, 
        ten, 
        jack, 
        queen, 
        king, 
        ace
    }

    enum CardSuit
    {
        Pic,
        Kreus,
        Karo,
        Herz
    }

    class User
    {
        private List<Card> _cards = new List<Card>();

        public void AddCard(Card card)
        {
            _cards.Add(card);
        }

        public void AddCards(List<Card> cards)
        {
            _cards.AddRange(cards);
        }

        public void TransferCards(Deck deck)
        {
            deck.AddCards(_cards.ToList());
            _cards.Clear();
        }

        public void ShowCards()
        {
            foreach (Card card in _cards)
            {
                card.Show();
                Console.WriteLine();
            }
        }
    }

    class Deck
    {
        private List<Card> _cards = new List<Card>();

        public Deck()
        {
            int numberSuits = Enum.GetValues(typeof(CardSuit)).Length;
            int numberCardsPerSuit = Enum.GetValues(typeof(CardValue)).Length;

            for (int i = 0; i < numberSuits; i++)
            {
                for (int j = 0; j < numberCardsPerSuit; j++)
                {
                    Card card = new Card((CardSuit)i, (CardValue)j);
                    _cards.Add(card);
                }
            }
        }

        public void TryTransferCard(User user)
        {
            Console.Clear();

            if (_cards.Count > 0)
            {
                Card card = _cards.Last();
                _cards.Remove(card);
                user.AddCard(card);
            }
            else
            {
                Console.WriteLine("Колода закончилас!");
                Console.ReadKey();
            }
        }

        public void TryTransferSomeCards(User user)
        {
            string userInput;
            int requiredNumberCards;
            bool isNumber;

            Console.Clear();
            Console.WriteLine("Введите количество карт, что хотите взять: ");
            userInput = Console.ReadLine();
            isNumber = int.TryParse(userInput, out requiredNumberCards);

            if (isNumber == true && _cards.Count >= requiredNumberCards)
            {
                for(int i = 0; i < requiredNumberCards; i++)
                {
                    TryTransferCard(user);
                }
            }
            else
            {
                Console.WriteLine("Стольких карт нет в колоде!");
                Console.ReadKey();
            }
        }

        public void TransferAllCards(User user)
        {
            user.AddCards(_cards.ToList());
            _cards.Clear();
        }

        public void AddCards(List<Card> cards)
        {
            _cards.AddRange(cards);
        }

        public int GetCardsCount()
        {
            return _cards.Count;
        }
    }

    class Card
    {
        private static string[] _namesValues;
        private static string[] _namesSuits;
        private CardValue _value;
        private CardSuit _suit;

        static Card()
        {
            string[] localizedNamesValues =
            {
                "Шестёрка",
                "Семёрка",
                "Восьмёрка",
                "Девятка",
                "Десятка",
                "Валет",
                "Дама",
                "Король",
                "Туз"
            };

            string[] localizedNamesSuits =
            {
                "Пик",
                "Крести",
                "Бубен",
                "Черви"
            };

            _namesSuits = Enum.GetNames(typeof(CardSuit));
            _namesValues = Enum.GetNames(typeof(CardValue));

            GetLocalizedNames(_namesSuits, localizedNamesSuits);
            GetLocalizedNames(_namesValues, localizedNamesValues);
        } 

        public Card( CardSuit suit, CardValue value)
        {
            _value = value;
            _suit = suit;
        }

        public void Show()
        {
            Console.Write($"{_namesValues[(int)_value]} {_namesSuits[(int)_suit]}");
        }

        private static void GetLocalizedNames(string[] names, string[] localizedNames)
        {
            for (int i = 0; i < names.Length; i++)
            {
                if (i < localizedNames.Length)
                {
                    names[i] = localizedNames[i];
                }
            }
        }
    }
}
