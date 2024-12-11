using System.Data;
using Dapper;
using Npgsql;

namespace AspireStarterDemo.DataService;

internal static class Repository
{
    public static async Task<string> GetSummary(NpgsqlDataSource dataSource)
    {
        await using var connection = await dataSource.OpenConnectionAsync();

        if (!await IsInitialized(connection))
        {
            await Initialize(connection);
        }

        var summary = await GetSummary(connection) ?? "none";
        return summary;
    }

    private static async Task<bool> IsInitialized(IDbConnection connection)
    {
        const string query = """
                             SELECT EXISTS (
                                 SELECT 1
                                 FROM information_schema.tables 
                                 WHERE table_schema = @Schema
                                   AND table_name = @TableName
                             )
                             """;

        var result = await connection.ExecuteScalarAsync<bool>(
            query, new { Schema = "public", TableName = "weather" });
        return result;
    }

    private static async Task Initialize(IDbConnection connection)
    {
        const string query = """
                             CREATE TABLE weather (
                                 id SERIAL PRIMARY KEY,
                                 summary TEXT NOT NULL
                             )
                             """;

        await connection.ExecuteAsync(query);

        string[] summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        const string insertQuery = "INSERT INTO weather (summary) VALUES (@Summary)";
        foreach (var summary in summaries)
        {
            await connection.ExecuteAsync(insertQuery, new { Summary = summary });
        }
    }

    private static async Task<string?> GetSummary(NpgsqlConnection connection)
    {
        var summary = await connection.QueryFirstOrDefaultAsync<string>(
            "SELECT summary FROM weather ORDER BY RANDOM() LIMIT 1");
        return summary;
    }
}