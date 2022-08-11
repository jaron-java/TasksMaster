using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksMaster;

internal class Tools
{
    internal static string GetTaskInput(string message = "Please enter the task:")
    {
        Console.WriteLine(message);

        string taskInput = Console.ReadLine();

        return taskInput;
    }


    internal static string GetDateInput(string connectionString)
    {
        Console.WriteLine("\n\nPlease insert the date: (Format: MM-dd-yy). Type 0 to return to main menu.\n\n");

        string dateInput = Console.ReadLine();

        if (dateInput == "0") GetUserInput(connectionString);

        while (!DateTime.TryParseExact(dateInput, "MM-dd-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            Console.WriteLine("\n\nInvalid date. (Format: MM-dd-yy). Type 0 to return to main menu or try again:\n\n");
            dateInput = Console.ReadLine();
        }
        return dateInput;
    }
    internal static void GetUserInput(string connectionString)
    {
        Console.Clear();
        bool closeApp = false;
        while (closeApp == false)
        {
            Console.WriteLine("\n\nMAIN MENU");
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("\nType 0 to Close Application.");
            Console.WriteLine("Type 1 to View All Tasks.");
            Console.WriteLine("Type 2 to Insert Task.");
            Console.WriteLine("Type 3 to Delete Task.");
            Console.WriteLine("------------------------------------------\n");

            string command = Console.ReadLine();

            switch (command)
            {
                case "0":
                    Console.WriteLine("\nGoodbye!\n");
                    closeApp = true;
                    Environment.Exit(0);
                    break;
                case "1":
                    DB.GetAllRecords(connectionString);
                    break;
                case "2":
                    DB.Insert(connectionString);
                    break;
                case "3":
                    DB.Delete(connectionString);
                    break;
                default:
                    Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                    break;
            }
        }
    }
}

