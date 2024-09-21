using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Entities
{
    public class BaseEntitie
    {
        public long Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public bool Active { get; set; } = true;
    }
}
