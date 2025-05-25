﻿namespace CloudFileStorage.FileMetadataApi.Models.DTOs
{
    public class FileMetadataDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int OwnerId { get; set; }
        public DateTime UploadDate { get; set; }
        public string FileName { get; set; } = null!;
    }
}
