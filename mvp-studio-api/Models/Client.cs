﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvp_studio_api.Models
{
    [Table("Clients")]
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string ImgUrl { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [ForeignKey(nameof(ClientType))]
        public int ClientTypeId { get; set; }
    }
}
