using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.Interfaces
{
    interface IFetchData<T>
    {
        List<T> GetData(string roomNumber);
    }
}
