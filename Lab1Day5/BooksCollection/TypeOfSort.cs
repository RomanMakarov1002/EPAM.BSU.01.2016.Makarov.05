using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksCollection
{
    public class SortByName :IComparer<Book>
    {
        public int Compare(Book lhs, Book rhs)
        {            
            return String.Compare(lhs.BookName, rhs.BookName, StringComparison.Ordinal);
        }
    }

    public class SortByYear : IComparer<Book>
    {
        public int Compare(Book lhs, Book rhs)
        {
            return lhs.Year > rhs.Year ? 1 : -1;
        }
    }

    public class SortByAuthor : IComparer<Book>
    {
        public int Compare(Book lhs, Book rhs)
        {
            return String.Compare(lhs.Authors, rhs.Authors, StringComparison.Ordinal);
        }
    }

    public class SortByGenre : IComparer<Book>
    {
        public int Compare(Book lhs, Book rhs)
        {
            return String.Compare(lhs.Genre, rhs.Genre, StringComparison.Ordinal);
        }
    }

    public class SortByPages : IComparer<Book>
    {
        public int Compare(Book lhs, Book rhs)
        {
            return lhs.NumberOfPages > rhs.NumberOfPages ? 1 : -1;
        }
    }

    public class SOrtByPublishment : IComparer<Book>
    {
        public int Compare(Book lhs, Book rhs)
        {
            return String.Compare(lhs.Publishment, rhs.Publishment, StringComparison.Ordinal);
        }
    }
}
