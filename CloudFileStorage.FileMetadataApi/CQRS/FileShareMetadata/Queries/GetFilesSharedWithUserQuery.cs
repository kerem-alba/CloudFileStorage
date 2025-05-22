using SMediator.Core.Abstractions;
using CloudFileStorage.Common.Models;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileShareMetadata.Queries
{
    public record GetFilesSharedWithUserQuery(int UserId) : IRequest<ServiceResponse<List<int>>>;
}
