namespace D2Bot;

partial class D2ProfileEditor
{
	private System.ComponentModel.IContainer components;

	public System.Windows.Forms.Button Apply;

	public System.Windows.Forms.Button Cancel;

	public System.Windows.Forms.Button OK;

	private System.Windows.Forms.Label label7;

	public System.Windows.Forms.ComboBox Difficulty;

	public System.Windows.Forms.TextBox RunsPerKey;

	private System.Windows.Forms.Label label9;

	public System.Windows.Forms.TextBox GamePass;

	public System.Windows.Forms.ComboBox Realm;

	public System.Windows.Forms.ComboBox Mode;

	public System.Windows.Forms.TextBox Parameters;

	public System.Windows.Forms.TextBox DiabloPath;

	private System.Windows.Forms.Label label8;

	private System.Windows.Forms.Label label10;

	private System.Windows.Forms.Label label11;

	private System.Windows.Forms.Label label;

	private System.Windows.Forms.Label label13;

	public System.Windows.Forms.Button D2Path;

	public System.Windows.Forms.CheckBox useSchedule;

	public System.Windows.Forms.Panel panel1;

	public System.Windows.Forms.CheckBox switchKeys;

	private System.Windows.Forms.Label label14;

	private System.Windows.Forms.Label label12;

	public System.Windows.Forms.ComboBox scheduleDrop;

	public System.Windows.Forms.ComboBox keyListDrop;

	public System.Windows.Forms.TextBox InfoTag;

	private System.Windows.Forms.Label label15;

	public System.Windows.Forms.CheckBox clientVisible;

	public System.Windows.Forms.TextBox clientLocation;

	private System.Windows.Forms.Label label17;

	private System.Windows.Forms.Label label5;

	private System.Windows.Forms.Label label4;

	private System.Windows.Forms.Label label3;

	private System.Windows.Forms.Label label2;

	public System.Windows.Forms.TextBox GameName;

	public System.Windows.Forms.TextBox Character;

	public System.Windows.Forms.TextBox Password;

	public System.Windows.Forms.TextBox Account;

	public System.Windows.Forms.TextBox ProfileName;

	private System.Windows.Forms.Label label1;

	private System.Windows.Forms.Panel panel2;

	private System.Windows.Forms.SplitContainer splitContainer1;

