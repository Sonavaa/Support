using AutoMapper;
using SupportApp.DTOs.FileUploadDTOs;
using SupportApp.Models;
using SupportApp.Repositories.Interfaces;
using SupportApp.Services.Interfaces;

namespace SupportApp.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IMapper _mapper;
        private readonly IFileUploadRepository _fileUploadRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _environment;
        public FileUploadService(IMapper mapper,
                                 IFileUploadRepository fileUploadRepository, 
                                 IHttpContextAccessor httpContextAccessor, 
                                 IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _fileUploadRepository = fileUploadRepository;
            _httpContextAccessor = httpContextAccessor;
            _environment = environment;
        }
        public async Task<FileUploadResponseDTO> FileUploadAsync(FileUploadDTO fileUploadDTO)
        {
            var file = fileUploadDTO.File;

            string extension = Path.GetExtension(file.FileName).ToLowerInvariant().Trim();

            string fileName = file.FileName;
            string GuidFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";

            var webRootPath = _environment.WebRootPath;

            if (string.IsNullOrWhiteSpace(webRootPath))
            {
                webRootPath = Path.Combine(_environment.ContentRootPath,"wwwroot");
            }

            var path = Path.Combine(webRootPath, "Uploads");
            Directory.CreateDirectory(path);

            string filePath = Path.Combine(path, GuidFileName);

            var request = _httpContextAccessor.HttpContext.Request;

            string rootUrl = $"{request.Scheme}://{request.Host}";

            string url = $"/Uploads/{GuidFileName}";


            using FileStream stream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(stream);

            var fileUpload = new FileUpload();


            fileUpload.FileName = file.FileName;
            fileUpload.GuidFileName = GuidFileName;
            fileUpload.FilePath = filePath;
            fileUpload.RootUrl = rootUrl;
            fileUpload.Url = url;
            fileUpload.FileSize = file.Length;
            fileUpload.ContentType = file.ContentType;

            int uploadFileId = await _fileUploadRepository.UploadFileAsync(fileUpload);

            return new FileUploadResponseDTO
            {
                Id = uploadFileId,
                FileName = GuidFileName,
            };
        }
    }
}
