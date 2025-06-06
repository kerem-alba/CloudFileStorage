namespace CloudFileStorage.FileStorageApi.Models.DTOs
{
    public class FileAccessResultDto
    {
        public bool HasAccess { get; set; }
        public string? Permission { get; set; }
    }

}
