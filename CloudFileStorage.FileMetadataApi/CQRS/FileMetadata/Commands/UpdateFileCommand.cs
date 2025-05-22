using SMediator.Core.Abstractions;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.Common.Models;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileMetadata.Commands
{
    public record UpdateFileCommand(int Id, int OwnerId, UpdateFileDto Dto) : IRequest<ServiceResponse<string>>;
}
