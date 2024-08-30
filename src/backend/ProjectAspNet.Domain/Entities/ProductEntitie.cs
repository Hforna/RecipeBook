using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Entities
{
    public class ProductEntitie : BaseEntitie
    {
        public string ProductName { get; set; } = string.Empty;
        public string Description {  get; set; } = string.Empty;
        public double Price { get; set; }
    }
}
