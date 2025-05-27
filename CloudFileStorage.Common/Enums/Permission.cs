using System.ComponentModel.DataAnnotations;

namespace CloudFileStorage.Common.Enums
{
    public enum Permission
    {
        [Display(Name = "Sadece Okuma")]
        ReadOnly,

        [Display(Name = "Düzenleme")]
        Edit
    }
}
