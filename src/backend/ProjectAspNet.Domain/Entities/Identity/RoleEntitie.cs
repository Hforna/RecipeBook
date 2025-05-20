using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Entities.Identity
{
    public class RoleEntitie : IdentityRole<long>
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public bool Active { get; set; } = true;
    }
}
