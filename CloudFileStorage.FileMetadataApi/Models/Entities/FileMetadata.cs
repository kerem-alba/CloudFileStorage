﻿using CloudFileStorage.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace CloudFileStorage.FileMetadataApi.Models.Entities
{
    public class FileMetadata
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string FileName { get; set; } = null!;

        [Required]
        public int OwnerId { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }
        public bool IsPublic { get; set; } = false;
        public Permission Permission { get; set; } = Permission.Edit;
    }
}
