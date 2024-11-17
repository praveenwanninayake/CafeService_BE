using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeService.Api.Models
{
    public class CafeModel
    {
        public Guid Id { get; set; }

    }
    public class CafeViewModel : CafeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Logo { get; set; }
        public string Location { get; set; }
        public int EmployeeCount { get; set; }
    }

    public class CafeInsertModel 
    {       
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        public IFormFile? Logo { get; set; } // Optional field

        [Required]
        [MaxLength(150)]
        public string Location { get; set; }

    }
    public class CafeEditModel : CafeModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        public IFormFile? Logo { get; set; } // Optional field

        [Required]
        [MaxLength(150)]
        public string Location { get; set; }

    }

    public class CafeDeleteModel : CafeModel
    {
        [Required]
        public Guid Id { get; set; }
    }
    public class DDListModel
    {
        public string key { get; set; }
        public string value { get; set; }
    }

}
