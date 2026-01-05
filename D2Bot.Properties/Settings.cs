using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace D2Bot.Properties;

[CompilerGenerated]
[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.5.0.0")]
public sealed class Settings : ApplicationSettingsBase
{
	private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

	public static Settings Default => defaultInstance;

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool Start_Hidden
	{
		get
		{
			return (bool)this["Start_Hidden"];
		}
		set
		{
			this["Start_Hidden"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string D2_Folder
	{
		get
		{
			return (string)this["D2_Folder"];
		}
		set
		{
			this["D2_Folder"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("10")]
	public int Wait_Time
	{
		get
		{
			return (int)this["Wait_Time"];
		}
		set
		{
			this["Wait_Time"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool Load_Delay
	{
		get
		{
			return (bool)this["Load_Delay"];
		}
		set
		{
			this["Load_Delay"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("0")]
	public int Delay_Time
	{
		get
		{
			return (int)this["Delay_Time"];
		}
		set
		{
			this["Delay_Time"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	public bool Item_Header
	{
		get
		{
			return (bool)this["Item_Header"];
		}
		set
		{
			this["Item_Header"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("1.14d")]
	public string D2_Version
	{
		get
		{
			return (string)this["D2_Version"];
		}
		set
		{
			this["D2_Version"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool Start_Server
	{
		get
		{
			return (bool)this["Start_Server"];
		}
		set
		{
			this["Start_Server"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool System_Font
	{
		get
		{
			return (bool)this["System_Font"];
		}
		set
		{
			this["System_Font"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string SSL_Certificate
	{
		get
		{
			return (string)this["SSL_Certificate"];
		}
		set
		{
			this["SSL_Certificate"] = value;
		}
	}

	private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
	{
	}

	private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
	{
	}
}
