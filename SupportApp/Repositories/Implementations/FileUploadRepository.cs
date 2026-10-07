using SupportApp.DB;
using SupportApp.Models;
using SupportApp.Repositories.Interfaces;

namespace SupportApp.Repositories.Implementations
{
    public class FileUploadRepository : IFileUploadRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public FileUploadRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }


        public async Task<int> UploadFileAsync(FileUpload fileUpload)
        {
            using var connection = _dbConnectionFactory.CreateMsSqlConnection();
            await connection.OpenAsync();

            using var command = _dbConnectionFactory.CreateProcedureCommand("SP_FILE_UPLOAD", connection);

            command.Parameters.AddWithValue("@FileName", fileUpload.FileName);
            command.Parameters.AddWithValue("@GuidFileName", fileUpload.GuidFileName);
            command.Parameters.AddWithValue("@FilePath", fileUpload.FilePath);
            command.Parameters.AddWithValue("@RootUrl", fileUpload.RootUrl);
            command.Parameters.AddWithValue("@Url", fileUpload.Url);
            command.Parameters.AddWithValue("@FileSize", fileUpload.FileSize);
            command.Parameters.AddWithValue("@ContentType", fileUpload.ContentType);


             var result = await command.ExecuteScalarAsync();

             return Convert.ToInt32(result);
        }

    }
}
