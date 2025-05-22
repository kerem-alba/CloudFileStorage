using SMediator.Core.Abstractions;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.Common.Models;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileShareMetadata.Commands
{
    public record UpdateFileShareCommand(int Id, UpdateFileShareMetadataDto Dto) : IRequest<ServiceResponse<string>>;
}
