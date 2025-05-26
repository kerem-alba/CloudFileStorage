namespace CloudFileStorage.UI.Models.DTOs
{
    public class FileMetadataDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime UploadDate { get; set; }
        public int OwnerId { get; set; }
        public string? OwnerName { get; set; }
        public string FileName { get; set; } = null!;

    }
}
