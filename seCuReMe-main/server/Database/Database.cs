using System;
using Npgsql;
using DotNetEnv;
using server.Models;

namespace Server.Database;

public class Database //Klass som ska ha en metod som heter Connection och returnerar NpgsqlDataSource och en konstruktor som sätter connection string och datasource
{
  private readonly string _connectionString; //Priva variabel med en connection string
  private readonly NpgsqlDataSource _dataSource; //Privat variabel som är av typen NpgsqlDataSource som innehåller en datasource

  public Database()
  {
    Env.Load("../.env");

    Console.WriteLine($"DB_HOST: {Env.GetString("DB_HOST")}");
    Console.WriteLine($"DB_PORT: {Env.GetString("DB_PORT")}");
    Console.WriteLine($"DB_USER: {Env.GetString("DB_USER")}");
    Console.WriteLine($"DB_PASSWORD: {Env.GetString("DB_PASSWORD")}");
    Console.WriteLine($"DB_NAME: {Env.GetString("DB_NAME")}");

    
    _connectionString = new NpgsqlConnectionStringBuilder // skapar en connection-string till Builder som innehåller inlog-data till server
    {
      Host = Env.GetString("DB_HOST"),
      Port = Env.GetInt("DB_PORT"),
      Username = Env.GetString("DB_USER"),
      Password = Env.GetString("DB_PASSWORD"),
      Database = Env.GetString("DB_NAME"),
      Pooling = true //Aktivera pooling för att återanvända anslutningar till databasen
    }.ToString();

    _dataSource = NpgsqlDataSource.Create(_connectionString); //Sätter _dataSource till en NpgsqlDataSource som skapar en datasource med connections string

    try
    {
      using var connection = _dataSource.OpenConnection();
      Console.WriteLine("Connection successful");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Connection failed: {ex.Message}");
    }
  }
  public NpgsqlDataSource Connection()
  {
    return _dataSource;
  }
}
