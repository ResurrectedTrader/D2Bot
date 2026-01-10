using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using BrightIdeasSoftware;
using D2Bot.Properties;
using D2BSItemlog;
using Newtonsoft.Json;
using PInvoke;
using RichTextBoxLinks;

namespace D2Bot;

public partial class Main : Form
{
	public delegate void ConsolePrintCallback(PrintMessage pm, ProfileBase profile);

	public delegate void ItemLogPrintCallback(D2Item d2item, D2Profile profile = null);

	public delegate void UpdateDrawCallback(bool show);

	public enum ProfileAction
	{
		None,
		Start,
		Stop,
		Show,
		Hide
	}

	public delegate void ShowCharLogCallback(ListViewItem[] list);

	public delegate void DupeListCallback(DataGridViewRow[] list);

	public delegate void ToggleCallback(bool enable);

	public delegate void AddNodeCallback(TreeNode root, TreeNode leaf);

	public delegate void AddTreeCallback(TreeView root, TreeNode leaf);

	public delegate void RemoveNodeCallback(TreeNode root);

	public delegate void AddKeyDataCallback(ListViewItem[] items);

	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static Action<WebItem> _003C_003E9__54_1;

		public static Converter<ListViewItem, WebItem> _003C_003E9__112_1;

		public static ImageGetterDelegate _003C_003E9__122_0;

		internal void _003CHandleWebApi_003Eb__54_1(WebItem wi)
		{
			wi.GenerateImage();
		}

		internal WebItem _003CSearchNodeItems_003Eb__112_1(ListViewItem e)
		{
			return ((Item)e.Tag).ToWebItem();
		}

