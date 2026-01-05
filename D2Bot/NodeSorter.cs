using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace D2Bot;

public class NodeSorter : IComparer
{
	[DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
	private static extern int StrCmpLogicalW(string x, string y);

	public int Compare(object x, object y)
	{
		TreeNode obj = (TreeNode)x;
		return StrCmpLogicalW(y: ((TreeNode)y).Name, x: obj.Name);
	}
}
