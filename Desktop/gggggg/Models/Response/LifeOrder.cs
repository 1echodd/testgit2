using PetaPoco;

namespace DXzonghejiaofei.Models.Response
{
    [TableName("LifeOrder")]
    [PrimaryKey("Id")]
    public class LifeOrder:TopOrder
    {
        /// <summary>
        /// 生活缴费类型
        /// </summary>
        public string OpType { set; get; }
        /// <summary>
        /// 充值账户名称
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
    }
}