		internal object _003CLoadList_003Eb__122_0(object row)
		{
			int type = (int)((ProfileBase)row).Type;
			if (type == 0 && ((ProfileBase)row).Status != Status.Stop)
			{
				return 2;
			}
			return type;
		}
	}

	public System.Timers.Timer Timer;

	public const int QUEUE_SIZE = 10;

	public BlockingCollection<Worker>[] Queue = new BlockingCollection<Worker>[10];

	public static ListViewItem LastSelected;

	public Mutex CharLogMutex = new Mutex();

	private FileSystemWatcher watcher;

	private HashSet<string> descriptions;

	private ConcurrentDictionary<string, ListViewItem> database;

	private Dictionary<string, ListViewItem> active;

	private ConcurrentDictionary<string, TreeNode> nodeCache;

	private Queue<string> EventBuffer;

	private Mutex EventSync;

	private bool keepAlive;

	private Thread ItemLoading;

	private Thread WindowChecker;

	private NotifyIcon notifyIcon;

	public Mutex objectLock = new Mutex();

	private int UpdateIndex;

	private int UpdateTotal;

	public int Ticker = Environment.TickCount & 0x7FFFFFFF;

	public int SEC_Count = 600;


	public void CheckWindows()
	{
		Thread.Sleep(6000);
		while (true)
		{
			Thread.Sleep(1000);
			if (debugMode.Checked || Program.ProfileList == null || Program.ProfileList.Count == 0)
			{
				continue;
			}
			IntPtr zero = IntPtr.Zero;
			zero = MessageHelper.FindWindow(null, "Diablo II Error");
			if (zero != IntPtr.Zero)
			{
				MessageHelper.SendMessageTimeout(zero, 274u, (IntPtr)61536, IntPtr.Zero, MessageHelper.SendMessageTimeoutFlags.SMTO_NOTIMEOUTIFNOTHUNG, 250u, out var _);
			}
			int num = 0;
			for (int i = 0; i < Program.ProfileList.Values.Count; i++)
			{
				ProfileBase profileBase = null;
				try
				{
					profileBase = Program.ProfileList.Values.ElementAt(i);
				}
				catch
				{
				}
				if (profileBase == null || profileBase.GetType() != typeof(D2Profile))
				{
					continue;
				}
				D2Profile d2Profile = profileBase as D2Profile;
				Process d2Process = d2Profile.D2Process;
				try
				{
					if (Program.WS != null && Program.WS.IsActive())
					{
						Program.WS.RunScheduler(d2Profile);
					}
				}
				catch
				{
				}
				if (d2Profile.ScheduleEnable)
				{
					bool flag = false;
					TimeSpan timeSpan = new TimeSpan(0, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
					Schedule schedule = GetSchedule(d2Profile.Schedule);
					for (int j = 0; j < schedule.Times.Count; j += 2)
					{
						TimeSpan period = schedule.Times[j].GetPeriod();
						TimeSpan period2 = schedule.Times[j + 1].GetPeriod();
						if (period > period2 && (period <= timeSpan || timeSpan < period2))
						{
							flag = true;
							break;
						}
						if ((period <= timeSpan && period2 > timeSpan) || period == period2)
						{
							flag = true;
							break;
						}
					}
					if (flag && d2Process == null && d2Profile.Status == Status.Stop)
					{
						int num2 = 0;
						if (loadDelay.Checked)
						{
							num2 = Settings.Default.Delay_Time;
						}
						num++;
						d2Profile.Load(num * num2);
					}
					if (!flag && d2Process != null)
					{
						d2Profile.Stop();
					}
				}
				if (!Program.Runtime.ContainsKey(d2Profile.MainWindowHandle) || d2Profile.Parameters.Contains("-L") || !d2Profile.StatusLock.TryEnterReadLock(10))
				{
					continue;
				}
				if (d2Profile.Status != Status.Run)
				{
					d2Profile.StatusLock.ExitReadLock();
					continue;
				}
				d2Profile.StatusLock.ExitReadLock();
				d2Profile.HeartAttack++;
				try
				{
					if (d2Process.HasExited)
					{
						d2Profile.StatusLock.EnterReadLock();
						if (d2Profile.Status == Status.Run)
						{
							d2Profile.StatusLock.ExitReadLock();
							PrintMessage pm = new PrintMessage("Window has unexpectedly exited... starting profile");
							ConsolePrint(pm, d2Profile);
							d2Profile.Error = true;
							Thread.Sleep(Settings.Default.Delay_Time);
							d2Profile.Restart();
							UpdateRestarts(d2Profile);
							UpdateCrashes(d2Profile);
						}
						else
						{
							d2Profile.StatusLock.ExitReadLock();
						}
					}
					else if (!d2Process.Responding)
					{
						d2Profile.NoResponse++;
					}
					else
					{
						d2Profile.NoResponse = 0;
					}
				}
				catch
				{
					d2Profile.NoResponse++;
				}
				if (d2Profile.HeartAttack > Settings.Default.Wait_Time || d2Profile.NoResponse > Settings.Default.Wait_Time)
				{
					d2Profile.StatusLock.EnterReadLock();
					if (d2Profile.Status == Status.Run)
					{
						d2Profile.StatusLock.ExitReadLock();
						PrintMessage pm2 = new PrintMessage("D2BS is not responding... starting profile");
						ConsolePrint(pm2, d2Profile);
						d2Profile.Error = true;
						d2Profile.NoResponse = 0;
						d2Profile.HeartAttack = 0;
						Thread.Sleep(Settings.Default.Delay_Time);
						d2Profile.Restart();
						UpdateRestarts(d2Profile);
						UpdateCrashes(d2Profile);
					}
					else
					{
						d2Profile.StatusLock.ExitReadLock();
					}
				}
				else if (d2Profile.Status == Status.Run && d2Profile.HeartAttack > 1)
				{
					Program.SendCopyData(d2Process.MainWindowHandle, "Handle", Program.Handle);
				}
				if (d2Profile.RunsPerKey <= 0 || d2Profile.KeyRuns < d2Profile.RunsPerKey)
				{
					continue;
				}
				d2Profile.StatusLock.EnterReadLock();
				if (d2Profile.Status == Status.Run)
				{
					d2Profile.StatusLock.ExitReadLock();
					Thread.Sleep(Settings.Default.Delay_Time);
					if (!d2Profile.SwitchKeys)
					{
						d2Profile.KeyRuns = 0;
					}
					d2Profile.Restart(d2Profile.SwitchKeys);
				}
				else
				{
					d2Profile.StatusLock.ExitReadLock();
				}
			}
		}
	}

	public void Save(object sender, EventArgs e)
	{
		Program.SaveAll();
	}

	public List<ProfileBase> GetSelectedProfiles()
	{
		List<ProfileBase> list = new List<ProfileBase>();
		bool flag = false;
		try
		{
			flag = objectLock.WaitOne();
			if (objectProfileList.SelectedObjects != null)
			{
				foreach (ProfileBase selectedObject in Program.GM.objectProfileList.SelectedObjects)
				{
					if (selectedObject != null)
					{
						list.Add(selectedObject);
					}
				}
			}
		}
		finally
		{
			if (flag)
			{
				objectLock.ReleaseMutex();
			}
		}
		return list;
	}

	public void StartProfiles(List<ProfileBase> profiles)
	{
		int num = 0;
		int num2 = 1;
		if (loadDelay.Checked)
		{
			num = Settings.Default.Delay_Time;
		}
		foreach (ProfileBase profile in profiles)
		{
			if (profile != null)
			{
				num2++;
				profile.Load(num * num2);
			}
		}
	}

	public void StopProfiles(List<ProfileBase> profiles)
	{
		foreach (ProfileBase profile in profiles)
		{
			profile?.Stop();
		}
	}

	public void StartProfile(object sender, EventArgs e)
	{
		List<ProfileBase> profiles = GetSelectedProfiles();
		Thread thread = new Thread((ThreadStart)delegate
		{
			StartProfiles(profiles);
		});
		thread.IsBackground = true;
		thread.Start();
	}

	public void StopProfile(object sender, EventArgs e)
	{
		List<ProfileBase> profiles = GetSelectedProfiles();
		Thread thread = new Thread((ThreadStart)delegate
		{
			StopProfiles(profiles);
		});
		thread.IsBackground = true;
		thread.Start();
	}

	public void Duplicate(object sender, EventArgs e)
	{
		if (objectProfileList.SelectedObjects == null)
		{
			return;
		}
		foreach (ProfileBase selectedObject in Program.GM.objectProfileList.SelectedObjects)
		{
			if (selectedObject == null)
			{
				continue;
			}
			ProfileBase profileBase2 = selectedObject.DeepCopy();
			string text = Regex.Match(profileBase2.Name, "^\\D+").Value;
			string s = Regex.Replace(profileBase2.Name, "^\\D+", "");
			string text2 = "";
			if (!int.TryParse(s, out var result))
			{
				result = 0;
			}
			result++;
			if (!text.EndsWith("-"))
			{
				text += "-";
			}
			text2 = text + result.ToString(new string('0', 3));
			while (GetProfile(text2) != null)
			{
				result++;
				if (result > 999)
				{
					throw new Exception("Cannot copy profile more than 999 times.");
				}
				text2 = text + result.ToString(new string('0', 3));
			}
			profileBase2.Name = text2;
			profileBase2.Add(rand: true);
			Thread.Sleep(10);
			Program.SaveProfiles();
		}
	}

	public void ShowWindow(object sender, EventArgs e)
	{
		if (objectProfileList.SelectedObjects == null)
		{
			return;
		}
		foreach (ProfileBase selectedObject in Program.GM.objectProfileList.SelectedObjects)
		{
			if (selectedObject != null && selectedObject.Type == ProfileType.D2)
			{
				((D2Profile)selectedObject).ShowWindow();
			}
		}
	}

	public void HideWindow(object sender, EventArgs e)
	{
		if (objectProfileList.SelectedObjects == null)
		{
			return;
		}
		foreach (ProfileBase selectedObject in Program.GM.objectProfileList.SelectedObjects)
		{
			if (selectedObject != null && selectedObject.Type == ProfileType.D2)
			{
				((D2Profile)selectedObject).HideWindow();
			}
		}
	}

	public void Remove(object sender, EventArgs e)
	{
		if (sender == null || MessageBox.Show("Apply profile delete?", "Confirm delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
		{
			bool flag = false;
			try
			{
				flag = objectLock.WaitOne();
				if (objectProfileList.SelectedObjects != null)
				{
					foreach (ProfileBase selectedObject in objectProfileList.SelectedObjects)
					{
						Program.ProfileList.TryRemove(selectedObject.Name.ToLower(), out var value);
						if (typeof(D2Profile) == value.GetType())
						{
							D2Profile d2Profile = value as D2Profile;
							if (d2Profile.CurrentKey != null)
							{
								d2Profile.CurrentKey.InUse(value: false);
							}
						}
					}
					objectProfileList.RemoveObjects((ICollection)objectProfileList.SelectedObjects);
				}
			}
			catch
			{
			}
			finally
			{
				if (flag)
				{
					objectLock.ReleaseMutex();
				}
			}
		}
		Program.SaveProfiles();
	}

	public void AddIRC(object sender, EventArgs e)
	{
		new IRCProfile().ShowEditor(init: true);
	}

	public void Add(object sender, EventArgs e)
	{
		new D2Profile().ShowEditor(init: true);
	}

	public void Edit(object sender, EventArgs e)
	{
		if (objectProfileList.SelectedObject is ProfileBase profileBase)
		{
			profileBase.ShowEditor(init: false);
		}
	}

	public ListViewItem GetSelected()
	{
		ListViewItem result = null;
		if (((ListView)(object)objectProfileList).SelectedIndices.Count > 0)
		{
			result = (ListViewItem)(object)objectProfileList.SelectedItem;
		}
		return result;
	}

	public void SetSelected(int index)
	{
		if (objectProfileList.Items.Count > index)
		{
			objectProfileList.Items[index].Selected = true;
		}
	}

	public static Color ColorFromInt(int num)
	{
		switch (num)
		{
		case 0:
		case 1:
		case 2:
		case 3:
			return Color.Black;
		case 4:
			return Color.Blue;
		case 5:
			return Color.Green;
		case 6:
			return Color.DarkGoldenrod;
		case 7:
			return Color.SaddleBrown;
		case 8:
			return Color.DarkOrange;
		case 9:
			return Color.Red;
		case 10:
			return Color.Gray;
		default:
			return Color.Black;
		}
	}

	public void ConsolePrint(PrintMessage pm, ProfileBase profile)
	{
		string text = null;
		string text2 = null;
		if (base.InvokeRequired)
		{
			ConsolePrintCallback method = ConsolePrint;
			BeginInvoke(method, pm, profile);
		}
		else if (pm.msg.Length != 0)
		{
			ConsoleBox.SelectionStart = ConsoleBox.Text.Length;
			ConsoleBox.ScrollToCaret();
			if (ConsoleBox.TextLength > 0 && !ConsoleBox.Text.EndsWith("\n"))
			{
				ConsoleBox.AppendText("\n");
			}
			text2 = DateTime.Now.ToString("HH:mm:ss ");
			if (profile != null && profile != null)
			{
				text2 = text2 + "(" + profile.Name + ") ";
			}
			text += pm.msg;
			ConsoleBox.SelectionColor = Color.Black;
			ConsoleBox.AppendText(text2);
			ConsoleBox.SelectionStart = ConsoleBox.TextLength;
			ConsoleBox.SelectionColor = ColorFromInt(pm.color);
			int num = -1;
			if (pm.trigger.Length > 0)
			{
				num = text.Length - text.IndexOf(pm.trigger) - pm.trigger.Length;
				text = text.Replace(pm.trigger, "");
			}
			ConsoleBox.AppendText(text);
			ConsoleBox.AppendText("\n");
			if (num > -1)
			{
				ConsoleBox.InsertLink(pm.trigger, pm.tooltip, ConsoleBox.Text.Length - num - 1);
			}
			ConsoleBox.SelectionStart = ConsoleBox.Text.Length;
			ConsoleBox.ScrollToCaret();
		}
	}

	public void ItemLogPrint(D2Item d2item, D2Profile profile = null)
	{
		if (base.InvokeRequired)
		{
			ItemLogPrintCallback method = ItemLogPrint;
			BeginInvoke(method, d2item, profile);
			return;
		}
		string text = "";
		string info = "";
		if (profile != null)
		{
			info = "(" + profile.Name + ")  ";
		}
		text = DateTime.Now.ToString("HH:mm:ss");
		ListViewItem value = d2item.ToItem().ListViewItem(text, info);
		ItemLogger.Items.Add(value);
		ItemLogger.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
		ItemLogger.EnsureVisible(ItemLogger.Items.Count - 1);
	}

	private void UpdateRuns(D2Profile profile)
	{
		profile.Runs++;
		if (profile.RunsPerKey > 0)
		{
			profile.KeyRuns++;
		}
		profile.NeedsUpdate = true;
	}

	public void SetStatus(ProfileBase profile, string message)
	{
		profile.State = message;
		profile.NeedsUpdate = true;
	}

	private void UpdateChickens(D2Profile profile)
	{
		profile.Chickens++;
		profile.NeedsUpdate = true;
	}

	private void UpdateDeaths(D2Profile profile)
	{
		profile.Deaths++;
		profile.NeedsUpdate = true;
	}

	private void UpdateCrashes(D2Profile profile)
	{
		profile.Crashes++;
		profile.NeedsUpdate = true;
	}

	public void UpdateRestarts(D2Profile profile)
	{
		profile.Restarts++;
		profile.NeedsUpdate = true;
	}

	public void UpdateMPQ(D2Profile profile)
	{
		profile.NeedsUpdate = true;
	}

	public void UpdateProfiles()
	{
		if (base.InvokeRequired)
		{
			BeginInvoke(new MethodInvoker(UpdateProfiles));
			return;
		}
		UpdateVisibleList();
		if (Program.Viewable.Count > 0)
		{
			objectProfileList.RefreshObjects((IList)Program.Viewable);
		}
	}

	public void UpdateDraw(bool show)
	{
		if (AprilFools.isToday())
		{
			if (base.InvokeRequired)
			{
				UpdateDrawCallback method = UpdateDraw;
				BeginInvoke(method, show);
			}
			else
			{
				AprilFools.Show(pictureBox1);
			}
		}
	}

	private void Main_Load(object sender, EventArgs e)
	{
	}

	public void Mouse_Over(object sender, MouseEventArgs e)
	{
		ListViewItem itemAt = (sender as ListView).GetItemAt(e.X, e.Y);
		if (itemAt != null && itemAt.ToolTipText != null)
		{
			Program.CurrentItem = (Item)itemAt.Tag;
			if (Program.Previous != itemAt)
			{
				Program.D2Tool.Active = false;
				Program.D2Tool.Active = true;
				Program.Previous = itemAt;
				Program.D2Tool.ShowD2Tooltip(itemAt.ToolTipText, sender as ListView, base.Top, base.Left, e.X, e.Y, isCentered: false);
			}
		}
		else
		{
			Program.D2Tool.Active = false;
			Program.D2Tool.Hide(this);
			Program.Previous = null;
		}
	}

	private void ItemLogger_MouseLeave(object sender, EventArgs e)
	{
		Program.D2Tool.Active = false;
		Program.D2Tool.Hide(this);
		Program.Previous = null;
	}

	private void Profile_Click(object sender, MouseEventArgs e)
	{
		Edit(null, null);
	}

	private void RemoveFromListToolStripMenuItem_Click(object sender, EventArgs e)
	{
		int count = KeyData.SelectedItems.Count;
		for (int i = 0; i < count; i++)
		{
			int index = KeyData.SelectedIndices[0];
			Program.RemoveFromLog(KeyData.Items[index].SubItems[0].Text, KeyData.Items[index].SubItems[1].Text);
			KeyData.Items[index].Remove();
		}
	}

	private void ClearDetailsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Program.ClearLog();
		KeyData.Items.Clear();
	}

	private void RemoveKeyToolStripMenuItem_Click(object sender, EventArgs e)
	{
		int count = KeyData.SelectedItems.Count;
		for (int i = 0; i < count; i++)
		{
			int index = KeyData.SelectedIndices[0];
			D2Profile validD2Profile = GetValidD2Profile(KeyData.Items[index].SubItems[0].Text);
			if (validD2Profile != null)
			{
				KeyList keyList = GetKeyList(validD2Profile.KeyList);
				foreach (CDKey cDKey in keyList.CDKeys)
				{
					if (KeyData.Items[index].SubItems[1].Text == cDKey.Name)
					{
						keyList.CDKeys.Remove(cDKey);
						break;
					}
				}
			}
			Program.RemoveFromLog(KeyData.Items[index].SubItems[0].Text, KeyData.Items[index].SubItems[1].Text);
			KeyData.Items[index].Remove();
		}
	}

	private void ClearItemsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		TabPage selectedTab = PrintTab.SelectedTab;
		if (selectedTab.Name == "charView")
		{
			CharItems.Items.Clear();
		}
		else if (selectedTab.Name == "Console")
		{
			ItemLogger.Items.Clear();
		}
	}

	private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ConsoleBox.Clear();
	}

	private void ToolItemSaveImage_Click(object sender, EventArgs e)
	{
		if (LastSelected != null)
		{
			ItemScreenShot.Take(LastSelected.Tag as Item);
		}
	}

	private void CreateDupeToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ProcessStartInfo startInfo = new ProcessStartInfo("CMD.exe");
		new Process();
		Process.Start(startInfo);
		MessageBox.Show("Error: dupe.ntj was not found!", "Invalid Object Reference", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
	}

	private void CopyImageToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (LastSelected == null)
		{
			return;
		}
		Image image = (LastSelected.Tag as Item).GenerateToolTip();
		try
		{
			Clipboard.Clear();
			Clipboard.SetImage(image);
		}
		catch
		{
		}
		finally
		{
			image?.Dispose();
		}
	}

	private void UploadImgurToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (LastSelected != null)
		{
			Thread thread = new Thread((ThreadStart)delegate
			{
				UploadToImgur();
			});
			thread.IsBackground = true;
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
		}
	}

	[STAThread]
	public string UploadToImgur(Item itm = null, bool show = true)
	{
		if (itm == null)
		{
			itm = LastSelected.Tag as Item;
		}
		string text = "";
		if (itm == null)
		{
			MessageBox.Show("Upload Failed, Item not found!", "Imgur", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		Image image = itm.GenerateToolTip();
		WebClient webClient = null;
		try
		{
			webClient = new WebClient();
			NameValueCollection nameValueCollection = new NameValueCollection();
			MemoryStream memoryStream = new MemoryStream();
			image.Save(memoryStream, ImageFormat.Png);
			nameValueCollection["image"] = Convert.ToBase64String(memoryStream.ToArray());
			webClient.Headers.Add("Authorization", "Client-ID 24d18153402171f");
			byte[] bytes = webClient.UploadValues("https://api.imgur.com/3/upload.xml", nameValueCollection);
			string input = Encoding.Default.GetString(bytes);
			text = new Regex("<link>(.*?)</link>").Match(input).Value;
			text = text.Replace("<link>", "").Replace("</link>", "");
			for (int i = 0; i < 5; Thread.Sleep(10), i++)
			{
				try
				{
					if (show)
					{
						Clipboard.Clear();
						Clipboard.SetText(text);
					}
					PrintMessage pm = new PrintMessage("<imgur>  " + itm.Name + ": " + text);
					ConsolePrint(pm, null);
				}
				catch
				{
					continue;
				}
				break;
			}
			if (show)
			{
				MessageBox.Show("Upload Complete" + Environment.NewLine + text, "Imgur", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch
		{
			if (show)
			{
				MessageBox.Show("Upload Failed", "Imgur", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		finally
		{
			webClient?.Dispose();
			image?.Dispose();
		}
		return text;
	}

	private void CopyDescriptionToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (LastSelected == null)
		{
			return;
		}
		string desc = (LastSelected.Tag as Item).Description.Replace("\n", "\r\n").Split('$')[0];
		desc = D2Tooltip.ReplaceColorCodes(desc);
		for (int i = 0; i < 5; i++)
		{
			try
			{
				Clipboard.Clear();
				Clipboard.SetText(desc);
				break;
			}
			catch
			{
			}
			Thread.Sleep(10);
		}
	}

	private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			if (MessageBox.Show("Are you sure you want to remove this item?", "Item Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
			{
				return;
			}
			Item item = LastSelected.Tag as Item;
			string[] array = item.Description.Replace("\n", "\r\n").Split('$');
			if (array.Length > 1 && item.Path != null && File.Exists(item.Path))
			{
				List<string> list = new List<string>(FileHandler.ReadLines(item.Path));
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].ToLower().Contains(array[1]))
					{
						list.RemoveAt(i);
						FileHandler.Write(item.Path, string.Join("\n", list));
						break;
					}
				}
			}
			if (database.ContainsKey(item.Searcheable))
			{
				database.TryRemove(item.Searcheable, out var _);
			}
			LastSelected.Remove();
		}
		catch
		{
		}
	}

	private static bool IsValidRegex(string pattern)
	{
		try
		{
			Regex.Match("", pattern);
		}
		catch (ArgumentException)
		{
			return false;
		}
		return true;
	}

	private void SearchEnter(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			QueryItem(null, null);
		}
	}

	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	protected override void WndProc(ref Message m)
	{
		if (m.Msg == 74)
		{
			MessageHelper.COPYDATASTRUCT cOPYDATASTRUCT = (MessageHelper.COPYDATASTRUCT)m.GetLParam(default(MessageHelper.COPYDATASTRUCT).GetType());
			try
			{
				IntPtr intPtr = new IntPtr(m.WParam.ToInt32());
				byte[] array = new byte[cOPYDATASTRUCT.cbData];
				Marshal.Copy(cOPYDATASTRUCT.lpData, array, 0, cOPYDATASTRUCT.cbData);
				string text = Encoding.UTF8.GetString(array);
				m.Result = (IntPtr)1;
				if (cOPYDATASTRUCT.dwData.ToInt32().Equals(48059) || text.Contains("heartBeat"))
				{
					if (Program.Runtime.TryGetValue(intPtr, out var value))
					{
						(value as D2Profile).HeartBeat();
					}
					else
					{
						HandleMessage(intPtr, text);
					}
				}
				else
				{
					int num = Math.Abs(intPtr.ToInt32()) % 10;
					Queue[num].Add(new Worker(intPtr, text));
				}
			}
			catch
			{
				m.Result = (IntPtr)1;
			}
		}
		base.WndProc(ref m);
	}

	public void WorkThread()
	{
		for (int i = 0; i < 10; i++)
		{
			int tmpId = i;
			Queue[tmpId] = new BlockingCollection<Worker>();
			Task.Factory.StartNew(delegate
			{
				foreach (Worker item in Queue[tmpId].GetConsumingEnumerable())
				{
					try
					{
						if (item != null)
						{
							if (item.action != ProfileAction.None)
							{
								HandleAction(item.d2p, item.action);
							}
							else if (item.handle != IntPtr.Zero)
							{
								HandleMessage(item.handle, item.msg);
							}
						}
					}
					catch (Exception e)
					{
						Program.LogCrash(e);
					}
				}
			});
		}
	}

	public HttpStatusCode HandleWebApi(string key, string message, out string response)
	{
		ProfileMessage profileMessage = null;
		WebUser webUser = null;
		D2Profile d2Profile = null;
		WebResponse webResponse = new WebResponse("invalid", "failed", "");
		HttpStatusCode result = HttpStatusCode.OK;
		try
		{
			profileMessage = JsonConvert.DeserializeObject<ProfileMessage>(message);
			if (WebServer.users.ContainsKey(profileMessage.profile))
			{
				webUser = WebServer.users[profileMessage.profile];
			}
		}
		catch (Exception e)
		{
			Program.LogCrash(e, "", show: false);
			response = JsonConvert.SerializeObject((object)webResponse);
			return HttpStatusCode.BadRequest;
		}
		webResponse.request = profileMessage.func;
		if (webUser == null || webUser.flag < 0)
		{
			webResponse.body = "invalid user";
			response = JsonConvert.SerializeObject((object)webResponse);
			return HttpStatusCode.OK;
		}
		string text = ((profileMessage.session == "null") ? null : AES.Decrypt(profileMessage.session, webUser.apikey));
		if (webUser.apikey.Length > 0 && profileMessage.func != "challenge" && text != key)
		{
			webResponse.body = "invalid session";
			response = JsonConvert.SerializeObject((object)webResponse);
			return HttpStatusCode.OK;
		}
		if (profileMessage.func.Equals("challenge"))
		{
			webResponse.status = "success";
			webResponse.body = key;
			response = JsonConvert.SerializeObject((object)webResponse);
			return HttpStatusCode.OK;
		}
		if (webUser.flag > 0)
		{
			Program.WS.AddPoll(profileMessage.profile);
			switch (profileMessage.func.ToLower())
			{
			case "registerevent":
				if (profileMessage.args.Length > 1 && profileMessage.args[0] != null && profileMessage.args[0].Length > 0 && WebEvents.RegisterEvent(profileMessage.args[0], profileMessage.args[1]))
				{
					webResponse.status = "success";
					webResponse.body = "event has been registered";
					result = HttpStatusCode.OK;
				}
				else
				{
					webResponse.status = "failed";
					webResponse.body = "event does not exist";
				}
				break;
			case "poll":
			{
				List<WebResponse> list2 = new List<WebResponse>();
				WebResponse wr = null;
				webResponse.body = "empty";
				while (Program.WS.DequeueNotify(profileMessage.profile, out wr))
				{
					list2.Add(wr);
				}
				if (list2.Count > 0)
				{
					webResponse.body = JsonConvert.SerializeObject((object)list2);
				}
				webResponse.status = "success";
				result = HttpStatusCode.OK;
				break;
			}
			case "ping":
				webResponse.status = "success";
				webResponse.body = "{\"pid\":" + Process.GetCurrentProcess().Id + "}";
				result = HttpStatusCode.OK;
				break;
			case "get":
				if (profileMessage.args.Length != 0)
				{
					if (!profileMessage.args[0].Contains("..") && File.Exists(Program.BOT_LIB + "\\data\\web\\" + profileMessage.args[0]))
					{
						string text2 = File.ReadAllText(Program.BOT_LIB + "\\data\\web\\" + profileMessage.args[0]);
						if (webUser.apikey.Length > 0)
						{
							text2 = AES.Encrypt(text2, webUser.apikey);
						}
						webResponse.status = "success";
						webResponse.body = text2;
						result = HttpStatusCode.OK;
					}
					else
					{
						webResponse.status = "failed";
						webResponse.body = "incorrect arguments";
					}
				}
				else
				{
					webResponse.status = "failed";
					webResponse.body = "incorrect arguments";
				}
				break;
			case "put":
				if (profileMessage.args.Length > 2 && (profileMessage.args[0].Equals("web") || profileMessage.args[0].Equals("secure")))
				{
					string fileName = Path.GetFileName(profileMessage.args[1]);
					string value = ((webUser.apikey.Length > 0) ? AES.Decrypt(profileMessage.args[2], webUser.apikey) : profileMessage.args[2]);
					FileHandler.Write(Program.BOT_LIB + "\\data\\" + profileMessage.args[0] + "\\" + fileName, value);
					webResponse.status = "success";
					webResponse.body = profileMessage.args[1];
					result = HttpStatusCode.OK;
				}
				else
				{
					webResponse.status = "failed";
					webResponse.body = "incorrect arguments";
				}
				break;
			case "store":
				if (profileMessage.args.Length == 2)
				{
					Program.DataCache[profileMessage.args[0]] = profileMessage.args[1];
					webResponse.status = "success";
					webResponse.body = profileMessage.args[1];
					result = HttpStatusCode.OK;
				}
				else
				{
					webResponse.status = "failed";
					webResponse.body = "incorrect arguments";
				}
				break;
			case "retrieve":
				if (profileMessage.args.Length == 1 && Program.DataCache.ContainsKey(profileMessage.args[0]))
				{
					webResponse.status = "success";
					webResponse.body = Program.DataCache[profileMessage.args[0]];
					result = HttpStatusCode.OK;
				}
				else
				{
					webResponse.status = "failed";
					webResponse.body = "content does not exist";
				}
				break;
			case "delete":
				if (profileMessage.args.Length == 1 && Program.DataCache.ContainsKey(profileMessage.args[0]))
				{
					Program.DataCache.TryRemove(profileMessage.args[0], out var value2);
					webResponse.status = "success";
					webResponse.body = value2;
					result = HttpStatusCode.OK;
				}
				else
				{
					webResponse.status = "failed";
					webResponse.body = "incorrect arguments";
				}
				break;
			case "profiles":
			{
				List<D2ProfileExport> list = new List<D2ProfileExport>();
				foreach (ProfileBase value3 in Program.ProfileList.Values)
				{
					D2ProfileExport d2ProfileExport = value3.Export();
					if (d2ProfileExport != null)
					{
						list.Add(d2ProfileExport);
					}
				}
				webResponse.status = "success";
				webResponse.body = JsonConvert.SerializeObject((object)list);
				break;
			}
			case "settag":
				if (profileMessage.args.Length > 1)
				{
					d2Profile = GetValidD2Profile(profileMessage.args[0]);
					if (d2Profile != null)
					{
						if (profileMessage.args.Length > 1)
						{
							d2Profile.InfoTag = profileMessage.args[1];
						}
						webResponse.status = "success";
						webResponse.body = JsonConvert.SerializeObject((object)d2Profile);
						result = HttpStatusCode.OK;
					}
				}
				else
				{
					webResponse.status = "failed";
					webResponse.body = "incorrect arguments";
				}
				break;
			case "start":
				if (profileMessage.args.Length != 0)
				{
					d2Profile = GetValidD2Profile(profileMessage.args[0]);
					if (d2Profile != null)
					{
						if (profileMessage.args.Length > 1)
						{
							d2Profile.InfoTag = profileMessage.args[1];
						}
						d2Profile.Load();
						webResponse.status = "success";
						webResponse.body = profileMessage.args[1];
						result = HttpStatusCode.OK;
					}
				}
				else
				{
					webResponse.status = "failed";
					webResponse.body = "incorrect arguments";
				}
				break;
			case "stop":
				if (profileMessage.args.Length != 0)
				{
					d2Profile = GetValidD2Profile(profileMessage.args[0]);
					if (d2Profile != null)
					{
						d2Profile.Stop();
						if (profileMessage.args.Length > 1 && Convert.ToBoolean(profileMessage.args[1]))
						{
							d2Profile.ReleaseKey();
						}
						webResponse.status = "success";
						webResponse.body = profileMessage.args[1];
						result = HttpStatusCode.OK;
					}
				}
				else
				{
					webResponse.status = "failed";
					webResponse.body = "incorrect arguments";
				}
				break;
			case "emit":
				if (profileMessage.args.Length > 1)
				{
					d2Profile = GetValidD2Profile(profileMessage.args[0]);
					if (d2Profile != null)
					{
						Program.SendCopyData(d2Profile.D2Process.MainWindowHandle, JsonConvert.SerializeObject((object)profileMessage.args[1]), (IntPtr)420);
						webResponse.status = "success";
						webResponse.body = JsonConvert.SerializeObject((object)d2Profile);
						result = HttpStatusCode.OK;
					}
				}
				else
				{
					webResponse.status = "failed";
					webResponse.body = "incorrect arguments";
				}
				break;
			case "gameaction":
				if (profileMessage.args != null && profileMessage.args.Length > 1 && !string.IsNullOrEmpty(profileMessage.args[0]) && !string.IsNullOrEmpty(profileMessage.args[1]))
				{
					Program.WS.AddGameAction(profileMessage.args[0], profileMessage.args[1]);
					webResponse.status = "success";
					webResponse.body = profileMessage.args[1];
					result = HttpStatusCode.OK;
				}
				else
				{
					webResponse.status = "failed";
					webResponse.body = "incorrect arguments";
				}
				break;
			}
		}
		bool flag = true;
		if (webUser.flag > -1)
		{
			switch (profileMessage.func)
			{
			case "accounts":
			{
				string text6 = Program.BOT_LIB + "\\mules\\";
				if (profileMessage.args.Length == 1 && !profileMessage.args[0].Contains(".."))
				{
					text6 = text6 + profileMessage.args[0] + "\\";
				}
				if (Directory.Exists(text6))
				{
					string[] files = Directory.GetFiles(text6, "*.txt", SearchOption.AllDirectories);
					text6 = Program.BOT_LIB + "\\mules\\";
					for (int num = 0; num < files.Length; num++)
					{
						files[num] = files[num].Substring(text6.Length, files[num].Length - 4 - text6.Length);
					}
					webResponse.status = "success";
					webResponse.body = JsonConvert.SerializeObject((object)files);
					result = HttpStatusCode.OK;
				}
				else
				{
					webResponse.status = "failed";
					webResponse.body = "incorrect arguments";
				}
				break;
			}
			case "fastQuery":
				flag = false;
				goto case "query";
			case "query":
				if (profileMessage.args.Length > 1)
				{
					Regex r = null;
					if (!string.IsNullOrEmpty(profileMessage.args[0]) && IsValidRegex(profileMessage.args[0]))
					{
						r = new Regex(profileMessage.args[0], RegexOptions.Compiled);
					}
					SafeList<WebItem> result2 = new SafeList<WebItem>(100);
					List<TreeNode> list3 = null;
					if (profileMessage.args.Length > 3)
					{
						string[] array = profileMessage.args[3].Split(',');
						list3 = new List<TreeNode>(array.Length);
						string[] array2 = array;
						foreach (string text3 in array2)
						{
							list3.Add(CreateNode(Program.BOT_LIB + "\\mules\\" + profileMessage.args[1] + "\\" + profileMessage.args[2] + "\\" + text3 + ".txt", isFile: true));
						}
					}
					else if (profileMessage.args.Length > 2)
					{
						string[] array3 = profileMessage.args[2].Split(',');
						list3 = new List<TreeNode>(array3.Length);
						string[] array2 = array3;
						foreach (string text4 in array2)
						{
							list3.Add(CreateNode(Program.BOT_LIB + "\\mules\\" + profileMessage.args[1] + "\\" + text4));
						}
					}
					else
					{
						string[] array4 = profileMessage.args[1].Split(',');
						list3 = new List<TreeNode>(array4.Length);
						string[] array2 = array4;
						foreach (string text5 in array2)
						{
							list3.Add(CreateNode(Program.BOT_LIB + "\\mules\\" + text5));
						}
					}
					if (list3 != null)
					{
						Parallel.ForEach(list3, delegate(TreeNode tn)
						{
							SearchNodeItems(tn, r, result2);
						});
						if (flag)
						{
							Parallel.ForEach(result2.GetList(), delegate(WebItem wi)
							{
								wi.GenerateImage();
							});
						}
						webResponse.status = "success";
						webResponse.body = JsonConvert.SerializeObject((object)result2.GetList());
					}
					else
					{
						webResponse.status = "failed";
						webResponse.body = "invalid argument format";
					}
				}
				else
				{
					webResponse.status = "failed";
					webResponse.body = "invalid argument count";
				}
				result = HttpStatusCode.OK;
				break;
			case "validate":
				webResponse.status = "success";
				webResponse.body = "apikey is valid";
				result = HttpStatusCode.OK;
				break;
			}
		}
		response = JsonConvert.SerializeObject((object)webResponse);
		return result;
	}

	public void HandleAction(D2Profile d2p, ProfileAction pa)
	{
		switch (pa)
		{
		case ProfileAction.Stop:
			d2p.StopThread();
			break;
		case ProfileAction.Show:
			d2p.ShowWindowThread();
			break;
		case ProfileAction.Hide:
			d2p.HideWindowThread();
			break;
		case ProfileAction.Start:
			break;
		}
	}

	public void HandleMessage(IntPtr hWnd, string message)
	{
		ProfileMessage profileMessage = null;
		try
		{
			profileMessage = JsonConvert.DeserializeObject<ProfileMessage>(message);
		}
		catch (Exception e)
		{
			Program.LogCrash(e, "", show: false);
			return;
		}
		if (profileMessage == null)
		{
			return;
		}
		D2Profile d2Profile = null;
		if (!Program.Runtime.TryGetValue(hWnd, out var value) || !(value is D2Profile { D2Process: not null, Status: Status.Run } d2Profile2))
		{
			return;
		}
		string func = profileMessage.func;
		if (func == null)
		{
			return;
		}
		switch (func)
		{
		case "loadDLL":
			Kernel32.LoadRemoteLibrary(d2Profile2.D2Process, Application.StartupPath + "\\data\\" + Path.GetFileName(profileMessage.args[0]));
			break;
		case "startEXE":
			if (profileMessage.args.Length != 0 && profileMessage.args[0].Length > 0)
			{
				ProcessStartInfo processStartInfo = new ProcessStartInfo
				{
					FileName = Application.StartupPath + "\\data\\" + Path.GetFileName(profileMessage.args[0])
				};
				if (profileMessage.args.Length > 1 && profileMessage.args[1].Length > 0)
				{
					processStartInfo.Arguments = profileMessage.args[1];
				}
				Process.Start(processStartInfo);
			}
			break;
		case "heartBeat":
			d2Profile2.HeartBeat();
			break;
		case "shoutGlobal":
			if (profileMessage.args.Length > 1 && profileMessage.args[0].Length > 0 && profileMessage.args[1].Length > 0)
			{
				Program.ShoutGlobal(profileMessage.args[0], (IntPtr)int.Parse(profileMessage.args[1]));
			}
			break;
		case "updateStatus":
			if (profileMessage.args.Length != 0)
			{
				Program.GM.SetStatus(d2Profile2, profileMessage.args[0]);
			}
			break;
		case "updateRuns":
			UpdateRuns(d2Profile2);
			break;
		case "updateChickens":
			UpdateChickens(d2Profile2);
			break;
		case "updateDeaths":
			UpdateDeaths(d2Profile2);
			break;
		case "CDKeyInUse":
			if (d2Profile2 != null && d2Profile2.CurrentKey != null)
			{
				string text4 = d2Profile2.CurrentKey.Name + " was in use! ";
				Program.AddToKeyinfo("[" + DateTime.Now.ToString("HH:mm:ss tt") + "] <" + d2Profile2.Name + "> " + text4);
			}
			break;
		case "CDKeyDisabled":
			if (d2Profile2 != null && d2Profile2.CurrentKey != null)
			{
				string text3 = d2Profile2.CurrentKey.Name + " was disabled! ";
				Program.AddToKeyinfo("[" + DateTime.Now.ToString("HH:mm:ss tt") + "] <" + d2Profile2.Name + "> " + text3);
			}
			break;
		case "CDKeyRD":
			if (d2Profile2 != null && d2Profile2.CurrentKey != null)
			{
				string text = d2Profile2.CurrentKey.Name + " realm down! ";
				Program.AddToKeyinfo("[" + DateTime.Now.ToString("HH:mm:ss tt") + "] <" + d2Profile2.Name + "> " + text);
			}
			break;
		case "printToConsole":
			if (profileMessage.args.Length != 0 && profileMessage.args[0].Length > 0)
			{
				PrintMessage pm = JsonConvert.DeserializeObject<PrintMessage>(profileMessage.args[0]);
				ConsolePrint(pm, d2Profile2);
			}
			break;
		case "printToItemLog":
			if (profileMessage.args.Length != 0 && profileMessage.args[0].Length > 0)
			{
				D2Item d2item = JsonConvert.DeserializeObject<D2Item>(profileMessage.args[0]);
				ItemLogPrint(d2item, d2Profile2);
			}
			break;
		case "saveItem":
			if (profileMessage.args.Length != 0 && profileMessage.args[0].Length > 0)
			{
				ItemScreenShot.Take(JsonConvert.DeserializeObject<D2Item>(profileMessage.args[0]));
			}
			break;
		case "uploadItem":
		{
			D2Item d2Item = JsonConvert.DeserializeObject<D2Item>(profileMessage.args[0]);
			string text2 = UploadToImgur(d2Item.ToItem(), show: false);
			if (text2.Length > 0)
			{
				Program.SendCopyData(d2Profile2.D2Process.MainWindowHandle, text2, (IntPtr)2559);
			}
			else
			{
				Program.SendCopyData(d2Profile2.D2Process.MainWindowHandle, "@imgur", (IntPtr)2559);
			}
			break;
		}
		case "restartProfile":
			if (profileMessage.args != null && profileMessage.args.Length > 1)
			{
				PrintMessage pm = ((profileMessage.args.Length <= 2) ? new PrintMessage(d2Profile2.CurrentKey.Name + " was last used ... Restarting with next keyset") : new PrintMessage(profileMessage.args[2] + "... Restarting with next keyset"));
				ConsolePrint(pm, d2Profile2);
				d2Profile2.Restart(increment: true);
			}
			else
			{
				PrintMessage pm = new PrintMessage("Restarting");
				ConsolePrint(pm, d2Profile2);
				d2Profile2.Restart();
			}
			break;
		case "requestGameInfo":
		{
			GameInfo gameInfo = new GameInfo(d2Profile2);
			Program.SendCopyData(d2Profile2.D2Process.MainWindowHandle, JsonConvert.SerializeObject((object)gameInfo), (IntPtr)2);
			d2Profile2.Error = false;
			break;
		}
		case "getLastError":
			if (d2Profile2.Error)
			{
				Program.SendCopyData(d2Profile2.D2Process.MainWindowHandle, "@error", (IntPtr)4);
				d2Profile2.Error = false;
			}
			else
			{
				Program.SendCopyData(d2Profile2.D2Process.MainWindowHandle, "@none", (IntPtr)4);
			}
			break;
		case "stopSchedule":
			if (profileMessage.args.Length != 0)
			{
				D2Profile validD2Profile2 = GetValidD2Profile(profileMessage.args[0]);
				if (validD2Profile2 != null)
				{
					validD2Profile2.ScheduleEnable = false;
				}
			}
			else
			{
				d2Profile2.ScheduleEnable = false;
			}
			break;
		case "startSchedule":
			if (profileMessage.args.Length != 0)
			{
				D2Profile validD2Profile = GetValidD2Profile(profileMessage.args[0]);
				if (validD2Profile != null)
				{
					validD2Profile.ScheduleEnable = true;
				}
			}
			else
			{
				d2Profile2.ScheduleEnable = true;
			}
			break;
		case "stop":
		{
			bool flag = false;
			D2Profile d2Profile3 = d2Profile2;
			if (profileMessage.args.Length == 2 && Convert.ToBoolean(profileMessage.args[1]))
			{
				flag = true;
			}
			if (profileMessage.args.Length != 0)
			{
				d2Profile3 = GetValidD2Profile(profileMessage.args[0]);
				if (d2Profile3 == null)
				{
					d2Profile3 = d2Profile2;
				}
			}
			d2Profile3.Stop();
			if (flag)
			{
				d2Profile3.ReleaseKey();
			}
			break;
		}
		case "start":
		{
			if (profileMessage.args.Length == 0)
			{
				break;
			}
			D2Profile validD2Profile3 = GetValidD2Profile(profileMessage.args[0]);
			if (validD2Profile3 != null)
			{
				if (profileMessage.args.Length > 1)
				{
					validD2Profile3.InfoTag = profileMessage.args[1];
				}
				validD2Profile3.Load();
			}
			break;
		}
		case "postToIRC":
		{
			if (profileMessage.args.Length == 0)
			{
				break;
			}
			IRCProfile validIRCProfile = GetValidIRCProfile(profileMessage.args[0]);
			if (validIRCProfile != null && validIRCProfile.StatusLock.TryEnterReadLock(0))
			{
				if (validIRCProfile.Status == Status.Run)
				{
					validIRCProfile.PostMsg(profileMessage.args[2], profileMessage.args[1]);
				}
				validIRCProfile.StatusLock.ExitReadLock();
			}
			break;
		}
		case "notify":
			notifyIcon.BalloonTipText = profileMessage.args[0];
			notifyIcon.Visible = true;
			notifyIcon.ShowBalloonTip(500);
			break;
		case "ircEvent":
			if (profileMessage.args[0] == "false")
			{
				d2Profile2.IRCEvent = false;
				PrintMessage pm = new PrintMessage("IRC Event Unsubscribed");
				ConsolePrint(pm, d2Profile2);
			}
			else
			{
				d2Profile2.IRCEvent = true;
				PrintMessage pm = new PrintMessage("IRC Event Subscribed");
				ConsolePrint(pm, d2Profile2);
			}
			break;
		case "store":
			if (profileMessage.args.Length == 2 && !string.IsNullOrEmpty(profileMessage.args[0]))
			{
				Program.DataCache[profileMessage.args[0]] = profileMessage.args[1];
			}
			break;
		case "retrieve":
			if (profileMessage.args.Length < 1 || !Program.DataCache.ContainsKey(profileMessage.args[0]))
			{
				Program.SendCopyData(d2Profile2.D2Process.MainWindowHandle, "null", (IntPtr)61732);
			}
			else
			{
				Program.SendCopyData(d2Profile2.D2Process.MainWindowHandle, Program.DataCache[profileMessage.args[0]], (IntPtr)61732);
			}
			break;
		case "delete":
			if (profileMessage.args.Length == 1 && Program.DataCache.ContainsKey(profileMessage.args[0]))
			{
				Program.DataCache.TryRemove(profileMessage.args[0], out var _);
			}
			break;
		case "winmsg":
		{
			if (profileMessage.args.Length < 2)
			{
				break;
			}
			uint result = 0u;
			if (uint.TryParse(profileMessage.args[0], out result))
			{
				int result2 = 0;
				if (int.TryParse(profileMessage.args[1], out result2))
				{
					IntPtr wParam = (IntPtr)result2;
					MessageHelper.SendMessageTimeout(d2Profile2.D2Process.MainWindowHandle, result, wParam, IntPtr.Zero, MessageHelper.SendMessageTimeoutFlags.SMTO_NOTIMEOUTIFNOTHUNG, 250u, out var _);
				}
			}
			break;
		}
		case "getProfile":
		{
			D2Profile d2Profile3 = d2Profile2;
			if (profileMessage.args.Length != 0)
			{
				D2Profile validD2Profile4 = GetValidD2Profile(profileMessage.args[0]);
				if (validD2Profile4 != null)
				{
					d2Profile3 = validD2Profile4;
				}
			}
			D2ProfileExport d2ProfileExport = d2Profile3.Export();
			Program.SendCopyData(d2Profile2.D2Process.MainWindowHandle, JsonConvert.SerializeObject((object)d2ProfileExport), (IntPtr)1638);
			d2Profile2.Error = false;
			break;
		}
		case "setProfile":
			Program.GM.objectLock.WaitOne();
			if (profileMessage.args.Length != 0 && !string.IsNullOrEmpty(profileMessage.args[0]))
			{
				d2Profile2.Account = profileMessage.args[0];
			}
			if (profileMessage.args.Length > 1 && !string.IsNullOrEmpty(profileMessage.args[1]))
			{
				d2Profile2.Password = profileMessage.args[1];
			}
			if (profileMessage.args.Length > 2 && !string.IsNullOrEmpty(profileMessage.args[2]))
			{
				d2Profile2.Character = profileMessage.args[2];
			}
			if (profileMessage.args.Length > 3 && !string.IsNullOrEmpty(profileMessage.args[3]))
			{
				d2Profile2.Difficulty = profileMessage.args[3];
			}
			if (profileMessage.args.Length > 4 && !string.IsNullOrEmpty(profileMessage.args[4]))
			{
				d2Profile2.Realm = profileMessage.args[4];
			}
			if (profileMessage.args.Length > 5 && !string.IsNullOrEmpty(profileMessage.args[5]))
			{
				d2Profile2.InfoTag = profileMessage.args[5];
			}
			if (profileMessage.args.Length > 6 && !string.IsNullOrEmpty(profileMessage.args[6]))
			{
				d2Profile2.D2Path = profileMessage.args[6];
			}
			Program.GM.objectProfileList.RefreshObject((object)d2Profile2);
			Program.GM.objectLock.ReleaseMutex();
			Program.SaveProfiles();
			break;
		case "setTag":
			if (profileMessage.args.Length != 0 && !string.IsNullOrEmpty(profileMessage.args[0]))
			{
				Program.GM.objectLock.WaitOne();
				d2Profile2.InfoTag = profileMessage.args[0];
				Program.GM.objectProfileList.RefreshObject((object)d2Profile2);
				Program.GM.objectLock.ReleaseMutex();
				Program.SaveProfiles();
				WebEvents.EmitEvent("setTag", JsonConvert.SerializeObject((object)d2Profile2.Export()));
			}
			break;
		case "setNotify":
			if (profileMessage.args.Length != 0 && !string.IsNullOrEmpty(profileMessage.args[0]))
			{
				string status = "success";
				if (profileMessage.args.Length > 1 && !string.IsNullOrEmpty(profileMessage.args[1]))
				{
					status = profileMessage.args[1];
				}
				GameAction gameAction = JsonConvert.DeserializeObject<GameAction>(profileMessage.args[0]);
				Program.WS.QueueNotify(gameAction.profile, new WebResponse("GameActionNotify", status, profileMessage.args[0]));
			}
			break;
		}
	}

	private void ItemLogger_SelectedIndexChanged(object sender, EventArgs e)
	{
		int num = -1;
		try
		{
			num = ItemLogger.SelectedIndices[0];
		}
		catch
		{
			return;
		}
		LastSelected = ItemLogger.Items[num];
		ItemLogger.Items[num].Selected = false;
	}

	private void CharLogger_SelectedIndexChanged(object sender, EventArgs e)
	{
		int num = -1;
		try
		{
			if (CharItems.SelectedIndices.Count > 0)
			{
				num = CharItems.SelectedIndices[0];
			}
		}
		catch
		{
			return;
		}
		if (num >= 0)
		{
			LastSelected = CharItems.Items[num];
			CharItems.Items[num].Selected = false;
		}
	}

	private void muleProfileToolStripMenuItem_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < objectProfileList.SelectedObjects.Count; i++)
		{
			if (objectProfileList.SelectedObjects[i] is D2Profile { Status: Status.Run } d2Profile)
			{
				Program.SendCopyData(d2Profile.D2Process.MainWindowHandle, "mule", IntPtr.Zero);
			}
		}
	}

	private void pauseKeyToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (dupeList.SelectedCells.Count > 0 && dupeList.SelectedCells[0].ColumnIndex == 0)
		{
			string text = dupeList.SelectedCells[0].Value as string;
			Program.HoldKeyList.Add(text.ToLower().Trim());
			dupeList.SelectedCells[0].Style.BackColor = Color.Yellow;
		}
	}

	private void resumeKeyToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (dupeList.SelectedCells.Count > 0 && dupeList.SelectedCells[0].ColumnIndex == 0)
		{
			string text = dupeList.SelectedCells[0].Value as string;
			if (Program.HoldKeyList.Contains(text.ToLower().Trim()))
			{
				Program.HoldKeyList.Remove(text.ToLower().Trim());
				dupeList.SelectedCells[0].Style.BackColor = Color.White;
			}
		}
	}

	private void resetStatsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < objectProfileList.SelectedObjects.Count; i++)
		{
			if (objectProfileList.SelectedObjects[i] is ProfileBase { Type: not ProfileType.IRC } profileBase)
			{
				D2Profile d2Profile = (D2Profile)profileBase;
				d2Profile.Runs = 0;
				d2Profile.Chickens = 0;
				d2Profile.Restarts = 0;
				d2Profile.Crashes = 0;
				d2Profile.KeyRuns = 0;
				d2Profile.Deaths = 0;
				objectProfileList.RefreshObject((object)d2Profile);
			}
		}
		Program.SaveProfiles();
	}

	private void disableScheduleToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (objectProfileList.SelectedObjects == null)
		{
			return;
		}
		for (int i = 0; i < objectProfileList.SelectedObjects.Count; i++)
		{
			if (objectProfileList.SelectedObjects[i] is D2Profile { Type: not ProfileType.IRC } d2Profile)
			{
				d2Profile.ScheduleEnable = false;
				d2Profile.Stop();
			}
		}
	}

	private void enableScheduleToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (objectProfileList.SelectedObjects == null)
		{
			return;
		}
		for (int i = 0; i < objectProfileList.SelectedObjects.Count; i++)
		{
			if (objectProfileList.SelectedObjects[i] is D2Profile { Type: not ProfileType.IRC } d2Profile)
			{
				d2Profile.ScheduleEnable = true;
			}
		}
	}

	private void toolStripMenuItem3_Click(object sender, EventArgs e)
	{
		ConsoleBox.SaveFile(Application.StartupPath + "\\logs\\Console.rtf");
	}

	private void importToolStripMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			ConsoleBox.LoadFile(Application.StartupPath + "\\logs\\Console.rtf");
		}
		catch
		{
		}
	}

	private void nextKeyToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (objectProfileList.SelectedObjects == null)
		{
			return;
		}
		for (int i = 0; i < objectProfileList.SelectedObjects.Count; i++)
		{
			if (objectProfileList.SelectedObjects[i] is D2Profile { Type: not ProfileType.IRC } d2Profile)
			{
				d2Profile.IncrementKey();
			}
		}
	}

	private void releaseKeyToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (objectProfileList.SelectedObjects == null)
		{
			return;
		}
		for (int i = 0; i < objectProfileList.SelectedObjects.Count; i++)
		{
			if (objectProfileList.SelectedObjects[i] is D2Profile { Type: not ProfileType.IRC } d2Profile)
			{
				d2Profile.ReleaseKey();
			}
		}
	}

	private void SetUpTrayIcon()
	{
		notifyIcon = new NotifyIcon
		{
			BalloonTipText = "Click to Maximize",
			BalloonTipTitle = "D2Bot #",
			Text = "D2Bot #",
			Icon = base.Icon
		};
		notifyIcon.Click += HandlerToMaximiseOnClick;
		notifyIcon.Visible = true;
	}

	private void Main_ResizeEnd(object sender, EventArgs e)
	{
		notifyIcon.BalloonTipText = "Click to Maximize";
		notifyIcon.Visible = true;
		notifyIcon.ShowBalloonTip(100);
		Hide();
	}

	private void HandlerToMaximiseOnClick(object sender, EventArgs e)
	{
		Show();
		base.WindowState = FormWindowState.Normal;
	}

	private void PrintTab_Selected(object sender, TabControlEventArgs e)
	{
		string text = e.TabPage.Text;
		if (text != null && text == "Key Wizard")
		{
			DupeListSelected();
		}
	}

	public void ShowCharLog(ListViewItem[] list)
	{
		if (base.InvokeRequired)
		{
			ShowCharLogCallback method = ShowCharLog;
			BeginInvoke(method, new object[1] { list });
			return;
		}
		if (list != null)
		{
			CharItems.Items.Clear();
			if (list.Length != 0)
			{
				CharItems.Items.AddRange(list);
			}
		}
		SearchBox.Enabled = true;
	}

	public void DupeListSelected()
	{
		Thread thread = new Thread((ThreadStart)delegate
		{
			DupeListSelectedThread();
		});
		thread.IsBackground = true;
		thread.Start();
	}

	private void DupeListSelectedThread()
	{
		ParseD2BotLog();
		Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
		foreach (KeyList value in Program.KeyLists.Values)
		{
			foreach (CDKey cDKey in value.CDKeys)
			{
				if (!dictionary.ContainsKey(cDKey.Name))
				{
					dictionary.Add(cDKey.Name, new List<string>());
				}
				dictionary[cDKey.Name].Add(value.Name);
			}
		}
		List<string> list = dictionary.Keys.ToList();
		list.Sort();
		List<DataGridViewRow> list2 = new List<DataGridViewRow>();
		foreach (string item2 in list)
		{
			DataGridViewRow dataGridViewRow = new DataGridViewRow();
			DataGridViewTextBoxCell dataGridViewTextBoxCell = new DataGridViewTextBoxCell();
			DataGridViewTextBoxCell dataGridViewTextBoxCell2 = new DataGridViewTextBoxCell();
			DataGridViewComboBoxCell dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
			string[] array = dictionary[item2].ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				string item = (string)(dataGridViewTextBoxCell2.Value = array[i]);
				dataGridViewComboBoxCell.Items.Add(item);
			}
			dataGridViewComboBoxCell.FlatStyle = FlatStyle.Popup;
			dataGridViewComboBoxCell.Value = dataGridViewComboBoxCell.Items[0];
			if (Program.HoldKeyList.Contains(item2))
			{
				dataGridViewTextBoxCell.Style.BackColor = Color.Yellow;
			}
			dataGridViewTextBoxCell.Value = item2;
			dataGridViewRow.Cells.Add(dataGridViewTextBoxCell);
			if (dataGridViewComboBoxCell.Items.Count > 1)
			{
				dataGridViewRow.Cells.Add(dataGridViewComboBoxCell);
			}
			else
			{
				dataGridViewRow.Cells.Add(dataGridViewTextBoxCell2);
			}
			dataGridViewRow.Height = 18;
			list2.Add(dataGridViewRow);
		}
		DupeListAdd(list2.ToArray());
	}

	public void DupeListAdd(DataGridViewRow[] list)
	{
		if (base.InvokeRequired)
		{
			DupeListCallback method = DupeListAdd;
			BeginInvoke(method, new object[1] { list });
		}
		else
		{
			dupeList.RowCount = 0;
			dupeList.Rows.AddRange(list);
		}
	}

	private void RestartWatch()
	{
		keepAlive = false;
		for (int i = 0; i < 5; i++)
		{
			if (!ItemLoading.IsAlive)
			{
				break;
			}
			Thread.Sleep(100);
		}
		if (ItemLoading.IsAlive)
		{
			try
			{
				ItemLoading.Abort();
			}
			catch
			{
			}
		}
		CharLogMutex.WaitOne();
		descriptions.Clear();
		database.Clear();
		active.Clear();
		nodeCache.Clear();
		EventBuffer.Clear();
		EventSync.Dispose();
		watcher.Dispose();
		CharItems.Items.Clear();
		CharTree.Nodes.Clear();
		CharLogMutex.ReleaseMutex();
		ItemLoading = new Thread(WatchInit)
		{
			IsBackground = true
		};
		ItemLoading.Start();
	}

	public void WatchInit()
	{
		descriptions = new HashSet<string>();
		database = new ConcurrentDictionary<string, ListViewItem>();
		active = new Dictionary<string, ListViewItem>();
		nodeCache = new ConcurrentDictionary<string, TreeNode>();
		EventBuffer = new Queue<string>();
		EventSync = new Mutex();
		CharTree.TreeViewNodeSorter = new NodeSorter();
		CharTree.Sorted = true;
		if (!Directory.Exists(Program.BOT_LIB + "\\mules\\"))
		{
			return;
		}
		try
		{
			ListDirectories(CharTree, Program.BOT_LIB + "\\mules\\");
		}
		catch
		{
		}
		watcher = new FileSystemWatcher(Program.BOT_LIB + "\\mules\\")
		{
			Filter = "*.txt",
			IncludeSubdirectories = true,
			InternalBufferSize = 64000
		};
		watcher.Changed += OnChanged;
		watcher.Created += OnChanged;
		watcher.Deleted += OnDeleted;
		watcher.EnableRaisingEvents = true;
		string text = null;
		keepAlive = true;
		while (keepAlive)
		{
			Thread.Sleep(100);
			if (EventBuffer.Count <= 0)
			{
				continue;
			}
			EventSync.WaitOne();
			if (EventBuffer.Count > 0)
			{
				text = EventBuffer.Dequeue();
			}
			EventSync.ReleaseMutex();
			if (string.IsNullOrEmpty(text))
			{
				continue;
			}
			for (int i = 0; i < 10; i++)
			{
				try
				{
					if (Directory.Exists(text) || File.Exists(text))
					{
						break;
					}
					continue;
				}
				catch
				{
					Thread.Sleep(100);
					continue;
				}
			}
			TreeNode treeNode = GenerateNode(text);
			if (treeNode.Tag != null && treeNode.Tag.GetType() == typeof(NodeTag))
			{
				GenerateNodeItems(treeNode);
			}
		}
	}

	private void OnChanged(object source, FileSystemEventArgs e)
	{
		EventSync.WaitOne();
		string item = string.Copy(e.FullPath);
		if (!EventBuffer.Contains(item))
		{
			EventBuffer.Enqueue(string.Copy(e.FullPath));
		}
		EventSync.ReleaseMutex();
	}

	private void OnDeleted(object source, FileSystemEventArgs e)
	{
		string fullPath = e.FullPath;
		if (nodeCache.ContainsKey(fullPath))
		{
			RemoveNode(nodeCache[fullPath]);
		}
	}

	public void CharTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		if (CharTree.HitTest(e.Location).Location != TreeViewHitTestLocations.PlusMinus)
		{
			SearchBox.Enabled = false;
			ThreadStart start = delegate
			{
				CharTree_ClickThread(e.Node);
			};
			ItemLoading = new Thread(start)
			{
				IsBackground = true
			};
			ItemLoading.Start();
		}
	}

	public void CharTree_ClickThread(TreeNode e)
	{
		CharLogMutex.WaitOne();
		active.Clear();
		ShowNodeItems(e);
		ShowCharLog(active.Values.ToArray());
		CharLogMutex.ReleaseMutex();
	}

	private void ListDirectories(TreeView treeView, string path)
	{
		nodeCache.Clear();
		treeView.Nodes.Clear();
		DirectoryInfo directoryInfo = new DirectoryInfo(path);
		CharLogMutex.WaitOne();
		ToggleCharLog(enable: false);
		Parallel.ForEach(directoryInfo.GetDirectories(), delegate(DirectoryInfo directory)
		{
			AddNode(treeView, CreateDirectoryNodes(directory));
		});
		ToggleCharLog(enable: true);
		CharLogMutex.ReleaseMutex();
	}

	private TreeNode CreateDirectoryNodes(DirectoryInfo directoryInfo)
	{
		TreeNode directoryNode = CreateNode(directoryInfo.FullName);
		Parallel.ForEach(directoryInfo.GetDirectories(), delegate(DirectoryInfo directory)
		{
			TreeNode treeNode = CreateDirectoryNodes(directory);
			treeNode.Name = directory.FullName;
			AddNode(directoryNode, treeNode);
		});
		Parallel.ForEach(directoryInfo.GetFiles(), delegate(FileInfo file)
		{
			if (file.FullName.EndsWith(".txt"))
			{
				TreeNode treeNode = CreateNode(file.FullName, isFile: true);
				NodeTag tag = new NodeTag(file.FullName);
				treeNode.Tag = tag;
				GenerateNodeItems(treeNode);
				AddNode(directoryNode, treeNode);
			}
		});
		return directoryNode;
	}

	private TreeNode GenerateNode(string node)
	{
		bool flag = false;
		if ((File.GetAttributes(node) & FileAttributes.Directory) != FileAttributes.Directory)
		{
			flag = true;
		}
		if (nodeCache.ContainsKey(node))
		{
			return nodeCache[node];
		}
		string[] array = node.Split(Path.DirectorySeparatorChar);
		Array.Reverse(array);
		TreeNode treeNode = CreateNode(node, flag);
		if (flag)
		{
			treeNode.Tag = new NodeTag(node);
		}
		for (int i = 1; i < array.Length; i++)
		{
			int length = node.IndexOf(array[i]);
			if (array[i].ToLower().Contains("mules"))
			{
				AddNode(CharTree, treeNode);
				break;
			}
			if (!nodeCache.ContainsKey(node.Substring(0, length) + array[i]))
			{
				TreeNode treeNode2 = CreateNode(node.Substring(0, length) + array[i]);
				AddNode(treeNode2, treeNode);
				treeNode = treeNode2;
				continue;
			}
			TreeNode root = nodeCache[node.Substring(0, length) + array[i]];
			AddNode(root, treeNode);
			break;
		}
		return nodeCache[node];
	}

	public void ToggleCharLog(bool enable)
	{
		if (base.InvokeRequired)
		{
			ToggleCallback method = ToggleCharLog;
			BeginInvoke(method, enable);
		}
		else
		{
			CharItems.Enabled = enable;
			SearchBox.Enabled = enable;
			SearchButton.Enabled = enable;
			CharTree.Enabled = enable;
		}
	}

	public void AddNode(TreeNode root, TreeNode leaf)
	{
		if (base.InvokeRequired)
		{
			AddNodeCallback method = AddNode;
			BeginInvoke(method, root, leaf);
		}
		else
		{
			root.Nodes.Add(leaf);
		}
	}

	public void AddNode(TreeView root, TreeNode leaf)
	{
		if (base.InvokeRequired)
		{
			AddTreeCallback method = AddNode;
			BeginInvoke(method, root, leaf);
		}
		else
		{
			root.Nodes.Add(leaf);
		}
	}

	public void RemoveNode(TreeNode root)
	{
		if (base.InvokeRequired)
		{
			RemoveNodeCallback method = RemoveNode;
			BeginInvoke(method, root);
		}
		else
		{
			if (root == null)
			{
				return;
			}
			root.Remove();
			nodeCache.TryRemove(root.Name, out var _);
			foreach (TreeNode node in root.Nodes)
			{
				RemoveNode(node);
			}
		}
	}

	private TreeNode CreateNode(string name, bool isFile = false)
	{
		if (!nodeCache.ContainsKey(name))
		{
			string text = Path.GetFileName(name);
			if (isFile)
			{
				text = text.Substring(0, text.Length - 4);
				if (text.Contains("."))
				{
					text = text.Substring(0, text.Length - 4) + " (" + text.Substring(text.Length - 3, 3).ToUpper() + ")";
				}
			}
			TreeNode treeNode = new TreeNode(text)
			{
				Name = name
			};
			nodeCache.TryAdd(treeNode.Name, treeNode);
		}
		return nodeCache[name];
	}

	private void GenerateNodeItems(TreeNode node)
	{
		if (node.Tag != null && node.Tag.GetType() == typeof(NodeTag))
		{
			NodeTag nodeTag = node.Tag as NodeTag;
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(nodeTag.Path);
			fileNameWithoutExtension = ((!fileNameWithoutExtension.Contains(".")) ? null : fileNameWithoutExtension.Substring(fileNameWithoutExtension.IndexOf(".") + 1, 3));
			string[] array = null;
			for (int i = 0; i < 10; i++)
			{
				try
				{
					array = File.ReadAllLines(nodeTag.Path);
				}
				catch
				{
					Thread.Sleep(100);
					continue;
				}
				break;
			}
			if (array == null)
			{
				return;
			}
			List<ListViewItem> list = new List<ListViewItem>(array.Length);
			Dictionary<string, List<WebItem>> dictionary = new Dictionary<string, List<WebItem>>();
			string[] array2 = array;
			foreach (string text in array2)
			{
				try
				{
					Item item = JsonConvert.DeserializeObject<D2Item>(text).ToItem();
					if (fileNameWithoutExtension != null)
					{
						item.Header = item.Header + " / " + fileNameWithoutExtension.ToUpper();
					}
					item.Path = node.Name;
					if (Program.WF != null)
					{
						foreach (Regex filter in Program.WF.GetFilters())
						{
							string key = filter.ToString();
							if (IsMatch(filter, item.Searcheable))
							{
								if (dictionary.ContainsKey(key))
								{
									dictionary[key].Add(item.ToWebItem());
									continue;
								}
								dictionary[key] = new List<WebItem>(1);
								dictionary[key].Add(item.ToWebItem());
							}
						}
					}
					if (!database.ContainsKey(item.Searcheable))
					{
						database[item.Searcheable] = item.ListViewItem("", "");
					}
					list.Add(database[item.Searcheable]);
				}
				catch (Exception e)
				{
					Program.LogCrash(e, "Item: " + text, show: false);
				}
			}
			nodeTag.Cache = dictionary;
			nodeTag.Items = list;
		}
		Parallel.ForEach(node.Nodes.OfType<TreeNode>(), delegate(TreeNode tn)
		{
			GenerateNodeItems(tn);
		});
	}

	private void ShowNodeItems(TreeNode node)
	{
		if (node.Tag != null && node.Tag.GetType() == typeof(NodeTag))
		{
			NodeTag obj = node.Tag as NodeTag;
			if (obj.Items.Count == 0)
			{
				GenerateNodeItems(node);
			}
			foreach (ListViewItem item2 in obj.Items)
			{
				Item item = item2.Tag as Item;
				if (!active.ContainsKey(item.Searcheable))
				{
					active.Add(item.Searcheable, item2);
				}
			}
		}
		foreach (TreeNode node2 in node.Nodes)
		{
			ShowNodeItems(node2);
		}
	}

	private void QueryItem(object sender, EventArgs e)
	{
		SearchBox.Enabled = false;
		Thread thread = new Thread((ThreadStart)delegate
		{
			QueryThread();
		});
		thread.IsBackground = true;
		thread.Start();
	}

	private void QueryThread()
	{
		CharLogMutex.WaitOne();
		if (!IsValidRegex(SearchBox.Text))
		{
			ShowCharLog(null);
			CharLogMutex.ReleaseMutex();
			return;
		}
		List<ListViewItem> list = new List<ListViewItem>();
		Regex regex = new Regex(SearchBox.Text.ToLower(), RegexOptions.Compiled);
		string[] array = active.Keys.ToArray();
		try
		{
			string[] array2 = array;
			foreach (string text in array2)
			{
				if (!string.IsNullOrEmpty(text) && regex.IsMatch(text, 0))
				{
					list.Add(active[text]);
				}
			}
		}
		catch
		{
			MessageBox.Show("Invalid Regular Expression");
		}
		ShowCharLog(list.ToArray());
		CharLogMutex.ReleaseMutex();
	}

	private void SearchNodeItems(TreeNode node, Regex query, SafeList<WebItem> result)
	{
		if (node.Tag != null && node.Tag.GetType() == typeof(NodeTag))
		{
			NodeTag nodeTag = node.Tag as NodeTag;
			if (nodeTag.Items.Count == 0)
			{
				GenerateNodeItems(node);
			}
			if (query == null)
			{
				result.AddRange(nodeTag.Items.ConvertAll((ListViewItem e) => ((Item)e.Tag).ToWebItem()));
			}
			else
			{
				string text = query.ToString();
				if (nodeTag.Cache.ContainsKey(text))
				{
					result.AddRange(nodeTag.Cache[text]);
				}
				else if (Program.WF == null || !Program.WF.valid.Contains(text))
				{
					foreach (ListViewItem item2 in nodeTag.Items)
					{
						Item item = item2.Tag as Item;
						if (IsMatch(query, item.Searcheable))
						{
							result.Add(item.ToWebItem());
						}
					}
				}
			}
		}
		Parallel.ForEach(node.Nodes.OfType<TreeNode>(), delegate(TreeNode tn)
		{
			SearchNodeItems(tn, query, result);
		});
	}

	public bool IsMatch(Regex query, string desc)
	{
		if (string.IsNullOrEmpty(desc))
		{
			return false;
		}
		return query.IsMatch(desc, 0);
	}

	public void LoadList()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Expected O, but got Unknown
		gameExe.FillsFreeSpace = true;
		objectProfileList.DragSource = (IDragSource)new SimpleDragSource(true);
		objectProfileList.DropSink = (IDropSink)new RearrangingDropSink(false);
		OLVColumn obj = profileCol;
		object obj2 = _003C_003Ec._003C_003E9__122_0;
		if (obj2 == null)
		{
			ImageGetterDelegate val = delegate(object row)
			{
				int type = (int)((ProfileBase)row).Type;
				return (type == 0 && ((ProfileBase)row).Status != Status.Stop) ? ((object)2) : ((object)type);
			};
			_003C_003Ec._003C_003E9__122_0 = val;
			obj2 = (object)val;
		}
		obj.ImageGetter = (ImageGetterDelegate)obj2;
	}

	public void SetD2BotTitle(string input = "")
	{
		Text = "D2Bot #  " + Program.VER.ToString() + input;
	}

	public Main()
	{
		InitializeComponent();
		SetD2BotTitle();
		charContainer.SplitterWidth = 1;
		keyWizardContainer.SplitterWidth = 1;
		systemFont.Checked = Settings.Default.System_Font;
		itemHeader.Checked = Settings.Default.Item_Header;
		startHidden.Checked = Settings.Default.Start_Hidden;
		serverToggle.Checked = Settings.Default.Start_Server;
		loadDelay.Checked = Settings.Default.Load_Delay;
		switch (Settings.Default.Wait_Time)
		{
		case 15:
			secondsToolStripMenuItem1.Checked = true;
			break;
		case 20:
			secondsToolStripMenuItem2.Checked = true;
			break;
		case 25:
			secondsToolStripMenuItem3.Checked = true;
			break;
		case 30:
			secondsToolStripMenuItem4.Checked = true;
			break;
		default:
			secondsToolStripMenuItem.Checked = true;
			break;
		}
		switch (Settings.Default.Delay_Time)
		{
		case 250:
			ms250.Checked = true;
			break;
		case 500:
			ms500.Checked = true;
			break;
		case 1000:
			ms1000.Checked = true;
			break;
		default:
			ms100.Checked = true;
			break;
		}
		SetUpTrayIcon();
		Image value = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("D2Bot.Resources.bgnd1.png"));
		Program.ItemBG.Add("bgnd1", value);
		value = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("D2Bot.Resources.bgnd2.png"));
		Program.ItemBG.Add("bgnd2", value);
		value = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("D2Bot.Resources.bgnd3.png"));
		Program.ItemBG.Add("bgnd3", value);
		value = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("D2Bot.Resources.bgnd4.png"));
		Program.ItemBG.Add("bgnd4", value);
		Program.D2Tool = new D2Tooltip(null);
		LoadListView();
		MessageHelper.SendMessage(((Control)(object)objectProfileList).Handle, 295, 1, 0);
		ItemLoading = new Thread(WatchInit)
		{
			IsBackground = true
		};
		ItemLoading.Start();
		WindowChecker = new Thread(CheckWindows)
		{
			IsBackground = true
		};
		WindowChecker.Start();
		WorkThread();
		Program.Handle = base.Handle;
		Timer = new System.Timers.Timer(1000.0);
		Timer.Elapsed += OnTimedEvent;
		Timer.Enabled = true;
	}

	private void OnTimedEvent(object source, EventArgs e)
	{
		SEC_Count++;
		if (base.WindowState != FormWindowState.Minimized)
		{
			UpdateProfiles();
			if (SEC_Count % 10 == 0)
			{
				UpdateDraw(show: true);
			}
		}
		if (Program.GA && SEC_Count > 600)
		{
			SEC_Count = 0;
			try
			{
				Program.ReportError.trackPage("d2bot.sharp", "/", "Home");
				Program.ReportError.trackScreen("Home");
			}
			catch
			{
			}
		}
	}

	private IList GetVisibleObjects()
	{
		int num = ((Control)(object)objectProfileList).Height / objectProfileList.RowHeight + 1;
		List<object> list = new List<object>(num);
		for (int i = 0; i < num; i++)
		{
			list.Add(objectProfileList.GetModelObject(objectProfileList.TopItemIndex + i));
		}
		return list;
	}

	public void LoadListView()
	{
		StyleHelper.DisableFlicker(ItemLogger);
		StyleHelper.DisableFlicker(CharTree);
		StyleHelper.DisableFlicker(CharItems);
		StyleHelper.DisableFlicker(KeyData);
	}

	public ProfileBase GetProfile(string name)
	{
		if (Program.ProfileList.ContainsKey(name.ToLower()))
		{
			return Program.ProfileList[name.ToLower()];
		}
		return null;
	}

	public D2Profile GetValidD2Profile(string name)
	{
		ProfileBase profile = GetProfile(name);
		if (profile != null && profile.Type == ProfileType.D2)
		{
			return (D2Profile)profile;
		}
		return null;
	}

	public IRCProfile GetValidIRCProfile(string name)
	{
		ProfileBase profile = GetProfile(name);
		if (profile != null && profile.Type == ProfileType.IRC)
		{
			return (IRCProfile)profile;
		}
		return null;
	}

	public KeyList GetKeyList(string name)
	{
		if (name == null || Program.KeyLists == null)
		{
			return null;
		}
		if (!Program.KeyLists.ContainsKey(name.ToLower()))
		{
			Program.KeyLists.TryAdd(name.ToLower(), new KeyList(name));
		}
		return Program.KeyLists[name.ToLower()];
	}

	public Schedule GetSchedule(string name)
	{
		if (name == null || Program.Schedules == null)
		{
			return null;
		}
		Program.Schedules.TryAdd(name.ToLower(), new Schedule(name));
		return Program.Schedules[name.ToLower()];
	}

	public void ParseD2BotLog()
	{
		int num = 0;
		string[] array = FileHandler.ReadLines(Application.StartupPath + "\\logs\\keyinfo.log");
		List<ListViewItem> list = new List<ListViewItem>();
		for (int i = 0; i < array.Length; i++)
		{
			int startIndex = array[i].IndexOf('<');
			array[i] = array[i].Substring(startIndex);
		}
		Array.Sort(array);
		string[] array2 = array;
		foreach (string text in array2)
		{
			Match match = Regex.Match(text, "\\<(.*?)\\>");
			Match match2 = Regex.Match(text, "\\> (.*?)\\ ");
			for (num = 0; num < list.Count && (!(match2.Groups[1].Value == list[num].SubItems[1].Text) || !(match.Groups[1].Value == list[num].SubItems[0].Text)); num++)
			{
			}
			if (num == list.Count)
			{
				ListViewItem listViewItem = new ListViewItem(match.Groups[1].Value);
				listViewItem.SubItems.Add(match2.Groups[1].Value);
				listViewItem.SubItems.Add("");
				listViewItem.SubItems.Add("");
				listViewItem.SubItems.Add("");
				list.Add(listViewItem);
			}
			if (text.Contains("use"))
			{
				if (list[num].SubItems[2].Text != "")
				{
					int num2 = int.Parse(list[num].SubItems[2].Text);
					list[num].SubItems[2].Text = (num2 + 1).ToString();
				}
				else
				{
					list[num].SubItems[2].Text = 1.ToString();
				}
			}
			else if (text.Contains("realm"))
			{
				if (list[num].SubItems[3].Text != "")
				{
					int num3 = int.Parse(list[num].SubItems[3].Text);
					list[num].SubItems[3].Text = (num3 + 1).ToString();
				}
				else
				{
					list[num].SubItems[3].Text = 1.ToString();
				}
			}
			else if (text.Contains("disabled"))
			{
				list[num].SubItems[4].Text = "DISABLED!";
			}
		}
		AddKeyData(list.ToArray());
	}

	public void AddKeyData(ListViewItem[] items)
	{
		if (base.InvokeRequired)
		{
			AddKeyDataCallback method = AddKeyData;
			BeginInvoke(method, new object[1] { items });
		}
		else
		{
			KeyData.Items.Clear();
			KeyData.Items.AddRange(items);
			KeyData.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			KeyData.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}
	}

	public void ProfileToList(D2Profile p)
	{
		objectProfileList.RefreshObject((object)p);
	}

	private void LinkClicked(object sender, LinkClickedEventArgs e)
	{
		if (e.LinkText.StartsWith("http://i.imgur.com"))
		{
			Process.Start(e.LinkText);
		}
		else if (e.LinkText.Contains("#"))
		{
			RichTextBoxEx richTextBoxEx = sender as RichTextBoxEx;
			if (richTextBoxEx.Tag == null || richTextBoxEx.Tag.GetType() != typeof(ToolTip))
			{
				richTextBoxEx.Tag = new ToolTip();
			}
			ToolTip toolTip = richTextBoxEx.Tag as ToolTip;
			if (string.IsNullOrEmpty(toolTip.GetToolTip(this)) || toolTip.GetToolTip(this) != e.LinkText.Split('#')[1])
			{
				Point point = PointToClient(Cursor.Position);
				point.X += 65;
				point.Y += 20;
				toolTip.Hide(this);
				toolTip.Show(e.LinkText.Split('#')[1].Replace("&$", Environment.NewLine), this, point, 2000);
			}
			else
			{
				toolTip.ShowAlways = false;
				toolTip.Hide(this);
			}
		}
	}

	private void ConsoleBox_CursorChanged(object sender, EventArgs e)
	{
		RichTextBoxEx richTextBoxEx = sender as RichTextBoxEx;
		if (richTextBoxEx.Cursor == Cursors.Hand)
		{
			ToolTip obj = richTextBoxEx.Tag as ToolTip;
			obj.ShowAlways = true;
			obj.Hide(this);
		}
	}

	private void ConsoleBox_MouseHover(object sender, MouseEventArgs e)
	{
		RichTextBoxEx richTextBoxEx = sender as RichTextBoxEx;
		if (richTextBoxEx.Cursor == Cursors.IBeam && richTextBoxEx.Tag is ToolTip toolTip)
		{
			toolTip.ShowAlways = false;
			toolTip.Hide(this);
		}
	}

	private void StartAll()
	{
		Thread thread = new Thread((ThreadStart)delegate
		{
			StartAllThread();
		});
		thread.IsBackground = true;
		thread.Start();
	}

	private void StartAllThread()
	{
		bool flag = false;
		List<ProfileBase> list = new List<ProfileBase>();
		int num = 0;
		if (loadDelay.Checked)
		{
			num = Settings.Default.Delay_Time;
		}
		try
		{
			flag = objectLock.WaitOne();
			foreach (ProfileBase value in Program.ProfileList.Values)
			{
				list.Add(value);
			}
		}
		catch
		{
		}
		finally
		{
			if (flag)
			{
				objectLock.ReleaseMutex();
			}
			for (int i = 0; i < list.Count; i++)
			{
				list[i].Load(num + i * num);
			}
		}
	}

	private void StopAll()
	{
		Thread thread = new Thread((ThreadStart)delegate
		{
			StopAllThread();
		});
		thread.IsBackground = true;
		thread.Start();
	}

	private void StopAllThread()
	{
		bool flag = false;
		try
		{
			flag = objectLock.WaitOne();
			foreach (ProfileBase value in Program.Runtime.Values)
			{
				while (value.Status == Status.Busy)
				{
					Thread.Sleep(10);
				}
				value.Stop();
			}
		}
		catch
		{
		}
		finally
		{
			if (flag)
			{
				objectLock.ReleaseMutex();
			}
		}
	}

	private void HideAll()
	{
		Thread thread = new Thread((ThreadStart)delegate
		{
			HideAllThread();
		});
		thread.IsBackground = true;
		thread.Start();
	}

	private void HideAllThread()
	{
		try
		{
			foreach (ProfileBase value in Program.Runtime.Values)
			{
				if (value is D2Profile d2Profile)
				{
					d2Profile.HideWindow();
				}
			}
		}
		catch
		{
		}
	}

	private void ShowAll()
	{
		Thread thread = new Thread((ThreadStart)delegate
		{
			ShowAllThread();
		});
		thread.IsBackground = true;
		thread.Start();
	}

	private void ShowAllThread()
	{
		try
		{
			foreach (ProfileBase value in Program.Runtime.Values)
			{
				if (value is D2Profile d2Profile)
				{
					d2Profile.ShowWindow();
				}
			}
		}
		catch
		{
		}
	}

	public void VersionHandler(object sender, EventArgs e)
	{
		foreach (ToolStripMenuItem dropDownItem in versionMenuItem.DropDownItems)
		{
			if (dropDownItem.ToString() == sender.ToString())
			{
				dropDownItem.Checked = true;
			}
			else
			{
				dropDownItem.Checked = false;
			}
		}
		Settings.Default.D2_Version = sender.ToString();
		Settings.Default.Save();
	}

	private void MenuHandler(object sender, EventArgs e)
	{
		string text = sender.ToString();
		if (text == null)
		{
			return;
		}
		switch (text)
		{
		case "Start All":
			StartAll();
			break;
		case "Stop All":
			StopAll();
			break;
		case "System Tray":
			Main_ResizeEnd(null, null);
			break;
		case "Exit":
			Application.Exit();
			break;
		case "Hide All":
			HideAll();
			break;
		case "Show All":
			ShowAll();
			break;
		case "Start Hidden":
			Settings.Default.Start_Hidden = startHidden.Checked;
			Settings.Default.Save();
			break;
		case "Use System Font":
			Settings.Default.System_Font = systemFont.Checked;
			Settings.Default.Save();
			break;
		case "Show Item Header":
			Settings.Default.Item_Header = itemHeader.Checked;
			Settings.Default.Save();
			break;
		case "Run API Server":
			if (serverToggle.Checked)
			{
				Settings.Default.Start_Server = true;
				Program.LoadWebConfig();
				if (Program.WC != null)
				{
					WebEvents.InitEvents();
					if (Program.WS != null && Program.WS.IsActive())
					{
						Program.WS.Stop();
					}
					Program.WS = new WebServer(Program.WC);
					Program.WS.Start();
				}
			}
			else
			{
				Settings.Default.Start_Server = false;
				if (Program.WC != null && Program.WS != null)
				{
					Program.WS.Stop();
				}
			}
			Settings.Default.Save();
			break;
		case "Developer Mode":
			if (debugMode.Checked)
			{
				MessageBox.Show("Developer mode is ON. Do not use unless you know what you are doing!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			break;
		case "10 Seconds":
			secondsToolStripMenuItem1.Checked = false;
			secondsToolStripMenuItem2.Checked = false;
			secondsToolStripMenuItem3.Checked = false;
			secondsToolStripMenuItem4.Checked = false;
			Settings.Default.Wait_Time = 10;
			Settings.Default.Save();
			break;
		case "15 Seconds":
			secondsToolStripMenuItem.Checked = false;
			secondsToolStripMenuItem2.Checked = false;
			secondsToolStripMenuItem3.Checked = false;
			secondsToolStripMenuItem4.Checked = false;
			Settings.Default.Wait_Time = 15;
			Settings.Default.Save();
			break;
		case "20 Seconds":
			secondsToolStripMenuItem.Checked = false;
			secondsToolStripMenuItem1.Checked = false;
			secondsToolStripMenuItem3.Checked = false;
			secondsToolStripMenuItem4.Checked = false;
			Settings.Default.Wait_Time = 20;
			Settings.Default.Save();
			break;
		case "25 Seconds":
			secondsToolStripMenuItem.Checked = false;
			secondsToolStripMenuItem1.Checked = false;
			secondsToolStripMenuItem2.Checked = false;
			secondsToolStripMenuItem4.Checked = false;
			Settings.Default.Wait_Time = 25;
			Settings.Default.Save();
			break;
		case "30 Seconds":
			secondsToolStripMenuItem.Checked = false;
			secondsToolStripMenuItem1.Checked = false;
			secondsToolStripMenuItem2.Checked = false;
			secondsToolStripMenuItem3.Checked = false;
			Settings.Default.Wait_Time = 30;
			Settings.Default.Save();
			break;
		case "Load Delay":
			Settings.Default.Load_Delay = loadDelay.Checked;
			Settings.Default.Save();
			break;
		case "100 ms":
			ms250.Checked = false;
			ms500.Checked = false;
			ms1000.Checked = false;
			ms2000.Checked = false;
			ms5000.Checked = false;
			Settings.Default.Delay_Time = 100;
			Settings.Default.Save();
			break;
		case "250 ms":
			ms100.Checked = false;
			ms500.Checked = false;
			ms1000.Checked = false;
			ms2000.Checked = false;
			ms5000.Checked = false;
			Settings.Default.Delay_Time = 250;
			Settings.Default.Save();
			break;
		case "500 ms":
			ms100.Checked = false;
			ms250.Checked = false;
			ms1000.Checked = false;
			ms2000.Checked = false;
			ms5000.Checked = false;
			Settings.Default.Delay_Time = 500;
			Settings.Default.Save();
			break;
		case "1000 ms":
			ms100.Checked = false;
			ms250.Checked = false;
			ms500.Checked = false;
			ms2000.Checked = false;
			ms5000.Checked = false;
			Settings.Default.Delay_Time = 1000;
			Settings.Default.Save();
			break;
		case "2000 ms":
			ms100.Checked = false;
			ms250.Checked = false;
			ms500.Checked = false;
			ms1000.Checked = false;
			ms5000.Checked = false;
			Settings.Default.Delay_Time = 2000;
			Settings.Default.Save();
			break;
		case "5000 ms":
			ms100.Checked = false;
			ms250.Checked = false;
			ms500.Checked = false;
			ms1000.Checked = false;
			ms2000.Checked = false;
			Settings.Default.Delay_Time = 5000;
			Settings.Default.Save();
			break;
		}
	}

	protected override void OnShown(EventArgs e)
	{
		MessageHelper.SendMessage(base.Handle, 296, Win32.MakeParam(1, 1), 0);
		base.OnShown(e);
	}

	public void Main_Close(object sender, FormClosingEventArgs e)
	{
		if (Program.ProfileList != null)
		{
			objectLock.WaitOne();
			foreach (ProfileBase value in Program.ProfileList.Values)
			{
				value.Stop();
				if (value.Type != ProfileType.IRC)
				{
					D2Profile d2Profile = (D2Profile)value;
					while (d2Profile.Client != null && d2Profile.Client.IsAlive)
					{
						Thread.Sleep(25);
					}
				}
			}
			objectLock.ReleaseMutex();
		}
		Program.SaveAll();
		notifyIcon.Visible = false;
		notifyIcon.Dispose();
	}

	private void AboutDialog(object sender, EventArgs e)
	{
		new About().Show();
	}

	private void ObjectProfileList_ModelDropped(object sender, ModelDropEventArgs e)
	{
		Program.SaveProfiles();
	}

	private void RefreshCharViewToolStripMenuItem_Click(object sender, EventArgs e)
	{
		RestartWatch();
	}

	private void CloseGameexeToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (MessageBox.Show("Are you sure you want to kill all Diablo 2 instances?", "Kill Game.exe", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
		{
			return;
		}
		StopAllThread();
		Process[] processes = Process.GetProcesses();
		for (int i = 0; i < processes.Length; i++)
		{
			try
			{
				if (processes[i].ProcessName.ToLower().Contains("game") && processes[i].MainModule.FileVersionInfo.FileDescription.ToLower().Contains("diablo"))
				{
					processes[i].Kill();
				}
			}
			catch
			{
			}
		}
	}

	private void ScheduleEdit_Click(object sender, EventArgs e)
	{
		new ListEditor(new ScheduleEditor()).Show();
	}

	private void KeysEdit_Click(object sender, EventArgs e)
	{
		new ListEditor(new KeylistEditor()).Show();
	}

	private void ImportProfilesToolStripMenuItem_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog
		{
			InitialDirectory = Application.StartupPath,
			RestoreDirectory = true
		};
		if (openFileDialog.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		bool flag = false;
		if (MessageBox.Show("This will stop all running profiles and remove them from this list.", "Continue Loading?", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
		{
			StopAll();
			int num = 0;
			while (Program.Runtime.Count > 0 && num < 500)
			{
				num++;
				Thread.Sleep(10);
			}
			try
			{
				flag = objectLock.WaitOne();
				if (Program.ProfileList.Count > 0)
				{
					objectProfileList.RemoveObjects((ICollection)Program.ProfileList.Values.ToList());
				}
			}
			catch
			{
			}
			finally
			{
				if (flag)
				{
					objectLock.ReleaseMutex();
				}
			}
			Program.Runtime.Clear();
			Program.ProfileList.Clear();
		}
		if (flag)
		{
			Program.LoadProfile("\\" + Path.GetFileName(openFileDialog.FileName));
			Program.SaveAll();
		}
	}

	private void UpdateVisibleList()
	{
		int topItemIndex = objectProfileList.TopItemIndex;
		int val = ((Control)(object)objectProfileList).Height / objectProfileList.RowHeight + 1;
		val = Math.Min(val, objectProfileList.GetItemCount() - topItemIndex);
		if (topItemIndex != UpdateIndex || val != UpdateTotal)
		{
			UpdateTotal = val;
			UpdateIndex = topItemIndex;
			Program.Viewable.Clear();
			for (int i = 0; i < UpdateTotal && UpdateIndex + i < objectProfileList.GetItemCount(); i++)
			{
				Program.Viewable.Add(objectProfileList.GetModelObject(UpdateIndex + i) as ProfileBase);
			}
		}
	}


}
