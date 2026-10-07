
using SupportApp.DTOs.SupportDTOs;
using SupportApp.DTOs.UserDTOs;
using SupportApp.Entities;

namespace SupportApp.Services.Interfaces
{
    public interface ISupportService
    {
        Task<GetSupportDTO> CreateSupportAsync(CreateSupportDTO createSupportDTO);
        Task<List<Support>> GetAllUsersSupportsAsync();
        Task<GetSupportDTO> GetSupportsByUserIdAsync(int userId);
    }
}
