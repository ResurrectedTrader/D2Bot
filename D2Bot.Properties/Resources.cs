using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace D2Bot.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
public class Resources
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static ResourceManager ResourceManager
	{
		get
		{
			if (resourceMan == null)
			{
				resourceMan = new ResourceManager("D2Bot.Properties.Resources", typeof(Resources).Assembly);
			}
			return resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static CultureInfo Culture
	{
		get
		{
			return resourceCulture;
		}
		set
		{
			resourceCulture = value;
		}
	}

	public static byte[] act1 => (byte[])ResourceManager.GetObject("act1", resourceCulture);

	public static byte[] font16 => (byte[])ResourceManager.GetObject("font16", resourceCulture);

	public static byte[] invgreybrown => (byte[])ResourceManager.GetObject("invgreybrown", resourceCulture);

	public static byte[] pal => (byte[])ResourceManager.GetObject("pal", resourceCulture);

	public static byte[] Pal1 => (byte[])ResourceManager.GetObject("Pal1", resourceCulture);

	internal Resources()
	{
	}
}
