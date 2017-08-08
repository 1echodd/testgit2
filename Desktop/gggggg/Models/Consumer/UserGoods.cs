using PetaPoco;

namespace DXzonghejiaofei.Models.Consumer
{
    [TableName("UserGoods")]
    [PrimaryKey("Id")]
    public class UserGoods
    {
        public int Id { set; get; }
        public string Gprs { set; get; }
        public string GoodsId { set; get; }
        public string GoodsName { set; get; }
        public string Scope { set; get; }
    }
}