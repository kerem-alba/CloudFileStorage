namespace CloudFileStorage.FileMetadataApi.Models.DTOs
{
    public class CreateFileShareMetadataDto
    {
        public int FileMetadataId { get; set; }
        public List<int> UserIds { get; set; } = new();
        public string Permission { get; set; } = null!;
    }

}
