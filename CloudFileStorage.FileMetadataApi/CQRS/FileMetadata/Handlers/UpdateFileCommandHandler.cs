using SMediator.Core.Abstractions;
using CloudFileStorage.FileMetadataApi.CQRS.FileMetadata.Commands;
using CloudFileStorage.FileMetadataApi.Services.Interfaces;
using CloudFileStorage.Common.Models;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileMetadata.Handlers
{
    public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommand, ServiceResponse<string>>
    {
        private readonly IFileMetadataService _fileService;

        public UpdateFileCommandHandler(IFileMetadataService fileService)
        {
            _fileService = fileService;
        }

        public async Task<ServiceResponse<string>> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
        {
            return await _fileService.UpdateFileAsync(request.Id, request.UserId, request.Dto);
        }
    }
}
