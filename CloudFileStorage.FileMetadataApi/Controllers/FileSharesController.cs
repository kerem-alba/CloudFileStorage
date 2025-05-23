﻿using CloudFileStorage.Common.Extensions;
using CloudFileStorage.FileMetadataApi.CQRS.FileShareMetadata.Commands;
using CloudFileStorage.FileMetadataApi.CQRS.FileShareMetadata.Queries;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMediator.Core.Abstractions;

namespace CloudFileStorage.FileMetadataApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FileSharesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileSharesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("shared-with-me")]
        public async Task<IActionResult> GetFilesSharedWithMe()
        {
            int userId = User.GetUserId();
            var response = await _mediator.Send(new GetFilesSharedWithUserQuery(userId));
            return this.HandleResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> ShareFile([FromBody] CreateFileShareMetadataDto dto)
        {
            var response = await _mediator.Send(new CreateFileShareCommand(dto));
            return this.HandleResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShare(int id, [FromBody] UpdateFileShareMetadataDto dto)
        {
            var response = await _mediator.Send(new UpdateFileShareCommand(id, dto));
            return this.HandleResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveShare(int id)
        {
            var response = await _mediator.Send(new DeleteFileShareCommand(id));
            return this.HandleResponse(response);
        }
    }
}
