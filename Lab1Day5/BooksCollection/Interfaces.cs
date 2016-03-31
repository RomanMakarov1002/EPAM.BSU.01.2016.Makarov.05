using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BooksCollection
{
    public interface IRepository<T>
    {
        IEnumerable<T> Load();
        void Save(IEnumerable<T> items);
    }

    
    class Interfaces
    {
    }
}
