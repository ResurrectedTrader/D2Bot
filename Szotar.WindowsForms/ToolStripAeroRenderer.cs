using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Szotar.WindowsForms;

public class ToolStripAeroRenderer : ToolStripSystemRenderer
{
	internal static class NativeMethods
	{
		public struct MARGINS
		{
			public int cxLeftWidth;

			public int cxRightWidth;

			public int cyTopHeight;

			public int cyBottomHeight;
		}

		[DllImport("uxtheme.dll")]
		public static extern int GetThemeMargins(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, int iPropId, IntPtr rect, out MARGINS pMargins);
	}

	private enum MenuParts
	{
		ItemTMSchema = 1,
		DropDownTMSchema,
		BarItemTMSchema,
		BarDropDownTMSchema,
		ChevronTMSchema,
		SeparatorTMSchema,
		BarBackground,
		BarItem,
		PopupBackground,
		PopupBorders,
		PopupCheck,
		PopupCheckBackground,
		PopupGutter,
		PopupItem,
		PopupSeparator,
		PopupSubmenu,
		SystemClose,
		SystemMaximize,
		SystemMinimize,
		SystemRestore
	}

	private enum MenuBarStates
	{
		Active = 1,
		Inactive
	}

	private enum MenuBarItemStates
	{
		Normal = 1,
		Hover,
		Pushed,
		Disabled,
		DisabledHover,
		DisabledPushed
	}

	private enum MenuPopupItemStates
	{
		Normal = 1,
		Hover,
		Disabled,
		DisabledHover
	}

	private enum MenuPopupCheckStates
	{
		CheckmarkNormal = 1,
		CheckmarkDisabled,
		BulletNormal,
		BulletDisabled
	}

	private enum MenuPopupCheckBackgroundStates
	{
		Disabled = 1,
		Normal,
		Bitmap
	}

	private enum MenuPopupSubMenuStates
	{
		Normal = 1,
		Disabled
	}

	private enum MarginTypes
	{
		Sizing = 3601,
		Content,
		Caption
	}

	private VisualStyleRenderer renderer;

	private static readonly int RebarBackground = 6;

	public ToolbarTheme Theme { get; set; }

	private string RebarClass => SubclassPrefix + "Rebar";

	private string ToolbarClass => SubclassPrefix + "ToolBar";

	private string MenuClass => SubclassPrefix + "Menu";

	private string SubclassPrefix => Theme switch
	{
		ToolbarTheme.MediaToolbar => "Media::", 
		ToolbarTheme.CommunicationsToolbar => "Communications::", 
		ToolbarTheme.BrowserTabBar => "BrowserTabBar::", 
		ToolbarTheme.HelpBar => "Help::", 
		_ => string.Empty, 
	};

