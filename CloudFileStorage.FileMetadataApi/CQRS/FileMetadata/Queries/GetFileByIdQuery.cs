using SMediator.Core.Abstractions;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.Common.Models;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileMetadata.Queries
{
    public record GetFileByIdQuery(int Id, int OwnerId) : IRequest<ServiceResponse<FileMetadataDto?>>;
}
