using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using LectioService.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LectioService
{
    public class LectioContext : IdentityDbContext<ApplicationUser>
    {
        public LectioContext()
            : base("Lectio", throwIfV1Schema: false)
        {
        }

        public static LectioContext Create()
        {
            return new LectioContext();
        }

        /// <summary>
        /// Use fluent api here to make changes to database
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        /////////////////////
        // Create table names here, for example:
        // public DbSet<ApplicationUser> Users { get; set; }
        // public DbSet<Comment> Comments { get; set; }
        /////////////////////

        #region Database Tables

        public DbSet<Video> Videos { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Course> Courses { get; set; } 
        
        #endregion
    }
}
