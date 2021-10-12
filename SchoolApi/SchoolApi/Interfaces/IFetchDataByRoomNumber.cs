using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.Interfaces
{
    //Used for when a controller fetches a certain object, depending on a roomNumber
    interface IFetchDataByRoomNumber<T>
    {
        /// <summary>
        /// Get a <see cref="List{T}"/> with a search criteria of <seealso cref="string"/> <paramref name=" roomNumber"/>
        /// </summary>
        /// <param name="roomNumber">The room's number which you want data of type <typeparamref name="T"/> from</param>
        /// <returns>Returns a list of <typeparamref name="T"/></returns>
        List<T> GetData(string roomNumber);
    }
}
