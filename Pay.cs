using System;
using System.Data;
using System.Data.SqlClient;

namespace ProjectAlif
{
    class Payment
    {
        SqlConnection connection = new SqlConnection(Constr.connectionString);
        public void Pay(string Serp)
        {
            System.Console.Write("Введите сумму: ");
            double summa = double.Parse(Console.ReadLine());
            System.Console.Write("Введите дату(дд.мм.гггг): ");
            string date = Console.ReadLine();
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand command = new SqlCommand("select * from Graphic where SerP = '"+Serp+"'",connection);
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    if(double.Parse(reader.GetValue(1).ToString()) < double.Parse(reader.GetValue(3).ToString()))
                    {
                        string datefor = reader.GetValue(2).ToString().Substring(0,10);
                        if(int.Parse(datefor.Substring(3,2)) > int.Parse(date.Substring(3,2)))
                        {
                            // SqlCommand command1 = new SqlCommand($"update Graphic set PaySumm = {summa},Pros = {1}",connection);
                            // command1.ExecuteNonQuery();
                        }
                        if(double.Parse(reader.GetValue(3).ToString()) < summa)
                        {

                        }
                        SqlCommand com = new SqlCommand($"update Graphic set PaySumm = '{Math.Round(summa,5)}', PayDat = '{date}'",connection);
                        com.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}