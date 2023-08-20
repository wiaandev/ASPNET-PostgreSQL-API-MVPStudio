using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvp_studio_api.Models
{
    [Table("Project")]
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [Required]
        public string Project_Name { get; set;}

        [Required]
        public string Description { get; set; }

        [Required]
        public int Project_Time { get; set; }

        [Required]
        public string Project_Type { get; set; }

        [Required]
        public int Project_Cost { get; set; }

        public int Amount_Paid { get; set; } = 0;
    }
}
