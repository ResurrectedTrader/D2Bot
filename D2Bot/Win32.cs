namespace D2Bot;

internal class Win32
{
	public const int WM_UPDATEUISTATE = 296;

	public const int UIS_SET = 1;

	public const int UIS_CLEAR = 2;

	public const int UISF_HIDEFOCUS = 1;

	public static int MakeParam(int loWord, int hiWord)
	{
		return (hiWord << 16) | (loWord & 0xFFFF);
	}
}
