using SupportApp.DTOs.SupportDTOs;

namespace SupportApp.DTOs.UserDTOs
{
    public class GetSupportDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RegNo { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public bool IsSolved { get; set; }
        public DateTime? SolvedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<GetSupportImagesDTO> Images { get; set; } = new();
    }
}
