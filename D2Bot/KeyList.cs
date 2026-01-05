using System.Collections.Generic;

namespace D2Bot;

public class KeyList : INamedItem
{
	public List<CDKey> CDKeys;

	private readonly object keyLock = new object();

	public string Name { get; set; }

	public int Index { get; set; }

	public KeyList(string name)
	{
		Name = name;
		Index = 0;
		CDKeys = new List<CDKey>();
	}

	public void AddKey(CDKey k)
	{
		lock (keyLock)
		{
			CDKeys.Add(k);
		}
	}

	public void Clear()
	{
		lock (keyLock)
		{
			Index = 0;
			CDKeys.Clear();
		}
	}

	private CDKey GetKey(int index)
	{
		lock (keyLock)
		{
			CDKey cDKey = CDKeys[index];
			cDKey.InUse(value: true);
			return cDKey;
		}
	}

	private int Next()
	{
		lock (keyLock)
		{
			if (CDKeys == null || CDKeys.Count == 0)
			{
				Index = 0;
				return -1;
			}
			int num = Index;
			do
			{
				int num2 = num;
				num = (num + 1) % CDKeys.Count;
				if (!CDKeys[num2].InUse())
				{
					Index = num;
					return num2;
				}
			}
			while (Index != num);
			return -1;
		}
	}

	public CDKey GetAndIncrement(CDKey old)
	{
		lock (keyLock)
		{
			int num = Next();
			if (num < 0)
			{
				return old;
			}
			old?.InUse(value: false);
			return GetKey(num);
		}
	}
}
