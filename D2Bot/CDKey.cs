using Newtonsoft.Json;

namespace D2Bot;

public class CDKey
{
	public string Name { get; set; }

	public string Classic { get; set; }

	public string Expansion { get; set; }

	[JsonIgnore]
	public bool Status { get; set; }

	public bool InUse()
	{
		if (!Status)
		{
			return Program.HoldKeyList.Contains(Name);
		}
		return true;
	}

	public void InUse(bool value)
	{
		Status = value;
	}

	public CDKey(string a, string b, string c)
	{
		Name = a;
		Classic = b;
		Expansion = c;
	}

	public CDKey DeepCopy()
	{
		return (CDKey)MemberwiseClone();
	}
}
