using Microsoft.Data.Sqlite;

// https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite
public class Program
{
    private const string CONNECTION_STRING = "Data Source=hello.db";

    public static void Main()
    {
        DisplayUsers();

        Console.WriteLine("Enter \n1 to add new Name \n2 to delete record \n3 to view records ");
        int i = Convert.ToInt32(Console.ReadLine());

        switch (i)
        {
            case 1:
                AddUserRecord("Mozammel");
                break;
        }
        //DeleteTable("user");
        //CreateTable();
        //UpdateUserRecord(2, "Rufana");

        //DeleteUserRecord(3);
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
            command.ExecuteNonQuery();
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

