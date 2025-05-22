using SMediator.Core.Abstractions;
using CloudFileStorage.Common.Models;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileMetadata.Commands
{
    public record DeleteFileCommand(int Id, int OwnerId) : IRequest<ServiceResponse<string>>;
}
