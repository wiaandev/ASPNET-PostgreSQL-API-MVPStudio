using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvp_studio_api.Models
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        //add foreign key to roles table for type of role

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Gender { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? Birth_Date { get; set; }
    }
}
