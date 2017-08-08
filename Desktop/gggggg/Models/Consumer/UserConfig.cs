using PetaPoco;

namespace DXzonghejiaofei.Models.Consumer
{
    [TableName("UserConfig")]
    [PrimaryKey("Id")]
    public class UserConfig
    {
        public long Id { set; get; }
        public string UserName { set; get; }
        public string UserKey { set; get; }
        public string UserPayKey { set; get; }
        public string UserUrl { get; set; }
    }
}