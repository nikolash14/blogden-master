using MindfireSolutions.Models;
using System.Data.Entity;

namespace MindfireSolutions.DataAccess
{
    public class DAL : DbContext
    {
        /// <summary>
        /// Constructor for DAL and Database name
        /// </summary>
        public DAL() : base("BlogDenDb")
        {

        }
        /// <summary>
        /// Data Access Layer For Accessing Information by Entity Framework from Database 
        /// </summary>

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<BlogTopic> BlogTopics { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<BlogStatusCount> GetBlogStatusCount { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogComment>()
               .HasMany(x => x.SubComment)
                .WithOptional()
                .HasForeignKey(g => g.ParentId);
            modelBuilder.Entity<BlogTopic>()
              .HasMany(x => x.SubTopic)
               .WithOptional()
               .HasForeignKey(g => g.ParentId);

        }
    }
}