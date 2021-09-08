using System.Data;

namespace SchoolApi
{
    public interface ISqlServerDataAccess
    {
        string Connectionstring { get; }
        DataTable ExecuteSPParam(string query, int paramId);
    }
}