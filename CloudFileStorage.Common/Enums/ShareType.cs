using System.ComponentModel.DataAnnotations;

namespace CloudFileStorage.Common.Enums
{
    public enum ShareType
    {
        [Display(Name = "Özel")]
        Private = 0,

        [Display(Name = "Herkese Açık")]
        Public = 1,

        [Display(Name = "Belirli Kişiler")]
        Specific = 2
    }

}
