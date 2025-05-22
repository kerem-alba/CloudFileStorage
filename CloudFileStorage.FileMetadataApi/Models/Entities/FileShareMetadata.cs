using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudFileStorage.FileMetadataApi.Models.Entities
{
    public class FileShareMetadata
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FileMetadataId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Permission { get; set; } = null!;

        [ForeignKey("FileMetadataId")]
        public FileMetadata FileMetadata { get; set; } = null!;
    }
}