	public bool IsSupported
	{
		get
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return false;
			}
			return VisualStyleRenderer.IsElementDefined(VisualStyleElement.CreateElement("Menu", 7, 1));
		}
	}

	public ToolStripAeroRenderer(ToolbarTheme theme)
	{
		Theme = theme;
	}

	private Padding GetThemeMargins(IDeviceContext dc, MarginTypes marginType)
	{
		try
		{
			IntPtr hdc = dc.GetHdc();
			if (NativeMethods.GetThemeMargins(renderer.Handle, hdc, renderer.Part, renderer.State, (int)marginType, IntPtr.Zero, out var pMargins) == 0)
			{
				return new Padding(pMargins.cxLeftWidth, pMargins.cyTopHeight, pMargins.cxRightWidth, pMargins.cyBottomHeight);
			}
			return new Padding(0);
		}
		finally
		{
			dc.ReleaseHdc();
		}
	}

	private static int GetItemState(ToolStripItem item)
	{
		bool selected = item.Selected;
		if (item.IsOnDropDown)
		{
			if (item.Enabled)
			{
				if (!selected)
				{
					return 1;
				}
				return 2;
			}
			if (!selected)
			{
				return 3;
			}
			return 4;
		}
		if (item.Pressed)
		{
			if (!item.Enabled)
			{
				return 6;
			}
			return 3;
		}
		if (item.Enabled)
		{
			if (!selected)
			{
				return 1;
			}
			return 2;
		}
		if (!selected)
		{
			return 4;
		}
		return 5;
	}

	private VisualStyleElement Subclass(VisualStyleElement element)
	{
		return VisualStyleElement.CreateElement(SubclassPrefix + element.ClassName, element.Part, element.State);
	}

	private bool EnsureRenderer()
	{
		if (!IsSupported)
		{
			return false;
		}
		if (renderer == null)
		{
			renderer = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Normal);
		}
		return true;
	}

	protected override void Initialize(ToolStrip toolStrip)
	{
		if (toolStrip.Parent is ToolStripPanel)
		{
			toolStrip.BackColor = Color.Transparent;
		}
		base.Initialize(toolStrip);
	}

	protected override void InitializePanel(ToolStripPanel toolStripPanel)
	{
		foreach (Control control in toolStripPanel.Controls)
		{
			if (control is ToolStrip)
			{
				Initialize((ToolStrip)control);
			}
		}
		base.InitializePanel(toolStripPanel);
	}

	protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
	{
		if (EnsureRenderer())
		{
			renderer.SetParameters(MenuClass, 10, 0);
			if (e.ToolStrip.IsDropDown)
			{
				Region clip = e.Graphics.Clip;
				Rectangle clientRectangle = e.ToolStrip.ClientRectangle;
				clientRectangle.Inflate(-1, -1);
				e.Graphics.ExcludeClip(clientRectangle);
				renderer.DrawBackground(e.Graphics, e.ToolStrip.ClientRectangle, e.AffectedBounds);
				e.Graphics.Clip = clip;
			}
		}
		else
		{
			base.OnRenderToolStripBorder(e);
		}
	}

	private Rectangle GetBackgroundRectangle(ToolStripItem item)
	{
		if (!item.IsOnDropDown)
		{
			return new Rectangle(default(Point), item.Bounds.Size);
		}
		Rectangle bounds = item.Bounds;
		bounds.X = item.ContentRectangle.X + 1;
		bounds.Width = item.ContentRectangle.Width - 1;
		bounds.Y = 0;
		return bounds;
	}

	protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
	{
		if (EnsureRenderer())
		{
			int part = (e.Item.IsOnDropDown ? 14 : 8);
			renderer.SetParameters(MenuClass, part, GetItemState(e.Item));
			Rectangle backgroundRectangle = GetBackgroundRectangle(e.Item);
			renderer.DrawBackground(e.Graphics, backgroundRectangle, backgroundRectangle);
		}
		else
		{
			base.OnRenderMenuItemBackground(e);
		}
	}

	protected override void OnRenderToolStripPanelBackground(ToolStripPanelRenderEventArgs e)
	{
		if (EnsureRenderer())
		{
			if (VisualStyleRenderer.IsElementDefined(VisualStyleElement.CreateElement(RebarClass, RebarBackground, 0)))
			{
				renderer.SetParameters(RebarClass, RebarBackground, 0);
			}
			else
			{
				renderer.SetParameters(RebarClass, 0, 0);
			}
			if (renderer.IsBackgroundPartiallyTransparent())
			{
				renderer.DrawParentBackground(e.Graphics, e.ToolStripPanel.ClientRectangle, e.ToolStripPanel);
			}
			renderer.DrawBackground(e.Graphics, e.ToolStripPanel.ClientRectangle);
			e.Handled = true;
		}
		else
		{
			base.OnRenderToolStripPanelBackground(e);
		}
	}

	protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
	{
		if (EnsureRenderer())
		{
			if (e.ToolStrip.IsDropDown)
			{
				renderer.SetParameters(MenuClass, 9, 0);
			}
			else
			{
				if (e.ToolStrip.Parent is ToolStripPanel)
				{
					return;
				}
				if (VisualStyleRenderer.IsElementDefined(VisualStyleElement.CreateElement(RebarClass, RebarBackground, 0)))
				{
					renderer.SetParameters(RebarClass, RebarBackground, 0);
				}
				else
				{
					renderer.SetParameters(RebarClass, 0, 0);
				}
			}
			if (renderer.IsBackgroundPartiallyTransparent())
			{
				renderer.DrawParentBackground(e.Graphics, e.ToolStrip.ClientRectangle, e.ToolStrip);
			}
			renderer.DrawBackground(e.Graphics, e.ToolStrip.ClientRectangle, e.AffectedBounds);
		}
		else
		{
			base.OnRenderToolStripBackground(e);
		}
	}

	protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
	{
		if (EnsureRenderer())
		{
			ToolStripSplitButton toolStripSplitButton = (ToolStripSplitButton)e.Item;
			base.OnRenderSplitButtonBackground(e);
			OnRenderArrow(new ToolStripArrowRenderEventArgs(e.Graphics, toolStripSplitButton, toolStripSplitButton.DropDownButtonBounds, Color.Red, ArrowDirection.Down));
		}
		else
		{
			base.OnRenderSplitButtonBackground(e);
		}
	}

	private Color GetItemTextColor(ToolStripItem item)
	{
		int part = (item.IsOnDropDown ? 14 : 8);
		renderer.SetParameters(MenuClass, part, GetItemState(item));
		return renderer.GetColor(ColorProperty.TextColor);
	}

	protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
	{
		if (EnsureRenderer())
		{
			e.TextColor = GetItemTextColor(e.Item);
		}
		base.OnRenderItemText(e);
	}

	protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
	{
		if (EnsureRenderer())
		{
			if (e.ToolStrip.IsDropDown)
			{
				renderer.SetParameters(MenuClass, 13, 0);
				Padding themeMargins = GetThemeMargins(e.Graphics, MarginTypes.Sizing);
				int num = e.ToolStrip.Width - e.ToolStrip.DisplayRectangle.Width - themeMargins.Left - themeMargins.Right - 1 - e.AffectedBounds.Width;
				Rectangle affectedBounds = e.AffectedBounds;
				affectedBounds.Y += 2;
				affectedBounds.Height -= 4;
				int width = renderer.GetPartSize(e.Graphics, ThemeSizeType.True).Width;
				if (e.ToolStrip.RightToLeft == RightToLeft.Yes)
				{
					affectedBounds = new Rectangle(affectedBounds.X - num, affectedBounds.Y, width, affectedBounds.Height);
					affectedBounds.X += width;
				}
				else
				{
					affectedBounds = new Rectangle(affectedBounds.Width + num - width, affectedBounds.Y, width, affectedBounds.Height);
				}
				renderer.DrawBackground(e.Graphics, affectedBounds);
			}
		}
		else
		{
			base.OnRenderImageMargin(e);
		}
	}

	protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
	{
		if (e.ToolStrip.IsDropDown && EnsureRenderer())
		{
			renderer.SetParameters(MenuClass, 15, 0);
			Rectangle rectangle = new Rectangle(e.ToolStrip.DisplayRectangle.Left, 0, e.ToolStrip.DisplayRectangle.Width, e.Item.Height);
			renderer.DrawBackground(e.Graphics, rectangle, rectangle);
		}
		else
		{
			base.OnRenderSeparator(e);
		}
	}

	protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
	{
		if (EnsureRenderer())
		{
			Rectangle bounds = GetBackgroundRectangle(e.Item);
			bounds.Width = bounds.Height;
			if (e.Item.RightToLeft == RightToLeft.Yes)
			{
				bounds = new Rectangle(e.ToolStrip.ClientSize.Width - bounds.X - bounds.Width, bounds.Y, bounds.Width, bounds.Height);
			}
			renderer.SetParameters(MenuClass, 12, (!e.Item.Enabled) ? 1 : 2);
			renderer.DrawBackground(e.Graphics, bounds);
			Rectangle imageRectangle = e.ImageRectangle;
			imageRectangle.X = bounds.X + bounds.Width / 2 - imageRectangle.Width / 2;
			imageRectangle.Y = bounds.Y + bounds.Height / 2 - imageRectangle.Height / 2;
			renderer.SetParameters(MenuClass, 11, e.Item.Enabled ? 1 : 2);
			renderer.DrawBackground(e.Graphics, imageRectangle);
		}
		else
		{
			base.OnRenderItemCheck(e);
		}
	}

	protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
	{
		if (EnsureRenderer())
		{
			e.ArrowColor = GetItemTextColor(e.Item);
		}
		base.OnRenderArrow(e);
	}

	protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
	{
		if (EnsureRenderer())
		{
			string className = RebarClass;
			if (Theme == ToolbarTheme.BrowserTabBar)
			{
				className = "Rebar";
			}
			int state = VisualStyleElement.Rebar.Chevron.Normal.State;
			if (e.Item.Pressed)
			{
				state = VisualStyleElement.Rebar.Chevron.Pressed.State;
			}
			else if (e.Item.Selected)
			{
				state = VisualStyleElement.Rebar.Chevron.Hot.State;
			}
			renderer.SetParameters(className, VisualStyleElement.Rebar.Chevron.Normal.Part, state);
			renderer.DrawBackground(e.Graphics, new Rectangle(Point.Empty, e.Item.Size));
		}
		else
		{
			base.OnRenderOverflowButtonBackground(e);
		}
	}
}
