namespace eLearning_System.Settings
{
    public class MongoDbConfig
    {
        /// <summary>
     /// Name of the database
     /// </summary>
        public string Name { get; init; }
        /// <summary>
        /// Hosting URL
        /// </summary>
        public string Host { get; init; }
        /// <summary>
        /// Port on which its running
        /// </summary>
        public string Port { get; init; }
        /// <summary>
        /// Hold the connection string
        /// </summary>
        public string ConnectionString => $"mongodb://{Host}:{Port}";
    }
}
