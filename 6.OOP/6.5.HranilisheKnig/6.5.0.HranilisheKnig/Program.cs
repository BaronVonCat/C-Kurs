using System;
using System.Collections.Generic;

namespace _6._5.HranilisheKnig
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            library.Work();
        }
    }

    class Library
    {
        private List<Book> _books;

        public Library()
        {
            _books = CreateBooks();
        }

        public void Work()
        {
            const string CommandExit = "0";
            const string CommandAddBook = "1";
            const string CommandDeleteBook = "2";
            const string CommandShowBooks = "3";
            const string CommandShowBooksByParameter = "4";

            bool isWorke = true;

            while (isWorke == true)
            {
                int inputPositionX;

                Console.Clear();
                Console.Write("Введите номер необходимой комманды: ");
                inputPositionX = Console.CursorLeft;
                Console.WriteLine("\n\nДоступные комманды:\n" +
                    $"\n{CommandExit}. Выйти из прогрммы" +
                    $"\n{CommandAddBook}. Добавить книгу" +
                    $"\n{CommandDeleteBook}. Удалить книгу" +
                    $"\n{CommandShowBooks}. Показать книги" +
                    $"\n{CommandShowBooksByParameter}. Показать книги по параметру");
                Console.SetCursorPosition(inputPositionX, 0);

                switch (Console.ReadLine())
                {
                    case CommandExit:
                        isWorke = false;
                        break;

                    case CommandAddBook:
                        AddBook();
                        break;

                    case CommandDeleteBook:
                        DeleteBook();
                        break;

                    case CommandShowBooks:
                        ShowBooks();
                        break;

                    case CommandShowBooksByParameter:
                        ShowBooksByParameter();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Некорректный запрос!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void AddBook()
        {
            string title = CreateTitle();
            string author = CreateAuthor();
            int yearWriting = CreateYearPublication();
            Book book = new Book(title, author, yearWriting);

            _books.Add(book);
            Console.Clear();
            Console.WriteLine($"Добавленная книга:\n");
            book.Show();
            Console.ReadKey();
        }

        private void DeleteBook()
        {
            Book book = SearchBook();

            if (book != null)
            {
                _books.Remove(book);
                Console.Clear();
                Console.WriteLine("Удалённая книга:\n");
                book.Show();
                Console.ReadKey();
            }
        }

        private void ShowBooks()
        {
            Console.Clear();
            Console.WriteLine("название|автор|год публикации");
            Console.WriteLine();

            foreach ( Book book in _books )
            {
                book.Show();
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        private void ShowBooksByParameter()
        {
            List<Book> books = SearchBooksByParameter();

            Console.Clear();

            if (books.Count > 0)
            {
                foreach (Book book in books)
                {
                    book.Show();
                    Console.WriteLine();
                }

                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Книг по вашему запросу не найдено!");
                Console.ReadKey();
            }
        }

        private List<Book> CreateBooks()
        {
            List<Book> books = new List<Book>
            {
                new Book("Иллиада", "Гомер", 0),
                new Book("Эпос о Гильгамеше", "неизвестно", 0),
                new Book("Блэкаут", "Марк Эльсберг", 2012),
                new Book("Триумфальная арка", "Эрих Мария Ремарк", 1939)
            };

            return books;
        }

        private string CreateTitle()
        {
            string title = null;
            bool isTitleEnteredCorrectly = false;

            while (isTitleEnteredCorrectly == false)
            {
                char simbolWhiteSpace = ' ';
                int countWhiteSpace = 0;

                Console.Clear();
                Console.Write("Введите название книги: ");
                title = Console.ReadLine();

                for (int i = 0; i < title.Length; i++)
                {
                    if (title[i] == simbolWhiteSpace)
                    {
                        countWhiteSpace++;
                    }
                }

                if (countWhiteSpace != title.Length)
                {
                    isTitleEnteredCorrectly = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Название не должно содержать одни лишь пробелы," +
                        " а так же быть пустым!");
                    Console.ReadKey();
                }
            }

            return title;
        }

        private string CreateAuthor()
        {
            string author = null;
            bool isAuthorEnteredCorrectly = false;

            while (isAuthorEnteredCorrectly == false)
            {
                char simbolWhiteSpace = ' ';
                int countWhiteSpace = 0;

                Console.Clear();
                Console.WriteLine("Введите автора книги или оставьте поле пустым: ");
                author = Console.ReadLine();

                if (author.Length > 0)
                {
                    for (int i = 0; i < author.Length; i++)
                    {
                        if (author[i] == simbolWhiteSpace)
                        {
                            countWhiteSpace++;
                        }
                    }

                    if (countWhiteSpace != author.Length)
                    {
                        isAuthorEnteredCorrectly = true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Строка не должна содержать одни лишь пробелы!");
                        Console.ReadKey();
                    }
                }
                else
                {
                    author = "неизвестно";
                    isAuthorEnteredCorrectly = true;
                }
            }

            return author;
        }

        private int CreateYearPublication()
        {
            int yearPublication = 0;
            bool isYearEnteredCorrectly = false;

            while (isYearEnteredCorrectly == false)
            {
                bool isNumber;
                string userInput;
                int inputPositionX;

                Console.Clear();
                Console.Write("Введите год публикации книги или оставьте поле пустым: ");
                inputPositionX = Console.CursorLeft;
                Console.WriteLine("\n\nЕсли хотите указать год до нашей эры, " +
                    "то перед числом поставьте знак '-'");
                Console.SetCursorPosition(inputPositionX, 0);
                userInput = Console.ReadLine();
                isNumber = int.TryParse(userInput, out yearPublication);

                if (isNumber == true)
                {
                    if (yearPublication != 0)
                    {
                        isYearEnteredCorrectly = true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Такого года не существует!");
                        Console.ReadKey();
                    }
                }
                else if (userInput.Length == 0)
                {
                    isYearEnteredCorrectly = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Некорректный запрос!");
                    Console.ReadKey();
                }
            }

            return yearPublication;
        }

        private Book SearchBook()
        {
            List<Book> books = SearchByTitle();
            Book book = null;

            if (books.Count > 0)
            {
                book = SelectBook(books);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Такой книги нет в библиотеке!");
                Console.ReadKey();
            }

            return book;
        }

        private Book SelectBook(List<Book> books)
        {
            Book selectedBook = null;

            if (books.Count > 1)
            {
                bool isNumber;
                string userInput;
                int inputPositionX;
                int bookNumber = 0;

                Console.Clear();
                Console.Write("Укажите номер книги, которая вам необходима: ");
                inputPositionX = Console.CursorLeft;
                Console.WriteLine("\n");

                for (int i = 0; i < books.Count; i++)
                {
                    int sequenceNumber = i + 1;

                    Console.Write($"{sequenceNumber}. ");
                    books[i].Show();
                    Console.WriteLine();
                }

                Console.SetCursorPosition(inputPositionX, 0);
                userInput = Console.ReadLine();
                isNumber = int.TryParse(userInput, out bookNumber);

                if (isNumber == true && bookNumber > 0 && bookNumber <= books.Count)
                {
                    selectedBook = books[bookNumber - 1];
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Некорректный запрос!");
                    Console.ReadKey();
                }
            }
            else if (books.Count == 1)
            {
                selectedBook = books[0];
            }

            return selectedBook;
        }

        private List<Book> SearchBooksByParameter()
        {
            const string ParameterTitle = "1";
            const string ParameterAuthor = "2";
            const string ParameterYearPublication = "3";

            List<Book> books = new List<Book>();
            int inputPositionX;

            Console.Clear();
            Console.Write("По какому параметру подобрать книги: ");
            inputPositionX = Console.CursorLeft;
            Console.WriteLine("\n\nПараметры:\n" +
                $"\n{ParameterTitle}. По названию" +
                $"\n{ParameterAuthor}. По автору" +
                $"\n{ParameterYearPublication}. По году публикации");
            Console.SetCursorPosition(inputPositionX, 0);

            switch (Console.ReadLine())
            {
                case ParameterTitle:
                    books = SearchByTitle();
                    break;

                case ParameterAuthor:
                    books = SearchByAuthor();
                    break;

                case ParameterYearPublication:
                    books = SearchByYearPublication();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Некорректный запрос!");
                    Console.ReadKey();
                    break;
            }

            return books;
        }

        private List<Book> SearchByTitle()
        {
            List<Book> books = new List<Book>();
            string userInput;

            Console.Clear();
            Console.Write("Укажите название книги:");
            userInput = Console.ReadLine();

            if (userInput.Length > 0)
            {
                userInput = userInput.ToUpper();

                foreach (Book book in _books)
                {
                    string title = book.Title.ToUpper();

                    if (title.Contains(userInput))
                    {
                        books.Add(book);
                    }
                }
            }

            return books;
        }

        private List<Book> SearchByAuthor()
        {
            List<Book> books = new List<Book>();
            string userInput;

            Console.Clear();
            Console.Write("Укажите искомого автора:");
            userInput = Console.ReadLine();

            if (userInput.Length > 0)
            {
                userInput = userInput.ToUpper();

                foreach (Book book in _books)
                {
                    string author = book.Author.ToUpper();

                    if (author == userInput)
                    {
                        books.Add(book);
                    }
                }
            }

            return books;
        }

        private List<Book> SearchByYearPublication()
        {
            List<Book> books = new List<Book>();
            string userInput;
            int userNumber = 0;
            bool isNumber;
            int inputPositionX;

            Console.Clear();
            Console.Write("Укажите год публикации: ");
            inputPositionX = Console.CursorLeft;
            Console.WriteLine("\n");
            Console.WriteLine("Если хотите указать год до нашей эры, " +
                "то перед числом поставьте знак '-'");
            Console.SetCursorPosition(inputPositionX, 0);
            userInput = Console.ReadLine();
            isNumber = int.TryParse(userInput, out userNumber);

            if (isNumber == true)
            {
                foreach (Book book in _books)
                {
                    if (book.YearPublication == userNumber)
                    {
                        books.Add(book);
                    }
                }
            }

            return books;
        }
    }

    class Book
    {
        public Book(string title, string author, int yearPublication)
        {
            Title = title;
            Author = author;
            YearPublication = yearPublication;
        }

        public string Title { get; private set; }
        public string Author { get; private set; }
        public int YearPublication { get; private set; }

        public void Show()
        {
            Console.Write($"{Title}|{Author}|");

            if (YearPublication > 0)
            {
                Console.Write($"{YearPublication} г.");
            }
            else if (YearPublication < 0)
            {
                Console.Write($"{Math.Abs(YearPublication)} г. до н.э.");
            }
            else
            {
                Console.Write($"неизвестно");
            }
        }
    }
}
