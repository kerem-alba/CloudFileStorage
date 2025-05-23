using CloudFileStorage.Common.Models;
using SMediator.Core.Abstractions;

namespace CloudFileStorage.FileStorageApi.CQRS.Queries
{
    public class DownloadFileQuery : IRequest<ServiceResponse<byte[]>>
    {
        public string FileName { get; set; }

        public DownloadFileQuery(string fileName)
        {
            FileName = fileName;
        }
    }
}
