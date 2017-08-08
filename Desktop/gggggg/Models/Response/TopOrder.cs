using System;
using DXzonghejiaofei.Enum;

namespace DXzonghejiaofei.Models.Response
{
    public abstract class TopOrder
    {
        //接口请求参数
        //public string custId { set; get; }
        //public string shopProductId { set; get; }
        //public string telPhone { set; get; }
        //public string requestNo { set; get; }

        //接口返回参数
       // public string code { set; get; }
       // public string errorcode { set; get; }
        //public Message message { set; get; }
       // public string sequence { set; get; }
        //public string successcount { set; get; }
        //public System.Collections.Generic.List<Object> trafficShopProduct { set; get; }
       // public System.Collections.Generic.List<Object> userInventorylist { set; get; }

        public string Account { set; get; }
        public decimal Amount { set; get; }
        public string GoodsName { set; get; }
        public int GoodsId { set; get; }
        public string OrderId { set; get; }
        public string Type { set; get; }

        public long Id { get; set; }
        public string ChannelOrderId { get; set; }
        public string State { set; get; }
        public string Remark { set; get; }
        public DateTime StartTime { set; get; }
        public DateTime EndTime { set; get; }
        public override string ToString()
        {
            return string.IsNullOrEmpty(Remark)
                ?$"{OrderId}-{Account}-{Amount}"
                : $"{OrderId}-{Account}-{Amount}|{Remark}";
        }
    }
}