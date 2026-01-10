using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace D2Bot;

public class About : Form
{
	private IContainer components;

	public Button Button;

	private Panel panel2;

	public Panel panel1;

	private LinkLabel linkLabel1;

	private Label label7;

	private Label Copyright;

	private Label label6;

	private Label Version;

	private Label label2;

	private Label Title;

	private LinkLabel linkLabel3;

	private LinkLabel linkLabel2;

	private Label label1;

	private PictureBox pictureBox1;

	private Label UpdateLabel;

	private Label label3;

	private LinkLabel linkLabel4;

	private LinkLabel linkLabel5;

	public About()
	{
		InitializeComponent();
		Button.Text = "OK";
		UpdateLabel.Visible = false;
		Version.Text = "Version: " + Program.VER;
		Copyright.Text = "Copyright © 2011 - " + DateTime.Today.Year;
	}

	private void Button_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void About_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Process.Start((sender as LinkLabel).Text);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(D2Bot.About));
		this.Button = new System.Windows.Forms.Button();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.linkLabel5 = new System.Windows.Forms.LinkLabel();
		this.linkLabel4 = new System.Windows.Forms.LinkLabel();
		this.label3 = new System.Windows.Forms.Label();
		this.linkLabel3 = new System.Windows.Forms.LinkLabel();
		this.linkLabel2 = new System.Windows.Forms.LinkLabel();
		this.label1 = new System.Windows.Forms.Label();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.UpdateLabel = new System.Windows.Forms.Label();
		this.linkLabel1 = new System.Windows.Forms.LinkLabel();
		this.label7 = new System.Windows.Forms.Label();
		this.Copyright = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.Version = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.Title = new System.Windows.Forms.Label();
		this.panel2.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.Button.Location = new System.Drawing.Point(200, 17);
		this.Button.Name = "Button";
		this.Button.Size = new System.Drawing.Size(88, 30);
		this.Button.TabIndex = 11;
		this.Button.Text = "OK";
		this.Button.UseVisualStyleBackColor = true;
		this.Button.Click += new System.EventHandler(Button_Click);
		this.panel2.Controls.Add(this.Button);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 302);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(300, 59);
		this.panel2.TabIndex = 120;
		this.panel1.BackColor = System.Drawing.SystemColors.Window;
		this.panel1.Controls.Add(this.linkLabel5);
		this.panel1.Controls.Add(this.linkLabel4);
		this.panel1.Controls.Add(this.label3);
		this.panel1.Controls.Add(this.linkLabel3);
		this.panel1.Controls.Add(this.linkLabel2);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.pictureBox1);
		this.panel1.Controls.Add(this.UpdateLabel);
		this.panel1.Controls.Add(this.linkLabel1);
		this.panel1.Controls.Add(this.label7);
		this.panel1.Controls.Add(this.Copyright);
		this.panel1.Controls.Add(this.label6);
		this.panel1.Controls.Add(this.Version);
		this.panel1.Controls.Add(this.label2);
		this.panel1.Controls.Add(this.Title);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(300, 361);
		this.panel1.TabIndex = 90;
		this.linkLabel5.AutoSize = true;
		this.linkLabel5.Location = new System.Drawing.Point(61, 237);
		this.linkLabel5.Name = "linkLabel5";
		this.linkLabel5.Size = new System.Drawing.Size(154, 17);
		this.linkLabel5.TabIndex = 15;
		this.linkLabel5.TabStop = true;
		this.linkLabel5.Text = "https://github.com/noah-";
		this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(About_LinkClicked);
		this.linkLabel4.AutoSize = true;
		this.linkLabel4.Location = new System.Drawing.Point(15, 124);
		this.linkLabel4.Name = "linkLabel4";
		this.linkLabel4.Size = new System.Drawing.Size(171, 17);
		this.linkLabel4.TabIndex = 14;
		this.linkLabel4.TabStop = true;
		this.linkLabel4.Text = "https://discord.gg/z844XRhxFC";
		this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(About_LinkClicked);
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(48, 30);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(145, 17);
		this.label3.TabIndex = 13;
		this.label3.Text = "A D2BS Game Manager";
		this.linkLabel3.AutoSize = true;
		this.linkLabel3.Location = new System.Drawing.Point(15, 158);
		this.linkLabel3.Name = "linkLabel3";
		this.linkLabel3.Size = new System.Drawing.Size(188, 17);
		this.linkLabel3.TabIndex = 12;
		this.linkLabel3.TabStop = true;
		this.linkLabel3.Text = "https://d2bot.discourse.group/";
		this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(About_LinkClicked);
		this.linkLabel2.AutoSize = true;
		this.linkLabel2.Location = new System.Drawing.Point(15, 141);
		this.linkLabel2.Name = "linkLabel2";
		this.linkLabel2.Size = new System.Drawing.Size(266, 17);
		this.linkLabel2.TabIndex = 11;
		this.linkLabel2.TabStop = true;
		this.linkLabel2.Text = "https://github.com/blizzhackers/kolbot";
		this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(About_LinkClicked);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(15, 107);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(236, 17);
		this.label1.TabIndex = 10;
		this.label1.Text = "For releases, guides, and support, visit:";
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new System.Drawing.Point(15, 15);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(32, 32);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.pictureBox1.TabIndex = 9;
		this.pictureBox1.TabStop = false;
		this.UpdateLabel.AutoSize = true;
		this.UpdateLabel.Location = new System.Drawing.Point(15, 206);
		this.UpdateLabel.Name = "UpdateLabel";
		this.UpdateLabel.Size = new System.Drawing.Size(106, 17);
		this.UpdateLabel.TabIndex = 8;
		this.UpdateLabel.Text = "Update available";
		this.linkLabel1.AutoSize = true;
		this.linkLabel1.Location = new System.Drawing.Point(15, 77);
		this.linkLabel1.Name = "linkLabel1";
		this.linkLabel1.Size = new System.Drawing.Size(228, 17);
		this.linkLabel1.TabIndex = 7;
		this.linkLabel1.TabStop = true;
		this.linkLabel1.Text = "https://github.com/d2botsharp/d2bot";
		this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(About_LinkClicked);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(15, 271);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(120, 17);
		this.label7.TabIndex = 6;
		this.label7.Text = "All Rights Reserved";
		this.Copyright.AutoSize = true;
		this.Copyright.Location = new System.Drawing.Point(15, 254);
		this.Copyright.Name = "Copyright";
		this.Copyright.Size = new System.Drawing.Size(154, 17);
		this.Copyright.TabIndex = 5;
		this.Copyright.Text = "Copyright © 2011 - 2020";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(15, 237);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(54, 17);
		this.label6.TabIndex = 4;
		this.label6.Text = "Author: ";
		this.Version.AutoSize = true;
		this.Version.Location = new System.Drawing.Point(15, 189);
		this.Version.Name = "Version";
		this.Version.Size = new System.Drawing.Size(58, 17);
		this.Version.TabIndex = 3;
		this.Version.Text = "Version: ";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(15, 60);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(179, 17);
		this.label2.TabIndex = 1;
		this.label2.Text = "For license, bug reports, visit:";
		this.Title.AutoSize = true;
		this.Title.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.Title.Location = new System.Drawing.Point(48, 15);
		this.Title.Name = "Title";
		this.Title.Size = new System.Drawing.Size(57, 17);
		this.Title.TabIndex = 0;
		this.Title.Text = "D2Bot #";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 17f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(300, 361);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(4);
		base.MaximizeBox = false;
		this.MaximumSize = new System.Drawing.Size(316, 400);
		base.MinimizeBox = false;
		this.MinimumSize = new System.Drawing.Size(316, 400);
		base.Name = "About";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "About";
		base.TopMost = true;
		this.panel2.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
	}
}