	public System.Windows.Forms.ComboBox EntryScript;

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
		this.SuspendLayout();
		//
		// Apply
		//
		this.Apply.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.Apply.Location = new System.Drawing.Point(584, 17);
		this.Apply.Name = "Apply";
		this.Apply.Size = new System.Drawing.Size(88, 30);
		this.Apply.TabIndex = 28;
		this.Apply.Text = "Apply";
		this.Apply.UseVisualStyleBackColor = true;
		this.Apply.Click += new System.EventHandler(this.Apply_Click);
		//
		// Cancel
		//
		this.Cancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.Cancel.Location = new System.Drawing.Point(490, 17);
		this.Cancel.Name = "Cancel";
		this.Cancel.Size = new System.Drawing.Size(88, 30);
		this.Cancel.TabIndex = 27;
		this.Cancel.Text = "Cancel";
		this.Cancel.UseVisualStyleBackColor = true;
		this.Cancel.Click += new System.EventHandler(this.CloseEditor);
		//
		// OK
		//
		this.OK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.OK.Location = new System.Drawing.Point(396, 17);
		this.OK.Name = "OK";
		this.OK.Size = new System.Drawing.Size(88, 30);
		this.OK.TabIndex = 26;
		this.OK.Text = "OK";
		this.OK.UseVisualStyleBackColor = true;
		this.OK.Click += new System.EventHandler(this.OK_Click);
		//
		// label7
		//
		this.label7.AutoSize = true;
		this.label7.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label7.Location = new System.Drawing.Point(36, 190);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(57, 17);
		this.label7.TabIndex = 81;
		this.label7.Text = "Difficulty";
		//
		// Difficulty
		//
		this.Difficulty.FormattingEnabled = true;
		this.Difficulty.Items.AddRange(new object[4] { "Hell", "Nightmare", "Normal", "Highest" });
		this.Difficulty.Location = new System.Drawing.Point(126, 187);
		this.Difficulty.Name = "Difficulty";
		this.Difficulty.Size = new System.Drawing.Size(174, 25);
		this.Difficulty.TabIndex = 11;
		this.Difficulty.Text = "Hell";
		//
		// RunsPerKey
		//
		this.RunsPerKey.Location = new System.Drawing.Point(129, 218);
		this.RunsPerKey.Name = "RunsPerKey";
		this.RunsPerKey.Size = new System.Drawing.Size(72, 25);
		this.RunsPerKey.TabIndex = 23;
		this.RunsPerKey.Text = "0";
		//
		// label9
		//
		this.label9.AutoSize = true;
		this.label9.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label9.Location = new System.Drawing.Point(34, 221);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(82, 17);
		this.label9.TabIndex = 23;
		this.label9.Text = "Runs/Restart";
		//
		// GamePass
		//
		this.GamePass.Location = new System.Drawing.Point(216, 156);
		this.GamePass.MaxLength = 25;
		this.GamePass.Name = "GamePass";
		this.GamePass.Size = new System.Drawing.Size(84, 25);
		this.GamePass.TabIndex = 5;
		this.GamePass.Text = "Password";
		//
		// Realm
		//
		this.Realm.FormattingEnabled = true;
		this.Realm.Items.AddRange(new object[4] { "East", "West", "Europe", "Asia" });
		this.Realm.Location = new System.Drawing.Point(126, 218);
		this.Realm.Name = "Realm";
		this.Realm.Size = new System.Drawing.Size(174, 25);
		this.Realm.TabIndex = 12;
		this.Realm.Text = "East";
		//
		// Mode
		//
		this.Mode.FormattingEnabled = true;
		this.Mode.Items.AddRange(new object[5] { "Battle.net", "Single Player", "Open Battle.net", "Host TCP/IP Game", "Join TCP/IP Game" });
		this.Mode.Location = new System.Drawing.Point(126, 249);
		this.Mode.Name = "Mode";
		this.Mode.Size = new System.Drawing.Size(174, 25);
		this.Mode.TabIndex = 13;
		this.Mode.Text = "Battle.net";
		//
		// Parameters
		//
		this.Parameters.Location = new System.Drawing.Point(129, 63);
		this.Parameters.Name = "Parameters";
		this.Parameters.Size = new System.Drawing.Size(174, 25);
		this.Parameters.TabIndex = 14;
		//
		// DiabloPath
		//
		this.DiabloPath.Location = new System.Drawing.Point(129, 32);
		this.DiabloPath.Name = "DiabloPath";
		this.DiabloPath.Size = new System.Drawing.Size(148, 25);
		this.DiabloPath.TabIndex = 15;
		//
		// label8
		//
		this.label8.AutoSize = true;
		this.label8.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label8.Location = new System.Drawing.Point(36, 221);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(44, 17);
		this.label8.TabIndex = 83;
		this.label8.Text = "Realm";
		//
		// label10
		//
		this.label10.AutoSize = true;
		this.label10.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label10.Location = new System.Drawing.Point(36, 252);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(43, 17);
		this.label10.TabIndex = 84;
		this.label10.Text = "Mode";
		//
		// label11
		//
		this.label11.AutoSize = true;
		this.label11.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label11.Location = new System.Drawing.Point(34, 97);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(74, 17);
		this.label11.TabIndex = 85;
		this.label11.Text = "Entry Script";
		//
		// label
		//
		this.label.AutoSize = true;
		this.label.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label.Location = new System.Drawing.Point(34, 35);
		this.label.Name = "label";
		this.label.Size = new System.Drawing.Size(71, 17);
		this.label.TabIndex = 86;
		this.label.Text = "Game Path";
		//
		// label13
		//
		this.label13.AutoSize = true;
		this.label13.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label13.Location = new System.Drawing.Point(34, 66);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(74, 17);
		this.label13.TabIndex = 87;
		this.label13.Text = "Parameters";
		//
		// D2Path
		//
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
		this.D2Path.Click += new System.EventHandler(this.OpenD2Path);
		//
		// panel1
		//
		this.panel1.BackColor = System.Drawing.SystemColors.Window;
		this.panel1.Controls.Add(this.splitContainer1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(684, 302);
		this.panel1.TabIndex = 89;
		//
		// splitContainer1
		//
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.IsSplitterFixed = true;
		this.splitContainer1.Location = new System.Drawing.Point(0, 0);
		this.splitContainer1.Name = "splitContainer1";
		//
		// splitContainer1.Panel1
		//
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
		//
		// splitContainer1.Panel2
		//
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
		this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
		this.splitContainer1.Size = new System.Drawing.Size(684, 302);
		this.splitContainer1.SplitterDistance = 340;
		this.splitContainer1.SplitterWidth = 1;
		this.splitContainer1.TabIndex = 111;
		//
		// Account
		//
		this.Account.Location = new System.Drawing.Point(126, 63);
		this.Account.MaxLength = 16;
		this.Account.Name = "Account";
		this.Account.Size = new System.Drawing.Size(174, 25);
		this.Account.TabIndex = 1;
		//
		// label5
		//
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label5.Location = new System.Drawing.Point(36, 159);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(68, 17);
		this.label5.TabIndex = 110;
		this.label5.Text = "Game Info";
		//
		// label4
		//
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label4.Location = new System.Drawing.Point(36, 127);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(64, 17);
		this.label4.TabIndex = 109;
		this.label4.Text = "Character";
		//
		// label3
		//
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label3.Location = new System.Drawing.Point(36, 97);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(64, 17);
		this.label3.TabIndex = 108;
		this.label3.Text = "Password";
		//
		// label2
		//
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label2.Location = new System.Drawing.Point(36, 66);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(54, 17);
		this.label2.TabIndex = 107;
		this.label2.Text = "Account";
		//
		// GameName
		//
		this.GameName.Location = new System.Drawing.Point(126, 156);
		this.GameName.MaxLength = 25;
		this.GameName.Name = "GameName";
		this.GameName.Size = new System.Drawing.Size(84, 25);
		this.GameName.TabIndex = 4;
		this.GameName.Text = "Name";
		//
		// Character
		//
		this.Character.Location = new System.Drawing.Point(126, 125);
		this.Character.MaxLength = 16;
		this.Character.Name = "Character";
		this.Character.Size = new System.Drawing.Size(174, 25);
		this.Character.TabIndex = 3;
		//
		// Password
		//
		this.Password.Location = new System.Drawing.Point(126, 94);
		this.Password.MaxLength = 25;
		this.Password.Name = "Password";
		this.Password.Size = new System.Drawing.Size(174, 25);
		this.Password.TabIndex = 2;
		this.Password.UseSystemPasswordChar = true;
		//
		// label1
		//
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label1.Location = new System.Drawing.Point(36, 35);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(84, 17);
		this.label1.TabIndex = 106;
		this.label1.Text = "Profile Name";
		//
		// ProfileName
		//
		this.ProfileName.AccessibleName = "ProfileName";
		this.ProfileName.Location = new System.Drawing.Point(126, 32);
		this.ProfileName.Name = "ProfileName";
		this.ProfileName.Size = new System.Drawing.Size(174, 25);
		this.ProfileName.TabIndex = 0;
		//
		// EntryScript
		//
		this.EntryScript.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.EntryScript.FormattingEnabled = true;
		this.EntryScript.Location = new System.Drawing.Point(129, 94);
		this.EntryScript.Name = "EntryScript";
		this.EntryScript.Size = new System.Drawing.Size(174, 25);
		this.EntryScript.Sorted = true;
		this.EntryScript.TabIndex = 101;
		//
		// scheduleDrop
		//
		this.scheduleDrop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.scheduleDrop.FormattingEnabled = true;
		this.scheduleDrop.Location = new System.Drawing.Point(129, 156);
		this.scheduleDrop.Name = "scheduleDrop";
		this.scheduleDrop.Size = new System.Drawing.Size(174, 25);
		this.scheduleDrop.Sorted = true;
		this.scheduleDrop.TabIndex = 19;
		//
		// clientVisible
		//
		this.clientVisible.AccessibleName = "clientVisible";
		this.clientVisible.AutoSize = true;
		this.clientVisible.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.clientVisible.Location = new System.Drawing.Point(238, 251);
		this.clientVisible.Name = "clientVisible";
		this.clientVisible.Size = new System.Drawing.Size(65, 21);
		this.clientVisible.TabIndex = 6;
		this.clientVisible.Text = "Visible";
		this.clientVisible.UseVisualStyleBackColor = true;
		//
		// clientLocation
		//
		this.clientLocation.Location = new System.Drawing.Point(129, 249);
		this.clientLocation.Name = "clientLocation";
		this.clientLocation.Size = new System.Drawing.Size(72, 25);
		this.clientLocation.TabIndex = 7;
		this.clientLocation.Text = " x, y";
		//
		// label17
		//
		this.label17.AutoSize = true;
		this.label17.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label17.Location = new System.Drawing.Point(34, 252);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(57, 17);
		this.label17.TabIndex = 100;
		this.label17.Text = "Location";
		//
		// useSchedule
		//
		this.useSchedule.AccessibleName = "useSchedule";
		this.useSchedule.AutoSize = true;
		this.useSchedule.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.useSchedule.Location = new System.Drawing.Point(219, 189);
		this.useSchedule.Name = "useSchedule";
		this.useSchedule.Size = new System.Drawing.Size(84, 21);
		this.useSchedule.TabIndex = 25;
		this.useSchedule.Text = "Scheduler";
		this.useSchedule.UseVisualStyleBackColor = true;
		//
		// InfoTag
		//
		this.InfoTag.Location = new System.Drawing.Point(129, 187);
		this.InfoTag.Name = "InfoTag";
		this.InfoTag.Size = new System.Drawing.Size(72, 25);
		this.InfoTag.TabIndex = 22;
		//
		// label15
		//
		this.label15.AutoSize = true;
		this.label15.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label15.Location = new System.Drawing.Point(34, 190);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(55, 17);
		this.label15.TabIndex = 95;
		this.label15.Text = "Info Tag";
		//
		// keyListDrop
		//
		this.keyListDrop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.keyListDrop.FormattingEnabled = true;
		this.keyListDrop.Location = new System.Drawing.Point(129, 125);
		this.keyListDrop.Name = "keyListDrop";
		this.keyListDrop.Size = new System.Drawing.Size(174, 25);
		this.keyListDrop.Sorted = true;
		this.keyListDrop.TabIndex = 8;
		//
		// label14
		//
		this.label14.AutoSize = true;
		this.label14.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label14.Location = new System.Drawing.Point(34, 159);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(60, 17);
		this.label14.TabIndex = 91;
		this.label14.Text = "Schedule";
		//
		// switchKeys
		//
		this.switchKeys.AccessibleName = "switchKeys";
		this.switchKeys.AutoSize = true;
		this.switchKeys.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.switchKeys.Location = new System.Drawing.Point(209, 220);
		this.switchKeys.Name = "switchKeys";
		this.switchKeys.Size = new System.Drawing.Size(94, 21);
		this.switchKeys.TabIndex = 24;
		this.switchKeys.Text = "Switch Keys";
		this.switchKeys.UseVisualStyleBackColor = true;
		//
		// label12
		//
		this.label12.AutoSize = true;
		this.label12.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label12.Location = new System.Drawing.Point(34, 128);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(52, 17);
		this.label12.TabIndex = 91;
		this.label12.Text = "Key List";
		//
		// panel2
		//
		this.panel2.Controls.Add(this.OK);
		this.panel2.Controls.Add(this.Cancel);
		this.panel2.Controls.Add(this.Apply);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 302);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(684, 59);
		this.panel2.TabIndex = 90;
		//
		// D2ProfileEditor
		//
		this.AcceptButton = this.OK;
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.CancelButton = this.Cancel;
		this.ClientSize = new System.Drawing.Size(684, 361);
		this.Controls.Add(this.panel1);
		this.Controls.Add(this.panel2);
		this.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.MaximizeBox = false;
		this.MaximumSize = new System.Drawing.Size(700, 400);
		this.MinimizeBox = false;
		this.MinimumSize = new System.Drawing.Size(700, 400);
		this.Name = "D2ProfileEditor";
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Profile Editor";
		this.TopMost = true;
		this.panel1.ResumeLayout(false);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel1.PerformLayout();
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.Panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.ResumeLayout(false);
	}
}
