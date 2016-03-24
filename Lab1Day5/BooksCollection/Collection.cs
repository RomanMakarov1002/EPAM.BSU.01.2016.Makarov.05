using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksCollection
{
    public sealed class AddBookException : Exception
    {
        public AddBookException(string message) : base(message)
        {
            
        }
    }

    public sealed class RemoveBookException : Exception
    {
        public RemoveBookException(string message) : base(message)
        {
            
        }
    }
    public class Collection :IEnumerable<Book>
    {
        private FileStream fs;

        public Repository Repository { get; }
        public IList<Book> books => Repository.GetCollection(); 

        public Collection(string filePath)
        {
            Repository = new Repository(filePath);
        }
        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();
            if (books.Contains(book))
                throw new AddBookException("This book already exists");
            Repository.AddItem(book);
        }

        public void RemoveBook(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException();
            bool find = false;
            foreach (var item in books)
            {
                if (item.BookName == name)
                {
                    Repository.RemoveItem(item);
                    find = true;
                }                
            }
            if (!find)
                throw new RemoveBookException("there is no this book in collection");
                 
        }

        public void RemoveBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();
            bool find = Repository.RemoveItem(book);
            if (!find)
                throw new RemoveBookException("there is no this book in collection");
        }

        public List<Book> FindByTag(string name)
        {
            List<Book> booksFound = new List<Book>();
            bool found = false;
            foreach (var item in books)
            {
                if (item.BookName == name)
                {
                    booksFound.Add(item);
                    found = true;
                }
            }
            if (!found)
                throw new KeyNotFoundException("there is no this book in collection");
            return booksFound;
        }

        public List<Book> FindByTag(string authorName, string authorSurname)
        {
            List<Book> booksFound = new List<Book>();
            bool found = false;
            foreach (var item in books)
            {
                if (item.Authors.Contains(authorName) && (item.Authors.Contains(authorSurname)))
                {
                    booksFound.Add(item);
                    found = true;
                }
            }
            if (!found)
                throw new KeyNotFoundException("there is no this book in collection");
            return booksFound;
        }

        public List<Book> FindByTag(int year)
        {
            List<Book> booksFound = new List<Book>();
            bool found = false;
            foreach (var item in books)
            {
                if (item.Year == year)
                {
                    booksFound.Add(item);
                    found = true;
                }
            }
            if (!found)
                throw new KeyNotFoundException("there is no this book in collection");
            return booksFound;
        }

        public void SortBooksByTag(IComparer<Book>  key)
        {
            
            List<Book> srt = books.ToList();
            srt.Sort(key);
            Repository.AddItems(srt,FileMode.Create);
        
        
        }

        public IEnumerator<Book> GetEnumerator()
        {
            return books.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return books.GetEnumerator();
        }

        public Book this[int index]
        {
            get { return books[index]; }
        }

        
    }
}
