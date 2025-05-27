using CloudFileStorage.Common.Models;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using SMediator.Core.Abstractions;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileMetadata.Queries
{
    public record GetAccessibleFileQuery(int FileId, int UserId) : IRequest<ServiceResponse<FileMetadataDto>>;

}
