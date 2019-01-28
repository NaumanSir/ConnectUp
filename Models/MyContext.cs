using Microsoft.EntityFrameworkCore;

namespace firstbelt.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Participant> Participants { get; set; }
    }
}