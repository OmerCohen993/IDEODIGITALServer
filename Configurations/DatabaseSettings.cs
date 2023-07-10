namespace Server.Configurations
{
    public class DatabaseSettings
    {
        public string ConnectionUri { get; set; } = null!;
        public string DataBaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
    }
}