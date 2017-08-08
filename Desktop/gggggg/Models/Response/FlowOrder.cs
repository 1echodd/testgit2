using PetaPoco;

namespace DXzonghejiaofei.Models.Response
{
    [TableName("FlowOrder")]
    [PrimaryKey("Id")]
    public class FlowOrder:TopOrder
    {
        /// <summary>
        /// 运营商
        /// </summary>
        public string OpType { set; get; }
        /// <summary>
        /// 使用范围描述
        /// </summary>
        public string Location { set; get; }
        /// <summary>
        /// 使用范围代码
        /// 0=全国
        /// 1=省内
        /// </summary>
        public int Scope { get; set; }
        /// <summary>
        /// 生效日期
        /// 0=立即生效
        /// </summary>
        public int EffectDate { get; set; }
    }
}