﻿using System.ComponentModel.DataAnnotations;

namespace CloudFileStorage.UI.Models.DTOs
{
    public class RegisterUserDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
