using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTest.InLineClasses
{
    public class CultureLanguagesForTest : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "pt-BR" };
            yield return new object[] { "en-US" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    }
}
