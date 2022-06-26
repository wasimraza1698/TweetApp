namespace TweetApp.DBSettings.Interfaces
{
    public interface IDBSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
