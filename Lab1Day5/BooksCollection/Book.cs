using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksCollection
{
    public class Book: IEquatable<Book>
    {
        public string BookName { get; }
        public int NumberOfPages { get; }
        public string Authors { get; }
        public string Publishment { get; }   //
        public string Genre { get; }
        public int Year { get; }
        

        public Book(string name, int pages, string authors, string publishment, string genre, int year)
        {
            BookName = name;
            NumberOfPages = pages;
            Authors = authors;
            Publishment = publishment;
            Genre = genre;
            Year = year;
        }

        public Book(string name, string authors) : this(name, 0, authors, null, null, 0)
        {
            
        }

        public Book(string name) : this(name, 0, null, null, null, 0)
        {
            
        }

        public override string ToString()
        {
            return
                $"BookName: {BookName},  PagesCount: {NumberOfPages},  Authors:  {Authors},  Publishment: {Publishment}, Genre:  {Genre},  Year:  {Year}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && this.Equals((Book)obj);
        }

        public bool Equals(Book other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(BookName, other.BookName) && NumberOfPages == other.NumberOfPages &&
                   string.Equals(Authors, other.Authors) && string.Equals(Publishment, other.Publishment) &&
                   string.Equals(Genre, other.Genre) && Year == other.Year;
        }
    }
    
    
}
