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
        public void SelectAllApplications()
        {
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            string comstr = $"select LastName,FirstName,Aim,Salary,CreditSumm,Term,[Status] from Applications join Customer on Customer.SerP = Applications.SerP";
            SqlCommand command = new SqlCommand(comstr,connection);
            System.Console.WriteLine($"Фамилия | Имя | Цель | Общий доход | Сумма кредита | Срок | Статус");
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    System.Console.WriteLine($"{reader.GetValue(0).ToString()} | {reader.GetValue(1).ToString()} | {reader.GetValue(2).ToString()} | {reader.GetValue(3).ToString()} | {reader.GetValue(4).ToString()} | {reader.GetValue(5).ToString()} | {reader.GetValue(6).ToString()}");
                }
            }
        }
        public void SelectAllApplicationsFromSerP(string SerP)
        {
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            string comstr = $"select LastName,FirstName,Aim,Salary,CreditSumm,Term,[Status] from Applications join Customer on Customer.SerP = Applications.SerP where Applications.Serp = '{SerP}'";
            SqlCommand command = new SqlCommand(comstr,connection);
            System.Console.WriteLine($"Фамилия | Имя | Цель | Общий доход | Сумма кредита | Срок | Статус");
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    System.Console.WriteLine($"{reader.GetValue(0).ToString()} | {reader.GetValue(1).ToString()} | {reader.GetValue(2).ToString()} | {reader.GetValue(3).ToString()} | {reader.GetValue(4).ToString()} | {reader.GetValue(5).ToString()} | {reader.GetValue(6).ToString()}");
                }
            }
        }
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