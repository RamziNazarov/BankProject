using System;
using System.Data;
using System.Data.SqlClient;

namespace ProjectAlif
{
    class Customer
    {
        public string GetSerP()
        {
            return SerP;
        }
        public string SerP { get; set; }
        public int log { get; set; }
        public string pas { get; set; }
        public string birthDate { get; set; }
        public string gender { get; set; }
        public string maritalStatus { get; set; }
        public string nation { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        string Aim { get; set; }
        double Salary { get; set; }
        double CreditSumm { get; set; }
        int Term { get; set; }
        string status { get; set; }
        SqlConnection connection = new SqlConnection(Constr.connectionString);
        public int AddCustomer()
        {
            System.Console.Write("Введите серию паспорта: ");
            this.SerP = Console.ReadLine();
            System.Console.Write("Введите номер телефона(логин): ");
            this.log = int.Parse(Console.ReadLine());
            System.Console.Write("Введите пароль: ");
            this.pas = Console.ReadLine();
            System.Console.Write("Введите дату рождения(дд.мм.гггг): ");
            this.birthDate = Console.ReadLine();
        gen:
            System.Console.WriteLine("Выберите пол:\n1. Мужской\n2. Женский");
            string gen = Console.ReadLine();
            this.gender = (gen == "1") ? "Муж" : (gen == "2") ? "Жен" : "Eror";
            if (gender == "Eror") goto gen;
            maritalStatus:
            System.Console.WriteLine("Выберите семейное положение:\n1. Холост\n2. Семянин\n3. В разводе\n4. Вдовец/Вдова");
            string mar = Console.ReadLine();
            this.maritalStatus = (mar == "1") ? "Холост" : (mar == "2") ? "Семянин" : (mar == "3") ? "В разводе" : (mar == "4") ? "Вдова/Вдовец" : "Er";
            if (maritalStatus == "Er") goto maritalStatus;
            System.Console.Write("Введите ваше гражданство: ");
            this.nation = (Console.ReadLine().ToLower() == "таджикистан") ? "Таджикистан" : "Зарубеж";
            System.Console.Write("Введите имя: ");
            this.firstName = Console.ReadLine();
            System.Console.Write("Введите фамилию: ");
            this.lastName = Console.ReadLine();
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string com = $"select * from Customer";
            bool res = false;
            using (SqlCommand command = new SqlCommand(com, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (SerP == reader.GetValue(0).ToString() || log == int.Parse(reader.GetValue(1).ToString()))
                        {
                            res = true;
                            break;
                        }
                    }
                }
            }
            if (!(res))
            {
                com = $"insert into Customer([SerP],[UserLogin],[UserPasswod],[FirstName],[LastName],[Gender],[MaritalStatus],[Nation],[BirthDate]) values ('{this.SerP}',{this.log},'{this.pas}','{this.firstName}','{this.lastName}','{this.gender}','{this.maritalStatus}','{this.nation}','{this.birthDate}')";
                SqlCommand command = new SqlCommand(com, connection);
                return command.ExecuteNonQuery();
            }
            else
            {
                return 0;
            }
        }
        public bool FindCustomer()
        {
            System.Console.Write("Введите логин: ");
            this.log = int.Parse(Console.ReadLine());
            System.Console.Write("Введите пароль: ");
            this.pas = Console.ReadLine();
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string com = $"select * from Customer";
            SqlCommand command = new SqlCommand(com, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (this.log == int.Parse(reader.GetValue(1).ToString()) && this.pas == reader.GetValue(2).ToString())
                    {
                        this.SerP = reader.GetValue(0).ToString();
                        this.nation = reader.GetValue(6).ToString();
                        this.firstName = reader.GetValue(7).ToString();
                        this.lastName = reader.GetValue(8).ToString();
                        this.maritalStatus = reader.GetValue(5).ToString();
                        this.gender = reader.GetValue(4).ToString();
                        this.birthDate = reader.GetValue(3).ToString().Substring(0, 10);
                        return true;
                    }
                }
            }
            return false;
        }
        public void ShowInfoWithSerp()
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand command = new SqlCommand("Select * from Customer where SerP = '" + SerP + "'", connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    System.Console.WriteLine($"Серия паспорат: {reader.GetValue(0).ToString()}\nЛогин: {reader.GetValue(1).ToString()}\nПароль: {reader.GetValue(2).ToString()}\nДата рожения: {reader.GetValue(3).ToString().Substring(0, 10)}\nПол: {reader.GetValue(4).ToString()}\nСемейное положение: {reader.GetValue(5).ToString()}\nГражданство: {reader.GetValue(6).ToString()}\nИмя: {reader.GetValue(7).ToString()}\nФамилия: {reader.GetValue(8).ToString()}");
                }
            }
        }
        public void ShowGraphicWithSerP()
        {
            int a = 0;
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand command = new SqlCommand($"select LastName,FirstName,SummForPay,DateForPay,PaySumm,PayDate,Pros from Graphic join Customer on Customer.SerP = Graphic.SerP where Graphic.Serp = '{SerP}'", connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    a++;
                    string res = reader.GetValue(5).ToString().ToLower();
                    if (res != "")
                    {
                        System.Console.WriteLine($"{a} месяц\nФамилия: {reader.GetValue(0).ToString()} \nИмя: {reader.GetValue(1).ToString()} \nСумма: {reader.GetValue(2).ToString()} \nДо даты: {reader.GetValue(3).ToString().Substring(0, 10)} \nОплачено: {reader.GetValue(4).ToString()} \nДата оплаты: {reader.GetValue(5).ToString().Substring(0, 10)} \nПросрочка: {reader.GetValue(6).ToString()}");
                        System.Console.WriteLine("------------------------------------");
                    }
                    else
                    {
                        System.Console.WriteLine($"{a} месяц\nФамилия: {reader.GetValue(0).ToString()} \nИмя: {reader.GetValue(1).ToString()} \nСумма: {reader.GetValue(2).ToString()} \nДо даты: {reader.GetValue(3).ToString().Substring(0, 10)} \nОплачено: {reader.GetValue(4).ToString()} \nДата оплаты: Нет \nПросрочка: {reader.GetValue(6).ToString()}");
                        System.Console.WriteLine("------------------------------------");
                    }
                }
            }
            if(a == 0)
            {
                System.Console.WriteLine("У вас нет открытых кредитов!");
            }
        }
        public void ShowCreditWithSerP()
        {
            int a = 0;
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand command = new SqlCommand($"select LastName,FirstName,Aim,CreditSumm,Term,Pros,StartDate,EndDate,Status,Ostatok,SummWithProcent from Credit join Customer on Customer.SerP = Credit.SerP where Credit.Serp = '{SerP}'", connection);
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    a++;
                    if(reader.GetValue(7).ToString() == "")
                    {
                        System.Console.WriteLine($"{a} Кредит\nФамилия: {reader.GetValue(0).ToString()} \nИмя: {reader.GetValue(1).ToString()} \nЦель: {reader.GetValue(2).ToString()} \nСумма кредита: {reader.GetValue(3).ToString()} \nСрок: {reader.GetValue(4).ToString()} \nПросрочка: {reader.GetValue(5).ToString()} \nДата открытия: {reader.GetValue(6).ToString().Substring(0,10)}\nДата закрытия: Еще не закрыт\nСтатус: {reader.GetValue(8).ToString()}\nОстаток: {reader.GetValue(9).ToString()}\nСумма оплаты: {reader.GetValue(10).ToString()}");
                        System.Console.WriteLine("------------------------------------");
                    }
                    else
                    {
                        System.Console.WriteLine($"{a} Кредит\nФамилия: {reader.GetValue(0).ToString()} \nИмя: {reader.GetValue(1).ToString()} \nЦель: {reader.GetValue(2).ToString()} \nСумма кредита: {reader.GetValue(3).ToString()} \nСрок: {reader.GetValue(4).ToString()} \nПросрочка: {reader.GetValue(5).ToString()} \nДата открытия: {reader.GetValue(6).ToString().Substring(0,10)}\nДата закрытия: {reader.GetValue(7).ToString().Substring(0,10)}\nСтатус: {reader.GetValue(8).ToString()}\nОстаток: {reader.GetValue(9).ToString()}\nСумма оплаты: {reader.GetValue(10).ToString()}");
                        System.Console.WriteLine("------------------------------------");
                    }
                }
            }
            if(a == 0)
            {
                System.Console.WriteLine("У вас нет кредитной истории!");
            }
        }
        public void SendApp()
        {
            string comstr;
            System.Console.Write("Цель кредите:\n1. Бытовая техника\n2. Телефон\n3. Ремонт\n4. Прочее\nВыбор: ");
            string ai = Console.ReadLine();
            Aim = (ai == "1") ? "Бытовая техника" : (ai == "2") ? "Телефон" : (ai == "3") ? "Ремонт" : (ai == "4") ? "Прочее" : "Eror";
            System.Console.Write("Сумма кредита: ");
            CreditSumm = double.Parse(Console.ReadLine());
            System.Console.Write("Общий доход в месяц: ");
            Salary = double.Parse(Console.ReadLine());
            System.Console.Write("На какой срок(в месяцах): ");
            Term = int.Parse(Console.ReadLine());
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            bool a = Calculator.Calculate(this,Aim,Salary,CreditSumm);
            if(a == false)
            {
                comstr = $"insert into Applications([Aim],[Salary],[CreditSumm],[Term],[Status],[SerP]) values ('{Aim}',{Salary},{CreditSumm},{Term},'Отказано','{SerP}')";
                using (SqlCommand command = new SqlCommand(comstr, connection))
                {
                    command.ExecuteNonQuery();
                }
                System.Console.WriteLine("Отказано");
            }
            else
            {
                comstr = $"insert into Applications([Aim],[Salary],[CreditSumm],[Term],[Status],[SerP]) values ('{Aim}',{Salary},{CreditSumm},{Term},'Одобрено','{SerP}')";
                using (SqlCommand command = new SqlCommand(comstr, connection))
                {
                    command.ExecuteNonQuery();
                }
                AddGraphic();
                AddCredit();
                System.Console.WriteLine("Одобрено!");
            }
        }
        public void AddCredit()
        {
            if(connection.State == ConnectionState.Closed)
                connection.Open();
                string scomstr = $"insert into Credit([Aim],[CreditSumm],[Term],[Pros],[StartDate],[EndDate],[Status],[Ostatok],[SerP],[SummWithProcent]) values ('{Aim}',{CreditSumm},{Term},0,'{DateTime.Now.ToString().Substring(0,10)}',null,'Открыт',{CreditSumm+CreditSumm * 0.2},'{SerP}',{CreditSumm + CreditSumm * 0.2})";
            SqlCommand command = new SqlCommand(scomstr,connection);
            command.ExecuteNonQuery();
        }
        public void AddGraphic()
        {
            DateTime date = DateTime.Now;
            date = date.AddMonths(1);
            double paysumm = (CreditSumm + (CreditSumm * 0.2)) / Term;
            for(int i = 0; i < Term;i++)
            {
                if(connection.State == ConnectionState.Closed)
                    connection.Open();
                string datefor = date.ToString().Substring(0,10);
                SqlCommand command = new SqlCommand($"insert into Graphic([SerP],[SummForPay],[DateForPay],[Pros],[PaySumm],[PayDate]) values ('{SerP}', '{Math.Round(paysumm,0)}' ,'{datefor}', '0', '0',null)",connection);
                command.ExecuteNonQuery();
                date = date.AddMonths(1);
            }
        }
        public void ShowApplicationWithSerP()
        {
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand com = new SqlCommand("select LastName,FirstName,Aim,Salary,CreditSumm,Term,Status from Applications join Customer on Customer.SerP = Applications.SerP where Applications.SerP = '"+SerP+"'",connection);
            int a =0;
            using(SqlDataReader reader = com.ExecuteReader())
            {
                while(reader.Read())
                {
                    a++;
                    System.Console.WriteLine($"{a} Заявка\nФамилия: {reader.GetValue(0).ToString()}\nИмя: {reader.GetValue(1).ToString()} \nЦель: {reader.GetValue(2).ToString()} \nДоход: {reader.GetValue(3).ToString()} \nСумма кредита: {reader.GetValue(4).ToString()} \nСрок: {reader.GetValue(5).ToString()} \nСтатус: {reader.GetValue(6).ToString()} ");
                    System.Console.WriteLine("---------------------------------");
                }
            }
            if(a == 0)
            {
                System.Console.WriteLine("У вас нет запросов!");
            }
        }
        public void Pay()
        {
            System.Console.Write("Введите сумму: ");
            double summa = double.Parse(Console.ReadLine());
            System.Console.Write("Введите дату(дд.мм.гггг): ");
            string date = Console.ReadLine();
            int dd = int.Parse(date.Substring(0,2));
            int mm = int.Parse(date.Substring(3,2));
            int yy = int.Parse(date.Substring(6,4));
            double vsyasumma = 0;
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand command = new SqlCommand($"select * from Graphic where SerP = '{SerP}'",connection);
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    int month = int.Parse(reader.GetValue(2).ToString().Substring(3,2));
                    int year = int.Parse(reader.GetValue(2).ToString().Substring(6,4));
                    int day = int.Parse(reader.GetValue(2).ToString().Substring(0,2));
                    double summforpay = double.Parse(reader.GetValue(1).ToString());
                    vsyasumma+=summforpay;
                    // if(double.Parse(reader.GetValue(1).ToString()) < double.Parse(reader.GetValue(3).ToString()))
                    // {
                    //     string datefor = reader.GetValue(2).ToString().Substring(0,10);
                    //     if(int.Parse(datefor.Substring(3,2)) > int.Parse(date.Substring(3,2)))
                    //     {
                    //         // SqlCommand command1 = new SqlCommand($"update Graphic set PaySumm = {summa},Pros = {1}",connection);
                    //         // command1.ExecuteNonQuery();
                    //     }
                    //     if(double.Parse(reader.GetValue(3).ToString()) < summa)
                    //     {

                    //     }
                    //     SqlCommand com = new SqlCommand($"update Graphic set PaySumm = '{Math.Round(summa,5)}', PayDat = '{date}'",connection);
                    //     com.ExecuteNonQuery();
                    // }
                }
            }
            if(vsyasumma == summa)
            {
                SqlCommand command1 = new SqlCommand($"update Credit set Status = 'Закрыт',Ostatok = '{vsyasumma - summa}', EndDate = '{date}' where SerP = '{SerP}'; delete from Graphic where SerP = '{SerP}'",connection);
                command1.ExecuteNonQuery();                
            }
        }
    }
}