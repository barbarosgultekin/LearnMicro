namespace Micro.MessageBus
{
    public class BaseMessage
    {
        public int Id { get; set; }
        public DateTime MessageCreated { get; set; }
        public string token { get; set; }
    }
}