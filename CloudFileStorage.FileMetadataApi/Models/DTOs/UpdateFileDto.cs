﻿namespace CloudFileStorage.FileMetadataApi.Models.DTOs
{
    public class UpdateFileDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
