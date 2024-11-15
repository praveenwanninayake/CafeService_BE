using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using static CafeService.Api.Enums.CommonResources;


namespace CafeService.Api.Entities
{
    [Table("M_Employee")]
    public class Employee
    {
        [Key]
        [Required]
        [MaxLength(9)]
        [RegularExpression(@"^UI[a-zA-Z0-9]{7}$", ErrorMessage = "ID must be in the format 'UIXXXXXXX'.")]
        public string Id { get; set; }

       
        public Guid? FK_CafeId { get; set; }
        //[ForeignKey(nameof(FK_CafeId))]
        public Cafe Cafe { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string EmailAddress { get; set; }

        [Required]
        [RegularExpression(@"^[89]\d{7}$", ErrorMessage = "Phone number must start with 8 or 9 and be 8 digits long.")]
        public string PhoneNumber { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

               
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        [Required]
        public DateTime ModifiedDateTime { get; set; } = DateTime.Now;

    }
}
