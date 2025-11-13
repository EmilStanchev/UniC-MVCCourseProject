using FirstProjectExampleMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstProjectExampleMVC.Data
{
    public class ApplictionDbContext:DbContext
    {
        public ApplictionDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
    }
}
