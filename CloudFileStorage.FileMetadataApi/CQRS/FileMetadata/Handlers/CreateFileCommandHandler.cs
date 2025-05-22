using SMediator.Core.Abstractions;
using CloudFileStorage.FileMetadataApi.CQRS.FileMetadata.Commands;
using CloudFileStorage.FileMetadataApi.Services.Interfaces;
using CloudFileStorage.Common.Models;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileMetadata.Handlers
{
    public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, ServiceResponse<string>>
    {
        private readonly IFileMetadataService _fileService;

        public CreateFileCommandHandler(IFileMetadataService fileService)
        {
            _fileService = fileService;
        }

        public async Task<ServiceResponse<string>> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            return await _fileService.CreateFileAsync(request.Dto, request.OwnerId);
        }
    }
}
