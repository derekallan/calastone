using CalastoneTextParser.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalastoneTextParser.Services.FilterService
{
    internal interface IFilterService
    {
        IEnumerable<string?> GetNextUnfilteredItem(IEnumerable<ISpanFilter> filters);
    }
}
