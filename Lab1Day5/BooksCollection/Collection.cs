using System;
using System.CodeDom;
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
    public class Service
    {
        //public IRepository<Book> Repository { get; }
        public List<Book> _books = new List<Book>(); 
        /*
        public Service(string filePath)
        {
            Repository = new Repository(filePath);
        }
        */
        public Service() { }

        public Service(List<Book> books)
        {
            _books = books;
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


        public void LoadCollectionFromRemoteFile(IRepository<Book> repository )
        {
            _books=repository.Load().ToList();
        }

        public void SaveCollectionToRemoteFile(IRepository<Book> repository )
        {
            repository.Save(_books);
        }

        
        public List<Book> FindByTag(string name )
        {
            return _books.FindAll(books=>books.BookName==name);
        }

        public List<Book> FindByTag(string authorName, string authorSurname)
        {
            return _books.FindAll(books => books.Authors == authorName + authorSurname);
        }

        public List<Book> FindByTag(int year)
        {
            return _books.FindAll(books => books.Year == year);
        }


        public void SortBooksByTag(IComparer<Book>  key)
        {            
            _books.ToList().Sort(key);             
        }

        

        public Book this[int index]
        {
            get { return _books[index]; }
        }

       
        
    }
}
