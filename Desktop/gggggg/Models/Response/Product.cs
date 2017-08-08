using PetaPoco;

namespace DXzonghejiaofei.Models.Response
{
    [TableName("product")]
    [PrimaryKey("shopProductId")]
    public class Product
    {
        public string shopProductId { set; get; }
        public string shopProductName { set; get; }
    }
}
