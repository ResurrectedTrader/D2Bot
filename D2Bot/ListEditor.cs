using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace D2Bot;

public partial class ListEditor : Form
{
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
}
