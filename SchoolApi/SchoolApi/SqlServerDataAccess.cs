using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi
{
    public class SqlServerDataAccess : ISqlServerDataAccess
    {
        private string connectionString;
        public string Connectionstring
        {
            get
            {
                return connectionString;
            }
            private set
            {
                connectionString = value;
            }
        }

        public SqlServerDataAccess(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultString");
        }

        internal bool SaveData(string temperature, string humidity, string light, string roomNumber)
        {
            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                SqlCommand command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SaveData",
                    Connection = conn
                };

                command.Parameters.Add(new SqlParameter("@Temperature", temperature));
                command.Parameters.Add(new SqlParameter("@Humidity", humidity));
                command.Parameters.Add(new SqlParameter("@Light", light));
                command.Parameters.Add(new SqlParameter("@RoomNumber", roomNumber));

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Executes a specified stores procedure
        /// </summary>
        /// <param name="query">What query to run</param>
        /// <returns>Returns a datatable of the query result</returns>
        public DataTable ExecuteSP(string query)
        {
            SqlConnection conn;
            using (conn = GetSqlConnection())
            {
                SqlCommand command = GetSqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = query;
                command.Connection = conn;
                DataTable dt = GetDataTable();
                SqlDataAdapter adapter = GetAdapter();
                SqlDataReader dataReader;

                conn.ConnectionString = Connectionstring;
                try
                {
                    conn.Open();
                    adapter.SelectCommand = command;
                    dataReader = command.ExecuteReader();
                    dataReader.Close();
                    adapter.Fill(dt);

                }
                catch (SqlException)
                {
                    return null;
                }
                finally
                {
                    adapter.Dispose();
                    conn.Close();
                }
                return dt;
            }
        }
        /// <summary>
        /// Method for running a stored procedure with referencing @Id as parameter
        /// </summary>
        /// <param name="query">The SP you wish to run</param>
        /// <param name="paramId">value of @Id in the where condition</param>
        /// <returns></returns>
        public DataTable ExecuteSPParam(string query, int paramId)
        {
            SqlConnection conn;
            using (conn = GetSqlConnection())
            {
                SqlCommand command = GetSqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = query;
                command.Connection = conn;
                command.Parameters.AddWithValue("@RoomNumber", paramId);
                DataTable dt = GetDataTable();
                SqlDataAdapter dataAdapter = GetAdapter();
                SqlDataReader dataReader;

                conn.ConnectionString = Connectionstring;
                try
                {
                    conn.Open();
                    dataAdapter.SelectCommand = command;
                    dataReader = command.ExecuteReader();
                    dataReader.Close();
                    dataAdapter.Fill(dt);

                }
                catch (SqlException)
                {
                    return null;
                }
                finally
                {
                    dataAdapter.Dispose();
                    conn.Close();
                }
                return dt;
            }

        }
        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection();
        }
        public SqlCommand GetSqlCommand()
        {
            return new SqlCommand();
        }
        public DataTable GetDataTable()
        {
            return new DataTable();
        }
        public SqlDataAdapter GetAdapter()
        {
            return new SqlDataAdapter();
        }
    }
}
