using CloudFileStorage.Common.Models;
using CloudFileStorage.FileStorageApi.CQRS.Queries;
using CloudFileStorage.FileStorageApi.Services;
using SMediator.Core.Abstractions;

public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, ServiceResponse<byte[]>>
{
    private readonly IFileStorageService _fileStorageService;

    public DownloadFileQueryHandler(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    public Task<ServiceResponse<byte[]>> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
    {
        return _fileStorageService.DownloadFileAsync(request.FileName);
    }
}
