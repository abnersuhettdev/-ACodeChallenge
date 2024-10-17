using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;
namespace SchoolManagement.Data
{
    public class DataBaseContent : DbContext
    {
        public DataBaseContent(DbContextOptions<DataBaseContent> options) :
            base(options)
        { }


        public DbSet<StudentModel> Students{ get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContatoMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
