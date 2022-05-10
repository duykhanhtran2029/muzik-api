namespace Database
{
    public class MongoDatabaseSettings : IMongoDatabaseSettings
    {
        public string SongsCollectionName { get; set; }
        public string TFPsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDatabaseSettings
    {
        string SongsCollectionName { get; set; }
        string TFPsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
