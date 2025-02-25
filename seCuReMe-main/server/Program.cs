using Server.Database;
using Microsoft.AspNetCore.Builder;//Why
using Microsoft.AspNetCore.Http;//Why
using Microsoft.Extensions.DependencyInjection; //Why
using Npgsql; 
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load("../.env");

var dbConfig = new DatabaseConfig
{
    Host = Env.GetString("DB_HOST"),
    Port = int.TryParse(Env.GetString("DB_PORT"), out var port) ? port : 5432,
    Username = Env.GetString("DB_USER"),
    Password = Env.GetString("DB_PASSWORD"),
    Database = Env.GetString("DB_NAME")
};
Console.WriteLine($"Database Host: {Env.GetString("DB_HOST")}");

// registrera databasen som a singleton
builder.Services.AddSingleton(dbConfig);
// registrera databasen som en service
builder.Services.AddSingleton<Database>();

var app = builder.Build();

//Hämtar databas från DI
var database = app.Services.GetRequiredService<Database>();

//Testa anslutningen till db
await database.TestConnectionAsync();

NpgsqlDataSource _db = database.Connection();

//dynamic Map
app.Map("/users", async (HttpContext context, NpgsqlConnection db) =>
{
    if (context.Request.Method == "GET")
    {
        // Handle GET (Select from DB)
        var id = context.Request.Query["id"];
        var result = await db.QueryAsync<User>("SELECT * FROM users WHERE id = @Id", new { Id = id });
        return Results.Ok(result);
    }
    else if (context.Request.Method == "POST")
    {
        // Handle POST (Insert into DB)
        var user = await context.Request.ReadFromJsonAsync<User>();
        await db.ExecuteAsync("INSERT INTO users (name, email) VALUES (@Name, @Email)", user);
        return Results.Ok("User added successfully");
    }
    return Results.BadRequest("Invalid method");
});
app.MapGet("/", () => "Hello World!");

app.Run();

