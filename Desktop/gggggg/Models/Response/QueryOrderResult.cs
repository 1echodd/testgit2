using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXzonghejiaofei.Models.Response
{
    public class QueryOrderResult
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public data Data { get; set; }
    }

    public class data
    {
        public string Id { get; set; }
        public string OrderCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
