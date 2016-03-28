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
        public IRepository<Book> Repository { get; }
        private IList<Book> _books = new List<Book>(); 

        public Collection(string filePath)
        {
            Repository = new Repository(filePath);
        }
        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();
            if (_books.Contains(book))
                throw new AddBookException("This book already exists");
            _books.Add(book);
        }

        public void RemoveBook(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException();
            bool find = false;
            foreach (var item in _books)
            {
                if (item.BookName == name)
                {
                    _books.Remove(item);
                    find = true;
                    break;
                }                
            }            
            if (!find)
                throw new RemoveBookException("there is no this book in collection");  
        }

        public void RemoveBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();
            bool find = _books.Remove(book);
            if (!find)
                throw new RemoveBookException("there is no this book in collection");
        }


        public void LoadCollectionFromRemoteFile()
        {
            _books=Repository.Load();
        }

        public void SaveCollectionToRemoteFile()
        {
            Repository.Save(_books);
        }


        public List<Book> FindByTag(string name)
        {
            List<Book> booksFound = new List<Book>();
            bool found = false;
            foreach (var item in _books)
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
            foreach (var item in _books)
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
            foreach (var item in _books)
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
            _books.ToList().Sort(key);             
        }

        public IEnumerator<Book> GetEnumerator()
        {
            return _books.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _books.GetEnumerator();
        }

        public Book this[int index]
        {
            get { return _books[index]; }
        }

        
    }
}
