using SMediator.Core.Abstractions;
using CloudFileStorage.FileMetadataApi.CQRS.FileShareMetadata.Queries;
using CloudFileStorage.Common.Models;
using CloudFileStorage.FileMetadataApi.Services.Interfaces;
using CloudFileStorage.FileMetadataApi.Models.DTOs;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileShareMetadata.Handlers
{
    public class GetFilesSharedWithUserQueryHandler : IRequestHandler<GetFilesSharedWithUserQuery, ServiceResponse<List<FileMetadataDto>>>
    {
        private readonly IFileShareMetadataService _service;

        public GetFilesSharedWithUserQueryHandler(IFileShareMetadataService service)
        {
            _service = service;
        }

        public async Task<ServiceResponse<List<FileMetadataDto>>> Handle(GetFilesSharedWithUserQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetFilesSharedWithUserAsync(request.UserId);
        }
    }

}
