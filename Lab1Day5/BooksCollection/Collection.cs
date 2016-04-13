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
        public List<Book> Books = new List<Book>(); 
        public Service() { }

        public Service(List<Book> books)
        {
            Books = books;
        }

        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();
            if (Books.Contains(book))
                throw new AddBookException("This book already exists");
            Books.Add(book);
        }

        public void RemoveBook(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException();
            bool find = false;
            foreach (var item in Books)
            {
                if (item.BookName == name)
                {
                    Books.Remove(item);
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
            bool find = Books.Remove(book);
            if (!find)
                throw new RemoveBookException("there is no this book in collection");
        }


        public void LoadCollectionFromRemoteFile(IRepository<Book> repository )
        {
            Books=repository.Load().ToList();
        }

        public void SaveCollectionToRemoteFile(IRepository<Book> repository )
        {
            repository.Save(Books);
        }

        
        public List<Book> FindByTag(string name )
        {
            return Books.FindAll(books => books.BookName == name);
        }

        public List<Book> FindByTag(string authorName, string authorSurname)
        {
            return Books.FindAll(books => books.Authors == authorName + authorSurname);
        }

        public List<Book> FindByTag(int year)
        {
            return Books.FindAll(books => books.Year == year);
        }


        public void SortBooksByTag(IComparer<Book>  key)
        {            
            Books.ToList().Sort(key);             
        }

        

        public Book this[int index]
        {
            get { return Books[index]; }
        }

       
        
    }
}
