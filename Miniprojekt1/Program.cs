using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Miniprojekt1
{
    class Program
    {

        static void Main(string[] args)
        {

            string currentDir = Environment.CurrentDirectory;
            List<Product> products = new List<Product>();

            Laptop laptop1 = new Laptop("2018-08-22", "MacBook", "MacOs", "Silver", 400);
            Mobile mobile1 = new Mobile("2018-06-22", "Nokia", "Yellow", 700);
            Laptop laptop2 = new Laptop("2018-11-22", "Asus", "Windows 10", "Blue", 500);
            Product mobile3 = new Mobile("2019-09-09", "Samsung", "Green", 500);
            Product laptop3 = new Laptop("2018-08-24", "Lenovo", "Linux", "Black", 500);
            Mobile mobile2 = new Mobile("2018-10-01", "Iphone", "Gold", 900);
            products.Add(mobile3);
            products.Add(laptop1);
            products.Add(mobile2);
            products.Add(laptop2);
            products.Add(laptop3);
            products.Add(mobile1);

            while (true)
            {
                Console.Write("\nEnter new Product, for Laptop Computers 'l' for Mobile Phones 'm', To Exit write 'q' ");
                string input = Console.ReadLine().ToLower().Trim();
                if (input == "q")
                {
                    break;
                }
                else if (input == "l")
                {
                    string purchaseDate = StringCheck("Purchase Date");
                    string modelName = StringCheck("Model Name");
                    string systemOperation = StringCheck("System Operation");
                    string color = StringCheck("Color");
                    int price = IntCheck("Price");
                    Product newProduct = new Laptop(purchaseDate, modelName, systemOperation, color, price);
                    products.Add(newProduct);
                }
                else if (input == "m")
                {
                    string purchaseDate = StringCheck("Purchase Date");
                    string modelName = StringCheck("Model Name");
                    string color = StringCheck("Color");
                    int price = IntCheck("Price");
                    Product newProduct = new Mobile(purchaseDate, modelName, color, price);
                    products.Add(newProduct);
                }
            }
            ProductsPrintOut(products);
            Console.ReadLine();
        }

        private static string StringCheck(string Name)
        {
            string StringInput;
            while (true)
            {
                Console.Write($"Enter the {Name}: ");
                StringInput = Console.ReadLine();
                bool stringInputIsInt = int.TryParse(StringInput, out int value);

                if (String.IsNullOrEmpty(StringInput))
                {
                    WriteLineColor($"The {Name} cant be Empty", ConsoleColor.Red);
                }
                else if (stringInputIsInt)
                {
                    Console.WriteLine($"The {Name} cant be a Number");
                    WriteLineColor($"The {Name} cant be  a Number", ConsoleColor.Red);
                }
                else
                {
                    break;
                }
            }
            return StringInput;
        }

        private static int IntCheck(string Name)
        {
            int intCheckValue;
            while (true)
            {
                Console.Write($"Enter the {Name}: ");
                string intCheck = Console.ReadLine();
                if (int.TryParse(intCheck, out intCheckValue))
                {
                    break;
                }
                else
                {
                    WriteLineColor($"Wrong formate of the {Name}", ConsoleColor.Red);

                }
            }
            return intCheckValue;
        }

        private static void ProductsPrintOut(List<Product> products)
        {
            int indexL = 0, indexM = 0;
            List<Product> sortedproducts = products.
                OrderBy(product => product.GetType().Name).
                ThenBy(product => product.PurchaseDate).
                ToList();

            string msgTitle = "\n\nPurchaseDate".PadRight(20) + "Model Name".PadRight(20) + "SystemOperation".PadRight(25) + "Color".PadRight(20) + "Price";
            Console.WriteLine(msgTitle);
            File.WriteAllText("ResultFile.txt", msgTitle + Environment.NewLine);
            string msg = "";
            foreach (Product prod in sortedproducts)
            {
                if (prod is Laptop)
                {
                    if (indexL == 0)
                    {
                        msg = "\nLaptop Computers ";
                        Console.WriteLine(msg);
                        File.AppendAllText("ResultFile.txt", msg + Environment.NewLine);
                    }
                    indexL++;
                    msg = " - " + prod.PurchaseDate.PadRight(20) + prod.ModelName.PadRight(20) + (prod as Laptop).SystemOperation.PadRight(20) + prod.Color.PadRight(20) + (prod as Laptop).Price;
                    WriteLineColor(msg, DateValidation(prod.PurchaseDate));
                }
                else
                {
                    if (indexM == 0)
                    {
                        msg = "\nMobile Phones ";
                        Console.WriteLine(msg);
                        File.AppendAllText("ResultFile.txt", msg + Environment.NewLine);
                    }
                    indexM++;
                    msg = " - " + prod.PurchaseDate.PadRight(20) + prod.ModelName.PadRight(20) + prod.Color.PadRight(20) + (prod as Mobile).Price;
                    WriteLineColor(msg, DateValidation(prod.PurchaseDate));
                }
                File.AppendAllText("ResultFile.txt", msg + Environment.NewLine);
            }
        }
        static ConsoleColor DateValidation(string purchaseDate)
        {
            ConsoleColor colorW = ConsoleColor.White;
            DateTime nowDate = new DateTime();
            nowDate = DateTime.Now;
            DateTime ValidDate = new DateTime();
            TimeSpan diffDate = new TimeSpan();
            int difDays = 0;
            bool isValidDate = DateTime.TryParse(purchaseDate, out ValidDate);
            if (isValidDate)
            {
                diffDate = nowDate - ValidDate;
                difDays = Convert.ToInt32(diffDate.TotalDays);
                if (difDays < 1080)
                {
                    colorW = difDays < 900 ? ConsoleColor.White : difDays < 990 ? ConsoleColor.Yellow : ConsoleColor.Red;
                }
            }
            else
            {
                Console.WriteLine("The time is not valide, please try again");
            }
            return colorW;
        }

        public static void WriteLineColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    class Product
    {
        public string PurchaseDate { get; set; }
        public string ModelName { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }
    }

    class Laptop : Product
    {
        public string SystemOperation { get; set; }
        public Laptop(string purchaseDate, string modelName, string systemOperation, string color, int price)
        {
            PurchaseDate = purchaseDate;
            ModelName = modelName;
            SystemOperation = systemOperation;
            Color = color;
            Price = price;
        }

    }

    class Mobile : Product
    {
        public Mobile(string purchaseDate, string modelName, string color, int price)
        {
            PurchaseDate = purchaseDate;
            ModelName = modelName;
            Color = color;
            Price = price;
        }
    }

}