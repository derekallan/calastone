// See https://aka.ms/new-console-template for more information
using CalastoneTextParser.Filters;
using CalastoneTextParser.Services.FilterService;

StreamReader sr = new StreamReader("../../../Files/testText.txt");

StreamFilterService filterService = new StreamFilterService(sr);
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