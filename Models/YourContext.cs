using spaceship.Models;
using Microsoft.EntityFrameworkCore;
 
namespace spaceship.Models
{
    public class YourContext : DbContext
    {
        public YourContext(DbContextOptions<YourContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Comment_Like> comment_likes { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<Post_Like> post_likes { get; set; }

    }
}