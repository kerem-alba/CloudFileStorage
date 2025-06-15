using CloudFileStorage.Common.Models;
using CloudFileStorage.FileStorageApi.CQRS.Commands;
using CloudFileStorage.FileStorageApi.Services;
using SMediator.Core.Abstractions;

namespace CloudFileStorage.FileStorageApi.CQRS.Handlers;

public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, ServiceResponse<string>>
{
    private readonly IFileStorageService _fileStorageService;

    public UploadFileCommandHandler(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    public Task<ServiceResponse<string>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        return _fileStorageService.UploadFileAsync(request.File);
    }
}