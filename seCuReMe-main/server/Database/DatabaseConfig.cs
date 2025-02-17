namespace Server.Database;

public class DatabaseConfig
{
    public string Host { get; set; } = "";
    public int Port { get; set; } = 5432;
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string Database { get; set; } = "";
}