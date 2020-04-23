using System;

namespace ProjectAlif
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine(int.Parse(DateTime.Now.ToString().Substring(6, 4)));
            Customer customer;
            Admin admin;
            string choise = "";
            bool res = false;
            int result = 0;
            System.Console.Write("1. Авторизация\n2. Вход\nВыбор: ");
            choise = Console.ReadLine();
            switch (choise)
            {

                case "1":
                    Console.Clear();
                    customer = new Customer();
                    result = customer.AddCustomer();
                    if (result >= 1)
                    {
                        System.Console.WriteLine("Welcome!");
                        goto come;
                    }
                    else
                    {
                        System.Console.WriteLine("eror");
                    }
                    break;
                case "2":
                come:
                    Console.Clear();
                    System.Console.Write("1. Войти как админ\n2. Войти как клиент\nВыбор: ");
                    choise = Console.ReadLine();
                    switch (choise)
                    {
                        case "1":
                            Console.Clear();
                            System.Console.WriteLine("Войти как админ");
                            admin = new Admin();
                            res = admin.FindAdmin();
                            if (res)
                            {
                                Console.Clear();
                                System.Console.WriteLine("1. Добавить админа: ");
                                choise = Console.ReadLine();
                                switch (choise)
                                {
                                    case "1":
                                        result = admin.AddAdmin();
                                        if (result >= 1)
                                        {
                                            System.Console.WriteLine("successfully");
                                        }
                                        else
                                        {
                                            System.Console.WriteLine("eror");
                                        }
                                        break;
                                    case "2":
                                        admin.SelectAllApplications();
                                        break;
                                    case "3":
                                        admin.SelectAllApplicationsFromSerP("a 12345679");
                                        break;
                                }
                            }
                            else
                            {
                                System.Console.WriteLine("eror");
                            }
                            break;
                        case "2":
                            Console.Clear();
                            customer = new Customer();
                            res = customer.FindCustomer();
                            if (res)
                            {
                            menu:
                                Console.Clear();
                                System.Console.WriteLine($"Доброе пожаловать {customer.firstName} {customer.lastName}!");
                                System.Console.Write("1. Оставить заявку на кредит\n2. Посмотреть историю заявок\n3. Посмотреть данные\n4. Посмотреть кредитную историю\n5. Посмотреть график погашения\n6. Оплатить\nВыбор: ");
                                choise = Console.ReadLine();
                                switch (choise)
                                {
                                    case "1":
                                        Console.Clear();
                                        Application application = new Application(customer);
                                        application.SendApp();
                                        System.Console.Write("Press any key to turn back...");
                                        Console.ReadKey();
                                        goto menu;
                                    case "2":
                                        Console.Clear();
                                        Application application1 = new Application(customer);
                                        application1.ShowApplicationWithSerP();
                                        System.Console.Write("Press any key to turn back...");
                                        Console.ReadKey();
                                        goto menu;
                                    case "3":
                                        Console.Clear();
                                        customer.ShowInfoWithSerp();
                                        System.Console.Write("Press any key to turn back...");
                                        Console.ReadKey();
                                        goto menu;
                                    case "4":
                                        Console.Clear();
                                        customer.ShowCreditWithSerP();
                                        System.Console.Write("Press any key to turn back...");
                                        Console.ReadKey();
                                        goto menu;
                                    case "5":
                                        Console.Clear();
                                        customer.ShowGraphicWithSerP();
                                        System.Console.Write("Press any key to turn back...");
                                        Console.ReadKey();
                                        goto menu;
                                }
                            }
                            else
                            {
                                System.Console.WriteLine("eror");
                            }
                            break;
                    }
                    break;
            }
            Console.ReadKey();
        }
    }
}
