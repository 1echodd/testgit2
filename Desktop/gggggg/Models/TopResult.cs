namespace DXzonghejiaofei.Models
{
    public class TopResult<T>
    {
        public bool IsSuccess { set; get; }//是否得到期望的响应
        public bool? DealSuccess{ set; get; }//订单的状态
        public string Message { set; get; }
        public T DataView { set; get; }

        public string FileCodeMessage { set; get; }
    }
}
