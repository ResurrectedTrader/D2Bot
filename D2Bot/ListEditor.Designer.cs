namespace D2Bot;

partial class ListEditor
{
	private System.ComponentModel.IContainer components;

	private System.Windows.Forms.Panel panel2;

	public System.Windows.Forms.Button Apply;

	public System.Windows.Forms.Button Okay;

	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

	private System.Windows.Forms.ListView LEListBox;

	private System.Windows.Forms.DataGridView LEDataGrid;

	public System.Windows.Forms.ColumnHeader leName;

	public System.Windows.Forms.Button Cancel;

	public System.Windows.Forms.TextBox NewListInput;

	public System.Windows.Forms.Button DeleteList;

	public System.Windows.Forms.Button ImportList;

	public System.Windows.Forms.Button AddNewList;

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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
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
		((System.ComponentModel.ISupportInitialize)(this.LEDataGrid)).BeginInit();
		this.SuspendLayout();
		//
		// panel2
		//
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
		//
		// DeleteList
		//
		this.DeleteList.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.DeleteList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DeleteList.BackgroundImage")));
		this.DeleteList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.DeleteList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.DeleteList.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
		this.DeleteList.Location = new System.Drawing.Point(171, 19);
		this.DeleteList.Margin = new System.Windows.Forms.Padding(0);
		this.DeleteList.Name = "DeleteList";
		this.DeleteList.Size = new System.Drawing.Size(25, 25);
		this.DeleteList.TabIndex = 26;
		this.DeleteList.UseVisualStyleBackColor = false;
		this.DeleteList.Click += new System.EventHandler(this.Delete_Click);
		//
		// ImportList
		//
		this.ImportList.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.ImportList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ImportList.BackgroundImage")));
		this.ImportList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.ImportList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.ImportList.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
		this.ImportList.Location = new System.Drawing.Point(138, 19);
		this.ImportList.Margin = new System.Windows.Forms.Padding(0);
		this.ImportList.Name = "ImportList";
		this.ImportList.Size = new System.Drawing.Size(25, 25);
		this.ImportList.TabIndex = 25;
		this.ImportList.UseVisualStyleBackColor = false;
		this.ImportList.Click += new System.EventHandler(this.New_Click);
		//
		// AddNewList
		//
		this.AddNewList.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.AddNewList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AddNewList.BackgroundImage")));
		this.AddNewList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.AddNewList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.AddNewList.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
		this.AddNewList.Location = new System.Drawing.Point(105, 19);
		this.AddNewList.Margin = new System.Windows.Forms.Padding(0);
		this.AddNewList.Name = "AddNewList";
		this.AddNewList.Size = new System.Drawing.Size(25, 25);
		this.AddNewList.TabIndex = 24;
		this.AddNewList.UseVisualStyleBackColor = false;
		this.AddNewList.Click += new System.EventHandler(this.Add_Click);
		//
		// NewListInput
		//
		this.NewListInput.Location = new System.Drawing.Point(12, 19);
		this.NewListInput.Name = "NewListInput";
		this.NewListInput.Size = new System.Drawing.Size(92, 25);
		this.NewListInput.TabIndex = 23;
		//
		// Okay
		//
		this.Okay.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.Okay.Location = new System.Drawing.Point(396, 17);
		this.Okay.Name = "Okay";
		this.Okay.Size = new System.Drawing.Size(88, 30);
		this.Okay.TabIndex = 14;
		this.Okay.Text = "OK";
		this.Okay.UseVisualStyleBackColor = true;
		this.Okay.Click += new System.EventHandler(this.OK_Click);
		//
		// Cancel
		//
		this.Cancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.Cancel.Location = new System.Drawing.Point(490, 17);
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
		this.Apply.Location = new System.Drawing.Point(584, 17);
		this.Apply.Name = "Apply";
		this.Apply.Size = new System.Drawing.Size(88, 30);
		this.Apply.TabIndex = 12;
		this.Apply.Text = "Apply";
		this.Apply.UseVisualStyleBackColor = true;
		this.Apply.Click += new System.EventHandler(this.Apply_Click);
		//
		// tableLayoutPanel1
		//
		this.tableLayoutPanel1.ColumnCount = 2;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
		this.tableLayoutPanel1.Controls.Add(this.LEListBox, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.LEDataGrid, 1, 0);
		this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 1;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(684, 302);
		this.tableLayoutPanel1.TabIndex = 122;
		//
		// LEListBox
		//
		this.LEListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.LEListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
		this.leName});
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
		this.LEListBox.SelectedIndexChanged += new System.EventHandler(this.LEListBox_SelectedIndexChanged);
		//
		// leName
		//
		this.leName.Width = 103;
		//
		// LEDataGrid
		//
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
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.LEDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle1;
		this.LEDataGrid.Size = new System.Drawing.Size(549, 302);
		this.LEDataGrid.TabIndex = 1;
		this.LEDataGrid.Click += new System.EventHandler(this.LEDataGrid_Click);
		//
		// ListEditor
		//
		this.AcceptButton = this.Okay;
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.CancelButton = this.Cancel;
		this.ClientSize = new System.Drawing.Size(684, 361);
		this.Controls.Add(this.tableLayoutPanel1);
		this.Controls.Add(this.panel2);
		this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.MaximizeBox = false;
		this.MaximumSize = new System.Drawing.Size(700, 4000);
		this.MinimizeBox = false;
		this.MinimumSize = new System.Drawing.Size(700, 400);
		this.Name = "ListEditor";
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "CSV Modal";
		this.TopMost = true;
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.tableLayoutPanel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)(this.LEDataGrid)).EndInit();
		this.ResumeLayout(false);
	}
}
