
namespace mvp_studio_api.Models.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }

        public string ClienName { get; set; } = string.Empty;

        public string Project_Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Project_Time { get; set; }

        public string Project_Type { get; set; } = string.Empty;

        public int Project_Cost { get; set; }

        public int Amount_Paid { get; set; } = 0;
    }
}
