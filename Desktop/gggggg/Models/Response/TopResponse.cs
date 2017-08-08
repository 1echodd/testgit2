namespace DXzonghejiaofei.Models.Response
{
    public class TopResponse<T>
    {
        public string Result { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public override string ToString()
        {
            return $"{Result}-{Message}";
        }
    }
}