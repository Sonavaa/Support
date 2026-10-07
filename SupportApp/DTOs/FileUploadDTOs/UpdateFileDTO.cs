namespace SupportApp.DTOs.FileUploadDTOs
{
    public class UpdateProductFileDTO
    {
        public int UploadFileId { get; set; }
        public IFormFile? File { get; set; }
        public bool IsMain { get; set; }
    }
}
