using PetaPoco;
using System;

namespace DXzonghejiaofei.Models.Response
{

    [TableName("TelOrder")]
    [PrimaryKey("Id")]
    public class TelOrder:TopOrder
    {
        /// <summary>
        /// 运营商
        /// </summary>
        public string OpType { set; get; }
        /// <summary>
        /// 归属地
        /// </summary>
        public string Location { set; get; }
       
      //  public DateTime QueryTime { set; get; } 
    }
}