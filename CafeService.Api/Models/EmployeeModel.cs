using CafeService.Api.Entities;
using System.ComponentModel.DataAnnotations;
using static CafeService.Api.Enums.CommonResources;

namespace CafeService.Api.Models
{
    public class EmployeeModel
    {
        public string? Id { get; set; }

    }
    public class EmployeeViewModel : EmployeeModel
    {
        public string? Name { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public int DaysWorked { get; set; }
        public string? Cafe { get; set; }
        public string? Gender { get; set; }
    }

    public class EmployeeInsertModel : EmployeeModel
    {
        [Required]
        [MaxLength(9)]
        [RegularExpression(@"^UI[a-zA-Z0-9]{7}$", ErrorMessage = "ID must be in the format 'UIXXXXXXX'.")]
        public new string? Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string? EmailAddress { get; set; }
        [Required]
        [RegularExpression(@"^[89]\d{7}$", ErrorMessage = "Phone number must start with 8 or 9 and be 8 digits long.")]
        public string? PhoneNumber { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public DateTime? StartDate { get; set; }
        public Guid? CafeId { get; set; }
    }

    public class EmployeeEditModel : EmployeeModel
    {
        [Required]
        [MaxLength(9)]
        [RegularExpression(@"^UI[a-zA-Z0-9]{7}$", ErrorMessage = "ID must be in the format 'UIXXXXXXX'.")]
        public new string? Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string? EmailAddress { get; set; }
        [Required]

        [RegularExpression(@"^[89]\d{7}$", ErrorMessage = "Phone number must start with 8 or 9 and be 8 digits long.")]
        public string? PhoneNumber { get; set; }
        [Required]
        public int Gender { get; set; }
        public DateTime? StartDate { get; set; }
        public Guid? CafeId { get; set; }
    }
    public class EmployeeDeleteModel : EmployeeModel
    {
        [Required]
        [MaxLength(9)]
        [RegularExpression(@"^UI[a-zA-Z0-9]{7}$", ErrorMessage = "ID must be in the format 'UIXXXXXXX'.")]
        public new string? Id { get; set; }
    }
}
