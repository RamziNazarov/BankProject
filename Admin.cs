using System;
using System.Data.SqlClient;
using System.Data;

namespace ProjectAlif
{
    class Admin
    {
        int log {get;set;}
        string pas{get;set;}
        SqlConnection connection = new SqlConnection(Constr.connectionString);
        public int AddAdmin()
        {
            System.Console.Write("Введите логин: ");
            this.log = int.Parse(Console.ReadLine());
            System.Console.Write("Введите пароль: ");
            this.pas = Console.ReadLine();
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            string com = $"select * from Admin";
            bool res = false;
            using(SqlCommand command = new SqlCommand(com,connection))
            {   
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        if(this.log == int.Parse(reader.GetValue(0).ToString()))
                        {
                            res = true;
                            break;
                        }
                    }
                }
            }
            if(!(res))
            {
                string comm = $"insert into Admin([AdminLogin],[AdminPassword]) values ({this.log},'{this.pas}')";
                SqlCommand command = new SqlCommand(comm,connection);
                return command.ExecuteNonQuery();
            }
            return 0;
        }
        public bool FindAdmin()
        {
            System.Console.Write("Введите логин: ");
            this.log = int.Parse(Console.ReadLine());
            System.Console.Write("Введите пароль: ");
            this.pas = Console.ReadLine();
            string comm = $"select * from Admin";
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand command = new SqlCommand(comm,connection);
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    if(this.log == int.Parse(reader.GetValue(0).ToString()) && this.pas == reader.GetValue(1).ToString())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}