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

            CardGame cardGame = new CardGame();
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
                cardGame.ShowInfo();
                Console.SetCursorPosition(inputPositionX, 0);

                switch (Console.ReadLine())
                {
                    case CommandExit:
                        isWorke = true;
                        break;

                    case CommandTakeCard:
                        cardGame.TransferCardToUser();
                        break;
                        
                    case CommandTakeSomeCards:
                        cardGame.TransferSomeCardsToUser();
                        break;

                    case CommandTakeAllCards:
                        cardGame.TransferCardsToUser();
                        break;

                    case CommandPutCardsBack:
                        cardGame.TransferCardsToDeck();
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
        Six,
        Seven, 
        Eight, 
        Nine, 
        Ten, 
        Jack, 
        Queen, 
        King, 
        Ace
    }

    enum CardSuit
    {
        Pic,
        Kreus,
        Karo,
        Herz,
    }

    class CardGame
    {
        private User _user = new User();
        private Deck _deck = new Deck();

        public void TransferCardToUser()
        {
            if (_deck.GetCardsCount() > 0)
            {
                _user.AddCard(_deck.PullCard());
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Колода закончилась!");
                Console.ReadKey();
            }
        }

        public void TransferSomeCardsToUser()
        {
            string userInput;
            int requiredNumberCards;
            bool isNumber;

            Console.Clear();
            Console.WriteLine("Введите количество карт, что хотите взять: ");
            userInput = Console.ReadLine();
            isNumber = int.TryParse(userInput, out requiredNumberCards);

            if (isNumber == true && _deck.GetCardsCount() >= requiredNumberCards)
            {
                for (int i = 0; i < requiredNumberCards; i++)
                {
                    TransferCardToUser();
                }
            }
            else
            {
                Console.WriteLine("Стольких карт нет в колоде!");
                Console.ReadKey();
            }
        }

        public void TransferCardsToUser()
        {
            _user.AddCards(_deck.PullCards());
        }

        public void TransferCardsToDeck()
        {
            _deck.AddCards(_user.PullCards());
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Карт в колоде: {_deck.GetCardsCount()}");
            Console.WriteLine($"Ваши карты:\n");
            _user.ShowCards();
        }
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

        public List<Card> PullCards()
        {
            List<Card> cards = _cards.ToList();
            _cards.Clear();
            return cards;
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

        public Card PullCard()
        {
            Card card = _cards.Last();
            _cards.Remove(card);

            return card;
        }

        public List<Card> PullCards()
        {
            List<Card> cards = _cards.ToList();
            _cards.Clear();
            return cards;
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
        private CardValue _value;
        private CardSuit _suit;

        public Card( CardSuit suit, CardValue value)
        {
            _value = value;
            _suit = suit;
        }

        public void Show()
        {
            Console.Write($"{_value} {_suit}");
        }
    }
}
