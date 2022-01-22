namespace eLearning_System.Interfaces
{
    public interface IDatabaseSettings
    {
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
