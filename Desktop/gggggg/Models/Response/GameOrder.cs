using PetaPoco;

namespace DXzonghejiaofei.Models.Response
{
    [TableName("GameOrder")]
    [PrimaryKey("Id")]
    public class GameOrder:TopOrder
    {
        /// <summary>
        /// 充值IP
        /// </summary>
        public string UserIp { get; set; }
        /// <summary>
        /// 充值类型
        /// </summary>
        public string ChargeType { get; set; }
        /// <summary>
        /// 充值大区
        /// </summary>
        public string ChargeRegion { get; set; }
        /// <summary>
        /// 充值大区
        /// </summary>
        public string ChargeServer { get; set; }
        /// <summary>
        /// 充值数量
        /// </summary>
        public int Number { get; set; }
    }
}