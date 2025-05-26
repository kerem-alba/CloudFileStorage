using CloudFileStorage.Common.Enums;

namespace CloudFileStorage.UI.Models.DTOs;

public class CreateFileDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public IFormFile File { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public ShareType ShareType { get; set; } = ShareType.Private;
    public List<int>? UserIds { get; set; }
    public string Permission { get; set; } = "Read";
}
