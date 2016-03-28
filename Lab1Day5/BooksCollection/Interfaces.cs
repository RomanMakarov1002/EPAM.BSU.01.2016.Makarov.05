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
        IList<T> Load();
        void Save(IList<T> items);
    }

    
    class Interfaces
    {
    }
}
