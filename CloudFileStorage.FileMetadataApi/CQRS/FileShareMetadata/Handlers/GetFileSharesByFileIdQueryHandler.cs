using CloudFileStorage.FileMetadataApi.CQRS.FileShareMetadata.Queries;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.FileMetadataApi.Services.Interfaces;
using SMediator.Core.Abstractions;
using CloudFileStorage.Common.Models;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileShareMetadata.Handlers
{
    public class GetFileSharesByFileIdQueryHandler : IRequestHandler<GetFileSharesByFileIdQuery, ServiceResponse<List<FileShareDto>>>
    {
        private readonly IFileShareMetadataService _service;

        public GetFileSharesByFileIdQueryHandler(IFileShareMetadataService service)
        {
            _service = service;
        }

        public async Task<ServiceResponse<List<FileShareDto>>> Handle(GetFileSharesByFileIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetFileSharesByFileIdAsync(request.FileMetadataId);
        }

    }
}
