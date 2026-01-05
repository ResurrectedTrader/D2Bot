using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace D2Bot;

public class ListEditor : Form
{
	private IContainer components;

	private Panel panel2;

	public Button Apply;

	public Button Okay;

	private TableLayoutPanel tableLayoutPanel1;

	private ListView LEListBox;

	private DataGridView LEDataGrid;

	public ColumnHeader leName;

	public Button Cancel;

	public TextBox NewListInput;

	public Button DeleteList;

	public Button ImportList;

	public Button AddNewList;

	private IListData Content { get; set; }

	public ListEditor(IListData content)
	{
		InitializeComponent();
		Content = content;
		LEListBox.Items.Clear();
		LEDataGrid.Rows.Clear();
		Text = Content.WindowTitle;
		leName.Text = Content.ListTitle;
		leName.Width = LEListBox.Width;
		for (int i = 0; i < Content.Columns.Length; i++)
		{
			LEDataGrid.Columns.Add(i.ToString(), Content.Columns[i]);
			LEDataGrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			LEDataGrid.Columns[i].FillWeight = ((i == 0) ? 16 : 42);
			LEDataGrid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
		}
		LEDataGrid.Columns[Content.Columns.Length - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		if (Content.ListNames == null)
		{
			return;
		}
		Content.ListNames.Sort();
		foreach (string listName in Content.ListNames)
		{
			LEListBox.Items.Add(new ListViewItem(listName));
		}
		if (LEListBox.Items.Count > 0)
		{
			LEListBox.Items[0].Selected = true;
		}
	}

	public void LoadListEditor()
	{
		if (LEListBox.SelectedItems.Count < 1 || LEListBox.SelectedItems[0].Text.Length < 1)
		{
			return;
		}
		List<string[]> columnData = Content.GetColumnData(LEListBox.SelectedItems[0].Text);
		LEDataGrid.Rows.Clear();
		LEDataGrid.Rows.Add(columnData.Count + 1);
		for (int i = 0; i < columnData.Count; i++)
		{
			for (int j = 0; j < LEDataGrid.Columns.Count; j++)
			{
				LEDataGrid.Rows[i].Cells[j].Value = columnData[i][j];
			}
		}
	}

	public void SaveListEditor()
	{
		if (LEListBox.SelectedItems.Count < 1 || LEListBox.SelectedItems[0].Text.Length < 1)
		{
			return;
		}
		List<string[]> list = new List<string[]>();
		string name = LEListBox.SelectedItems[0].Text;
		for (int i = 0; i < LEDataGrid.Rows.Count; i++)
		{
			if (LEDataGrid.Rows[i].Cells[0].Value == null || LEDataGrid.Rows[i].Cells[0].Value.ToString().Length == 0)
			{
				continue;
			}
			string[] array = new string[LEDataGrid.Columns.Count];
			for (int j = 0; j < LEDataGrid.Columns.Count; j++)
			{
				if (LEDataGrid.Rows[i].Cells[j].Value != null)
				{
					array[j] = LEDataGrid.Rows[i].Cells[j].Value.ToString();
				}
				else
				{
					array[j] = "";
				}
			}
			list.Add(array);
		}
		try
		{
			Content.SaveListData(name, list);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "An error has occured!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void New_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog
		{
			InitialDirectory = Application.StartupPath,
			RestoreDirectory = true,
			CheckFileExists = false,
			CheckPathExists = false,
			Filter = "CSV (*.csv)|*.csv",
			FilterIndex = 1
		};
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			string text = Path.GetFileNameWithoutExtension(openFileDialog.FileName).ToLower();
			if (Content.Contains(text))
			{
				MessageBox.Show("This name already exists!", "An error has occured!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else if (File.Exists(openFileDialog.FileName))
			{
				ImportContent(openFileDialog.FileName, text);
			}
			else
			{
				AddNew(text);
			}
		}
	}

	private void Add_Click(object sender, EventArgs e)
	{
		string text = NewListInput.Text.ToLower();
		if (text.Length != 0)
		{
			if (Content.Contains(text))
			{
				MessageBox.Show("This name already exists!", "An error has occured!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				AddNew(text);
			}
		}
	}

	private void AddNew(string v)
	{
		LEListBox.Items.Add(v.ToLower());
		LEListBox.Items[LEListBox.Items.Count - 1].Selected = true;
	}

	private void ImportContent(string fileName, string name)
	{
		string[] array = File.ReadAllLines(fileName);
		List<string[]> list = new List<string[]>(array.Length);
		LEDataGrid.Rows.Clear();
		LEDataGrid.Rows.Add(array.Length);
		for (int i = 0; i < array.Length; i++)
		{
			string[] item = array[i].Split(',');
			list.Add(item);
		}
		Content.SaveListData(name, list);
		LEListBox.Items.Add(new ListViewItem(name.ToLower()));
		LEListBox.Items[LEListBox.Items.Count - 1].Selected = true;
	}

	private void Delete_Click(object sender, EventArgs e)
	{
		if (LEListBox.SelectedItems.Count >= 1 && LEListBox.SelectedItems[0].Text.Length >= 1)
		{
			string text = LEListBox.SelectedItems[0].Text;
			int num = LEListBox.SelectedIndices[0];
			LEListBox.Items.RemoveAt(num);
			Content.DeleteList(text.ToLower());
			if (num > 0)
			{
				LEListBox.Items[num - 1].Selected = true;
			}
			else
			{
				LEDataGrid.Rows.Clear();
			}
		}
	}

	private void Apply_Click(object sender, EventArgs e)
	{
		SaveListEditor();
	}

	private void OK_Click(object sender, EventArgs e)
	{
		Apply_Click(sender, e);
		Cancel_Click(sender, e);
	}

	private void Cancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void LEDataGrid_Click(object sender, EventArgs e)
	{
		if (LEListBox.SelectedItems.Count >= 1 && LEListBox.SelectedItems[0].Text.Length >= 1)
		{
			DataGridView dataGridView = sender as DataGridView;
			if (dataGridView.Rows.Count == 0 || dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[0].Value != null)
			{
				dataGridView.Rows.Add(1);
			}
		}
	}

	private void LEListBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		LoadListEditor();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(D2Bot.ListEditor));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		this.panel2 = new System.Windows.Forms.Panel();
		this.DeleteList = new System.Windows.Forms.Button();
		this.ImportList = new System.Windows.Forms.Button();
		this.AddNewList = new System.Windows.Forms.Button();
		this.NewListInput = new System.Windows.Forms.TextBox();
		this.Okay = new System.Windows.Forms.Button();
		this.Cancel = new System.Windows.Forms.Button();
		this.Apply = new System.Windows.Forms.Button();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.LEListBox = new System.Windows.Forms.ListView();
		this.leName = new System.Windows.Forms.ColumnHeader();
		this.LEDataGrid = new System.Windows.Forms.DataGridView();
		this.panel2.SuspendLayout();
		this.tableLayoutPanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.LEDataGrid).BeginInit();
		base.SuspendLayout();
		this.panel2.Controls.Add(this.DeleteList);
		this.panel2.Controls.Add(this.ImportList);
		this.panel2.Controls.Add(this.AddNewList);
		this.panel2.Controls.Add(this.NewListInput);
		this.panel2.Controls.Add(this.Okay);
		this.panel2.Controls.Add(this.Cancel);
		this.panel2.Controls.Add(this.Apply);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 302);
		this.panel2.MinimumSize = new System.Drawing.Size(684, 59);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(684, 59);
		this.panel2.TabIndex = 121;
		this.DeleteList.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.DeleteList.BackgroundImage = (System.Drawing.Image)resources.GetObject("DeleteList.BackgroundImage");
		this.DeleteList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.DeleteList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.DeleteList.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
		this.DeleteList.Location = new System.Drawing.Point(171, 19);
		this.DeleteList.Margin = new System.Windows.Forms.Padding(0);
		this.DeleteList.Name = "DeleteList";
		this.DeleteList.Size = new System.Drawing.Size(25, 25);
		this.DeleteList.TabIndex = 26;
		this.DeleteList.UseVisualStyleBackColor = false;
		this.DeleteList.Click += new System.EventHandler(Delete_Click);
		this.ImportList.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.ImportList.BackgroundImage = (System.Drawing.Image)resources.GetObject("ImportList.BackgroundImage");
		this.ImportList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.ImportList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.ImportList.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
		this.ImportList.Location = new System.Drawing.Point(138, 19);
		this.ImportList.Margin = new System.Windows.Forms.Padding(0);
		this.ImportList.Name = "ImportList";
		this.ImportList.Size = new System.Drawing.Size(25, 25);
		this.ImportList.TabIndex = 25;
		this.ImportList.UseVisualStyleBackColor = false;
		this.ImportList.Click += new System.EventHandler(New_Click);
		this.AddNewList.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.AddNewList.BackgroundImage = (System.Drawing.Image)resources.GetObject("AddNewList.BackgroundImage");
		this.AddNewList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.AddNewList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.AddNewList.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
		this.AddNewList.Location = new System.Drawing.Point(105, 19);
		this.AddNewList.Margin = new System.Windows.Forms.Padding(0);
		this.AddNewList.Name = "AddNewList";
		this.AddNewList.Size = new System.Drawing.Size(25, 25);
		this.AddNewList.TabIndex = 24;
		this.AddNewList.UseVisualStyleBackColor = false;
		this.AddNewList.Click += new System.EventHandler(Add_Click);
		this.NewListInput.Location = new System.Drawing.Point(12, 19);
		this.NewListInput.Name = "NewListInput";
		this.NewListInput.Size = new System.Drawing.Size(92, 25);
		this.NewListInput.TabIndex = 23;
		this.Okay.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.Okay.Location = new System.Drawing.Point(396, 17);
		this.Okay.Name = "Okay";
		this.Okay.Size = new System.Drawing.Size(88, 30);
		this.Okay.TabIndex = 14;
		this.Okay.Text = "OK";
		this.Okay.UseVisualStyleBackColor = true;
		this.Okay.Click += new System.EventHandler(OK_Click);
		this.Cancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.Cancel.Location = new System.Drawing.Point(490, 17);
		this.Cancel.Name = "Cancel";
		this.Cancel.Size = new System.Drawing.Size(88, 30);
		this.Cancel.TabIndex = 11;
		this.Cancel.Text = "Cancel";
		this.Cancel.UseVisualStyleBackColor = true;
		this.Cancel.Click += new System.EventHandler(Cancel_Click);
		this.Apply.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.Apply.Location = new System.Drawing.Point(584, 17);
		this.Apply.Name = "Apply";
		this.Apply.Size = new System.Drawing.Size(88, 30);
		this.Apply.TabIndex = 12;
		this.Apply.Text = "Apply";
		this.Apply.UseVisualStyleBackColor = true;
		this.Apply.Click += new System.EventHandler(Apply_Click);
		this.tableLayoutPanel1.ColumnCount = 2;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135f));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel1.Controls.Add(this.LEListBox, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.LEDataGrid, 1, 0);
		this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 1;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(684, 302);
		this.tableLayoutPanel1.TabIndex = 122;
		this.LEListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.LEListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { this.leName });
		this.LEListBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.LEListBox.FullRowSelect = true;
		this.LEListBox.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
		this.LEListBox.HideSelection = false;
		this.LEListBox.Location = new System.Drawing.Point(0, 0);
		this.LEListBox.Margin = new System.Windows.Forms.Padding(0);
		this.LEListBox.MinimumSize = new System.Drawing.Size(129, 2);
		this.LEListBox.MultiSelect = false;
		this.LEListBox.Name = "LEListBox";
		this.LEListBox.Size = new System.Drawing.Size(135, 302);
		this.LEListBox.TabIndex = 0;
		this.LEListBox.UseCompatibleStateImageBehavior = false;
		this.LEListBox.View = System.Windows.Forms.View.Details;
		this.LEListBox.SelectedIndexChanged += new System.EventHandler(LEListBox_SelectedIndexChanged);
		this.leName.Width = 103;
		this.LEDataGrid.AllowUserToAddRows = false;
		this.LEDataGrid.AllowUserToResizeRows = false;
		this.LEDataGrid.BackgroundColor = System.Drawing.SystemColors.Window;
		this.LEDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.LEDataGrid.ColumnHeadersHeight = 24;
		this.LEDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.LEDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
		this.LEDataGrid.Location = new System.Drawing.Point(135, 0);
		this.LEDataGrid.Margin = new System.Windows.Forms.Padding(0);
		this.LEDataGrid.Name = "LEDataGrid";
		this.LEDataGrid.RowHeadersWidth = 35;
		this.LEDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Consolas", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.LEDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle;
		this.LEDataGrid.Size = new System.Drawing.Size(549, 302);
		this.LEDataGrid.TabIndex = 1;
		this.LEDataGrid.Click += new System.EventHandler(LEDataGrid_Click);
		base.AcceptButton = this.Okay;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		base.CancelButton = this.Cancel;
		base.ClientSize = new System.Drawing.Size(684, 361);
		base.Controls.Add(this.tableLayoutPanel1);
		base.Controls.Add(this.panel2);
		this.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		base.MaximizeBox = false;
		this.MaximumSize = new System.Drawing.Size(700, 4000);
		base.MinimizeBox = false;
		this.MinimumSize = new System.Drawing.Size(700, 400);
		base.Name = "ListEditor";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "CSV Modal";
		base.TopMost = true;
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.tableLayoutPanel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.LEDataGrid).EndInit();
		base.ResumeLayout(false);
	}
}
