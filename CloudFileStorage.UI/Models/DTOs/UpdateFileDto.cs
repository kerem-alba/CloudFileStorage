namespace CloudFileStorage.UI.Models.DTOs
{
    public class UpdateFileDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
