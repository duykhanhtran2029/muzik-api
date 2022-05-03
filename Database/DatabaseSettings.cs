namespace Database
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string SongsCollectionName { get; set; }
        public string TFPsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string LocalConnection { get; set; }
    }

    public interface IDatabaseSettings
    {
        string SongsCollectionName { get; set; }
        string TFPsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string LocalConnection { get; set; }
    }
}
