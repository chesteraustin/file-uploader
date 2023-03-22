namespace file_uploader.Models
{
    public class UserFileModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileLocation { get; set; }
        public int Version { get; set; }
    }
}
