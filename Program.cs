using System;

namespace ProjectAlif
{
    class Program
    {
        static void Main(string[] args)
        {
        start:
            Console.Clear();
            Customer customer;
            Admin admin;
            Console.Write("1. Авторизация\n2. Вход\nВыбор: ");
            switch (Console.ReadLine())
            {
                case "1":
                    customer = new Customer();
                    if (customer.AddCustomer() >= 1) goto come;
                    else goto start;
                case "2":
                come:
                    Console.Clear();
                    Console.Write("1. Войти как админ\n2. Войти как клиент\n3. В стартовое окно\nВыбор: ");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            admin = new Admin();
                            if (admin.FindAdmin())
                            {
                            adminmenu:
                                Console.Clear();
                                Console.Write("1. Добавить админа\n2. Посмотреть все заявки\n3. Посмотреть заявки одного клиента\n4. Посмотреть график погашения всех клиентов\n5. Посмотреть график погашения одного клиента\n6. Посмотреть историю кредитов всех клиентов\n7. Посмотреть историю кредитов одного клиента\n8. Посмотреть список всех клиентов\n9. Посмотреть данные одного клиента\n10. Вернуться в меню входа\nВыбор: ");
                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        if (admin.AddAdmin() >= 1) goto adminmenu;
                                        else goto adminmenu;
                                    case "2": admin.SelectAllApplications(); goto adminmenu;
                                    case "3": admin.SelectAllApplicationsBySerP(); goto adminmenu;
                                    case "4": admin.SelectAllGraphic(); goto adminmenu;
                                    case "5": admin.SelectAllGraphicBySerP(); goto adminmenu;
                                    case "6": admin.ShowCreditHistory(); goto adminmenu;
                                    case "7": admin.ShowCreditHistoryBySerp(); goto adminmenu;
                                    case "8": admin.ShowCustomers(); goto adminmenu;
                                    case "9": admin.ShowCustomersBySerp(); goto adminmenu;
                                    case "10": goto come;
                                    default: goto adminmenu;
                                }
                            }
                            else goto come;
                        case "2":
                            customer = new Customer();
                            if (customer.FindCustomer())
                            {
                            menu:
                                Console.Clear();
                                Console.WriteLine($"Доброе пожаловать {customer.firstName} {customer.lastName}!");
                                Console.Write("1. Оставить заявку на кредит\n2. Посмотреть историю заявок\n3. Посмотреть данные\n4. Посмотреть кредитную историю\n5. Посмотреть график погашения\n6. Оплатить\n7. Венруться в меню входа\nВыбор: ");
                                switch (Console.ReadLine())
                                {
                                    case "1": customer.SendApp(); goto menu;
                                    case "2": customer.ShowApplicationWithSerP(); goto menu;
                                    case "3": customer.ShowInfoWithSerp(); goto menu;
                                    case "4": customer.ShowCreditWithSerP(); goto menu;
                                    case "5": customer.ShowGraphicWithSerP(); goto menu;
                                    case "6": if(customer.SearchOpenCredit()) customer.Pay(); goto menu;
                                    case "7": goto come;
                                    default: goto menu;
                                }
                            }
                            else goto come;
                        case "3": goto start;
                        default: goto come;
                    }
                default: goto start;
            }
        }
    }
}
