using System.Reflection;
using System.Windows.Forms;

namespace D2Bot;

public class StyleHelper
{
	public static void DisableFlicker(Control ctrl)
	{
		MethodInfo method = ctrl.GetType().GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
		if (method != null)
		{
			method.Invoke(ctrl, new object[2]
			{
				ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer,
				true
			});
		}
	}
}
