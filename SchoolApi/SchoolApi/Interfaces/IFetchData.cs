using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.Interfaces
{
    interface IFetchData<T>
    {
        ICollection<T> GetData(string roomNumber);
        //ICollection<> FetchAllData();
        //ICollection<T> FetchNewElements();
    }
}
