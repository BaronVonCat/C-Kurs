using System;
using System.Collections.Generic;
using System.Linq;

namespace _6._4.KolodKart
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CardGame cardGame = new CardGame();

            cardGame.Work();
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

        public void Work()
        {
            const string CommandExit = "0";
            const string CommandTakeCard = "1";
            const string CommandTakeSomeCards = "2";
            const string CommandTakeAllCards = "3";
            const string CommandPutCardsBack = "4";
            const string CommandShufflDeck = "5";

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
                    $"{CommandPutCardsBack}. Положить карды обратно\n" +
                    $"{CommandShufflDeck}. Перемешать колоду\n");
                ShowInfo();
                Console.SetCursorPosition(inputPositionX, 0);

                switch (Console.ReadLine())
                {
                    case CommandExit:
                        isWorke = true;
                        break;

                    case CommandTakeCard:
                        TransferCardToUser();
                        break;

                    case CommandTakeSomeCards:
                        TransferSomeCardsToUser();
                        break;

                    case CommandTakeAllCards:
                        _user.AddCards(_deck.GiveCards());
                        break;

                    case CommandPutCardsBack:
                        _deck.AddCards(_user.PullCards());
                        break;

                    case CommandShufflDeck:
                        _deck.ShufflCards();
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

        private void TransferCardToUser()
        {
            if (_deck.GetCardsCount() > 0)
            {
                _user.AddCard(_deck.GiveCard());
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Колода закончилась!");
                Console.ReadKey();
            }
        }

        private void TransferSomeCardsToUser()
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

        private void ShowInfo()
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
        private List<Card> _cards;

        public Deck()
        {
            _cards = CreateCards();
        }

        public void ShufflCards()
        {
            Random random = new Random();
            int shuffleCardNumber;

            for (int i = 0; i < _cards.Count; i++)
            {
                shuffleCardNumber = random.Next(0, _cards.Count);
                (_cards[i], _cards[shuffleCardNumber]) = (_cards[shuffleCardNumber], _cards[i]);
            }
        }

        public Card GiveCard()
        {
            Card card = _cards.Last();
            _cards.Remove(card);

            return card;
        }

        public List<Card> GiveCards()
        {
            List<Card> cards = _cards;
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

        private List<Card> CreateCards()
        {
            List<Card> cards = new List<Card>();

            int numberSuits = Enum.GetValues(typeof(CardSuit)).Length;
            int numberCardsPerSuit = Enum.GetValues(typeof(CardValue)).Length;

            for (int i = 0; i < numberSuits; i++)
            {
                for (int j = 0; j < numberCardsPerSuit; j++)
                {
                    Card card = new Card((CardSuit)i, (CardValue)j);
                    cards.Add(card);
                }
            }

            return cards;
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
