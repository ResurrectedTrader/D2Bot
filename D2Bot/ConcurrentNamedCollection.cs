using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace D2Bot;

public class ConcurrentNamedCollection<T> : KeyedCollection<string, T> where T : INamedItem
{
	public ConcurrentNamedCollection()
		: base((IEqualityComparer<string>)StringComparer.OrdinalIgnoreCase)
	{
	}

	protected override string GetKeyForItem(T item)
	{
		return item.Name;
	}

	public bool CanRenameItem(string oldName, string newName)
	{
		oldName = oldName.Trim();
		newName = newName.Trim();
		if (oldName.Equals(newName, StringComparison.OrdinalIgnoreCase))
		{
			return true;
		}
		if (!Contains(oldName))
		{
			return false;
		}
		if (Contains(newName))
		{
			return false;
		}
		return true;
	}

	public bool RenameItem(string oldName, string newName)
	{
		oldName = oldName.Trim();
		newName = newName.Trim();
		if (!CanRenameItem(oldName, newName))
		{
			return false;
		}
		if (oldName.Equals(newName, StringComparison.OrdinalIgnoreCase))
		{
			return true;
		}
		T item = base[oldName];
		int index = IndexOf(item);
		Remove(item);
		item.Name = newName;
		InsertItem(index, item);
		return true;
	}
}
