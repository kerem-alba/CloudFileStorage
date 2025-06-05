using SMediator.Core.Abstractions;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.Common.Models;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileMetadata.Commands
{
    public record CreateFileCommand(CreateFileDto Dto, int OwnerId) : IRequest<ServiceResponse<FileMetadataDto>>;
}
