
using Microsoft.AspNetCore.Identity;
using SupportApp.DTOs.FileUploadDTOs;
using SupportApp.DTOs.SupportDTOs;
using SupportApp.DTOs.UserDTOs;
using SupportApp.Entities;
using SupportApp.Repositories.Interfaces;
using SupportApp.Services.Interfaces;

namespace SupportApp.Services.Implementations
{
    public class SupportService : ISupportService
    {
        private readonly ISupportRepository _supportRepository;
        private readonly IFileUploadService _fileUploadService;

        public SupportService(ISupportRepository supportRepository, IFileUploadService fileUploadService)
        {
            _supportRepository = supportRepository;
            _fileUploadService = fileUploadService;
        }

        public async Task<GetSupportDTO> CreateSupportAsync(CreateSupportDTO createSupportDTO)
        {
            var uploadedFiles = new List<SupportImageDTO>();

            if (createSupportDTO.Images != null && createSupportDTO.Images.Any())
            {
                foreach (var image in createSupportDTO.Images) 
                {
                    var UploadedFile = await _fileUploadService.FileUploadAsync(
                            new FileUploadDTO
                            {
                                File = image
                            });

                    uploadedFiles.Add(new SupportImageDTO
                    {
                        UploadFileId = UploadedFile.Id
                    });
                } 
            }

            return await _supportRepository.CreateSupportAsync(createSupportDTO, uploadedFiles);
        }

        public Task<List<Support>> GetAllUsersSupportsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetSupportDTO> GetSupportsByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
