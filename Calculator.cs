using System;
using System.Data;
using System.Data.SqlClient;

namespace ProjectAlif
{
    static class Calculator
    {
        public static bool Calculate(Customer customer,string aim,double salary,double creditsumm)
        {
            SqlConnection connection = new SqlConnection(Constr.connectionString);    
            int calc = 0;
            int count = 0;
            int countofcredit = 0;
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            using(SqlCommand command = new SqlCommand($" select * from Credit where SerP= '{customer.SerP}'",connection))
            {
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        if(reader.GetValue(6).ToString() != "Открыт")
                        {
                            countofcredit ++;
                        }
                        count += int.Parse(reader.GetValue(3).ToString());
                    }
                }
            }
            calc += (customer.gender == "Муж")?1:2;
            calc += (customer.maritalStatus == "Холост")?1:(customer.maritalStatus == "Семянин")?2:(customer.maritalStatus == "В разводе")?1:2;
            calc += (customer.nation == "таджикистан")?1:0;
            int age = int.Parse(DateTime.Now.ToString().Substring(6,4)) - int.Parse(customer.birthDate.Substring(6,4));
            calc += (age > 62)?1:(age > 35)?2:(age > 25)?1:0;
            calc += 1;
            calc += (aim == "Бытовая техника")?2:(aim == "Ремонт")?1:(aim == "Прочее")?-1:0;
            Console.Write("Введите количество закрытых кредитов в других банках: ");
            countofcredit += int.Parse(Console.ReadLine());
            Console.Write("Введите количество просрочек в других банках: ");
            count += int.Parse(Console.ReadLine());
            Console.WriteLine("Общее количество закрытых кредитов: " + countofcredit);
            Console.WriteLine("Общее количество просрочек: "+count);
            calc -= (count < 4)?0:(count == 4)?1:(count < 8)?2:3;
            calc += (countofcredit == 0)?-1:(countofcredit<3)?1:2;
            double find = creditsumm * 100 / salary;
            calc += (find < 80)?4:(find < 150)?3:(find < 250)?2:1;
            Console.WriteLine("Общее количество баллов:" + calc);
            return (calc < 12)?false:true;
        }
    }
}