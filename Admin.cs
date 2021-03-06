using System;
using System.Data.SqlClient;
using System.Data;

namespace ProjectAlif
{
    class Admin
    {
        int log { get; set; }
        string pas { get; set; }
        SqlConnection connection = new SqlConnection(Constr.connectionString);
        public void SelectAllApplications()
        {
            Console.Clear();
            int a = 0;
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string comstr = $"select LastName,FirstName,Aim,Salary,CreditSumm,Term,[Status] from Applications join Customer on Customer.SerP = Applications.SerP";
            SqlCommand command = new SqlCommand(comstr, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    a++;
                    Console.WriteLine($"Фамилия: {reader.GetValue(0).ToString()} \nИмя: {reader.GetValue(1).ToString()} \nЦель: {reader.GetValue(2).ToString()} \nОбщий доход: {reader.GetValue(3).ToString()} \nСумма: {reader.GetValue(4).ToString()} \nСрок: {reader.GetValue(5).ToString()} \nСтатус: {reader.GetValue(6).ToString()}");
                    Console.WriteLine("------------------------");
                }
            }
            if (a == 0)
            {
                Console.WriteLine("Заявок нет!");
            }
            Console.Write("Нажмите на любую клавишу чтобы вернуться...");
            Console.ReadKey();
        }
        public void SelectAllApplicationsBySerP()
        {
            Console.Clear();
            Console.Write("Введите серию паспорта или номер(логин): ");
            string SerP = Console.ReadLine();
            int a = 0;
            string comandtext = $"";
            if (int.TryParse(SerP, out a))
            {
                comandtext = $"select LastName,FirstName,Aim,Salary,CreditSumm,Term,[Status] from Applications join Customer on Customer.SerP = Applications.SerP where Applications.Serp = '{SerP}' or Customer.UserLogin = '{SerP}'";
            }
            else
            {
                comandtext = $"select LastName,FirstName,Aim,Salary,CreditSumm,Term,[Status] from Applications join Customer on Customer.SerP = Applications.SerP where Applications.Serp = '{SerP}'";
            }
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand command = new SqlCommand(comandtext, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    a++;
                    Console.WriteLine($"Фамилия: {reader.GetValue(0).ToString()} \nИмя: {reader.GetValue(1).ToString()} \nЦель: {reader.GetValue(2).ToString()} \nОбщий доход: {reader.GetValue(3).ToString()} \nСумма: {reader.GetValue(4).ToString()} \nСрок: {reader.GetValue(5).ToString()} \nСтатус: {reader.GetValue(6).ToString()}");
                    Console.WriteLine("-------------------------------");
                }
            }
            if (a == 0)
            {
                Console.WriteLine("У этого клиента нет заявок!");
            }
            Console.Write("Нажмите на любую клавишу чтобы вернуться...");
            Console.ReadKey();
        }
        public int AddAdmin()
        {
            Console.Clear();
            Console.Write("Введите логин: ");
            this.log = int.Parse(Console.ReadLine());
            Console.Write("Введите пароль: ");
            this.pas = Console.ReadLine();
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string com = $"select * from Admin";
            bool res = false;
            using (SqlCommand command = new SqlCommand(com, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (this.log == int.Parse(reader.GetValue(0).ToString()))
                        {
                            res = true;
                            break;
                        }
                    }
                }
            }
            if (!(res))
            {
                string comm = $"insert into Admin([AdminLogin],[AdminPassword]) values ({this.log},'{this.pas}')";
                SqlCommand command = new SqlCommand(comm, connection);
                int a = command.ExecuteNonQuery();
                Console.Clear();
                Console.WriteLine("Админ успешно добавлен!");
                Console.Write("Нажмите на любую клавишу чтобы вернуться...");
                Console.ReadKey();
                return a;
            }
            else
            {
                Console.WriteLine("Ошбика, админ с таким логином сущесвует!");
                Console.Write("Нажмите на любую клавишу чтобы вернуться...");
                Console.ReadKey();
                return 0;
            }
        }
        public bool FindAdmin()
        {
            Console.Clear();
            Console.Write("Введите логин: ");
            this.log = int.Parse(Console.ReadLine());
            Console.Write("Введите пароль: ");
            this.pas = Console.ReadLine();
            string comm = $"select * from Admin";
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand command = new SqlCommand(comm, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (this.log == int.Parse(reader.GetValue(0).ToString()) && this.pas == reader.GetValue(1).ToString())
                    {
                        Console.Clear();
                        return true;
                    }
                }
            }
            Console.WriteLine("Неправильный логин или пароль!");
            Console.Write("Нажмите на любую клавишу чтобы вернуться...");
            Console.ReadKey();
            return false;
        }
        public void SelectAllGraphic()
        {
            Console.Clear();
            int a = 0;
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand command = new SqlCommand($"select LastName,FirstName,SummForPay,DateForPay,PaySumm,PayDate,Pros from Graphic join Customer on Customer.SerP = Graphic.SerP", connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    a++;
                    if (reader.GetValue(5).ToString() != "")
                    {
                        Console.WriteLine($"Фамилия: {reader.GetValue(0).ToString()}\nИмя: {reader.GetValue(1).ToString()}\nСумма для оплаты: {reader.GetValue(2).ToString()}\nДо даты: {reader.GetValue(3).ToString().Substring(0, 10)}\nОплаченная сумма: {reader.GetValue(4).ToString()}\nДата оплаты: {reader.GetValue(5).ToString().Substring(0, 10)}\nПросрочка: {reader.GetValue(6).ToString()}");
                        Console.WriteLine("----------------------------");
                    }
                    else
                    {
                        Console.WriteLine($"Фамилия: {reader.GetValue(0).ToString()}\nИмя: {reader.GetValue(1).ToString()}\nСумма для оплаты: {reader.GetValue(2).ToString()}\nДо даты: {reader.GetValue(3).ToString().Substring(0, 10)}\nОплаченная сумма: {reader.GetValue(4).ToString()}\nДата оплаты: Нет\nПросрочка: {reader.GetValue(6).ToString()}");
                        Console.WriteLine("----------------------------");
                    }
                }
            }
            if (a == 0)
            {
                Console.WriteLine("Нет открытых кредитов");
            }
            Console.Write("Нажмите на любую клавишу чтобы вернуться...");
            Console.ReadKey();
        }
        public void ShowCustomers()
        {
            Console.Clear();
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string comstr = $"Select * from Customer";
            SqlCommand command = new SqlCommand(comstr, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Серия паспорта: {reader.GetValue("SerP").ToString()}\nФамилия: {reader.GetValue("LastName").ToString()}\nИмя: {reader.GetValue("FirstName").ToString()}\nДата рождения: {reader.GetValue("BirthDate").ToString().Substring(0, 10)}\nСемейное положение: {reader.GetValue("MaritalStatus").ToString()}\nГражданство: {reader.GetValue("Nation").ToString()}\nПол: {reader.GetValue("Gender").ToString()}\nНомер(Логин): {reader.GetValue("UserLogin").ToString()}");
                    Console.WriteLine("------------------------");
                }
            }
            Console.Write("Нажмите на любую клавишу чтобы вернуться...");
            Console.ReadKey();
        }
        public void ShowCustomersBySerp()
        {
            Console.Clear();
            Console.Write("Введите серию паспорта или номер(логин): ");
            string SerP = Console.ReadLine();
            int a;
            string comandtext = "";
            if (int.TryParse(SerP, out a))
            {
                comandtext = $"select * from Customer where SerP = '{SerP}' or UserLogin = {SerP}";
            }
            else
            {
                comandtext = $"select * from Customer where SerP = '{SerP}'";
            }
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand command = new SqlCommand(comandtext, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Серия паспорта: {reader.GetValue("SerP").ToString()}\nФамилия: {reader.GetValue("LastName").ToString()}\nИмя: {reader.GetValue("FirstName").ToString()}\nДата рождения: {reader.GetValue("BirthDate").ToString().Substring(0, 10)}\nСемейное положение: {reader.GetValue("MaritalStatus").ToString()}\nГражданство: {reader.GetValue("Nation").ToString()}\nПол: {reader.GetValue("Gender").ToString()}\nНомер(Логин): {reader.GetValue("UserLogin").ToString()}");
                    Console.WriteLine("------------------------");
                }
            }
            Console.Write("Нажмите на любую клавишу чтобы вернуться...");
            Console.ReadKey();
        }
        public void SelectAllGraphicBySerP()
        {
            Console.Clear();
            Console.Write("Введите серию паспорта или номер(логин): ");
            string SerP = Console.ReadLine();
            int a = 0;
            string comandtext = "";
            if (int.TryParse(SerP, out a))
            {
                comandtext = $"select LastName,FirstName,SummForPay,DateForPay,PaySumm,PayDate,Pros from Graphic join Customer on Customer.SerP = Graphic.SerP where Graphic.SerP = '{SerP}' or  Customer.UserLogin = {SerP}";
            }
            else
            {
                comandtext = $"select LastName,FirstName,SummForPay,DateForPay,PaySumm,PayDate,Pros from Graphic join Customer on Customer.SerP = Graphic.SerP where Graphic.SerP = '{SerP}'";
            }
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand command = new SqlCommand(comandtext, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    a++;
                    if (reader.GetValue(5).ToString() != "")
                    {
                        Console.WriteLine($"Фамилия: {reader.GetValue(0).ToString()}\nИмя: {reader.GetValue(1).ToString()}\nСумма для оплаты: {reader.GetValue(2).ToString()}\nДо даты: {reader.GetValue(3).ToString().Substring(0, 10)}\nОплаченная сумма: {reader.GetValue(4).ToString()}\nДата оплаты: {reader.GetValue(5).ToString().Substring(0, 10)}\nПросрочка: {reader.GetValue(6).ToString()}");
                        Console.WriteLine("----------------------------");
                    }
                    else
                    {
                        Console.WriteLine($"Фамилия: {reader.GetValue(0).ToString()}\nИмя: {reader.GetValue(1).ToString()}\nСумма для оплаты: {reader.GetValue(2).ToString()}\nДо даты: {reader.GetValue(3).ToString().Substring(0, 10)}\nОплаченная сумма: {reader.GetValue(4).ToString()}\nДата оплаты: Нет\nПросрочка: {reader.GetValue(6).ToString()}");
                        Console.WriteLine("----------------------------");
                    }
                }
            }
            if (a == 0)
            {
                Console.WriteLine("У этого клиента нет открытого кредита");
            }
            Console.Write("Нажмите на любую клавишу чтобы вернуться...");
            Console.ReadKey();
        }
        public void ShowCreditHistory()
        {
            Console.Clear();
            int a = 0;
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand command = new SqlCommand($"select LastName,FirstName,Aim,CreditSumm,Term,Pros,StartDate,EndDate,Status,Ostatok,SummWithProcent from Credit join Customer on Customer.SerP = Credit.SerP", connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    a++;
                    if (reader.GetValue(7).ToString() == "")
                    {
                        Console.WriteLine($"Фамилия: {reader.GetValue(0).ToString()} \nИмя: {reader.GetValue(1).ToString()} \nЦель: {reader.GetValue(2).ToString()} \nСумма кредита: {reader.GetValue(3).ToString()} \nСрок: {reader.GetValue(4).ToString()} \nПросрочка: {reader.GetValue(5).ToString()} \nДата открытия: {reader.GetValue(6).ToString().Substring(0, 10)}\nДата закрытия: Еще не закрыт\nСтатус: {reader.GetValue(8).ToString()}\nОстаток: {reader.GetValue(9).ToString()}\nСумма оплаты: {reader.GetValue(10).ToString()}");
                        Console.WriteLine("------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine($"Фамилия: {reader.GetValue(0).ToString()} \nИмя: {reader.GetValue(1).ToString()} \nЦель: {reader.GetValue(2).ToString()} \nСумма кредита: {reader.GetValue(3).ToString()} \nСрок: {reader.GetValue(4).ToString()} \nПросрочка: {reader.GetValue(5).ToString()} \nДата открытия: {reader.GetValue(6).ToString().Substring(0, 10)}\nДата закрытия: {reader.GetValue(7).ToString().Substring(0, 10)}\nСтатус: {reader.GetValue(8).ToString()}\nОстаток: {reader.GetValue(9).ToString()}\nСумма оплаты: {reader.GetValue(10).ToString()}");
                        Console.WriteLine("------------------------------------");
                    }
                }
            }
            Console.Write("Нажмите на любую клавишу чтобы вернуться...");
            Console.ReadKey();
        }
        public void ShowCreditHistoryBySerp()
        {
            Console.Clear();
            Console.Write("Введите серию паспорта или номер(логин): ");
            string SerP = Console.ReadLine();
            int a = 0;
            string comandtext = "";
            if (int.TryParse(SerP, out a))
            {
                comandtext = $"select LastName,FirstName,Aim,CreditSumm,Term,Pros,StartDate,EndDate,Status,Ostatok,SummWithProcent from Credit join Customer on Customer.SerP = Credit.SerP where Credit.Serp = '{SerP}' or Customer.UserLogin = {SerP}";
            }
            else
            {
                comandtext = $"select LastName,FirstName,Aim,CreditSumm,Term,Pros,StartDate,EndDate,Status,Ostatok,SummWithProcent from Credit join Customer on Customer.SerP = Credit.SerP where Credit.Serp = '{SerP}'";
            }
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand command = new SqlCommand(comandtext, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    a++;
                    if (reader.GetValue(7).ToString() == "")
                    {
                        Console.WriteLine($"Фамилия: {reader.GetValue(0).ToString()} \nИмя: {reader.GetValue(1).ToString()} \nЦель: {reader.GetValue(2).ToString()} \nСумма кредита: {reader.GetValue(3).ToString()} \nСрок: {reader.GetValue(4).ToString()} \nПросрочка: {reader.GetValue(5).ToString()} \nДата открытия: {reader.GetValue(6).ToString().Substring(0, 10)}\nДата закрытия: Еще не закрыт\nСтатус: {reader.GetValue(8).ToString()}\nОстаток: {reader.GetValue(9).ToString()}\nСумма оплаты: {reader.GetValue(10).ToString()}");
                        Console.WriteLine("------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine($"Фамилия: {reader.GetValue(0).ToString()} \nИмя: {reader.GetValue(1).ToString()} \nЦель: {reader.GetValue(2).ToString()} \nСумма кредита: {reader.GetValue(3).ToString()} \nСрок: {reader.GetValue(4).ToString()} \nПросрочка: {reader.GetValue(5).ToString()} \nДата открытия: {reader.GetValue(6).ToString().Substring(0, 10)}\nДата закрытия: {reader.GetValue(7).ToString().Substring(0, 10)}\nСтатус: {reader.GetValue(8).ToString()}\nОстаток: {reader.GetValue(9).ToString()}\nСумма оплаты: {reader.GetValue(10).ToString()}");
                        Console.WriteLine("------------------------------------");
                    }
                }
            }
            if (a == 0)
            {
                Console.WriteLine("У данного клиента нет кредитной истории");
            }
            Console.Write("Нажмите на любую клавишу чтобы вернуться...");
            Console.ReadKey();
        }
    }
}