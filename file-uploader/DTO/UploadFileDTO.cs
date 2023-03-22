namespace file_uploader.DTO
{
    public class UploadFileDTO
    {
        public string userName { get; set; }
        public string password { get; set; }
        public IFormFileCollection userFiles { get; set; }
    }
}
