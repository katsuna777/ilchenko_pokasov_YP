namespace KushayVkusno.Data;

public static class DatabaseConfig
{
    public const string Host = "85.236.189.123";
    public const int Port = 13306;
    public const string Database = "isp-44_ilchenko_dm_kushay_vkusno";
    public const string User = "isp-44_ilchenko_dm";
    public const string Password = "isp-44_ilchenko_dm";

    public static string ConnectionString =>
        $"Server={Host};Port={Port};Database={Database};User ID={User};Password={Password};" +
        "Character Set=utf8mb4;SslMode=None;AllowPublicKeyRetrieval=True;Connection Timeout=15;";
}
