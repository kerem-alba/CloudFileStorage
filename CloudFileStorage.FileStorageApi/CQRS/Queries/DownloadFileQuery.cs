using CloudFileStorage.Common.Models;
using SMediator.Core.Abstractions;

namespace CloudFileStorage.FileStorageApi.CQRS.Queries
{
    public record DownloadFileQuery(int FileId, string FileName) : IRequest<ServiceResponse<byte[]>>;

}
