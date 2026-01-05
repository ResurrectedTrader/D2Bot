using System.Collections.Generic;

namespace D2Bot;

internal class SafeList<T>
{
	private List<T> _list;

	private object _sync = new object();

	public SafeList(int capacity = 0)
	{
		_list = new List<T>(capacity);
	}

	public void Add(T value)
	{
		lock (_sync)
		{
			_list.Add(value);
		}
	}

	public List<T> GetList()
	{
		return _list;
	}

	public void AddRange(List<T> values)
	{
		lock (_sync)
		{
			if (_list.Capacity < _list.Count + values.Count)
			{
				_list.Capacity *= 2;
			}
			_list.AddRange(values);
		}
	}
}
