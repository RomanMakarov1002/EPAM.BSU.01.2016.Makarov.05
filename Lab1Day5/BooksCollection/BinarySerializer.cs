using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BooksCollection
{
    public class BinarySerializer : IRepository<Book>
    {
        private string FilePath { get; }
        private readonly IFormatter _formatter;

        public BinarySerializer(string filepath)
        {
            if (String.IsNullOrWhiteSpace(filepath))
                throw new ArgumentException();
            FilePath = filepath;
            _formatter = new BinaryFormatter();
        }


        public IEnumerable<Book> Load()
        {
            FileMode mode = !File.Exists(FilePath) ? FileMode.Create : FileMode.Open;
            IEnumerable<Book> books;
            using (FileStream fs = File.Open(FilePath, mode, FileAccess.Read))
            {
                books = (IEnumerable<Book>) _formatter.Deserialize(fs);
            }
            return books;
        }


        public void Save(IEnumerable<Book> booksCollection)
        {
            if (booksCollection == null)
                throw new ArgumentNullException();
            using (FileStream fs = File.Open(FilePath, FileMode.Append, FileAccess.Write))
            {
                _formatter.Serialize(fs,booksCollection);
            }
        }
    }
}
