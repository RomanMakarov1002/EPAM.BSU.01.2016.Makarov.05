using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BooksCollection
{
    public class XMLRepository : IRepository<Book>
    {
        private string FilePath { get; }

        public XMLRepository(string filePath)
        {
            if (String.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException();
            FilePath = filePath;
        }

        public IEnumerable<Book> Load()
        {
            XDocument xDoc = XDocument.Load(FilePath);
            List<Book> library = new List<Book>();
            string name = null;
            int pages = 0;
            string authors = null;
            string publshment = null;
            int year = 0;
            string genre = null;
            if (xDoc.Root == null)
                return library;
            foreach (var item in xDoc.Root.Elements())
            {
                genre = item.Attribute("Genre").Value;
                foreach (var attribute in item.Elements())
                {
                    switch (attribute.Name.ToString())
                    {                       
                        case "Name":
                            name = attribute.Value;
                            break;
                        case "Authors":
                            authors = attribute.Value;
                            break;
                        case "NumberOfPages":
                            pages = Convert.ToInt32(attribute.Value);
                            break;
                        case "Publishment":
                            publshment = attribute.Value;
                            break;
                        case "Year":
                            year = Convert.ToInt32(attribute.Value);
                            break;
                    }                    
                }
                library.Add(new Book(name, pages, authors, publshment, genre, year));
            }
            return  library;
        }

        public void Save(IEnumerable<Book> items)
        {
            if (items == null)
                throw new ArgumentNullException();
            XDocument xDoc = new XDocument(new XComment("Created: " + DateTime.Now),new XElement("Library"));
            foreach (var book in items)
            {
                XElement booXElement = new XElement(
                    new XElement("Book",
                        new XAttribute("Genre", book.Genre),
                        new XElement("Name", book.BookName),
                        new XElement("NumberOfPages", book.NumberOfPages),
                        new XElement("Authors", book.Authors),
                        new XElement("Publishment", book.Publishment),
                        new XElement("Year", book.Year)));
                if (xDoc.Root != null) xDoc.Root.Add(booXElement);
            }
            xDoc.Save(FilePath);
        }
    }
}
