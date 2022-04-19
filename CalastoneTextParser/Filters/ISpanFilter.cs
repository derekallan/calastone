using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalastoneTextParser.Filters
{
    public interface ISpanFilter
    {
        public bool ShouldBeFiltered(ReadOnlySpan<char> input);
    }
}
