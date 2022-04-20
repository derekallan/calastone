// See https://aka.ms/new-console-template for more information
using CalastoneTextParser.Filters;
using CalastoneTextParser.Services.FilterService;


using StreamFilterService filterService = new(new StreamReader("../../../Files/testText.txt"));

var filters = new List<ISpanFilter>
{
    new LengthLessThan3Filter(),
    new LetterTFilter(),
    new MiddleVowelFilter(),
};

var list = new LinkedList<string?>();
foreach (var item in filterService.GetNextUnfilteredItem(filters))
{
    list.AddLast(item);
}
Console.WriteLine(string.Join('\n', list));
