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
            int a = 0;
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            string comstr = $"select LastName,FirstName,Aim,Salary,CreditSumm,Term,[Status] from Applications join Customer on Customer.SerP = Applications.SerP";
            SqlCommand command = new SqlCommand(comstr,connection);
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    a++;
                    System.Console.WriteLine($"Фамилия: {reader.GetValue(0).ToString()} \nИмя: {reader.GetValue(1).ToString()} \nЦель: {reader.GetValue(2).ToString()} \nОбщий доход: {reader.GetValue(3).ToString()} \nСумма: {reader.GetValue(4).ToString()} \nСрок: {reader.GetValue(5).ToString()} \nСтатус: {reader.GetValue(6).ToString()}");
                    System.Console.WriteLine("------------------------");
//System.Console.WriteLine($"{reader.GetValue(0).ToString()} | {reader.GetValue(1).ToString()} | {reader.GetValue(2).ToString()} | {reader.GetValue(3).ToString()} | {reader.GetValue(4).ToString()} | {reader.GetValue(5).ToString()} | {reader.GetValue(6).ToString()}");
                }
            }
            if(a ==0)
            {
                System.Console.WriteLine("Заявок нет!");
            }
        }
        public void SelectAllApplications(string SerP)
        {
            int a =0;
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            string comstr = $"select LastName,FirstName,Aim,Salary,CreditSumm,Term,[Status] from Applications join Customer on Customer.SerP = Applications.SerP where Applications.Serp = '{SerP}' or Customer.UserLogin = '{SerP}'";
            SqlCommand command = new SqlCommand(comstr,connection);
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    a++;
                    System.Console.WriteLine($"Фамилия: {reader.GetValue(0).ToString()} \nИмя: {reader.GetValue(1).ToString()} \nЦель: {reader.GetValue(2).ToString()} \nОбщий доход: {reader.GetValue(3).ToString()} \nСумма: {reader.GetValue(4).ToString()} \nСрок: {reader.GetValue(5).ToString()} \nСтатус: {reader.GetValue(6).ToString()}");
                    System.Console.WriteLine("-------------------------------");
                }
            }
            if(a == 0)
            System.Console.WriteLine("У этого клиента нет заявок!");
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
        public void SelectAllGraphic()
        {
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand command = new SqlCommand($"select LastName,FirstName,SummForPay,DateForPay,PaySumm,PayDate,Pros from Graphic join Customer on Customer.SerP = Graphic.SerP",connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    if(reader.GetValue(5).ToString() != "")
                    {
                        System.Console.WriteLine($"Фамилия: {reader.GetValue(0).ToString()}\nИмя: {reader.GetValue(1).ToString()}\nСумма для оплаты: {reader.GetValue(2).ToString()}\nДо даты: {reader.GetValue(3).ToString().Substring(0,10)}\nОплаченная сумма: {reader.GetValue(4).ToString()}\nДата оплаты: {reader.GetValue(5).ToString().Substring(0,10)}\nПросрочка: {reader.GetValue(6).ToString()}");
                        System.Console.WriteLine("----------------------------");
                    }
                    else
                    {
                        System.Console.WriteLine($"Фамилия: {reader.GetValue(0).ToString()}\nИмя: {reader.GetValue(1).ToString()}\nСумма для оплаты: {reader.GetValue(2).ToString()}\nДо даты: {reader.GetValue(3).ToString().Substring(0,10)}\nОплаченная сумма: {reader.GetValue(4).ToString()}\nДата оплаты: Нет\nПросрочка: {reader.GetValue(6).ToString()}");
                        System.Console.WriteLine("----------------------------");
                    }
                }
            }
        }
        public void SelectAllGraphic(string SerP)
        {
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand command = new SqlCommand($"select LastName,FirstName,SummForPay,DateForPay,PaySumm,PayDate,Pros from Graphic join Customer on Customer.SerP = Graphic.SerP where Graphic.SerP = '{SerP}' or  Customer.UserLogin = {SerP}",connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    if(reader.GetValue(5).ToString() != "")
                    {
                        System.Console.WriteLine($"Фамилия: {reader.GetValue(0).ToString()}\nИмя: {reader.GetValue(1).ToString()}\nСумма для оплаты: {reader.GetValue(2).ToString()}\nДо даты: {reader.GetValue(3).ToString().Substring(0,10)}\nОплаченная сумма: {reader.GetValue(4).ToString()}\nДата оплаты: {reader.GetValue(5).ToString().Substring(0,10)}\nПросрочка: {reader.GetValue(6).ToString()}");
                        System.Console.WriteLine("----------------------------");
                    }
                    else
                    {
                        System.Console.WriteLine($"Фамилия: {reader.GetValue(0).ToString()}\nИмя: {reader.GetValue(1).ToString()}\nСумма для оплаты: {reader.GetValue(2).ToString()}\nДо даты: {reader.GetValue(3).ToString().Substring(0,10)}\nОплаченная сумма: {reader.GetValue(4).ToString()}\nДата оплаты: Нет\nПросрочка: {reader.GetValue(6).ToString()}");
                        System.Console.WriteLine("----------------------------");
                    }
                }
            }
        }
    }
}