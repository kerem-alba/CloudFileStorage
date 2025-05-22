using SMediator.Core.Abstractions;
using CloudFileStorage.Common.Models;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileShareMetadata.Commands
{
    public record DeleteFileShareCommand(int Id) : IRequest<ServiceResponse<string>>;
}
