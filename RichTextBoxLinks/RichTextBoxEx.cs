using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RichTextBoxLinks;

public class RichTextBoxEx : RichTextBox
{
	private struct CHARFORMAT2_STRUCT
	{
		public uint cbSize;

		public uint dwMask;

		public uint dwEffects;

		public int yHeight;

		public int yOffset;

		public int crTextColor;

		public byte bCharSet;

		public byte bPitchAndFamily;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		public char[] szFaceName;

		public ushort wWeight;

		public ushort sSpacing;

		public int crBackColor;

		public int lcid;

		public int dwReserved;

		public short sStyle;

		public short wKerning;

		public byte bUnderlineType;

		public byte bAnimation;

		public byte bRevAuthor;

		public byte bReserved1;
	}

	private const int WM_USER = 1024;

	private const int EM_GETCHARFORMAT = 1082;

	private const int EM_SETCHARFORMAT = 1092;

	private const int SCF_SELECTION = 1;

	private const int SCF_WORD = 2;

	private const int SCF_ALL = 4;

	private const uint CFE_BOLD = 1u;

	private const uint CFE_ITALIC = 2u;

	private const uint CFE_UNDERLINE = 4u;

	private const uint CFE_STRIKEOUT = 8u;

	private const uint CFE_PROTECTED = 16u;

	private const uint CFE_LINK = 32u;

	private const uint CFE_AUTOCOLOR = 1073741824u;

	private const uint CFE_SUBSCRIPT = 65536u;

	private const uint CFE_SUPERSCRIPT = 131072u;

	private const int CFM_SMALLCAPS = 64;

	private const int CFM_ALLCAPS = 128;

	private const int CFM_HIDDEN = 256;

	private const int CFM_OUTLINE = 512;

	private const int CFM_SHADOW = 1024;

	private const int CFM_EMBOSS = 2048;

	private const int CFM_IMPRINT = 4096;

	private const int CFM_DISABLED = 8192;

	private const int CFM_REVISED = 16384;

	private const int CFM_BACKCOLOR = 67108864;

	private const int CFM_LCID = 33554432;

	private const int CFM_UNDERLINETYPE = 8388608;

	private const int CFM_WEIGHT = 4194304;

	private const int CFM_SPACING = 2097152;

	private const int CFM_KERNING = 1048576;

	private const int CFM_STYLE = 524288;

	private const int CFM_ANIMATION = 262144;

	private const int CFM_REVAUTHOR = 32768;

	private const uint CFM_BOLD = 1u;

	private const uint CFM_ITALIC = 2u;

	private const uint CFM_UNDERLINE = 4u;

	private const uint CFM_STRIKEOUT = 8u;

	private const uint CFM_PROTECTED = 16u;

	private const uint CFM_LINK = 32u;

	private const uint CFM_SIZE = 2147483648u;

	private const uint CFM_COLOR = 1073741824u;

	private const uint CFM_FACE = 536870912u;

	private const uint CFM_OFFSET = 268435456u;

	private const uint CFM_CHARSET = 134217728u;

	private const uint CFM_SUBSCRIPT = 196608u;

	private const uint CFM_SUPERSCRIPT = 196608u;

	private const byte CFU_UNDERLINENONE = 0;

	private const byte CFU_UNDERLINE = 1;

	private const byte CFU_UNDERLINEWORD = 2;

	private const byte CFU_UNDERLINEDOUBLE = 3;

	private const byte CFU_UNDERLINEDOTTED = 4;

	private const byte CFU_UNDERLINEDASH = 5;

	private const byte CFU_UNDERLINEDASHDOT = 6;

	private const byte CFU_UNDERLINEDASHDOTDOT = 7;

	private const byte CFU_UNDERLINEWAVE = 8;

	private const byte CFU_UNDERLINETHICK = 9;

	private const byte CFU_UNDERLINEHAIRLINE = 10;

	[DefaultValue(false)]
	public new bool DetectUrls
	{
		get
		{
			return base.DetectUrls;
		}
		set
		{
			base.DetectUrls = value;
		}
	}

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

	public RichTextBoxEx()
	{
		DetectUrls = false;
	}

	public void InsertLink(string text)
	{
		InsertLink(text, base.SelectionStart);
	}

	public void InsertLink(string text, int position)
	{
		if (position < 0 || position > Text.Length)
		{
			throw new ArgumentOutOfRangeException("position");
		}
		base.SelectionStart = position;
		SelectedText = text;
		Select(position, text.Length);
		SetSelectionLink(link: true);
		Select(position + text.Length, 0);
	}

	public void InsertLink(string text, string hyperlink)
	{
		InsertLink(text, hyperlink, base.SelectionStart);
	}

	public void InsertLink(string text, string hyperlink, int position)
	{
		if (position < 0 || position > Text.Length)
		{
			throw new ArgumentOutOfRangeException("position");
		}
		base.SelectionStart = position;
		base.SelectedRtf = "{\\rtf1\\ansi " + text + "\\v #" + hyperlink + "\\v0}";
		Select(position, text.Length + hyperlink.Length + 1);
		SetSelectionLink(link: true);
		Select(position + text.Length + hyperlink.Length + 1, 0);
	}

	public void SetSelectionLink(bool link)
	{
		SetSelectionStyle(32u, link ? 32u : 0u);
	}

	public int GetSelectionLink()
	{
		return GetSelectionStyle(32u, 32u);
	}

	private void SetSelectionStyle(uint mask, uint effect)
	{
		CHARFORMAT2_STRUCT cHARFORMAT2_STRUCT = default(CHARFORMAT2_STRUCT);
		cHARFORMAT2_STRUCT.cbSize = (uint)Marshal.SizeOf((object)cHARFORMAT2_STRUCT);
		cHARFORMAT2_STRUCT.dwMask = mask;
		cHARFORMAT2_STRUCT.dwEffects = effect;
		IntPtr wParam = new IntPtr(1);
		IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf((object)cHARFORMAT2_STRUCT));
		Marshal.StructureToPtr((object)cHARFORMAT2_STRUCT, intPtr, false);
		SendMessage(base.Handle, 1092, wParam, intPtr);
		Marshal.FreeCoTaskMem(intPtr);
	}

	private int GetSelectionStyle(uint mask, uint effect)
	{
		CHARFORMAT2_STRUCT cHARFORMAT2_STRUCT = default(CHARFORMAT2_STRUCT);
		cHARFORMAT2_STRUCT.cbSize = (uint)Marshal.SizeOf((object)cHARFORMAT2_STRUCT);
		cHARFORMAT2_STRUCT.szFaceName = new char[32];
		IntPtr wParam = new IntPtr(1);
		IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf((object)cHARFORMAT2_STRUCT));
		Marshal.StructureToPtr((object)cHARFORMAT2_STRUCT, intPtr, false);
		SendMessage(base.Handle, 1082, wParam, intPtr);
		cHARFORMAT2_STRUCT = (CHARFORMAT2_STRUCT)Marshal.PtrToStructure(intPtr, typeof(CHARFORMAT2_STRUCT));
		int result = (((cHARFORMAT2_STRUCT.dwMask & mask) != mask) ? (-1) : (((cHARFORMAT2_STRUCT.dwEffects & effect) == effect) ? 1 : 0));
		Marshal.FreeCoTaskMem(intPtr);
		return result;
	}
}
