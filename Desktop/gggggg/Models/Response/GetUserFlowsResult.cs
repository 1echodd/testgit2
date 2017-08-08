using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXzonghejiaofei.Models.Response
{
 public  class GetUserFlowsResult
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public List<Datas> Data { get; set; }
    }
    public class Datas
    {
        public string FlowId { get; set; }
        public string OperatorName { get; set; }
        public string FlowName { get; set; }
        public string Flow { get; set; }
        public string IsLocalFlow { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string DefaultPrice { get; set; }
        public string CustomerPrice { get; set; }
        public string FlowStatus { get; set; }
        public string Remark { get; set; }
    }

    }
