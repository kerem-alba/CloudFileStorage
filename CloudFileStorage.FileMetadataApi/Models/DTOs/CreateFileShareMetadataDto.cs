using CloudFileStorage.Common.Enums;

namespace CloudFileStorage.FileMetadataApi.Models.DTOs
{
    public class CreateFileShareMetadataDto
    {
        public int FileMetadataId { get; set; }
        public int UserId { get; set; }
        public Permission Permission { get; set; }
    }

}
