using CloudFileStorage.Common.Enums;

namespace CloudFileStorage.FileMetadataApi.Models.DTOs
{
    public class FileShareDto
    {
        public int UserId { get; set; }
        public Permission Permission { get; set; }
    }
}
