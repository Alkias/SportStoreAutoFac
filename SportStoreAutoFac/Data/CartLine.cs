using System.ComponentModel.DataAnnotations;

namespace SportStoreAutoFac.Data
{
    public class CartLine:BaseEntity {
        //[Key]
        //public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}