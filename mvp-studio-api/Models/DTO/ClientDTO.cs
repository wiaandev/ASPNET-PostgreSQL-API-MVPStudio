using System.ComponentModel.DataAnnotations.Schema;

namespace mvp_studio_api.Models.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public string Email { get; set; }

        public int ClientTypeId { get; set; }
    }
}
