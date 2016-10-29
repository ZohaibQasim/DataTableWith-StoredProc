using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MvcApplication2.Models
{
    public class DBClass
    {
        public List<Employee> GetEmployee(string search,int sortCol, string sortDir,int DisplayStart, int DisplayLength)
        {
            //SqlConnection conn = new SqlConnection();
            //conn.ConnectionString =
            //"Data Source=ServerName;" +
            //"Initial Catalog=DataBaseName;" +
            //"User id=UserName;" +
            //"Password=Secret;";
            //conn.Open();
            try {

                string conn = System.Configuration.ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(conn))
            {
                // Create the command and set its properties.
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "GetEmployeeData";
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@DisplayLength";
                parameter1.SqlDbType = SqlDbType.Int;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = DisplayLength;

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = "@DisplayStart";
                parameter2.SqlDbType = SqlDbType.Int;
                parameter2.Direction = ParameterDirection.Input;
                parameter2.Value = DisplayStart;

                SqlParameter parameter3 = new SqlParameter();
                parameter3.ParameterName = "@SortCol";
                parameter3.SqlDbType = SqlDbType.Int;
                parameter3.Direction = ParameterDirection.Input;
                parameter3.Value = sortCol;

                SqlParameter parameter4 = new SqlParameter();
                parameter4.ParameterName = "@SortDir";
                parameter4.SqlDbType = SqlDbType.NVarChar;
                parameter4.Direction = ParameterDirection.Input;
                parameter4.Value = sortDir;

                // Add the input parameter and set its properties.
                SqlParameter parameter5 = new SqlParameter();
                parameter5.ParameterName = "@Search";
                parameter5.SqlDbType = SqlDbType.NVarChar;
                parameter5.Direction = ParameterDirection.Input;
                parameter5.Value = search;

                // Add the parameter to the Parameters collection. 
                command.Parameters.Add(parameter1);
                command.Parameters.Add(parameter2);
                command.Parameters.Add(parameter3);
                command.Parameters.Add(parameter4);
                command.Parameters.Add(parameter5);

                List<Employee> empl = new List<Employee>();

                // Open the connection and execute the reader.
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Employee emp = new Employee();
                        emp.RowNum = int.Parse(reader[0].ToString());
                        emp.TotalCount = int.Parse(reader[1].ToString());
                        emp.ID = int.Parse(reader[2].ToString());
                        emp.Name = reader[3].ToString();
                        emp.Age = int.Parse(reader[4].ToString());
                        empl.Add(emp);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();

                return empl;
            }
            }
          catch(Exception e)
            {
                return null;
                }
            
        }
    }
}