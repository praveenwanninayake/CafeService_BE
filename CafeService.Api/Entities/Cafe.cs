using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeService.Api.Entities
{
    [Table("M_Cafe")]
    public class Cafe
    {

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        public string? Logo { get; set; } // Optional field

        [Required]
        [MaxLength(150)]
        public string Location { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        [Required]
        public DateTime ModifiedDateTime { get; set; } = DateTime.Now;

        public ICollection<Employee> Employees { get; set; }

    }
}
