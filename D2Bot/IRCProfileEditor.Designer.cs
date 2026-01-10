namespace D2Bot;

partial class IRCProfileEditor
{
	private System.ComponentModel.IContainer components;

	public System.Windows.Forms.Panel panel1;

	private System.Windows.Forms.Label label13;

	public System.Windows.Forms.TextBox Host;

	public System.Windows.Forms.Button OK;

	public System.Windows.Forms.Button Cancel;

	public System.Windows.Forms.Button Apply;

	public System.Windows.Forms.TextBox Port;

	public System.Windows.Forms.TextBox Password;

	private System.Windows.Forms.Label label2;

	public System.Windows.Forms.TextBox Username;

	private System.Windows.Forms.Label label4;

	public System.Windows.Forms.TextBox Nick;

	private System.Windows.Forms.Label label6;

	public System.Windows.Forms.TextBox Confirmation;

	private System.Windows.Forms.Label label5;

	public System.Windows.Forms.TextBox Channel;

	private System.Windows.Forms.Label label7;

	public System.Windows.Forms.TextBox Profile;

	public System.Windows.Forms.CheckBox UseChannel;

	private System.Windows.Forms.Label label8;

	public System.Windows.Forms.TextBox Prefix;

	private System.Windows.Forms.Label label3;

	private System.Windows.Forms.Panel panel2;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(D2Bot.IRCProfileEditor));
		this.panel1 = new System.Windows.Forms.Panel();
		this.UseChannel = new System.Windows.Forms.CheckBox();
		this.label8 = new System.Windows.Forms.Label();
		this.Prefix = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.Profile = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.Confirmation = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.Channel = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.Nick = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.Password = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.Username = new System.Windows.Forms.TextBox();
		this.Port = new System.Windows.Forms.TextBox();
		this.label13 = new System.Windows.Forms.Label();
		this.Host = new System.Windows.Forms.TextBox();
		this.OK = new System.Windows.Forms.Button();
		this.Cancel = new System.Windows.Forms.Button();
		this.Apply = new System.Windows.Forms.Button();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.SuspendLayout();
		//
		// panel1
		//
		this.panel1.BackColor = System.Drawing.SystemColors.Window;
		this.panel1.Controls.Add(this.UseChannel);
		this.panel1.Controls.Add(this.label8);
		this.panel1.Controls.Add(this.Prefix);
		this.panel1.Controls.Add(this.label7);
		this.panel1.Controls.Add(this.Profile);
		this.panel1.Controls.Add(this.label6);
		this.panel1.Controls.Add(this.Confirmation);
		this.panel1.Controls.Add(this.label5);
		this.panel1.Controls.Add(this.Channel);
		this.panel1.Controls.Add(this.label4);
		this.panel1.Controls.Add(this.Nick);
		this.panel1.Controls.Add(this.label3);
		this.panel1.Controls.Add(this.Password);
		this.panel1.Controls.Add(this.label2);
		this.panel1.Controls.Add(this.Username);
		this.panel1.Controls.Add(this.Port);
		this.panel1.Controls.Add(this.label13);
		this.panel1.Controls.Add(this.Host);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(300, 361);
		this.panel1.TabIndex = 90;
		//
		// UseChannel
		//
		this.UseChannel.AutoSize = true;
		this.UseChannel.Location = new System.Drawing.Point(164, 255);
		this.UseChannel.Margin = new System.Windows.Forms.Padding(4);
		this.UseChannel.Name = "UseChannel";
		this.UseChannel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.UseChannel.Size = new System.Drawing.Size(103, 21);
		this.UseChannel.TabIndex = 9;
		this.UseChannel.Text = "Msg Channel";
		this.UseChannel.UseVisualStyleBackColor = true;
		//
		// label8
		//
		this.label8.AutoSize = true;
		this.label8.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label8.Location = new System.Drawing.Point(25, 256);
		this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(70, 17);
		this.label8.TabIndex = 119;
		this.label8.Text = "Msg Prefix";
		//
		// Prefix
		//
		this.Prefix.Location = new System.Drawing.Point(119, 253);
		this.Prefix.Margin = new System.Windows.Forms.Padding(4);
		this.Prefix.Name = "Prefix";
		this.Prefix.Size = new System.Drawing.Size(36, 25);
		this.Prefix.TabIndex = 8;
		//
		// label7
		//
		this.label7.AutoSize = true;
		this.label7.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label7.Location = new System.Drawing.Point(25, 33);
		this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(45, 17);
		this.label7.TabIndex = 117;
		this.label7.Text = "Profile";
		//
		// Profile
		//
		this.Profile.Location = new System.Drawing.Point(119, 30);
		this.Profile.Margin = new System.Windows.Forms.Padding(0);
		this.Profile.Name = "Profile";
		this.Profile.Size = new System.Drawing.Size(148, 25);
		this.Profile.TabIndex = 0;
		//
		// label6
		//
		this.label6.AutoSize = true;
		this.label6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label6.Location = new System.Drawing.Point(25, 225);
		this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(83, 17);
		this.label6.TabIndex = 113;
		this.label6.Text = "Confirmation";
		//
		// Confirmation
		//
		this.Confirmation.Location = new System.Drawing.Point(119, 222);
		this.Confirmation.Margin = new System.Windows.Forms.Padding(4);
		this.Confirmation.Name = "Confirmation";
		this.Confirmation.Size = new System.Drawing.Size(148, 25);
		this.Confirmation.TabIndex = 7;
		this.Confirmation.UseSystemPasswordChar = true;
		//
		// label5
		//
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label5.Location = new System.Drawing.Point(25, 193);
		this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(54, 17);
		this.label5.TabIndex = 111;
		this.label5.Text = "Channel";
		//
		// Channel
		//
		this.Channel.Location = new System.Drawing.Point(119, 190);
		this.Channel.Margin = new System.Windows.Forms.Padding(4);
		this.Channel.Name = "Channel";
		this.Channel.Size = new System.Drawing.Size(148, 25);
		this.Channel.TabIndex = 6;
		//
		// label4
		//
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label4.Location = new System.Drawing.Point(25, 161);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(33, 17);
		this.label4.TabIndex = 109;
		this.label4.Text = "Nick";
		//
		// Nick
		//
		this.Nick.Location = new System.Drawing.Point(119, 158);
		this.Nick.Margin = new System.Windows.Forms.Padding(4);
		this.Nick.Name = "Nick";
		this.Nick.Size = new System.Drawing.Size(148, 25);
		this.Nick.TabIndex = 5;
		//
		// label3
		//
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label3.Location = new System.Drawing.Point(25, 129);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(64, 17);
		this.label3.TabIndex = 107;
		this.label3.Text = "Password";
		//
		// Password
		//
		this.Password.Location = new System.Drawing.Point(119, 126);
		this.Password.Margin = new System.Windows.Forms.Padding(4);
		this.Password.Name = "Password";
		this.Password.Size = new System.Drawing.Size(148, 25);
		this.Password.TabIndex = 4;
		this.Password.UseSystemPasswordChar = true;
		//
		// label2
		//
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label2.Location = new System.Drawing.Point(25, 97);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(67, 17);
		this.label2.TabIndex = 105;
		this.label2.Text = "Username";
		//
		// Username
		//
		this.Username.Location = new System.Drawing.Point(119, 94);
		this.Username.Margin = new System.Windows.Forms.Padding(4);
		this.Username.Name = "Username";
		this.Username.Size = new System.Drawing.Size(148, 25);
		this.Username.TabIndex = 3;
		//
		// Port
		//
		this.Port.Location = new System.Drawing.Point(221, 62);
		this.Port.Margin = new System.Windows.Forms.Padding(4);
		this.Port.Name = "Port";
		this.Port.Size = new System.Drawing.Size(46, 25);
		this.Port.TabIndex = 2;
		this.Port.Text = "Port";
		//
		// label13
		//
		this.label13.AutoSize = true;
		this.label13.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.label13.Location = new System.Drawing.Point(25, 65);
		this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(35, 17);
		this.label13.TabIndex = 87;
		this.label13.Text = "Host";
		//
		// Host
		//
		this.Host.Location = new System.Drawing.Point(119, 62);
		this.Host.Margin = new System.Windows.Forms.Padding(4);
		this.Host.Name = "Host";
		this.Host.Size = new System.Drawing.Size(94, 25);
		this.Host.TabIndex = 1;
		this.Host.Text = "Server";
		//
		// OK
		//
		this.OK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.OK.Location = new System.Drawing.Point(12, 17);
		this.OK.Name = "OK";
		this.OK.Size = new System.Drawing.Size(88, 30);
		this.OK.TabIndex = 10;
		this.OK.Text = "OK";
		this.OK.UseVisualStyleBackColor = true;
		this.OK.Click += new System.EventHandler(this.OK_Click);
		//
		// Cancel
		//
		this.Cancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.Cancel.Location = new System.Drawing.Point(106, 17);
		this.Cancel.Name = "Cancel";
		this.Cancel.Size = new System.Drawing.Size(88, 30);
		this.Cancel.TabIndex = 11;
		this.Cancel.Text = "Cancel";
		this.Cancel.UseVisualStyleBackColor = true;
		this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
		//
		// Apply
		//
		this.Apply.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.Apply.Location = new System.Drawing.Point(200, 17);
		this.Apply.Name = "Apply";
		this.Apply.Size = new System.Drawing.Size(88, 30);
		this.Apply.TabIndex = 12;
		this.Apply.Text = "Apply";
		this.Apply.UseVisualStyleBackColor = true;
		this.Apply.Click += new System.EventHandler(this.Apply_Click);
		//
		// panel2
		//
		this.panel2.Controls.Add(this.OK);
		this.panel2.Controls.Add(this.Cancel);
		this.panel2.Controls.Add(this.Apply);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 302);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(300, 59);
		this.panel2.TabIndex = 120;
		//
		// IRCProfileEditor
		//
		this.AcceptButton = this.OK;
		this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.CancelButton = this.Cancel;
		this.ClientSize = new System.Drawing.Size(300, 361);
		this.Controls.Add(this.panel2);
		this.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.Margin = new System.Windows.Forms.Padding(4);
		this.MaximizeBox = false;
		this.MaximumSize = new System.Drawing.Size(316, 400);
		this.MinimizeBox = false;
		this.MinimumSize = new System.Drawing.Size(316, 400);
		this.Name = "IRCProfileEditor";
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "IRC Profile";
		this.TopMost = true;
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.panel2.ResumeLayout(false);
		this.ResumeLayout(false);
	}
}
