using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.Common.Models;
using SMediator.Core.Abstractions;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileShareMetadata.Queries
{
    public class GetFileSharesByFileIdQuery : IRequest<ServiceResponse<List<FileShareDto>>>
    {
        public int FileMetadataId { get; }

        public GetFileSharesByFileIdQuery(int fileMetadataId)
        {
            FileMetadataId = fileMetadataId;
        }
    }
}
