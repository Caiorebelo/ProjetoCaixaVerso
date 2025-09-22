using System.Diagnostics.CodeAnalysis;
using Microsoft.Data.Sqlite;

[ExcludeFromCodeCoverage]
public class Db
{
    public string ConnectionString { get; }

    public Db()
    {
        ConnectionString = "Data Source=dbcaixaverso.db";
    }

    public Task InitializeAsync()
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                CREATE TABLE IF NOT EXISTS Produtos (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nome TEXT NOT NULL,
                    TaxaJurosAnual REAL NOT NULL,
                    PrazoMaximoMeses INTEGER NOT NULL
                );
            ";

            command.ExecuteNonQuery();
        }

        return Task.CompletedTask;
    }
}
