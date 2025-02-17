using System;
using Npgsql;
//Tar bara hand om uppkopplingen till databasen. .env laddas i program.cs
namespace Server.Database;

public class Database //Klass som ska ha en metod som heter Connection och returnerar NpgsqlDataSource och en konstruktor som sätter connection string och datasource
{
  private readonly string _connectionString; //Privat variabel med en connection string
  private readonly NpgsqlDataSource _dataSource; //Privat variabel som är av typen NpgsqlDataSource som innehåller en datasource

  public Database(DatabaseConfig config)
  {
    Console.WriteLine($"DB_HOST: {config.Host}");
    Console.WriteLine($"DB_PORT: {config.Port}");
    Console.WriteLine($"DB_USER: {config.Username}");
    Console.WriteLine($"DB_PASSWORD: {config.Password}");
    Console.WriteLine($"DB_NAME: {config.Database}");


    _connectionString =
      new
        NpgsqlConnectionStringBuilder // skapar en connection-string till Builder som innehåller inlog-data till server
        {
          Host = config.Host,
          Port = config.Port,
          Username = config.Username,
          Password = config.Password,
          Database = config.Database,
          Pooling = true //Aktivera pooling för att återanvända anslutningar till databasen
        }.ToString();

    _dataSource =
      NpgsqlDataSource.Create(
        _connectionString); //Sätter _dataSource till en NpgsqlDataSource som skapar en datasource med connections string
  }
  
  public NpgsqlDataSource Connection()
  {
    return _dataSource;
  }
  public async Task TestConnectionAsync()
  {
    try
    {
      await using var connection = await _dataSource.OpenConnectionAsync();
      Console.WriteLine("Connection successful");
      //await connection.DisposeAsync();
    }
    catch (NpgsqlException npgsqlEx)
    {
      Console.WriteLine($"Database error: {npgsqlEx.Message}");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Connection failed: {ex.Message}");
    }
  }
}


