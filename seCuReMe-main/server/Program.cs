using Server.Database;
using Microsoft.AspNetCore.Builder;//Why
using Microsoft.AspNetCore.Http;//Why
using Microsoft.Extensions.DependencyInjection;
using Npgsql; //Why


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


var database = new Database();
NpgsqlDataSource _db = database.Connection();

app.MapGet("/", () => "Hello World!");

app.Run();
