using SupportApp.DB;
using SupportApp.DTOs.SupportDTOs;
using SupportApp.DTOs.UserDTOs;
using SupportApp.Entities;
using SupportApp.Repositories.Interfaces;
using System.Reflection.PortableExecutable;

namespace SupportApp.Repositories.Implementations
{
    public class SupportRepository : ISupportRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public SupportRepository(IDbConnectionFactory dbConnectionFactory)
        {
                _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<GetSupportDTO> CreateSupportAsync(CreateSupportDTO createSupportDTO, List<SupportImageDTO> uploadedFiles)
        {
            using var connection = _dbConnectionFactory.CreateMsSqlConnection();
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();

            using var command = _dbConnectionFactory.CreateProcedureCommand("SP_CREATE_SUPPORT", connection);

            command.Transaction = transaction;

            try
            {
                command.Parameters.AddWithValue("@UserId", createSupportDTO.UserId);
                command.Parameters.AddWithValue("@Title", createSupportDTO.Title);
                command.Parameters.AddWithValue("@Content", createSupportDTO.Content);

                using var reader = await command.ExecuteReaderAsync();


                if (!await reader.ReadAsync())
                {
                    throw new Exception("Support yaradılmadı.");
                }

                int supportId = reader.GetInt32(reader.GetOrdinal("ID"));
                await reader.CloseAsync();

                foreach (var image in uploadedFiles)
                {
                    using var imageCommand = _dbConnectionFactory.CreateProcedureCommand("SP_CREATE_SUPPORT_IMAGE", connection);


                    imageCommand.Parameters.AddWithValue("@SupportId", supportId);
                    imageCommand.Parameters.AddWithValue("@UploadFileId", image.UploadFileId);

                    await imageCommand.ExecuteNonQueryAsync();
                }

                await transaction.CommitAsync();

                return new GetSupportDTO
                {
                    UserId = createSupportDTO.UserId,
                    Title = createSupportDTO.Title,
                    Content = createSupportDTO.Content
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

        }

        public async Task<List<GetSupportDTO>> GetAllUsersSupportsAsync()
        {
            List<GetSupportDTO> supports = new List<GetSupportDTO>();

            using var connection = _dbConnectionFactory.CreateMsSqlConnection();
            await connection.OpenAsync();

            using var command = _dbConnectionFactory.CreateProcedureCommand("SP_GET_ALL_USERS_SUPPORTS", connection);

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var support = new GetSupportDTO
                {
                    UserId = reader.GetInt32(reader.GetOrdinal("USER_ID")),
                    RegNo = reader.GetInt32(reader.GetOrdinal("REG_NO")),
                    Title = reader.GetString(reader.GetOrdinal("TITLE")),
                    Content = reader.GetString(reader.GetOrdinal("CONTENT")),
                    IsSolved = reader.GetBoolean(reader.GetOrdinal("IS_SOLVED")),
                    Images = new List<GetSupportImagesDTO>()
                };

                var image = new GetSupportImagesDTO
                {
                    Url = reader.GetString(reader.GetOrdinal("URL"))
                };

                if (image != null)
                {
                    support.Images.Add(image);
                }

                supports.Add(support);
            }
            return supports;
        }

        public async Task<GetSupportDTO?> GetSupportsByUserIdAsync(int userId)
        {
            using var connection = _dbConnectionFactory.CreateMsSqlConnection();
            await connection.OpenAsync();

            using var command = _dbConnectionFactory.CreateProcedureCommand("SP_GET_SUPPORTS_BY_USER_ID", connection);

            command.Parameters.AddWithValue("@UserId", userId);

            using var reader = await command.ExecuteReaderAsync();

            GetSupportDTO supports = null;

            while (await reader.ReadAsync())
            {
                if (supports == null)
                {
                    var support = new GetSupportDTO
                    {
                        UserId = reader.GetInt32(reader.GetOrdinal("USER_ID")),
                        RegNo = reader.GetInt32(reader.GetOrdinal("REG_NO")),
                        Title = reader.GetString(reader.GetOrdinal("TITLE")),
                        Content = reader.GetString(reader.GetOrdinal("CONTENT")),
                        IsSolved = reader.GetBoolean(reader.GetOrdinal("IS_SOLVED")),
                        Images = new List<GetSupportImagesDTO>()
                    };
                }

                var image = new GetSupportImagesDTO
                {
                    Url = reader.GetString(reader.GetOrdinal("URL"))
                };

                supports.Images.Add(image);
            }
            return supports;
        }
    }
}
