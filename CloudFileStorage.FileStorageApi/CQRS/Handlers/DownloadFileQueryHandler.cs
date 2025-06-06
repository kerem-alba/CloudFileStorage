using CloudFileStorage.Common.Models;
using CloudFileStorage.FileStorageApi.CQRS.Queries;
using CloudFileStorage.FileStorageApi.Services;
using CloudFileStorage.Common.Extensions;
using SMediator.Core.Abstractions;

public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, ServiceResponse<byte[]>>
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public DownloadFileQueryHandler(IFileStorageService fileStorageService, IHttpContextAccessor httpContextAccessor)
    {
        _fileStorageService = fileStorageService;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<ServiceResponse<byte[]>> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User.GetUserId();
        return _fileStorageService.DownloadFileAsync(request.FileId, request.FileName, userId);
    }
}
