using Microsoft.EntityFrameworkCore;
namespace file_uploader.Models
{
    public class FileUploaderContext : DbContext
    {
        public FileUploaderContext( DbContextOptions<FileUploaderContext> options) : base(options)
        {
            
        }
        public DbSet<UserModel> Users => Set<UserModel>();
    }
}
