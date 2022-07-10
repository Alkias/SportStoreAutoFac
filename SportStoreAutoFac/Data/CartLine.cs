namespace SportStoreAutoFac.Data
{
    public class CartLine : BaseEntity
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}