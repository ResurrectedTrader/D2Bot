namespace D2Bot;

public class PatchData
{
	public string Name { get; set; }

	public string Version { get; set; }

	public Patch.Dll Module { get; set; }

	public int Offset { get; set; }

	public byte[] Data { get; set; }
}
