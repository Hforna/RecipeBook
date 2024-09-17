using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.AutoMapperBuilder
{
    public class sqIdsBuilder
    {
        public static SqidsEncoder<long> Build()
        {
            return new SqidsEncoder<long>(new ()
            {
                MinLength = 4,
                Alphabet = "P75zKJ1N6IpOxQ3tRTqoD9hVCaBkr4dg8ASFEmZcvnbfjwLesU2XYuHMWlGi0y"
            });
        }
    }
}
