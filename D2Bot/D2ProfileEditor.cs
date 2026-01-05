using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using D2Bot.Properties;
using Microsoft.Win32;

namespace D2Bot;

public class D2ProfileEditor : Form
{
	private D2Profile m_profile;

	private bool m_isNew = true;

	private IContainer components;

	public Button Apply;

	public Button Cancel;

	public Button OK;

	private Label label7;

	public ComboBox Difficulty;

	public TextBox RunsPerKey;

	private Label label9;

	public TextBox GamePass;

	public ComboBox Realm;

	public ComboBox Mode;

	public TextBox Parameters;

	public TextBox DiabloPath;

	private Label label8;

	private Label label10;

	private Label label11;

	private Label label;

	private Label label13;

	public Button D2Path;

	public CheckBox useSchedule;

	public Panel panel1;

	public CheckBox switchKeys;

	private Label label14;

	private Label label12;

	public ComboBox scheduleDrop;

	public ComboBox keyListDrop;

	public TextBox InfoTag;

	private Label label15;

	public CheckBox clientVisible;

	public TextBox clientLocation;

	private Label label17;

	private Label label5;

	private Label label4;

	private Label label3;

	private Label label2;

	public TextBox GameName;

	public TextBox Character;

	public TextBox Password;

	public TextBox Account;

	public TextBox ProfileName;

	private Label label1;

	private Panel panel2;

	private SplitContainer splitContainer1;

