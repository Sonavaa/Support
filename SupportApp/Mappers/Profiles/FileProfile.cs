using AutoMapper;
using SupportApp.DTOs.FileUploadDTOs;
using SupportApp.Models;

namespace SupportApp.Mappers.Profiles
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<FileUpload, FileUploadDTO>();
            CreateMap<FileUploadDTO, FileUpload>();
        }
    }
}
