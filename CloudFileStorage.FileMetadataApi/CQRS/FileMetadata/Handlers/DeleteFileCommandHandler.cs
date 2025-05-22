using SMediator.Core.Abstractions;
using CloudFileStorage.FileMetadataApi.CQRS.FileMetadata.Commands;
using CloudFileStorage.FileMetadataApi.Services.Interfaces;
using CloudFileStorage.Common.Models;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileMetadata.Handlers
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, ServiceResponse<string>>
    {
        private readonly IFileMetadataService _fileService;

        public DeleteFileCommandHandler(IFileMetadataService fileService)
        {
            _fileService = fileService;
        }

        public async Task<ServiceResponse<string>> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            return await _fileService.DeleteFileAsync(request.Id, request.OwnerId);
        }
    }
}
