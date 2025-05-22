using SMediator.Core.Abstractions;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.Common.Models;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileMetadata.Queries
{
    public record GetAllFilesQuery(int OwnerId) : IRequest<ServiceResponse<List<FileMetadataDto>>>;
}
