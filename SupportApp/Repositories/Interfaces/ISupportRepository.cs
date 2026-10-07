using SupportApp.DTOs.SupportDTOs;
using SupportApp.DTOs.UserDTOs;
using SupportApp.Entities;

namespace SupportApp.Repositories.Interfaces
{
    public interface ISupportRepository
    {
        Task<GetSupportDTO> CreateSupportAsync(CreateSupportDTO createSupportDTO, List<SupportImageDTO> uploadedFiles);
        Task<List<GetSupportDTO>> GetAllUsersSupportsAsync();
        Task<GetSupportDTO?> GetSupportsByUserIdAsync(int userId);
    }
}
