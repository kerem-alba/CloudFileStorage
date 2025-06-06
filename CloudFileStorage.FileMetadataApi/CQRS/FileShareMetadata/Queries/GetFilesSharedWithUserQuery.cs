using SMediator.Core.Abstractions;
using CloudFileStorage.Common.Models;
using CloudFileStorage.FileMetadataApi.Models.DTOs;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileShareMetadata.Queries
{
    public record GetFilesSharedWithUserQuery(int UserId) : IRequest<ServiceResponse<List<FileMetadataDto>>>;
}
