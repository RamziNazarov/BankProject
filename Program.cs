using System;

namespace ProjectAlif
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer customer;
            Admin admin;
            System.Console.Write("1. Авторизация\n2. Вход\nВыбор: ");
            switch (Console.ReadLine())
            {

                case "1":
                    regagain:
                    Console.Clear();
                    customer = new Customer();
                    if (customer.AddCustomer() >= 1)
                    {
                        System.Console.WriteLine("Вы успешно зарегистрировались.");
                        System.Console.WriteLine("Press any key to log in...");
                        Console.ReadKey();
                        goto come;
                    }
                    else
                    {
                        System.Console.WriteLine("Клиент с таким логином или серией паспорта существует!");
                        System.Console.WriteLine("Press any key to reg again...");
                        Console.ReadKey();
                        goto regagain;
                    }
                case "2":
                come:
                    Console.Clear();
                    System.Console.Write("1. Войти как админ\n2. Войти как клиент\nВыбор: ");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            secondchance:
                            Console.Clear();
                            admin = new Admin();
                            if (admin.FindAdmin())
                            {
                            adminmenu:
                                Console.Clear();
                                System.Console.WriteLine("1. Добавить админа\n2. Посмотреть все заявки\n3. Посмотреть заявки одного клиента\n4. Посмотреть график погашения всех клиентов\n5. Посмотреть график погашения одного клиента\n6. ");
                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        if (admin.AddAdmin() >= 1)
                                        {
                                            Console.Clear();
                                            System.Console.WriteLine("Админ успешно добавлен!");
                                            System.Console.WriteLine("Press any key tot turn back...");
                                            Console.ReadKey();
                                            goto adminmenu;
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            System.Console.WriteLine("Ошбика, админ с таким логином сущесвует!");
                                            System.Console.WriteLine("Press any key to turn back...");
                                            Console.ReadKey();
                                            goto adminmenu;
                                        }
                                    case "2":
                                        Console.Clear();
                                        admin.SelectAllApplications();
                                        System.Console.WriteLine("Press any key to turn back...");
                                        Console.ReadKey();
                                        goto adminmenu;
                                    case "3":
                                        Console.Clear();
                                        System.Console.Write("Введите серию паспорта или номер(логин): ");
                                        admin.SelectAllApplications(Console.ReadLine());
                                        System.Console.WriteLine("Press any key to turn back...");
                                        Console.ReadKey();
                                        goto adminmenu;
                                    case "4":
                                        Console.Clear();
                                        admin.SelectAllGraphic();
                                        System.Console.WriteLine("Press any key to turn back...");
                                        Console.ReadKey();
                                        goto adminmenu;
                                    case "5":
                                        Console.Clear();
                                        System.Console.Write("Введите серию паспорта или номер(логин): ");
                                        admin.SelectAllGraphic(Console.ReadLine());
                                        System.Console.WriteLine("Press any key to turn back...");
                                        Console.ReadKey();
                                        goto adminmenu;
                                }
                            }
                            else
                            {
                                System.Console.WriteLine("Неправильный логин или пароль!");
                                goto secondchance;
                            }
                            break;
                        case "2":
                            chance:
                            Console.Clear();
                            customer = new Customer();
                            if (customer.FindCustomer())
                            {
                            menu:
                                Console.Clear();
                                System.Console.WriteLine($"Доброе пожаловать {customer.firstName} {customer.lastName}!");
                                System.Console.Write("1. Оставить заявку на кредит\n2. Посмотреть историю заявок\n3. Посмотреть данные\n4. Посмотреть кредитную историю\n5. Посмотреть график погашения\n6. Оплатить\nВыбор: ");
                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        Console.Clear();
                                        customer.SendApp();
                                        System.Console.Write("Press any key to turn back...");
                                        Console.ReadKey();
                                        goto menu;
                                    case "2":
                                        Console.Clear();
                                        customer.ShowApplicationWithSerP();
                                        Console.Write("Press any key to turn back...");
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
                                    case "6":
                                        Console.Clear();
                                        customer.Pay();
                                        System.Console.Write("Press any key to turn back...");
                                        Console.ReadKey();
                                        goto menu;
                                }
                            }
                            else
                            {
                                System.Console.WriteLine("Неправильный логин или пароль!");
                            }
                            goto chance;
                    }
                    break;
            }
            Console.ReadKey();
        }
    }
}
