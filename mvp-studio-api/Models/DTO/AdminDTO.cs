using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mvp_studio_api.Models.DTO
{
    public class AdminDTO
    {
        public int Id { get; set; }

        public string RoleId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Birth_Date { get; set; }
    }
}
