using System;
using System.Data;
using System.Data.SqlClient;

namespace ProjectAlif
{
    class Application
    {
        Customer customer;
        public Application(Customer customer)
        {
            this.customer = customer;
        }
        
        string Aim { get; set; }
        double Salary { get; set; }
        double CreditSumm { get; set; }
        int Term { get; set; }
        string status { get; set; }
        SqlConnection connection = new SqlConnection(Constr.connectionString);
        public int SendApp()
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
            bool a = Calculator.Calculate(customer,Aim,Salary,CreditSumm);
            if(a == false)
            {
                comstr = $"insert into Applications([Aim],[Salary],[CreditSumm],[Term],[Status],[SerP]) values ('{Aim}',{Salary},{CreditSumm},{Term},'{a}','{customer.GetSerP()}')";
                using (SqlCommand command = new SqlCommand(comstr, connection))
                {
                    command.ExecuteNonQuery();
                }
                System.Console.WriteLine("Отказано");
                return 0;
            }
            else
            {
                comstr = $"insert into Applications([Aim],[Salary],[CreditSumm],[Term],[Status],[SerP]) values ('{Aim}',{Salary},{CreditSumm},{Term},'{a}','{customer.GetSerP()}')";
                using (SqlCommand command = new SqlCommand(comstr, connection))
                {
                    command.ExecuteNonQuery();
                }
                // for (int i = 0; i < Term; i++)
                // {
                    
                //     // string constr = $"insert into Graphic([SerP],[SummForPay],[DateForPay])";
                //     // using (SqlCommand command = new SqlCommand(constr, connection))
                //     // {
                //     //     command.ExecuteNonQuery();
                //     // }
                    
                // }
                AddGraphic();
                comstr = $"insert into Credit([Aim],[CreditSumm],[Term],[Pros],[StartDate],[EndDate],[Status],[Ostatok],[SerP],[SummWithProcent]) values ('{Aim}',{CreditSumm},{Term},0,'{DateTime.Now.ToString().Substring(0,10)}',null,'Open',{CreditSumm * 0.2},'{customer.GetSerP()}',{CreditSumm + CreditSumm * 0.2})";
                using( SqlCommand command = new SqlCommand(comstr, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            return 1;
        }
        public void AddCredit()
        {
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand command = new SqlCommand($"",connection);
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
                SqlCommand command = new SqlCommand($"insert into Graphic([SerP],[SummForPay],[DateForPay],[Pros],[PaySumm],[PayDate]) values ('{customer.SerP}', '{Math.Round(paysumm,5)}' ,'{datefor}', '0', '0',null)",connection);
                command.ExecuteNonQuery();
                date = date.AddMonths(1);
            }
        }
        public void ShowApplicationWithSerP()
        {
            System.Console.WriteLine($"Фамилия | Имя | Цель | Доход | Сумма кредита | Срок | Статус ");
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand com = new SqlCommand("select LastName,FirstName,Aim,Salary,CreditSumm,Term,Status from Applications join Customer on Customer.SerP = Applications.SerP where Applications.SerP = '"+customer.SerP+"'",connection);
            using(SqlDataReader reader = com.ExecuteReader())
            {
                while(reader.Read())
                {
                    System.Console.WriteLine($"{reader.GetValue(0).ToString()} | {reader.GetValue(1).ToString()} | {reader.GetValue(2).ToString()} | {reader.GetValue(3).ToString()} | {reader.GetValue(4).ToString()} | {reader.GetValue(5).ToString()} | {reader.GetValue(6).ToString()} ");
                }
            }
        }
    }
}