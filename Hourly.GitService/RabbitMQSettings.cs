namespace Hourly.GitService
{
    public class RabbitMQSettings
    {
        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 5672;
        public string UserName { get; set; } = "devuser";
        public string Password { get; set; } = "devpassword";
    }
}
