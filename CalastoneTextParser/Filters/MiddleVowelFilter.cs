using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalastoneTextParser.Filters
{
    public class MiddleVowelFilter : ISpanFilter
    {
        private static HashSet<char> _vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
        public bool ShouldBeFiltered(ReadOnlySpan<char> input)
        {
            if (input.Length == 1)
                return charIsVowel(input[0]);
            var middle = input.Length / 2;
            if (IsOddLength(input.Length))
            {
                return charIsVowel(input[middle]);
            }

            return charIsVowel(input[middle - 1]) || charIsVowel(input[middle]);
        }

        private bool IsOddLength(int length)
        {
            return length % 2 != 0;
        }

        private bool charIsVowel(char c)
        {
            return _vowels.Contains(c);
        }
    }
}
