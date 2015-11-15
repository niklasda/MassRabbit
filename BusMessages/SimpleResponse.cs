namespace BusMessages
{
    public class SimpleResponse : ISimpleResponse
    {
        public string CusomerName { get; set; }
    }

    public interface ISimpleResponse
    {
        string CusomerName { get; }
    }
}