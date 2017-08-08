using System.Collections.Concurrent;

namespace DXzonghejiaofei.Models
{
    public static class TopQueue<T>
    {
        public static ConcurrentQueue<T> SubmitOrders = new ConcurrentQueue<T>();
        public static ConcurrentQueue<T> QueryOrders = new ConcurrentQueue<T>();
        public static ConcurrentQueue<T> DealOrders = new ConcurrentQueue<T>();
    }
}