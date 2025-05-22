namespace CloudFileStorage.FileMetadataApi.Models.DTOs
{
    public class CreateFileDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
