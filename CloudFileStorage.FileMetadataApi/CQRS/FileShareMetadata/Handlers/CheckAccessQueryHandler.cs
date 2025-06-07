using CloudFileStorage.Common.Models;
using SMediator.Core.Abstractions;
using CloudFileStorage.FileMetadataApi.CQRS.FileShareMetadata.Queries;
using CloudFileStorage.FileMetadataApi.Services.Interfaces;
using CloudFileStorage.FileMetadataApi.Models.DTOs;


namespace CloudFileStorage.FileMetadataApi.CQRS.FileShareMetadata.Handlers
{
    public class CheckAccessQueryHandler : IRequestHandler<CheckAccessQuery, ServiceResponse<HasAccessResultDto>>
    {
        private readonly IFileShareMetadataService _fileShareService;

        public CheckAccessQueryHandler(IFileShareMetadataService fileShareService)
        {
            _fileShareService = fileShareService;
        }

        public async Task<ServiceResponse<HasAccessResultDto>> Handle(CheckAccessQuery request, CancellationToken cancellationToken)
        {
            return await _fileShareService.GetAccessInfoAsync(request.UserId, request.FileMetadataId);
        }
    }
}
