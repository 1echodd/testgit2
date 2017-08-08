namespace DXzonghejiaofei.Models.Consumer
{
    public class SupResponse<T>
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public  T Object { get; set; }
        public int Code { get; set; }
    }
}