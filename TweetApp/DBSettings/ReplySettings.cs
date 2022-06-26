using TweetApp.DBSettings.Interfaces;

namespace TweetApp.DBSettings
{
    public class ReplySettings : IDBSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