	public ComboBox EntryScript;

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
			panel1.Width = base.ClientSize.Width - 15;
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

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(D2Bot.D2ProfileEditor));
		this.Apply = new System.Windows.Forms.Button();
		this.Cancel = new System.Windows.Forms.Button();
		this.OK = new System.Windows.Forms.Button();
		this.label7 = new System.Windows.Forms.Label();
		this.Difficulty = new System.Windows.Forms.ComboBox();
		this.RunsPerKey = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.GamePass = new System.Windows.Forms.TextBox();
		this.Realm = new System.Windows.Forms.ComboBox();
		this.Mode = new System.Windows.Forms.ComboBox();
		this.Parameters = new System.Windows.Forms.TextBox();
		this.DiabloPath = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.D2Path = new System.Windows.Forms.Button();
		this.panel1 = new System.Windows.Forms.Panel();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.Account = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.GameName = new System.Windows.Forms.TextBox();
		this.Character = new System.Windows.Forms.TextBox();
		this.Password = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.ProfileName = new System.Windows.Forms.TextBox();
		this.EntryScript = new System.Windows.Forms.ComboBox();
		this.scheduleDrop = new System.Windows.Forms.ComboBox();
		this.clientVisible = new System.Windows.Forms.CheckBox();
		this.clientLocation = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.useSchedule = new System.Windows.Forms.CheckBox();
		this.InfoTag = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.keyListDrop = new System.Windows.Forms.ComboBox();
		this.label14 = new System.Windows.Forms.Label();
		this.switchKeys = new System.Windows.Forms.CheckBox();
		this.label12 = new System.Windows.Forms.Label();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.Apply.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.Apply.Location = new System.Drawing.Point(584, 17);
		this.Apply.Name = "Apply";
		this.Apply.Size = new System.Drawing.Size(88, 30);
		this.Apply.TabIndex = 28;
		this.Apply.Text = "Apply";
		this.Apply.UseVisualStyleBackColor = true;
		this.Apply.Click += new System.EventHandler(Apply_Click);
		this.Cancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.Cancel.Location = new System.Drawing.Point(490, 17);
		this.Cancel.Name = "Cancel";
		this.Cancel.Size = new System.Drawing.Size(88, 30);
		this.Cancel.TabIndex = 27;
		this.Cancel.Text = "Cancel";
		this.Cancel.UseVisualStyleBackColor = true;
		this.Cancel.Click += new System.EventHandler(CloseEditor);
		this.OK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.OK.Location = new System.Drawing.Point(396, 17);
		this.OK.Name = "OK";
		this.OK.Size = new System.Drawing.Size(88, 30);
		this.OK.TabIndex = 26;
		this.OK.Text = "OK";
		this.OK.UseVisualStyleBackColor = true;
		this.OK.Click += new System.EventHandler(OK_Click);
		this.label7.AutoSize = true;
		this.label7.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label7.Location = new System.Drawing.Point(36, 190);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(57, 17);
		this.label7.TabIndex = 81;
		this.label7.Text = "Difficulty";
		this.Difficulty.FormattingEnabled = true;
		this.Difficulty.Items.AddRange(new object[4] { "Hell", "Nightmare", "Normal", "Highest" });
		this.Difficulty.Location = new System.Drawing.Point(126, 187);
		this.Difficulty.Name = "Difficulty";
		this.Difficulty.Size = new System.Drawing.Size(174, 25);
		this.Difficulty.TabIndex = 11;
		this.Difficulty.Text = "Hell";
		this.RunsPerKey.Location = new System.Drawing.Point(129, 218);
		this.RunsPerKey.Name = "RunsPerKey";
		this.RunsPerKey.Size = new System.Drawing.Size(72, 25);
		this.RunsPerKey.TabIndex = 23;
		this.RunsPerKey.Text = "0";
		this.label9.AutoSize = true;
		this.label9.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label9.Location = new System.Drawing.Point(34, 221);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(82, 17);
		this.label9.TabIndex = 23;
		this.label9.Text = "Runs/Restart";
		this.GamePass.Location = new System.Drawing.Point(216, 156);
		this.GamePass.MaxLength = 25;
		this.GamePass.Name = "GamePass";
		this.GamePass.Size = new System.Drawing.Size(84, 25);
		this.GamePass.TabIndex = 5;
		this.GamePass.Text = "Password";
		this.Realm.FormattingEnabled = true;
		this.Realm.Items.AddRange(new object[4] { "East", "West", "Europe", "Asia" });
		this.Realm.Location = new System.Drawing.Point(126, 218);
		this.Realm.Name = "Realm";
		this.Realm.Size = new System.Drawing.Size(174, 25);
		this.Realm.TabIndex = 12;
		this.Realm.Text = "East";
		this.Mode.FormattingEnabled = true;
		this.Mode.Items.AddRange(new object[5] { "Battle.net", "Single Player", "Open Battle.net", "Host TCP/IP Game", "Join TCP/IP Game" });
		this.Mode.Location = new System.Drawing.Point(126, 249);
		this.Mode.Name = "Mode";
		this.Mode.Size = new System.Drawing.Size(174, 25);
		this.Mode.TabIndex = 13;
		this.Mode.Text = "Battle.net";
		this.Parameters.Location = new System.Drawing.Point(129, 63);
		this.Parameters.Name = "Parameters";
		this.Parameters.Size = new System.Drawing.Size(174, 25);
		this.Parameters.TabIndex = 14;
		this.DiabloPath.Location = new System.Drawing.Point(129, 32);
		this.DiabloPath.Name = "DiabloPath";
		this.DiabloPath.Size = new System.Drawing.Size(148, 25);
		this.DiabloPath.TabIndex = 15;
		this.label8.AutoSize = true;
		this.label8.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label8.Location = new System.Drawing.Point(36, 221);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(44, 17);
		this.label8.TabIndex = 83;
		this.label8.Text = "Realm";
		this.label10.AutoSize = true;
		this.label10.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label10.Location = new System.Drawing.Point(36, 252);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(43, 17);
		this.label10.TabIndex = 84;
		this.label10.Text = "Mode";
		this.label11.AutoSize = true;
		this.label11.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label11.Location = new System.Drawing.Point(34, 97);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(74, 17);
		this.label11.TabIndex = 85;
		this.label11.Text = "Entry Script";
		this.label.AutoSize = true;
		this.label.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label.Location = new System.Drawing.Point(34, 35);
		this.label.Name = "label";
		this.label.Size = new System.Drawing.Size(71, 17);
		this.label.TabIndex = 86;
		this.label.Text = "Game Path";
		this.label13.AutoSize = true;
		this.label13.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label13.Location = new System.Drawing.Point(34, 66);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(74, 17);
		this.label13.TabIndex = 87;
		this.label13.Text = "Parameters";
		this.D2Path.BackgroundImage = (System.Drawing.Image)resources.GetObject("D2Path.BackgroundImage");
		this.D2Path.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.D2Path.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.D2Path.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
		this.D2Path.Location = new System.Drawing.Point(278, 32);
		this.D2Path.Margin = new System.Windows.Forms.Padding(0);
		this.D2Path.Name = "D2Path";
		this.D2Path.Size = new System.Drawing.Size(25, 25);
		this.D2Path.TabIndex = 16;
		this.D2Path.UseVisualStyleBackColor = true;
		this.D2Path.Click += new System.EventHandler(OpenD2Path);
		this.panel1.BackColor = System.Drawing.SystemColors.Window;
		this.panel1.Controls.Add(this.splitContainer1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(684, 302);
		this.panel1.TabIndex = 89;
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.IsSplitterFixed = true;
		this.splitContainer1.Location = new System.Drawing.Point(0, 0);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.Account);
		this.splitContainer1.Panel1.Controls.Add(this.label5);
		this.splitContainer1.Panel1.Controls.Add(this.Realm);
		this.splitContainer1.Panel1.Controls.Add(this.label4);
		this.splitContainer1.Panel1.Controls.Add(this.label7);
		this.splitContainer1.Panel1.Controls.Add(this.label3);
		this.splitContainer1.Panel1.Controls.Add(this.Mode);
		this.splitContainer1.Panel1.Controls.Add(this.label2);
		this.splitContainer1.Panel1.Controls.Add(this.Difficulty);
		this.splitContainer1.Panel1.Controls.Add(this.GameName);
		this.splitContainer1.Panel1.Controls.Add(this.label8);
		this.splitContainer1.Panel1.Controls.Add(this.Character);
		this.splitContainer1.Panel1.Controls.Add(this.label10);
		this.splitContainer1.Panel1.Controls.Add(this.Password);
		this.splitContainer1.Panel1.Controls.Add(this.GamePass);
		this.splitContainer1.Panel1.Controls.Add(this.label1);
		this.splitContainer1.Panel1.Controls.Add(this.ProfileName);
		this.splitContainer1.Panel2.Controls.Add(this.EntryScript);
		this.splitContainer1.Panel2.Controls.Add(this.scheduleDrop);
		this.splitContainer1.Panel2.Controls.Add(this.clientVisible);
		this.splitContainer1.Panel2.Controls.Add(this.Parameters);
		this.splitContainer1.Panel2.Controls.Add(this.clientLocation);
		this.splitContainer1.Panel2.Controls.Add(this.DiabloPath);
		this.splitContainer1.Panel2.Controls.Add(this.label17);
		this.splitContainer1.Panel2.Controls.Add(this.label9);
		this.splitContainer1.Panel2.Controls.Add(this.RunsPerKey);
		this.splitContainer1.Panel2.Controls.Add(this.useSchedule);
		this.splitContainer1.Panel2.Controls.Add(this.InfoTag);
		this.splitContainer1.Panel2.Controls.Add(this.label11);
		this.splitContainer1.Panel2.Controls.Add(this.label15);
		this.splitContainer1.Panel2.Controls.Add(this.keyListDrop);
		this.splitContainer1.Panel2.Controls.Add(this.label);
		this.splitContainer1.Panel2.Controls.Add(this.label14);
		this.splitContainer1.Panel2.Controls.Add(this.switchKeys);
		this.splitContainer1.Panel2.Controls.Add(this.label13);
		this.splitContainer1.Panel2.Controls.Add(this.label12);
		this.splitContainer1.Panel2.Controls.Add(this.D2Path);
		this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(splitContainer1_Panel2_Paint);
		this.splitContainer1.Size = new System.Drawing.Size(684, 302);
		this.splitContainer1.SplitterDistance = 340;
		this.splitContainer1.SplitterWidth = 1;
		this.splitContainer1.TabIndex = 111;
		this.Account.Location = new System.Drawing.Point(126, 63);
		this.Account.MaxLength = 16;
		this.Account.Name = "Account";
		this.Account.Size = new System.Drawing.Size(174, 25);
		this.Account.TabIndex = 1;
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label5.Location = new System.Drawing.Point(36, 159);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(68, 17);
		this.label5.TabIndex = 110;
		this.label5.Text = "Game Info";
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label4.Location = new System.Drawing.Point(36, 127);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(64, 17);
		this.label4.TabIndex = 109;
		this.label4.Text = "Character";
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label3.Location = new System.Drawing.Point(36, 97);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(64, 17);
		this.label3.TabIndex = 108;
		this.label3.Text = "Password";
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label2.Location = new System.Drawing.Point(36, 66);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(54, 17);
		this.label2.TabIndex = 107;
		this.label2.Text = "Account";
		this.GameName.Location = new System.Drawing.Point(126, 156);
		this.GameName.MaxLength = 25;
		this.GameName.Name = "GameName";
		this.GameName.Size = new System.Drawing.Size(84, 25);
		this.GameName.TabIndex = 4;
		this.GameName.Text = "Name";
		this.Character.Location = new System.Drawing.Point(126, 125);
		this.Character.MaxLength = 16;
		this.Character.Name = "Character";
		this.Character.Size = new System.Drawing.Size(174, 25);
		this.Character.TabIndex = 3;
		this.Password.Location = new System.Drawing.Point(126, 94);
		this.Password.MaxLength = 25;
		this.Password.Name = "Password";
		this.Password.Size = new System.Drawing.Size(174, 25);
		this.Password.TabIndex = 2;
		this.Password.UseSystemPasswordChar = true;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label1.Location = new System.Drawing.Point(36, 35);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(84, 17);
		this.label1.TabIndex = 106;
		this.label1.Text = "Profile Name";
		this.ProfileName.AccessibleName = "ProfileName";
		this.ProfileName.Location = new System.Drawing.Point(126, 32);
		this.ProfileName.Name = "ProfileName";
		this.ProfileName.Size = new System.Drawing.Size(174, 25);
		this.ProfileName.TabIndex = 0;
		this.EntryScript.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.EntryScript.FormattingEnabled = true;
		this.EntryScript.Location = new System.Drawing.Point(129, 94);
		this.EntryScript.Name = "EntryScript";
		this.EntryScript.Size = new System.Drawing.Size(174, 25);
		this.EntryScript.Sorted = true;
		this.EntryScript.TabIndex = 101;
		this.scheduleDrop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.scheduleDrop.FormattingEnabled = true;
		this.scheduleDrop.Location = new System.Drawing.Point(129, 156);
		this.scheduleDrop.Name = "scheduleDrop";
		this.scheduleDrop.Size = new System.Drawing.Size(174, 25);
		this.scheduleDrop.Sorted = true;
		this.scheduleDrop.TabIndex = 19;
		this.clientVisible.AccessibleName = "clientVisible";
		this.clientVisible.AutoSize = true;
		this.clientVisible.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.clientVisible.Location = new System.Drawing.Point(238, 251);
		this.clientVisible.Name = "clientVisible";
		this.clientVisible.Size = new System.Drawing.Size(65, 21);
		this.clientVisible.TabIndex = 6;
		this.clientVisible.Text = "Visible";
		this.clientVisible.UseVisualStyleBackColor = true;
		this.clientLocation.Location = new System.Drawing.Point(129, 249);
		this.clientLocation.Name = "clientLocation";
		this.clientLocation.Size = new System.Drawing.Size(72, 25);
		this.clientLocation.TabIndex = 7;
		this.clientLocation.Text = " x, y";
		this.label17.AutoSize = true;
		this.label17.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label17.Location = new System.Drawing.Point(34, 252);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(57, 17);
		this.label17.TabIndex = 100;
		this.label17.Text = "Location";
		this.useSchedule.AccessibleName = "useSchedule";
		this.useSchedule.AutoSize = true;
		this.useSchedule.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.useSchedule.Location = new System.Drawing.Point(219, 189);
		this.useSchedule.Name = "useSchedule";
		this.useSchedule.Size = new System.Drawing.Size(84, 21);
		this.useSchedule.TabIndex = 25;
		this.useSchedule.Text = "Scheduler";
		this.useSchedule.UseVisualStyleBackColor = true;
		this.InfoTag.Location = new System.Drawing.Point(129, 187);
		this.InfoTag.Name = "InfoTag";
		this.InfoTag.Size = new System.Drawing.Size(72, 25);
		this.InfoTag.TabIndex = 22;
		this.label15.AutoSize = true;
		this.label15.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label15.Location = new System.Drawing.Point(34, 190);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(55, 17);
		this.label15.TabIndex = 95;
		this.label15.Text = "Info Tag";
		this.keyListDrop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.keyListDrop.FormattingEnabled = true;
		this.keyListDrop.Location = new System.Drawing.Point(129, 125);
		this.keyListDrop.Name = "keyListDrop";
		this.keyListDrop.Size = new System.Drawing.Size(174, 25);
		this.keyListDrop.Sorted = true;
		this.keyListDrop.TabIndex = 8;
		this.label14.AutoSize = true;
		this.label14.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label14.Location = new System.Drawing.Point(34, 159);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(60, 17);
		this.label14.TabIndex = 91;
		this.label14.Text = "Schedule";
		this.switchKeys.AccessibleName = "switchKeys";
		this.switchKeys.AutoSize = true;
		this.switchKeys.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.switchKeys.Location = new System.Drawing.Point(209, 220);
		this.switchKeys.Name = "switchKeys";
		this.switchKeys.Size = new System.Drawing.Size(94, 21);
		this.switchKeys.TabIndex = 24;
		this.switchKeys.Text = "Switch Keys";
		this.switchKeys.UseVisualStyleBackColor = true;
		this.label12.AutoSize = true;
		this.label12.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label12.Location = new System.Drawing.Point(34, 128);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(52, 17);
		this.label12.TabIndex = 91;
		this.label12.Text = "Key List";
		this.panel2.Controls.Add(this.OK);
		this.panel2.Controls.Add(this.Cancel);
		this.panel2.Controls.Add(this.Apply);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 302);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(684, 59);
		this.panel2.TabIndex = 90;
		base.AcceptButton = this.OK;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		base.CancelButton = this.Cancel;
		base.ClientSize = new System.Drawing.Size(684, 361);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel2);
		this.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		this.MaximumSize = new System.Drawing.Size(700, 400);
		base.MinimizeBox = false;
		this.MinimumSize = new System.Drawing.Size(700, 400);
		base.Name = "D2ProfileEditor";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Profile Editor";
		base.TopMost = true;
		this.panel1.ResumeLayout(false);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel1.PerformLayout();
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.Panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
