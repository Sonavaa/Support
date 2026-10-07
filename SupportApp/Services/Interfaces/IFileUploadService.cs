using SupportApp.DTOs.FileUploadDTOs;

namespace SupportApp.Services.Interfaces
{
    public interface IFileUploadService
    {
        Task<FileUploadResponseDTO> FileUploadAsync(FileUploadDTO fileUploadDTO);
    }
}
    