using PetaPoco;

namespace DXzonghejiaofei.Models
{
    [TableName("SystemConfig")]
    [PrimaryKey("Id")]
    public class SystemConfig
    {
        public long Id { set; get; }

        /// <summary>
        /// 平台地址
        /// </summary>
        public string ServerUrl { set; get; }

        /// <summary>
        /// 渠道商编号
        /// </summary>
        public string ChannelId { set; get; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string Secretkey { set; get; }
    }
}