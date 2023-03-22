namespace file_uploader.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; }
        public string FileLocation { get; set; }
        public int Version { get; set; }
    }
}
