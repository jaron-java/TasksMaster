using Microsoft.Data.Sqlite;
using System.Globalization;

namespace TasksMaster;

internal class DB
{
    internal static void CreateConnection(string connectionString)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();

            tableCmd.CommandText =
                @"CREATE TABLE IF NOT EXISTS todoList (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date TEXT,
                        Task TEXT
                        )";

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
        Console.WriteLine("Connected to the database!");
    }


    public static void PrintRecords(string connectionString)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"SELECT * FROM todoList ";

            List<Todo> TodoList = new();

            SqliteDataReader reader = tableCmd.ExecuteReader();

            Console.WriteLine("Here are your Tasks, Master");
            Console.Clear();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TodoList.Add(
                    new Todo
                    {
                        Id = reader.GetInt32(0),
                        Date = DateTime.ParseExact(reader.GetString(1), "MM-dd-yy", new CultureInfo("en-US")),
                        Task = reader.GetString(2)
                    }); ;
                }
            }
            else
            {
                Console.WriteLine("No Tasks To Do");
            }

            connection.Close();

            Console.WriteLine("------------------------------------------\n");
            foreach (var todo in TodoList)
            {
                Console.WriteLine($"{todo.Id} - {todo.Date.ToString("MM-dd-yy")} - Task: {todo.Task}");
            }
            Console.WriteLine("------------------------------------------\n");
        }
        Console.ReadKey();

    }
    public static void GetAllRecords(string connectionString)
    {
        PrintRecords(connectionString);
        Console.ReadKey();
        Tools.GetUserInput(connectionString);
    }

    public static void Insert(string connectionString)
    {
        string date = Tools.GetDateInput(connectionString);
        string task = Tools.GetTaskInput();

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"INSERT INTO todoList(Date, Task) VALUES('{date}', '{task}')";

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
        Console.WriteLine($"Added {task} to your list!");

        Tools.GetUserInput(connectionString);
    }

    public static void Delete(string connectionString)
    {
        Console.Clear();

        PrintRecords(connectionString);

        string taskDelete = Tools.GetTaskInput("\n\nPlease type the name of the task you wish to delete or type 0 to go back to Main Menu\n\n");

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();

            tableCmd.CommandText = $"DELETE from todoList WHERE Task = '{taskDelete}'";

            int rowCount = tableCmd.ExecuteNonQuery();

            if (rowCount == 0)
            {
                Console.WriteLine($"\n\nTask ({taskDelete}) doesn't exist. \n\n");
                Tools.GetUserInput(connectionString);
            }
        }

        Console.WriteLine($"\n\n{taskDelete} was deleted. \n\n");

        Tools.GetUserInput(connectionString);
    }
}