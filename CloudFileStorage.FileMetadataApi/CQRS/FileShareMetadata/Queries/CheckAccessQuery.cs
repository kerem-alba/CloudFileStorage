using CloudFileStorage.Common.Models;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using SMediator.Core.Abstractions;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileShareMetadata.Queries
{
    public record CheckAccessQuery(int UserId, int FileMetadataId) : IRequest<ServiceResponse<HasAccessResultDto>>;

}
