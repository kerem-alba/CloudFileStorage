using SMediator.Core.Abstractions;
using CloudFileStorage.FileMetadataApi.CQRS.FileShareMetadata.Commands;
using CloudFileStorage.Common.Models;
using CloudFileStorage.FileMetadataApi.Services.Interfaces;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileShareMetadata.Handlers
{
    public class UpdateFileShareCommandHandler : IRequestHandler<UpdateFileShareCommand, ServiceResponse<string>>
    {
        private readonly IFileShareMetadataService _service;

        public UpdateFileShareCommandHandler(IFileShareMetadataService service)
        {
            _service = service;
        }

        public async Task<ServiceResponse<string>> Handle(UpdateFileShareCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateAsync(request.Id, request.Dto);
        }
    }
}
