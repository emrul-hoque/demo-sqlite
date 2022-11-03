using Microsoft.Data.Sqlite;

// https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite
public class Program
{
    private const string CONNECTION_STRING = "Data Source=hello.db";

    public static void Main()
    {
        DisplayUsers();


        var optContinue = true;

        while (optContinue)
        {
            Console.WriteLine("Enter \n1 to add new Name \n2 to delete record \n3 to update a record");
            int i = Convert.ToInt32(Console.ReadLine());

            switch (i)
            {
                case 1:
                    Console.WriteLine("Enter family member's name: ");
                    string name = Console.ReadLine();
                    AddUserRecord(name);
                    break;

                case 2:
                    Console.WriteLine("Enter the record number of the person that needs to be deleted ");
                    int num = Convert.ToInt32(Console.ReadLine());
                    DeleteUserRecord(num);
                    break;

                case 3:
                    Console.WriteLine("\nEnter the record number of the person that you want to edit and then type the person's name");
                    int record = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine($"User enterted: {record}");
                    Console.WriteLine("\nNow enter the name of the person");
                    string? person = Console.ReadLine();
                    Console.WriteLine($"User enterted: {person}");
                    UpdateUserRecord(record, person);
                    break;

            }

            Console.WriteLine("Press Y to continue or N to quit");
            var readUserOpt = Console.ReadLine();
           
            if (readUserOpt?.ToUpper() == "N" )
            {
                optContinue = false;
            }
        }
    }

    private static void PromptUserForOpt()
    {
        Console.WriteLine("Press Y to continue or N to quit");
        var readUserOpt = Console.ReadLine();
    }

    public static void CreateTable()
    {
        using (var connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText =
                            @"
                    CREATE TABLE user (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        name TEXT NOT NULL
                    );                    
                ";
            command.ExecuteNonQuery();
        }
    }

    public static void DeleteTable(string table_name)
    {
        using (var connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText =
                            $"DROP TABLE [ IF EXISTS ] {table_name};";
            command.ExecuteNonQuery();
        }
    }

    public static void UpdateUserRecord(int id, string name)
    {
        using (var connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText =
                            $"UPDATE user SET name = '{name}' WHERE id = {id}";
            var i = command.ExecuteNonQuery();

            Console.WriteLine($"Affected rows: {i}");
        }
    }

    public static void AddUserRecord(string name)
    {
        using (var connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText =
                            $"INSERT INTO user (name) VALUES ('{name}')";
            command.ExecuteNonQuery();
        }
    }

    public static void DeleteUserRecord(int id)
    {
        using (var connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText = $"DELETE FROM user WHERE id = {id}";
            command.ExecuteNonQuery();
        }
    }

    public static void DisplayUsers()
    {
        using (var connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText = $"SELECT * FROM user";
            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader["id"] + ": " + reader["name"]);
            }
        }
    }
}

