using CloudFileStorage.Common.Constants;
using CloudFileStorage.Common.Extensions;
using CloudFileStorage.FileStorageApi.CQRS.Commands;
using CloudFileStorage.FileStorageApi.CQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMediator.Core.Abstractions;

namespace CloudFileStorage.FileStorageApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FileStorageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileStorageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            var response = await _mediator.Send(new UploadFileCommand(file));
            return this.HandleResponse(response);
        }

        [HttpGet("download")]
        public async Task<IActionResult> Download([FromQuery] int fileId, [FromQuery] string fileName)
        {
            var response = await _mediator.Send(new DownloadFileQuery(fileId, fileName));

            if (!response.Success || response.Data == null)
                return this.HandleResponse(response);

            return File(response.Data, HttpContentTypes.OctetStream, fileName);
        }
    }
}
