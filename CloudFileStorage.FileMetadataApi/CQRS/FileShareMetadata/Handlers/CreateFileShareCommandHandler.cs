using SMediator.Core.Abstractions;
using CloudFileStorage.FileMetadataApi.CQRS.FileShareMetadata.Commands;
using CloudFileStorage.Common.Models;
using CloudFileStorage.FileMetadataApi.Services.Interfaces;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileShareMetadata.Handlers
{
    public class CreateFileShareCommandHandler : IRequestHandler<CreateFileShareCommand, ServiceResponse<string>>
    {
        private readonly IFileShareMetadataService _service;

        public CreateFileShareCommandHandler(IFileShareMetadataService service)
        {
            _service = service;
        }

        public async Task<ServiceResponse<string>> Handle(CreateFileShareCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateAsync(request.Dto);
        }
    }
}
