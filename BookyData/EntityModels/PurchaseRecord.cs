namespace BookyData.Models
{
    public class PurchaseRecord
    {
        public int Id { get; set; }
        public Purchase Purchase { get; set; }
        public Book Book { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
    }
}
