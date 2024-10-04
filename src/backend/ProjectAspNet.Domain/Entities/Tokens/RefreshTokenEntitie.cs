using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Entities.Tokens
{
    public class RefreshTokenEntitie : BaseEntitie
    {
        public string Value { get; set; } = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        public long UserId { get; set; }
        public UserEntitie User { get; set; }
    }
}
