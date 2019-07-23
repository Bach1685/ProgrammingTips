using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yield
{
    class Program
    {
        static void Main(string[] args)
        {
            Book book = new Book("Вызов экуменизма");

            Console.ReadKey();
        }
    }

    class Book
    {
        public Book(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }
    }

    class Library
    {
        private Book[] books;

        public Library()
        {
            books = new Book[] { new Book("Отцы и дети"), new Book("Война и мир"),
                new Book("Евгений Онегин") };
        }

        public int Length
        {
            get { return books.Length; }
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < books.Length; i++)
            {
                yield return books[i];
            }
        }
    }
}
