using System.Collections.Generic;

namespace DXzonghejiaofei.Models.Response
{
    public class GetOrdersResponse<T>
    {
        public List<T> Orders { get; set; }
    }
}