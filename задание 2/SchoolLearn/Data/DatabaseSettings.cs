using MySqlConnector;

namespace SchoolLearn.Data;

public sealed record DatabaseSettings
{
    public string Host { get; init; } = "85.236.189.123";
    public uint Port { get; init; } = 13306;
    public string Database { get; init; } = "isp-44_ilchenko_dm_learn_task2";
    public string User { get; init; } = "isp-44_ilchenko_dm";
    public string Password { get; init; } = "isp-44_ilchenko_dm";

    public static DatabaseSettings Load() => new();

    public string ConnectionString => new MySqlConnectionStringBuilder
    {
        Server = Host, Port = Port, Database = Database, UserID = User, Password = Password,
        CharacterSet = "utf8mb4", ConnectionTimeout = 10, DefaultCommandTimeout = 20,
        SslMode = MySqlSslMode.None, AllowPublicKeyRetrieval = true, Pooling = true
    }.ConnectionString;
}
