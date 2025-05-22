using CloudFileStorage.Common.Models;
using SMediator.Core.Abstractions;

namespace CloudFileStorage.FileStorageApi.CQRS.Commands
{
    public class UploadFileCommand : IRequest<ServiceResponse<string>>
    {
        public IFormFile File { get; set; }

        public UploadFileCommand(IFormFile file)
        {
            File = file;
        }
    }
}
