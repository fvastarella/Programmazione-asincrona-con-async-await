using System.ComponentModel.DataAnnotations;

namespace asyncawait.Data
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}