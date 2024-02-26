using MySqlConnector;

namespace App.Data;

public static class ConnectionHelper
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("db");
        var databaseUrl = Environment.GetEnvironmentVariable("MYSQL_URL");
        return string.IsNullOrEmpty(databaseUrl) ? connectionString : BuildConnectionString(databaseUrl);
    }

    public static string BuildConnectionString(string databaseUrl)
    {
        var databaseUri = new Uri(databaseUrl);
        var userInfo = databaseUri.UserInfo.Split(':');

        var builder = new MySqlConnectionStringBuilder
        {
            Server = databaseUri.Host,
            Port = (uint)databaseUri.Port,
            Database = databaseUri.LocalPath.TrimStart('/'),
            UserID = userInfo[0],
            Password = userInfo[1]
        };

        return builder.ToString();
    }
}

