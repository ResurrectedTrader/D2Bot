using System.Collections.Generic;
using System.IO;
using System.Text;

namespace D2Bot;

internal static class FileHandler
{
	private static object _meta = new object();

	private static Dictionary<string, object> _flock = new Dictionary<string, object>();

	private static object GetSync(string key)
	{
		lock (_meta)
		{
			if (!_flock.ContainsKey(key))
			{
				_flock[key] = new object();
			}
		}
		return _flock[key];
	}

	public static void Append(string file, string value, Encoding type = null)
	{
		if (string.IsNullOrEmpty(file) || !File.Exists(file))
		{
			return;
		}
		if (type == null)
		{
			type = Encoding.Default;
		}
		lock (GetSync(file))
		{
			using StreamWriter streamWriter = new StreamWriter(file, append: true, type);
			streamWriter.WriteLine(value);
		}
	}

	public static void Write(string file, string value, Encoding type = null)
	{
		if (string.IsNullOrEmpty(file))
		{
			return;
		}
		if (type == null)
		{
			type = Encoding.Default;
		}
		lock (GetSync(file))
		{
			File.WriteAllText(file, value, type);
		}
	}

	public static string[] ReadLines(string file)
	{
		lock (GetSync(file))
		{
			return File.ReadAllLines(file);
		}
	}

	public static string Read(string file)
	{
		lock (GetSync(file))
		{
			return File.ReadAllText(file);
		}
	}
}
