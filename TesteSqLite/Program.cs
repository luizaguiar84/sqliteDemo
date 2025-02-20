using Microsoft.Data.Sqlite;

namespace TesteSqLite;

class Program
{
    static void Main(string[] args)
    {
        Directory.CreateDirectory("C:/Temp");
        const string connectionString = "Data Source=c://temp//database.db";

        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var createTable = @"
                CREATE TABLE IF NOT EXISTS Usuarios (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nome TEXT NOT NULL,
                    Idade INTEGER NOT NULL
                );";
            
        using (var command = new SqliteCommand(createTable, connection))
        {
            command.ExecuteNonQuery();
        }

        var insertQuery = "INSERT INTO Usuarios (Nome, Idade) VALUES (@nome, @idade);";
        using var commandInsert = new SqliteCommand(insertQuery, connection);
        commandInsert.Parameters.AddWithValue("@nome", "João");
        commandInsert.Parameters.AddWithValue("@idade", 30);
        commandInsert.ExecuteNonQuery();

        // Ler os dados
        var selectQuery = "SELECT Id, Nome, Idade FROM Usuarios;";
        using (var command = new SqliteCommand(selectQuery, connection))
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["Id"]}, Nome: {reader["Nome"]}, Idade: {reader["Idade"]}");
            }
        }
    }
}