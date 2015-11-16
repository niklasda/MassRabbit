namespace BusMessages
{
    public class SimpleResponse : ISimpleResponse
    {
        public string CustomerName { get; set; }
    }

    public interface ISimpleResponse
    {
        string CustomerName { get; }
    }
}