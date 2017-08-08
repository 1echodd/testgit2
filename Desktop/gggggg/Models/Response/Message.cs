using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXzonghejiaofei.Models.Response
{
    public class Message
    {
        public string isPay { set; get; }
        public string orderId { set; get; }
        public string rechargeStatus { set; get; }
        public string failcode { set; get; }
        public string requestNo { set; get; }   
    }
}
