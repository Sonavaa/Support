using SupportApp.Models;

namespace SupportApp.Repositories.Interfaces
{
    public interface IFileUploadRepository 
    {
        Task<int> UploadFileAsync(FileUpload fileUpload);
    }
}
