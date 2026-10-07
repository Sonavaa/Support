namespace SupportApp.DTOs.SupportDTOs
{
    public class CreateSupportDTO
    {
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public List<IFormFile>? Images { get; set; }
    }
}
