using TweetApp.DBSettings.Interfaces;

namespace TweetApp.DBSettings
{
    public class TweetSettings : IDBSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
