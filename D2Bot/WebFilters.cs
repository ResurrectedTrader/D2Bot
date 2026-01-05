using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace D2Bot;

public class WebFilters
{
	public string[] filters;

	private List<Regex> regex;

	public HashSet<string> valid;

	public bool LoadFilters()
	{
		if (filters == null)
		{
			return false;
		}
		valid = new HashSet<string>();
		regex = new List<Regex>();
		string[] array = filters;
		foreach (string text in array)
		{
			valid.Add(text);
			regex.Add(new Regex(text, RegexOptions.Compiled));
		}
		return true;
	}

	public List<Regex> GetFilters()
	{
		return regex;
	}
}
