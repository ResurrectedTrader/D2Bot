using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace D2Bot;

public partial class About : Form
{
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
}
