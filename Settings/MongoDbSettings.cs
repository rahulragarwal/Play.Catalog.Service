namespace Play.Catalog.Service.Settings
{
    public class MongoDbSettings
    {

        public static string SettingName = "MongoDbSettings";

        public string Host { get; init; }

        public string Port { get; init; }

    }
}