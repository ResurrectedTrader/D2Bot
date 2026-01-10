using System;
using System.Windows.Forms;

namespace D2Bot;

public partial class IRCProfileEditor : Form
{
	private IRCProfile m_profile;

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
				Text = "Add IRC Profile";
				Profile.Text = string.Empty;
				Username.Text = string.Empty;
				Password.Text = string.Empty;
				Nick.Text = string.Empty;
				Channel.Text = string.Empty;
				Confirmation.Text = string.Empty;
				UseChannel.Checked = false;
				Prefix.Text = string.Empty;
			}
			else
			{
				Text = "Edit IRC Profile";
				Profile.Text = m_profile.Name;
				Host.Text = m_profile.Host;
				Username.Text = m_profile.Username;
				Password.Text = m_profile.Password;
				Nick.Text = m_profile.Nick;
				Channel.Text = m_profile.Channel;
				Port.Text = m_profile.Port.ToString();
				Confirmation.Text = m_profile.Confirmation;
				UseChannel.Checked = m_profile.MsgChannel;
				Prefix.Text = m_profile.Prefix;
			}
		}
	}

	public IRCProfile ProfileToEdit
	{
		get
		{
			return m_profile;
		}
		set
		{
			m_profile = value;
		}
	}

	public IRCProfileEditor()
	{
		InitializeComponent();
	}

	private bool UpdateProfile()
	{
		if (!uint.TryParse(Port.Text, out var result))
		{
			MessageBox.Show("Invalid Port Specified!", "D2Bot", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Port.Select();
			return false;
		}
		if (m_isNew)
		{
			if (Program.ProfileList.ContainsKey(Profile.Text.Trim().ToLower()))
			{
				MessageBox.Show("Profile name already exists, please choose unique name!", "D2Bot", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Profile.Select();
				return false;
			}
			m_profile.Name = Profile.Text.Trim();
			m_profile.Add();
		}
		else
		{
			if (!Program.CanRenameItem(m_profile.Name, Profile.Text.Trim()))
			{
				MessageBox.Show("Profile name already exists, please choose unique name!", "D2Bot", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Profile.Select();
				return false;
			}
			Program.RenameItem(m_profile.Name, Profile.Text.Trim());
		}
		m_profile.Channel = Channel.Text.Trim();
		m_profile.Confirmation = Confirmation.Text.Trim();
		m_profile.Host = Host.Text.Trim();
		m_profile.Nick = Nick.Text.Trim();
		m_profile.Password = Password.Text;
		m_profile.Username = Username.Text.Trim();
		m_profile.Prefix = Prefix.Text.Trim();
		m_profile.MsgChannel = UseChannel.Checked;
		m_profile.Port = result;
		Program.GM.objectProfileList.RefreshObject((object)m_profile);
		Program.SaveProfiles();
		return true;
	}

	private void OK_Click(object sender, EventArgs e)
	{
		if (UpdateProfile())
		{
			Close();
		}
	}

	private void Cancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void Apply_Click(object sender, EventArgs e)
	{
		if (UpdateProfile())
		{
			IsNew = false;
		}
	}
}
