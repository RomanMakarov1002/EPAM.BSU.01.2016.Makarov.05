using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BooksCollection
{
    interface IRepository<T>
    {
        IList<T> GetCollection();
        void AddItem(T item);
        void AddItems(List<T> items, FileMode mode);
        bool RemoveItem(T item);
    }

    
    class Interfaces
    {
    }
}
