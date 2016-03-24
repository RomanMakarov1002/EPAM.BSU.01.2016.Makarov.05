using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BooksCollection
{
    public class Repository : IRepository<Book> 
    {
        private string FilePath { get; }
        public Repository(string filePath)
        {
            FilePath = filePath;
        }

        public IList<Book> GetCollection()
        {
            FileMode mode = !File.Exists(FilePath) ? FileMode.Create : FileMode.Open;
            List<Book> books = new List<Book>();
            using (BinaryReader binaryReader = new BinaryReader(new FileStream(FilePath, mode)))
            {
                while (binaryReader.PeekChar() > -1)
                {
                    string name = binaryReader.ReadString();
                    int pages = binaryReader.ReadInt32();
                    string author = binaryReader.ReadString();
                    string publishment = binaryReader.ReadString();
                    string genre = binaryReader.ReadString();
                    int year = binaryReader.ReadInt32();
                    books.Add(new Book(name,pages,author,publishment,genre,year));
                }
            }
            return books;
        }

        public void AddItem(Book book)
        {
                AddItems(new List<Book> {book}, FileMode.Append);
        }

        public void  AddItems(List<Book> booksCollection , FileMode mode)
        {
            if (booksCollection == null)
                throw new ArgumentNullException();
            using (BinaryWriter binaryWriter = new BinaryWriter(new FileStream(FilePath, mode, FileAccess.Write)))
            {
                foreach (var book in booksCollection)
                {
                    binaryWriter.Write(book.BookName);
                    binaryWriter.Write(book.NumberOfPages);
                    binaryWriter.Write(book.Authors);
                    binaryWriter.Write(book.Publishment);
                    binaryWriter.Write(book.Genre);
                    binaryWriter.Write(book.Year);
                }
            }
        }

        public bool RemoveItem(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();
            bool find = false;
            List<Book> books = GetCollection().ToList();
            find = books.Remove(book);
            AddItems(books , FileMode.Create);
            return find;
        }
    }
}
