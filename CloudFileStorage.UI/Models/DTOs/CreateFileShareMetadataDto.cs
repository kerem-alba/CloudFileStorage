namespace CloudFileStorage.UI.Models.DTOs
{
    public class CreateFileShareMetadataDto
    {
        public int FileMetadataId { get; set; }
        public int UserId { get; set; }
        public string Permission { get; set; } = null!;
    }
}
