using System;
using System.IO;
using System.Windows.Forms;
using D2Bot.Properties;
using Microsoft.Win32;

namespace D2Bot;

public partial class D2ProfileEditor : Form
{
	private D2Profile m_profile;

	private bool m_isNew = true;

	public bool IsNew
	{
		get
		{
			return m_isNew;
		}
		set
		{
			m_isNew = value;
			if (m_isNew)
			{
				Text = "Add D2 Profile";
				Parameters.Text = "-w -sleepy -ftj";
				return;
			}
			Text = "Edit D2 Profile";
			ProfileName.Text = m_profile.Name;
			Account.Text = m_profile.Account;
			Password.Text = m_profile.Password;
			GameName.Text = m_profile.GameName;
			GamePass.Text = m_profile.GamePass;
			Character.Text = m_profile.Character;
			Difficulty.Text = m_profile.Difficulty;
			Realm.Text = m_profile.Realm;
			Mode.Text = m_profile.Mode;
			EntryScript.Text = Path.GetFileName(m_profile.Entry);
			DiabloPath.Text = m_profile.D2Path;
			Parameters.Text = m_profile.Parameters;
			keyListDrop.Text = m_profile.KeyList;
			scheduleDrop.Text = m_profile.Schedule;
			useSchedule.Checked = m_profile.ScheduleEnable;
			switchKeys.Checked = m_profile.SwitchKeys;
			clientVisible.Checked = m_profile.Visible;
			clientLocation.Text = m_profile.Location;
			InfoTag.Text = m_profile.InfoTag;
			if (m_profile.RunsPerKey > -1)
			{
				RunsPerKey.Text = m_profile.RunsPerKey.ToString();
			}
		}
	}

	public D2Profile ProfileToEdit
	{
		get
		{
			return m_profile;
		}
		set
		{
			m_profile = value;
			string[] files = Directory.GetFiles(Program.BOT_LIB, "D2Bot*.dbj");
			foreach (string path in files)
			{
				EntryScript.Items.Add(Path.GetFileName(path));
			}
			scheduleDrop.Items.Add("");
			foreach (Schedule value2 in Program.Schedules.Values)
			{
				scheduleDrop.Items.Add(value2.Name);
			}
			keyListDrop.Items.Add("");
			foreach (KeyList value3 in Program.KeyLists.Values)
			{
				keyListDrop.Items.Add(value3.Name);
			}
			panel1.Width = ClientSize.Width - 15;
		}
	}

	public D2ProfileEditor()
	{
		InitializeComponent();
	}

	private bool UpdateProfile()
	{
		try
		{
			Program.GM.objectLock.WaitOne();
			D2Profile profile = m_profile;
			if (ProfileName.Text.Length > 14)
			{
				MessageBox.Show("Profile Name too long!", "D2Bot");
				return false;
			}
			if (string.IsNullOrWhiteSpace(EntryScript.Text))
			{
				MessageBox.Show("Please Choose an Entry Script!", "D2Bot");
				return false;
			}
			if (!File.Exists(DiabloPath.Text))
			{
				MessageBox.Show("Invalid Diablo Path!", "D2Bot");
				return false;
			}
			if (!File.Exists(Program.BOT_LIB + Path.DirectorySeparatorChar + EntryScript.Text))
			{
				MessageBox.Show("Invalid Entry Specified!", "D2Bot");
				return false;
			}
			if (m_isNew)
			{
				if (Program.ProfileList.ContainsKey(ProfileName.Text.Trim().ToLower()))
				{
					MessageBox.Show("Profile name already exists, please choose unique name!", "D2Bot");
					return false;
				}
				profile.Name = ProfileName.Text.Trim();
				profile.Add();
				profile.KeyRuns = 0;
				profile.NoResponse = 0;
				profile.HeartAttack = 0;
				profile.Crashed = 0;
			}
			else
			{
				if (!Program.CanRenameItem(profile.Name, ProfileName.Text.Trim()))
				{
					MessageBox.Show("Profile name already exists, please choose unique name!", "D2Bot");
					return false;
				}
				Program.RenameItem(profile.Name, ProfileName.Text.Trim());
			}
			profile.Account = Account.Text.Trim();
			profile.Character = Character.Text.Trim();
			profile.D2Path = DiabloPath.Text.Trim();
			profile.Difficulty = Difficulty.Text;
			profile.Entry = EntryScript.Text.Trim();
			profile.GameName = GameName.Text.Trim();
			profile.GamePass = GamePass.Text.Trim();
			profile.Mode = Mode.Text;
			profile.Name = ProfileName.Text.Trim();
			profile.Parameters = Parameters.Text;
			profile.Password = Password.Text;
			profile.Realm = Realm.Text;
			profile.ScheduleEnable = useSchedule.Checked;
			profile.SwitchKeys = switchKeys.Checked;
			profile.Visible = clientVisible.Checked;
			profile.Location = clientLocation.Text.Trim();
			profile.Schedule = scheduleDrop.Text;
			profile.KeyList = keyListDrop.Text;
			profile.InfoTag = InfoTag.Text;
			int result = -1;
			if (!int.TryParse(RunsPerKey.Text, out result))
			{
				MessageBox.Show("Runs Per Key could not be parsed!", "D2Bot");
				return false;
			}
			profile.RunsPerKey = result;
			Program.GM.objectProfileList.RefreshObject((object)profile);
			return true;
		}
		catch
		{
			MessageBox.Show("Error in Parsing EDITOR\n Please check your inputs.", "D2Bot # Exception");
			return false;
		}
		finally
		{
			Program.GM.objectLock.ReleaseMutex();
			Program.SaveProfiles();
		}
	}

	private void OpenD2Path(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		try
		{
			if (!string.IsNullOrWhiteSpace(DiabloPath.Text))
			{
				openFileDialog.InitialDirectory = Path.GetDirectoryName(DiabloPath.Text);
			}
			else if (Directory.Exists(Settings.Default.D2_Folder))
			{
				openFileDialog.InitialDirectory = Settings.Default.D2_Folder;
			}
			else
			{
				openFileDialog.InitialDirectory = Registry.CurrentUser.OpenSubKey("Software\\Blizzard Entertainment\\Diablo II").GetValue("InstallPath").ToString();
			}
		}
		catch
		{
			openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		}
		openFileDialog.Filter = "Diablo II|*.exe|Diablo II|Game*.exe";
		openFileDialog.FilterIndex = 2;
		openFileDialog.RestoreDirectory = true;
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			DiabloPath.Clear();
			DiabloPath.AppendText(openFileDialog.FileName);
			Settings.Default.D2_Folder = Path.GetDirectoryName(openFileDialog.FileName);
			Settings.Default.Save();
		}
	}

	private void CloseEditor(object sender, EventArgs e)
	{
		Close();
	}

	private void OK_Click(object sender, EventArgs e)
	{
		if (UpdateProfile())
		{
			Close();
		}
	}

	private void Apply_Click(object sender, EventArgs e)
	{
		if (UpdateProfile())
		{
			IsNew = false;
		}
	}

	private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
	{
	}
}
