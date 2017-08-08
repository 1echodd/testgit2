using PetaPoco;

namespace DXzonghejiaofei.Models.Consumer
{
    [TableName("UserSale")]
    [PrimaryKey("Id")]
    public class UserSale
    {
        public long Id { set; get; }
        public int GoodsId { set; get; }
        public decimal TopUpSum { set; get; }
    }
}