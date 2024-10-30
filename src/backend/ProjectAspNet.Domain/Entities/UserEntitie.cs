using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Entities
{
    [Table("users")]
    public class UserEntitie : IdentityUser<long>
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public bool Active { get; set; } = true;
        [MaxLength(255, ErrorMessage = "Name field must be less than 255")]
        public string Name { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Invalid email field")]
        public override string Email { get; set; } = string.Empty;
        [MinLength(8, ErrorMessage = "Password must have 8 or more digits")]
        public string Password {  get; set; } = string.Empty;
        public Guid UserIdentifier { get; set; } = Guid.NewGuid();
    }
}
