using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.Interfaces
{
    //Used for when a controller fetches a certain object, depending on a roomNumber
    interface IFetchDataByRoomNumber<T>
    {
        List<T> GetData(string roomNumber);
    }
}
