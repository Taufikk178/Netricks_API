namespace Netricks.Core.Entities.Common
{
    public class ResponseAPI
    {
        public int Code { get; set; } = 0;
        public string Message { get; set; } = null;
        public dynamic? Data { get; set; }
    }
}
