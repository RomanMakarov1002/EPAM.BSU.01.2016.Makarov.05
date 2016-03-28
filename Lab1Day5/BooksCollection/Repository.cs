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

        public IList<Book> Load()
        {
            FileMode mode = !File.Exists(FilePath) ? FileMode.Create : FileMode.Open;
            IList<Book> books = new List<Book>();
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

        public void  Save(IList<Book> booksCollection)
        {
            if (booksCollection == null)
                throw new ArgumentNullException();
            using (BinaryWriter binaryWriter = new BinaryWriter(new FileStream(FilePath, FileMode.Append, FileAccess.Write)))
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
    }
}
