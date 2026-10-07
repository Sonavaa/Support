namespace SupportApp.Models
{
    public class FileUpload
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; } = null!;
        public string GuidFileName { get; set; }
        public string FilePath { get; set; } = null!;
        public string RootUrl { get; set; } = null!;
        public string Url { get; set; } = null!;
        public long FileSize { get; set; }
        public string ContentType { get; set; } = null!;
    }
}
