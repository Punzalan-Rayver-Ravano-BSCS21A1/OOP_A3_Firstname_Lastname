using System;
using System.Collections.Generic;

namespace OOPCoffee
{
    class Program
    {
        static void Main()
        {
            Menu menu = new Menu();
            Order order = new Order();
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Welcome to the but First Coffee!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1. Add Menu Item");
                Console.WriteLine("2. View Menu");
                Console.WriteLine("3. Place Order");
                Console.WriteLine("4. View Order");
                Console.WriteLine("5. Calculate Total");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int choice) && choice >= 1 && choice <= 6)
                {
                    switch (choice)
                    {
                        case 1:
                            menu.AddMenuItem();
                            break;
                        case 2:
                            menu.ViewMenu();
                            break;
                        case 3:
                            order.PlaceOrder(menu);
                            break;
                        case 4:
                            order.ViewOrder();
                            break;
                        case 5:
                            order.CalculateTotal();
                            break;
                        case 6:
                            exit = true;
                            break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Choose a number between 1 to 6.");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                if (!exit)
                {
                    Console.WriteLine("Press any key to return to the main menu...");
                    Console.ReadKey();
                }
            }
        }
    }

    class Menu
    {
        private List<Tuple<string, decimal>> menuItems = new List<Tuple<string, decimal>>();

        public void AddMenuItem()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Enter a menu item. Type 'exit' if you are done: ");
                string foodName = Console.ReadLine();
                if (foodName.ToLower() == "exit")
                    break;

                Console.Write("Enter the price of the item: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    menuItems.Add(new Tuple<string, decimal>(foodName, price));
                    Console.WriteLine($"Item '{foodName}' added with a price of {price:C}.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid price. Please enter a valid decimal value.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public void ViewMenu()
        {
            Console.Clear();
            if (menuItems.Count == 0)
            {
                Console.WriteLine("No menu items available.");
                return;
            }

            Console.WriteLine("Menu Items:");
            int number = 1;
            foreach (var item in menuItems)
            {
                Console.WriteLine($"{number}. {item.Item1} - {item.Item2:C}");
                number++;
            }
        }

        public List<Tuple<string, decimal>> GetMenuItems()
        {
            return menuItems;
        }
    }

    class Order
    {
        private List<Tuple<string, decimal>> checkOut = new List<Tuple<string, decimal>>();

        public void PlaceOrder(Menu menu)
        {
            List<Tuple<string, decimal>> menuItems = menu.GetMenuItems();
            if (menuItems.Count == 0)
            {
                Console.WriteLine("No items available to order.");
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Available Menu Items:");
                int number = 1;
                foreach (var item in menuItems)
                {
                    Console.WriteLine($"{number}. {item.Item1} - {item.Item2:C}");
                    number++;
                }

                Console.Write("Enter the number of the item you want to order or 'exit' to return: ");
                string input = Console.ReadLine();
                if (input.ToLower() == "exit")
                    break;

                if (int.TryParse(input, out int itemNumber) && itemNumber > 0 && itemNumber <= menuItems.Count)
                {
                    var selectedItem = menuItems[itemNumber - 1];
                    checkOut.Add(selectedItem);
                    Console.WriteLine($"Item '{selectedItem.Item1}' added to your order.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid item number.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public void ViewOrder()
        {
            Console.Clear();
            if (checkOut.Count == 0)
            {
                Console.WriteLine("Your order is empty.");
                return;
            }

            Console.WriteLine("Your Order:");
            foreach (var item in checkOut)
            {
                Console.WriteLine($"{item.Item1} - {item.Item2:C}");
            }
        }

        public void CalculateTotal()
        {
            Console.Clear();
            decimal total = 0;
            foreach (var item in checkOut)
            {
                total += item.Item2;
            }
            Console.WriteLine($"Total amount: {total:C}");
        }
    }
}
