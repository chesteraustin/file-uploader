using Microsoft.EntityFrameworkCore;
namespace file_uploader.Models
{
    public class FileUploaderContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite(@"Data Source=Database\FileUploader.db");
    }
}
