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
        public string SerP{get;set;}
        public int log{get;set;}
        public string pas{get;set;}
        public string birthDate{get;set;}
        public string gender{get;set;}
        public string maritalStatus{get;set;}
        public string nation{get;set;}
        public string firstName{get;set;}
        public string lastName{get;set;}
        SqlConnection connection = new SqlConnection(Constr.connectionString);
        public int AddCustomer()
        {
            System.Console.Write("Введите серию паспорта: ");
            this.SerP =Console.ReadLine();
            System.Console.Write("Введите номер телефона(логин): ");
            this.log =int.Parse(Console.ReadLine());
            System.Console.Write("Введите пароль: ");
            this.pas =Console.ReadLine();
            System.Console.Write("Введите дату рождения(дд.мм.гггг): ");
            this.birthDate =Console.ReadLine();
            gen:
            System.Console.WriteLine("Выберите пол:\n1. Мужской\n2. Женский");
            string gen = Console.ReadLine();
            this.gender = (gen == "1")?"Муж":(gen == "2")?"Жен":"Eror";
            if(gender == "Eror")goto gen;
            maritalStatus:
            System.Console.WriteLine("Выберите семейное положение:\n1. Холост\n2. Семянин\n3. В разводе\n4. Вдовец/Вдова");
            string mar = Console.ReadLine();
            this.maritalStatus =(mar == "1")?"Холост":(mar == "2")?"Семянин":(mar == "3")?"В разводе":(mar == "4")?"Вдова/Вдовец":"Er";
            if(maritalStatus == "Er")goto maritalStatus;
            System.Console.Write("Введите ваше гражданство: ");
            this.nation =(Console.ReadLine().ToLower() == "таджикистан")?"Таджикистан":"Зарубеж";
            System.Console.Write("Введите имя: ");
            this.firstName =Console.ReadLine();
            System.Console.Write("Введите фамилию: ");
            this.lastName =Console.ReadLine();
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            string com = $"select * from Customer";
            bool res = false;
            using(SqlCommand command = new SqlCommand(com,connection))
            {   
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        if(SerP == reader.GetValue(0).ToString() || log == int.Parse(reader.GetValue(1).ToString()))
                        {
                            res = true;
                            break;
                        }
                    }
                }
            }
            if(!(res))
            {
                com = $"insert into Customer([SerP],[UserLogin],[UserPasswod],[FirstName],[LastName],[Gender],[MaritalStatus],[Nation],[BirthDate]) values ('{this.SerP}',{this.log},'{this.pas}','{this.firstName}','{this.lastName}','{this.gender}','{this.maritalStatus}','{this.nation}','{this.birthDate}')";
                SqlCommand command = new SqlCommand(com,connection);
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
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            string com = $"select * from Customer";
            SqlCommand command = new SqlCommand(com,connection);
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    if(this.log == int.Parse(reader.GetValue(1).ToString()) && this.pas == reader.GetValue(2).ToString())
                    {
                        this.SerP = reader.GetValue(0).ToString();
                        this.nation = reader.GetValue(6).ToString();
                        this.firstName = reader.GetValue(7).ToString();
                        this.lastName = reader.GetValue(8).ToString();
                        this.maritalStatus = reader.GetValue(5).ToString();
                        this.gender = reader.GetValue(4).ToString();
                        this.birthDate = reader.GetValue(3).ToString().Substring(0,10);
                        return true;
                    }
                }
            }
            return false;
        }
        public void ShowInfoWithSerp()
        {
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand command = new SqlCommand("Select * from Customer where SerP = '"+SerP+"'",connection);
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    System.Console.WriteLine($"Серия паспорат: {reader.GetValue(0).ToString()}\nЛогин: {reader.GetValue(1).ToString()}\nПароль: {reader.GetValue(2).ToString()}\nДата рожения: {reader.GetValue(3).ToString().Substring(0,10)}\nПол: {reader.GetValue(4).ToString()}\nСемейное положение: {reader.GetValue(5).ToString()}\nГражданство: {reader.GetValue(6).ToString()}\nИмя: {reader.GetValue(7).ToString()}\nФамилия: {reader.GetValue(8).ToString()}");
                }
            }
        }
        public void ShowGraphicWithSerP()
        {
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            System.Console.WriteLine("Фамилия | Имя | Сумма | До даты | Оплачено | Дата оплаты | Просрочка ");
            SqlCommand command = new SqlCommand($"select LastName,FirstName,SummForPay,DateForPay,PaySumm,PayDate,Pros from Graphic join Customer on Customer.SerP = Applications.SerP where Applications.Serp = '{SerP}'",connection);
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    if(reader.GetValue(5).ToString().ToLower() != "null")
                    System.Console.WriteLine($"{reader.GetValue(0).ToString()} | {reader.GetValue(1).ToString()} | {reader.GetValue(2).ToString()} | {reader.GetValue(3).ToString().Substring(0,10)} | {reader.GetValue(4).ToString()} | {reader.GetValue(5).ToString().Substring(0,10)} | {reader.GetValue(6).ToString()}");
                    else
                    System.Console.WriteLine($"{reader.GetValue(0).ToString()} | {reader.GetValue(1).ToString()} | {reader.GetValue(2).ToString()} | {reader.GetValue(3).ToString().Substring(0,10)} | {reader.GetValue(4).ToString()} | Нет | {reader.GetValue(6).ToString()}");
                }
            }
        }
    }
}