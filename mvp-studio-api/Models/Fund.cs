using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvp_studio_api.Models
{
    [Table("Fund")]
    public class Fund
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Fund_Type { get; set; }

        public int Amount { get; set; }
    }
}
