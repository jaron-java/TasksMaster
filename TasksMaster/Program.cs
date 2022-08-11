using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Sqlite;

namespace TasksMaster;

class Program
{
    static void Main()
    {
        string connectionString = @"Data Source=todoList.db";
        DB.CreateConnection(connectionString);
        Console.WriteLine("Welcome to Tasks Master!");

        bool closeApp = false;
        while (!closeApp)
        {
            Tools.GetUserInput(connectionString);
        }
    }
}
