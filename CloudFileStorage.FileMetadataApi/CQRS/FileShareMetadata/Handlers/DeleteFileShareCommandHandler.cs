using SMediator.Core.Abstractions;
using CloudFileStorage.FileMetadataApi.CQRS.FileShareMetadata.Commands;
using CloudFileStorage.Common.Models;
using CloudFileStorage.FileMetadataApi.Services.Interfaces;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileShareMetadata.Handlers
{
    public class DeleteFileShareCommandHandler : IRequestHandler<DeleteFileShareCommand, ServiceResponse<string>>
    {
        private readonly IFileShareMetadataService _service;

        public DeleteFileShareCommandHandler(IFileShareMetadataService service)
        {
            _service = service;
        }

        public async Task<ServiceResponse<string>> Handle(DeleteFileShareCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteAsync(request.Id);
        }
    }
}
