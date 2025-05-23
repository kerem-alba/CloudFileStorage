using CloudFileStorage.Common.Constants;
using CloudFileStorage.Common.Models;
using CloudFileStorage.FileStorageApi.Services;

public class FileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _env;

    public FileStorageService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<ServiceResponse<string>> UploadFileAsync(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = ResponseMessages.FileNotSelected,
                    StatusCode = 400
                };
            }

            var uploadsPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsPath, uniqueFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return new ServiceResponse<string>
            {
                Data = uniqueFileName,
                Message = ResponseMessages.FileUploadSuccess,
                StatusCode = 201
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<string>
            {
                Success = false,
                Message = string.Format(ResponseMessages.FileUploadFail, ex.Message),
                StatusCode = 500
            };
        }
    }
    public async Task<ServiceResponse<byte[]>> DownloadFileAsync(string fileName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return new ServiceResponse<byte[]>
                {
                    Success = false,
                    Message = ResponseMessages.FileNameRequired,
                    StatusCode = 400
                };
            }

            var uploadsPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");
            var filePath = Path.Combine(uploadsPath, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return new ServiceResponse<byte[]>
                {
                    Success = false,
                    Message = ResponseMessages.FileNotFound,
                    StatusCode = 404
                };
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return new ServiceResponse<byte[]>
            {
                Data = fileBytes,
                Message = ResponseMessages.FileDownloadSuccess,
                StatusCode = 200
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<byte[]>
            {
                Success = false,
                Message = string.Format(ResponseMessages.FileDownloadFail, ex.Message),
                StatusCode = 500
            };
        }
    }

}
