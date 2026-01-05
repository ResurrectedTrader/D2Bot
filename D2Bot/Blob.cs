namespace D2Bot;

internal class Blob
{
	public string name;

	public string path;

	public string url;

	public string sha;

	public Blob(string _name, string _path, string _url, string _sha)
	{
		name = _name;
		path = _path;
		url = _url;
		sha = _sha;
	}
}
