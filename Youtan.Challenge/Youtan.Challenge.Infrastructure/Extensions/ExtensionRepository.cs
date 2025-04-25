﻿using Microsoft.Extensions.Configuration;

namespace Youtan.Challenge.Infrastructure.Extensions;

public static class ExtensionRepository
{
    public static string GetConnection(this IConfiguration configurationManager)
    {
        var connection = configurationManager.GetConnectionString("Connection");
        return connection;
    }
    public static string GetDatabaseName(this IConfiguration configurationManager)
    {
        var databaseName = configurationManager.GetConnectionString("DatabaseName");
        return databaseName;
    }

    public static string GetFullConnection(this IConfiguration configurationManager)
    {
        var databaseName = configurationManager.GetDatabaseName();
        var connection = configurationManager.GetConnection();
        Console.WriteLine("#################################################################################################################");
        Console.WriteLine($"{connection}Database={databaseName};");
        return $"{connection}Database={databaseName};";
    }
}
