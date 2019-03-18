using BookyData.Models;
using Microsoft.EntityFrameworkCore;

namespace BookyData
{
    public class BookyContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseRecord> PurchaseRecords { get; set; }

        public BookyContext(DbContextOptions options) : base(options)
        {

        }
    }
}