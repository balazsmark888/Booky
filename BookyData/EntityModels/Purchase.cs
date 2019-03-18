using System;

namespace BookyData.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int TotalPrice { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
    }
}
