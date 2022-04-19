using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalastoneTextParser.Filters
{
    public class LetterTFilter : ISpanFilter
    {
        public bool ShouldBeFiltered(ReadOnlySpan<char> input)
        {
            foreach (var c in input)
            {
                if (c == 't' || c == 'T')
                    return true;
            }
            return false;
        }
    }
}
