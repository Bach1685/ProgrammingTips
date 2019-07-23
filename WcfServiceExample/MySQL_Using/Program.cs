using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MySQL_Using
{
    class Program
    {
        static void Main(string[] args)
        {
            MySqlDAO mySqlDAO = new MySqlDAO();

           // Console.WriteLine(mySqlDAO.GetOneValue("users", "age", "22"));
            mySqlDAO.GetTable("users", "name", "22");


            Console.WriteLine("Нажмите любую кнопку, чтобы завершить");
            Console.ReadKey();
        }
    }

    class MySqlDAO
    {
        string ConnectionString { get; set; }
        public MySqlConnection Connection { get; set; }
        MySqlDataReader Reader { get; set; }

        public MySqlDAO()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["MySql"].ConnectionString; // для ConfigurationManager вручную нужно добавить ссылку System.Configuration
            Connection = new MySqlConnection(ConnectionString);
        }

        public object GetOneValue(string table, string column, string id)
        {
            string sqlRequest = $"SELECT {column} FROM {table} WHERE id = {id}";

            Connection.Open();

            MySqlCommand command = new MySqlCommand(sqlRequest, Connection);

            object name = command.ExecuteScalar();

            Connection.Close();

            return name;
        }

        public void GetTable(string table, string column, string age)
        {
            string sqlRequest = $"SELECT {column} FROM {table} WHERE age = {age}";

            Connection.Open();

            MySqlCommand command = new MySqlCommand(sqlRequest, Connection);

            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                Console.WriteLine(Reader[0]);
            }

            var variable = Reader[0];
            Connection.Close();
        }
    }
}
