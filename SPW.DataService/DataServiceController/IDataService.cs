using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.DataService
{
    public interface IDataService<T>
    {
        void Add(T obj);
        void AddList(List<T> obj);
        void Edit(T obj);
        void EditList(List<T> obj);
        T Select();
        List<T> GetAll();
    }
}
