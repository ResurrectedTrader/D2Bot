using System.ComponentModel;
using System.Windows.Forms;
using BrightIdeasSoftware;
using RichTextBoxLinks;

namespace D2Bot;

partial class Main
{
	private IContainer components;

	private ToolStripButton guiStart;

	private ToolStripButton guiStop;

	private ToolStripButton guiAdd;

	private ToolStripButton guiEdit;

	private ToolStripButton guiSave;

	private ToolStripButton guiCopy;

	private ToolStripButton guiShow;

	private ToolStripButton guiHide;

	private ToolStripButton guiAbout;

	private ToolStrip toolStrip1;

	private ToolStripButton guiRemove;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem resetStatsToolStripMenuItem;

	private ToolStripMenuItem disableScheduleToolStripMenuItem;

	private ContextMenuStrip contextMenuStrip2;

	private ToolStripMenuItem removeKeyToolStripMenuItem;

	private ToolStripMenuItem removeFromListToolStripMenuItem;

	private ContextMenuStrip contextMenuStrip3;

	private ToolStripMenuItem toolStripMenuItem3;

	private ToolStripMenuItem importToolStripMenuItem;

	private ContextMenuStrip contextMenuStrip4;

	private ToolStripMenuItem toolItemSaveImage;

	private ToolStripMenuItem createDupeToolStripMenuItem;

	private ToolStripMenuItem removeToolStripMenuItem;

	private ToolStripMenuItem enableScheduleToolStripMenuItem;

	private ToolStripMenuItem clearItemsToolStripMenuItem;

	private ToolStripMenuItem clearToolStripMenuItem;

	private ToolStripMenuItem copyImageToolStripMenuItem;

	private ToolStripMenuItem copyDescriptionToolStripMenuItem;

	private ToolStripMenuItem muleProfileToolStripMenuItem;

	private SplitContainer mainContainer;

	private ContextMenuStrip contextMenuStrip5;

	private ToolStripMenuItem pauseKeyToolStripMenuItem;

	private ToolStripMenuItem resumeKeyToolStripMenuItem;

	private ToolStripMenuItem nextKeyToolStripMenuItem;

	private ToolStripButton guiAddIRC;

	private ToolStripMenuItem releaseKeyToolStripMenuItem;

	public ObjectListView objectProfileList;

	private OLVColumn profileCol;

	private OLVColumn statusCol;

	private OLVColumn runsCol;

	private OLVColumn chickensCol;

	private OLVColumn deathsCol;

	private OLVColumn crashesCol;

	private OLVColumn restartsCol;

	private OLVColumn keyCol;

	private OLVColumn gameExe;

	private ToolStripMenuItem uploadImgurToolStripMenuItem;

	private ImageList profileImages;

	private ToolStripButton keysEdit;

	private ToolStripButton scheduleEdit;

	private ToolStripMenuItem importProfilesToolStripMenuItem;

	private TabControl PrintTab;

	private TabPage Console;

	public ListView ItemLogger;

	private ColumnHeader ItemDateProfile;

	private ColumnHeader ItemLogColumn;

	private RichTextBoxEx ConsoleBox;

	private TabPage charView;

	private SplitContainer charContainer;

	public TreeView CharTree;

	private TableLayoutPanel tableLayoutPanel2;

	private TableLayoutPanel charSearchBar;

	private TextBox SearchBox;

	private Button SearchButton;

	public ListView CharItems;

	private ColumnHeader CharItemColumn;

	private TabPage KeyAnalysis;

	private SplitContainer keyWizardContainer;

	public DataGridView dupeList;

	private ListView KeyData;

	private ColumnHeader KeyProfile;

	private ColumnHeader KeyName;

	private ColumnHeader KeyInUse;

	private ColumnHeader KeyRD;

	private ColumnHeader KeyDisabled;

	private TableLayoutPanel tableLayoutPanel1;

	private Panel keyWizBorder;

	private ToolStripDropDownButton settingsDropDown;

	private ToolStripMenuItem menuShow;

	private ToolStripMenuItem menuHide;

	private ToolStripMenuItem responseTimeToolStripMenuItem;

	private ToolStripMenuItem secondsToolStripMenuItem;

	private ToolStripMenuItem secondsToolStripMenuItem1;

	private ToolStripMenuItem secondsToolStripMenuItem2;

	private ToolStripMenuItem secondsToolStripMenuItem3;

	private ToolStripMenuItem secondsToolStripMenuItem4;

	private ToolStripMenuItem loadDelay;

	private ToolStripMenuItem ms100;

	private ToolStripMenuItem ms250;

	private ToolStripMenuItem ms500;

	private ToolStripMenuItem ms1000;

	private ToolStripMenuItem startHidden;

	private ToolStripMenuItem systemFont;

	private ToolStripMenuItem itemHeader;

	private ToolStripMenuItem refreshCharViewToolStripMenuItem;

	private ToolStripMenuItem closeGameexeToolStripMenuItem;

	private ToolStripMenuItem debugMode;

	private ToolStripDropDownButton menuDropDown;

	private ToolStripMenuItem menuStart;

	private ToolStripMenuItem menuStop;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem menuSave;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripMenuItem menuExit;

	private ToolStripMenuItem systemTrayToolStripMenuItem;

	private DataGridViewTextBoxColumn keyDupe;

	private DataGridViewComboBoxColumn profileDupe;

	private ToolStripMenuItem clearDetailsToolStripMenuItem;

	public ToolStripMenuItem versionMenuItem;

	private ToolStripMenuItem ms2000;

	private ToolStripMenuItem ms5000;

	private SplitContainer splitContainer1;

	private PictureBox pictureBox1;

	private ToolStripMenuItem serverToggle;

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
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Expected O, but got Unknown
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Expected O, but got Unknown
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Expected O, but got Unknown
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Expected O, but got Unknown
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Expected O, but got Unknown
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Expected O, but got Unknown
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Expected O, but got Unknown
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Expected O, but got Unknown
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Expected O, but got Unknown
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(D2Bot.Main));
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.importProfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.resetStatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.enableScheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.disableScheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.muleProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.nextKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.releaseKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
		this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip4 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.toolItemSaveImage = new System.Windows.Forms.ToolStripMenuItem();
		this.uploadImgurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.copyImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.copyDescriptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.createDupeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.clearItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip5 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.pauseKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.resumeKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.removeKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.removeFromListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.clearDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.guiStart = new System.Windows.Forms.ToolStripButton();
		this.guiStop = new System.Windows.Forms.ToolStripButton();
		this.guiAdd = new System.Windows.Forms.ToolStripButton();
		this.guiEdit = new System.Windows.Forms.ToolStripButton();
		this.guiSave = new System.Windows.Forms.ToolStripButton();
		this.guiCopy = new System.Windows.Forms.ToolStripButton();
		this.guiShow = new System.Windows.Forms.ToolStripButton();
		this.guiHide = new System.Windows.Forms.ToolStripButton();
		this.guiAbout = new System.Windows.Forms.ToolStripButton();
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this.menuDropDown = new System.Windows.Forms.ToolStripDropDownButton();
		this.menuStart = new System.Windows.Forms.ToolStripMenuItem();
		this.menuStop = new System.Windows.Forms.ToolStripMenuItem();
		this.menuShow = new System.Windows.Forms.ToolStripMenuItem();
		this.menuHide = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
		this.systemTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.settingsDropDown = new System.Windows.Forms.ToolStripDropDownButton();
		this.versionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.responseTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.secondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.secondsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.secondsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
		this.secondsToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
		this.secondsToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
		this.loadDelay = new System.Windows.Forms.ToolStripMenuItem();
		this.ms100 = new System.Windows.Forms.ToolStripMenuItem();
		this.ms250 = new System.Windows.Forms.ToolStripMenuItem();
		this.ms500 = new System.Windows.Forms.ToolStripMenuItem();
		this.ms1000 = new System.Windows.Forms.ToolStripMenuItem();
		this.ms2000 = new System.Windows.Forms.ToolStripMenuItem();
		this.ms5000 = new System.Windows.Forms.ToolStripMenuItem();
		this.startHidden = new System.Windows.Forms.ToolStripMenuItem();
		this.systemFont = new System.Windows.Forms.ToolStripMenuItem();
		this.itemHeader = new System.Windows.Forms.ToolStripMenuItem();
		this.refreshCharViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.closeGameexeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.serverToggle = new System.Windows.Forms.ToolStripMenuItem();
		this.debugMode = new System.Windows.Forms.ToolStripMenuItem();
		this.guiRemove = new System.Windows.Forms.ToolStripButton();
		this.guiAddIRC = new System.Windows.Forms.ToolStripButton();
		this.keysEdit = new System.Windows.Forms.ToolStripButton();
		this.scheduleEdit = new System.Windows.Forms.ToolStripButton();
		this.mainContainer = new System.Windows.Forms.SplitContainer();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.objectProfileList = new ObjectListView();
		this.profileCol = new OLVColumn();
		this.statusCol = new OLVColumn();
		this.runsCol = new OLVColumn();
		this.chickensCol = new OLVColumn();
		this.deathsCol = new OLVColumn();
		this.crashesCol = new OLVColumn();
		this.restartsCol = new OLVColumn();
		this.keyCol = new OLVColumn();
		this.gameExe = new OLVColumn();
		this.profileImages = new System.Windows.Forms.ImageList(this.components);
		this.PrintTab = new System.Windows.Forms.TabControl();
		this.Console = new System.Windows.Forms.TabPage();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.ItemLogger = new System.Windows.Forms.ListView();
		this.ItemDateProfile = new System.Windows.Forms.ColumnHeader();
		this.ItemLogColumn = new System.Windows.Forms.ColumnHeader();
		this.charView = new System.Windows.Forms.TabPage();
		this.charContainer = new System.Windows.Forms.SplitContainer();
		this.CharTree = new System.Windows.Forms.TreeView();
		this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
		this.charSearchBar = new System.Windows.Forms.TableLayoutPanel();
		this.SearchBox = new System.Windows.Forms.TextBox();
		this.SearchButton = new System.Windows.Forms.Button();
		this.CharItems = new System.Windows.Forms.ListView();
		this.CharItemColumn = new System.Windows.Forms.ColumnHeader();
		this.KeyAnalysis = new System.Windows.Forms.TabPage();
		this.keyWizardContainer = new System.Windows.Forms.SplitContainer();
		this.keyWizBorder = new System.Windows.Forms.Panel();
		this.dupeList = new System.Windows.Forms.DataGridView();
		this.keyDupe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.profileDupe = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.KeyData = new System.Windows.Forms.ListView();
		this.KeyProfile = new System.Windows.Forms.ColumnHeader();
		this.KeyName = new System.Windows.Forms.ColumnHeader();
		this.KeyInUse = new System.Windows.Forms.ColumnHeader();
		this.KeyRD = new System.Windows.Forms.ColumnHeader();
		this.KeyDisabled = new System.Windows.Forms.ColumnHeader();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.ConsoleBox = new RichTextBoxLinks.RichTextBoxEx();
		this.contextMenuStrip1.SuspendLayout();
		this.contextMenuStrip3.SuspendLayout();
		this.contextMenuStrip4.SuspendLayout();
		this.contextMenuStrip5.SuspendLayout();
		this.contextMenuStrip2.SuspendLayout();
		this.toolStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.mainContainer).BeginInit();
		this.mainContainer.Panel1.SuspendLayout();
		this.mainContainer.Panel2.SuspendLayout();
		this.mainContainer.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.objectProfileList).BeginInit();
		this.PrintTab.SuspendLayout();
		this.Console.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		this.charView.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.charContainer).BeginInit();
		this.charContainer.Panel1.SuspendLayout();
		this.charContainer.Panel2.SuspendLayout();
		this.charContainer.SuspendLayout();
		this.tableLayoutPanel2.SuspendLayout();
		this.charSearchBar.SuspendLayout();
		this.KeyAnalysis.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.keyWizardContainer).BeginInit();
		this.keyWizardContainer.Panel1.SuspendLayout();
		this.keyWizardContainer.Panel2.SuspendLayout();
		this.keyWizardContainer.SuspendLayout();
		this.keyWizBorder.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dupeList).BeginInit();
		this.tableLayoutPanel1.SuspendLayout();
		this.SuspendLayout();
		this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.importProfilesToolStripMenuItem, this.resetStatsToolStripMenuItem, this.enableScheduleToolStripMenuItem, this.disableScheduleToolStripMenuItem, this.muleProfileToolStripMenuItem, this.nextKeyToolStripMenuItem, this.releaseKeyToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(176, 158);
		this.importProfilesToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("importProfilesToolStripMenuItem.Image");
		this.importProfilesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.importProfilesToolStripMenuItem.Name = "importProfilesToolStripMenuItem";
		this.importProfilesToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
		this.importProfilesToolStripMenuItem.Text = "Import Profiles";
		this.importProfilesToolStripMenuItem.Click += new System.EventHandler(this.ImportProfilesToolStripMenuItem_Click);
		this.resetStatsToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("resetStatsToolStripMenuItem.Image");
		this.resetStatsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.resetStatsToolStripMenuItem.Name = "resetStatsToolStripMenuItem";
		this.resetStatsToolStripMenuItem.ShowShortcutKeys = false;
		this.resetStatsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
		this.resetStatsToolStripMenuItem.Text = "Reset Stats";
		this.resetStatsToolStripMenuItem.Click += new System.EventHandler(this.resetStatsToolStripMenuItem_Click);
		this.enableScheduleToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("enableScheduleToolStripMenuItem.Image");
		this.enableScheduleToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.enableScheduleToolStripMenuItem.Name = "enableScheduleToolStripMenuItem";
		this.enableScheduleToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
		this.enableScheduleToolStripMenuItem.Text = "Enable Schedule";
		this.enableScheduleToolStripMenuItem.Click += new System.EventHandler(this.enableScheduleToolStripMenuItem_Click);
		this.disableScheduleToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("disableScheduleToolStripMenuItem.Image");
		this.disableScheduleToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.disableScheduleToolStripMenuItem.Name = "disableScheduleToolStripMenuItem";
		this.disableScheduleToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
		this.disableScheduleToolStripMenuItem.Text = "Disable Schedule";
		this.disableScheduleToolStripMenuItem.Click += new System.EventHandler(this.disableScheduleToolStripMenuItem_Click);
		this.muleProfileToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("muleProfileToolStripMenuItem.Image");
		this.muleProfileToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.muleProfileToolStripMenuItem.Name = "muleProfileToolStripMenuItem";
		this.muleProfileToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
		this.muleProfileToolStripMenuItem.Text = "Mule Profile";
		this.muleProfileToolStripMenuItem.Click += new System.EventHandler(this.muleProfileToolStripMenuItem_Click);
		this.nextKeyToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("nextKeyToolStripMenuItem.Image");
		this.nextKeyToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.nextKeyToolStripMenuItem.Name = "nextKeyToolStripMenuItem";
		this.nextKeyToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
		this.nextKeyToolStripMenuItem.Text = "Next Key";
		this.nextKeyToolStripMenuItem.Click += new System.EventHandler(this.nextKeyToolStripMenuItem_Click);
		this.releaseKeyToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("releaseKeyToolStripMenuItem.Image");
		this.releaseKeyToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.releaseKeyToolStripMenuItem.Name = "releaseKeyToolStripMenuItem";
		this.releaseKeyToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
		this.releaseKeyToolStripMenuItem.Text = "Release Key";
		this.releaseKeyToolStripMenuItem.Click += new System.EventHandler(this.releaseKeyToolStripMenuItem_Click);
		this.contextMenuStrip3.Font = new System.Drawing.Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.contextMenuStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.toolStripMenuItem3, this.importToolStripMenuItem, this.clearToolStripMenuItem });
		this.contextMenuStrip3.Name = "contextMenuStrip3";
		this.contextMenuStrip3.Size = new System.Drawing.Size(117, 70);
		this.toolStripMenuItem3.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStripMenuItem3.Image = (System.Drawing.Image)resources.GetObject("toolStripMenuItem3.Image");
		this.toolStripMenuItem3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.toolStripMenuItem3.Name = "toolStripMenuItem3";
		this.toolStripMenuItem3.Size = new System.Drawing.Size(116, 22);
		this.toolStripMenuItem3.Text = "Export";
		this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
		this.importToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.importToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("importToolStripMenuItem.Image");
		this.importToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.importToolStripMenuItem.Name = "importToolStripMenuItem";
		this.importToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
		this.importToolStripMenuItem.Text = "Import";
		this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
		this.clearToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.clearToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("clearToolStripMenuItem.Image");
		this.clearToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
		this.clearToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
		this.clearToolStripMenuItem.Text = "Clear";
		this.clearToolStripMenuItem.Click += new System.EventHandler(this.ClearToolStripMenuItem_Click);
		this.contextMenuStrip4.Font = new System.Drawing.Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.contextMenuStrip4.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.toolItemSaveImage, this.uploadImgurToolStripMenuItem, this.copyImageToolStripMenuItem, this.copyDescriptionToolStripMenuItem, this.createDupeToolStripMenuItem, this.removeToolStripMenuItem, this.clearItemsToolStripMenuItem });
		this.contextMenuStrip4.Name = "contextMenuStrip4";
		this.contextMenuStrip4.Size = new System.Drawing.Size(178, 158);
		this.toolItemSaveImage.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.toolItemSaveImage.Image = (System.Drawing.Image)resources.GetObject("toolItemSaveImage.Image");
		this.toolItemSaveImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.toolItemSaveImage.Name = "toolItemSaveImage";
		this.toolItemSaveImage.Size = new System.Drawing.Size(177, 22);
		this.toolItemSaveImage.Text = "Save Image";
		this.toolItemSaveImage.Click += new System.EventHandler(this.ToolItemSaveImage_Click);
		this.uploadImgurToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.uploadImgurToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("uploadImgurToolStripMenuItem.Image");
		this.uploadImgurToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.uploadImgurToolStripMenuItem.Name = "uploadImgurToolStripMenuItem";
		this.uploadImgurToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
		this.uploadImgurToolStripMenuItem.Text = "Upload to Imgur";
		this.uploadImgurToolStripMenuItem.Click += new System.EventHandler(this.UploadImgurToolStripMenuItem_Click);
		this.copyImageToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.copyImageToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("copyImageToolStripMenuItem.Image");
		this.copyImageToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.copyImageToolStripMenuItem.Name = "copyImageToolStripMenuItem";
		this.copyImageToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
		this.copyImageToolStripMenuItem.Text = "Copy Image";
		this.copyImageToolStripMenuItem.Click += new System.EventHandler(this.CopyImageToolStripMenuItem_Click);
		this.copyDescriptionToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.copyDescriptionToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("copyDescriptionToolStripMenuItem.Image");
		this.copyDescriptionToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.copyDescriptionToolStripMenuItem.Name = "copyDescriptionToolStripMenuItem";
		this.copyDescriptionToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
		this.copyDescriptionToolStripMenuItem.Text = "Copy Description";
		this.copyDescriptionToolStripMenuItem.Click += new System.EventHandler(this.CopyDescriptionToolStripMenuItem_Click);
		this.createDupeToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.createDupeToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("createDupeToolStripMenuItem.Image");
		this.createDupeToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.createDupeToolStripMenuItem.Name = "createDupeToolStripMenuItem";
		this.createDupeToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
		this.createDupeToolStripMenuItem.Text = "Create Dupe";
		this.createDupeToolStripMenuItem.Click += new System.EventHandler(this.CreateDupeToolStripMenuItem_Click);
		this.removeToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.removeToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("removeToolStripMenuItem.Image");
		this.removeToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
		this.removeToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
		this.removeToolStripMenuItem.Text = "Remove Item";
		this.removeToolStripMenuItem.Click += new System.EventHandler(this.RemoveToolStripMenuItem_Click);
		this.clearItemsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.clearItemsToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("clearItemsToolStripMenuItem.Image");
		this.clearItemsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.clearItemsToolStripMenuItem.Name = "clearItemsToolStripMenuItem";
		this.clearItemsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
		this.clearItemsToolStripMenuItem.Text = "Clear Items";
		this.clearItemsToolStripMenuItem.Click += new System.EventHandler(this.ClearItemsToolStripMenuItem_Click);
		this.contextMenuStrip5.Font = new System.Drawing.Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.contextMenuStrip5.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.pauseKeyToolStripMenuItem, this.resumeKeyToolStripMenuItem });
		this.contextMenuStrip5.Name = "contextMenuStrip5";
		this.contextMenuStrip5.Size = new System.Drawing.Size(149, 48);
		this.pauseKeyToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.pauseKeyToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("pauseKeyToolStripMenuItem.Image");
		this.pauseKeyToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.pauseKeyToolStripMenuItem.Name = "pauseKeyToolStripMenuItem";
		this.pauseKeyToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
		this.pauseKeyToolStripMenuItem.Text = "Pause Key";
		this.pauseKeyToolStripMenuItem.Click += new System.EventHandler(this.pauseKeyToolStripMenuItem_Click);
		this.resumeKeyToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.resumeKeyToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("resumeKeyToolStripMenuItem.Image");
		this.resumeKeyToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.resumeKeyToolStripMenuItem.Name = "resumeKeyToolStripMenuItem";
		this.resumeKeyToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
		this.resumeKeyToolStripMenuItem.Text = "Resume Key";
		this.resumeKeyToolStripMenuItem.Click += new System.EventHandler(this.resumeKeyToolStripMenuItem_Click);
		this.contextMenuStrip2.Font = new System.Drawing.Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.removeKeyToolStripMenuItem, this.removeFromListToolStripMenuItem, this.clearDetailsToolStripMenuItem });
		this.contextMenuStrip2.Name = "contextMenuStrip2";
		this.contextMenuStrip2.Size = new System.Drawing.Size(182, 70);
		this.removeKeyToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.removeKeyToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("removeKeyToolStripMenuItem.Image");
		this.removeKeyToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.removeKeyToolStripMenuItem.Name = "removeKeyToolStripMenuItem";
		this.removeKeyToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
		this.removeKeyToolStripMenuItem.Text = "Remove Key";
		this.removeKeyToolStripMenuItem.Click += new System.EventHandler(this.RemoveKeyToolStripMenuItem_Click);
		this.removeFromListToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.removeFromListToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("removeFromListToolStripMenuItem.Image");
		this.removeFromListToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.removeFromListToolStripMenuItem.Name = "removeFromListToolStripMenuItem";
		this.removeFromListToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
		this.removeFromListToolStripMenuItem.Text = "Remove From List";
		this.removeFromListToolStripMenuItem.Click += new System.EventHandler(this.RemoveFromListToolStripMenuItem_Click);
		this.clearDetailsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.clearDetailsToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("clearDetailsToolStripMenuItem.Image");
		this.clearDetailsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.clearDetailsToolStripMenuItem.Name = "clearDetailsToolStripMenuItem";
		this.clearDetailsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
		this.clearDetailsToolStripMenuItem.Text = "Clear Wizard";
		this.clearDetailsToolStripMenuItem.Click += new System.EventHandler(this.ClearDetailsToolStripMenuItem_Click);
		this.guiStart.AccessibleName = "guiName";
		this.guiStart.AutoSize = false;
		this.guiStart.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.guiStart.Image = (System.Drawing.Image)resources.GetObject("guiStart.Image");
		this.guiStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.guiStart.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.guiStart.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.guiStart.Margin = new System.Windows.Forms.Padding(0);
		this.guiStart.Name = "guiStart";
		this.guiStart.Size = new System.Drawing.Size(145, 36);
		this.guiStart.Text = "  Start";
		this.guiStart.ToolTipText = "Start Selected Profiles";
		this.guiStart.Click += new System.EventHandler(this.StartProfile);
		this.guiStop.AutoSize = false;
		this.guiStop.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.guiStop.Image = (System.Drawing.Image)resources.GetObject("guiStop.Image");
		this.guiStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.guiStop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.guiStop.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.guiStop.Margin = new System.Windows.Forms.Padding(0);
		this.guiStop.Name = "guiStop";
		this.guiStop.Size = new System.Drawing.Size(145, 36);
		this.guiStop.Text = "  Stop";
		this.guiStop.ToolTipText = "Stop Selected Profiles";
		this.guiStop.Click += new System.EventHandler(this.StopProfile);
		this.guiAdd.AutoSize = false;
		this.guiAdd.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.guiAdd.Image = (System.Drawing.Image)resources.GetObject("guiAdd.Image");
		this.guiAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.guiAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.guiAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.guiAdd.Margin = new System.Windows.Forms.Padding(0);
		this.guiAdd.Name = "guiAdd";
		this.guiAdd.Size = new System.Drawing.Size(145, 36);
		this.guiAdd.Text = "  Add";
		this.guiAdd.ToolTipText = "Add New Profile";
		this.guiAdd.Click += new System.EventHandler(this.Add);
		this.guiEdit.AutoSize = false;
		this.guiEdit.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.guiEdit.Image = (System.Drawing.Image)resources.GetObject("guiEdit.Image");
		this.guiEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.guiEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.guiEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.guiEdit.Margin = new System.Windows.Forms.Padding(0);
		this.guiEdit.Name = "guiEdit";
		this.guiEdit.Size = new System.Drawing.Size(145, 36);
		this.guiEdit.Text = "  Edit";
		this.guiEdit.ToolTipText = "Edit Selected Profiles";
		this.guiEdit.Click += new System.EventHandler(this.Edit);
		this.guiSave.AutoSize = false;
		this.guiSave.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.guiSave.Image = (System.Drawing.Image)resources.GetObject("guiSave.Image");
		this.guiSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.guiSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.guiSave.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.guiSave.Margin = new System.Windows.Forms.Padding(0);
		this.guiSave.Name = "guiSave";
		this.guiSave.Size = new System.Drawing.Size(145, 36);
		this.guiSave.Text = "  Save";
		this.guiSave.ToolTipText = "Save Runtime Info";
		this.guiSave.Click += new System.EventHandler(this.Save);
		this.guiCopy.AutoSize = false;
		this.guiCopy.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.guiCopy.Image = (System.Drawing.Image)resources.GetObject("guiCopy.Image");
		this.guiCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.guiCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.guiCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.guiCopy.Margin = new System.Windows.Forms.Padding(0);
		this.guiCopy.Name = "guiCopy";
		this.guiCopy.Size = new System.Drawing.Size(145, 36);
		this.guiCopy.Text = "  Clone";
		this.guiCopy.ToolTipText = "Clone Selected Profiles";
		this.guiCopy.Click += new System.EventHandler(this.Duplicate);
		this.guiShow.AutoSize = false;
		this.guiShow.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.guiShow.Image = (System.Drawing.Image)resources.GetObject("guiShow.Image");
		this.guiShow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.guiShow.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.guiShow.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.guiShow.Margin = new System.Windows.Forms.Padding(0);
		this.guiShow.Name = "guiShow";
		this.guiShow.Size = new System.Drawing.Size(145, 36);
		this.guiShow.Text = "  Show";
		this.guiShow.ToolTipText = "Show Selected Profiles";
		this.guiShow.Click += new System.EventHandler(this.ShowWindow);
		this.guiHide.AutoSize = false;
		this.guiHide.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.guiHide.Image = (System.Drawing.Image)resources.GetObject("guiHide.Image");
		this.guiHide.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.guiHide.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.guiHide.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.guiHide.Margin = new System.Windows.Forms.Padding(0);
		this.guiHide.Name = "guiHide";
		this.guiHide.Size = new System.Drawing.Size(145, 36);
		this.guiHide.Text = "  Hide";
		this.guiHide.ToolTipText = "Hide Selected Profiles";
		this.guiHide.Click += new System.EventHandler(this.HideWindow);
		this.guiAbout.AutoSize = false;
		this.guiAbout.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.guiAbout.Image = (System.Drawing.Image)resources.GetObject("guiAbout.Image");
		this.guiAbout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.guiAbout.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.guiAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.guiAbout.Margin = new System.Windows.Forms.Padding(0);
		this.guiAbout.Name = "guiAbout";
		this.guiAbout.Size = new System.Drawing.Size(145, 36);
		this.guiAbout.Text = "  About";
		this.guiAbout.ToolTipText = "About";
		this.guiAbout.Click += new System.EventHandler(this.AboutDialog);
		this.toolStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.toolStrip1.CanOverflow = false;
		this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
		this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
		this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 32);
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[15]
		{
			this.menuDropDown, this.settingsDropDown, this.guiStart, this.guiStop, this.guiShow, this.guiHide, this.guiAdd, this.guiRemove, this.guiEdit, this.guiCopy,
			this.guiAddIRC, this.keysEdit, this.scheduleEdit, this.guiSave, this.guiAbout
		});
		this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
		this.toolStrip1.Location = new System.Drawing.Point(1, 1);
		this.toolStrip1.MinimumSize = new System.Drawing.Size(0, 600);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
		this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
		this.toolStrip1.Size = new System.Drawing.Size(146, 600);
		this.toolStrip1.Stretch = true;
		this.toolStrip1.TabIndex = 3;
		this.toolStrip1.Text = "toolStrip1";
		this.menuDropDown.AutoSize = false;
		this.menuDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[9] { this.menuStart, this.menuStop, this.menuShow, this.menuHide, this.toolStripSeparator2, this.menuSave, this.toolStripSeparator3, this.menuExit, this.systemTrayToolStripMenuItem });
		this.menuDropDown.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.menuDropDown.Image = (System.Drawing.Image)resources.GetObject("menuDropDown.Image");
		this.menuDropDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.menuDropDown.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.menuDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.menuDropDown.Margin = new System.Windows.Forms.Padding(0);
		this.menuDropDown.Name = "menuDropDown";
		this.menuDropDown.Size = new System.Drawing.Size(145, 36);
		this.menuDropDown.Text = "  Menu";
		this.menuStart.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.menuStart.Image = (System.Drawing.Image)resources.GetObject("menuStart.Image");
		this.menuStart.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.menuStart.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.menuStart.Name = "menuStart";
		this.menuStart.ShortcutKeys = System.Windows.Forms.Keys.A | System.Windows.Forms.Keys.Alt;
		this.menuStart.Size = new System.Drawing.Size(171, 22);
		this.menuStart.Text = "Start All";
		this.menuStart.Click += new System.EventHandler(this.MenuHandler);
		this.menuStop.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.menuStop.Image = (System.Drawing.Image)resources.GetObject("menuStop.Image");
		this.menuStop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.menuStop.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.menuStop.Name = "menuStop";
		this.menuStop.ShortcutKeys = System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Alt;
		this.menuStop.Size = new System.Drawing.Size(171, 22);
		this.menuStop.Text = "Stop All";
		this.menuStop.Click += new System.EventHandler(this.MenuHandler);
		this.menuShow.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.menuShow.Image = (System.Drawing.Image)resources.GetObject("menuShow.Image");
		this.menuShow.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.menuShow.Name = "menuShow";
		this.menuShow.ShortcutKeys = System.Windows.Forms.Keys.Y | System.Windows.Forms.Keys.Control;
		this.menuShow.Size = new System.Drawing.Size(171, 22);
		this.menuShow.Text = "Show All";
		this.menuShow.Click += new System.EventHandler(this.MenuHandler);
		this.menuHide.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.menuHide.Image = (System.Drawing.Image)resources.GetObject("menuHide.Image");
		this.menuHide.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.menuHide.Name = "menuHide";
		this.menuHide.ShortcutKeys = System.Windows.Forms.Keys.H | System.Windows.Forms.Keys.Control;
		this.menuHide.Size = new System.Drawing.Size(171, 22);
		this.menuHide.Text = "Hide All";
		this.menuHide.Click += new System.EventHandler(this.MenuHandler);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(168, 6);
		this.menuSave.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.menuSave.Image = (System.Drawing.Image)resources.GetObject("menuSave.Image");
		this.menuSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.menuSave.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.menuSave.Name = "menuSave";
		this.menuSave.ShortcutKeys = System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Control;
		this.menuSave.Size = new System.Drawing.Size(171, 22);
		this.menuSave.Text = "Save";
		this.menuSave.Click += new System.EventHandler(this.Save);
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(168, 6);
		this.menuExit.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.menuExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.menuExit.Name = "menuExit";
		this.menuExit.ShortcutKeys = System.Windows.Forms.Keys.F4 | System.Windows.Forms.Keys.Alt;
		this.menuExit.Size = new System.Drawing.Size(171, 22);
		this.menuExit.Text = "Exit";
		this.menuExit.Click += new System.EventHandler(this.MenuHandler);
		this.systemTrayToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.systemTrayToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.systemTrayToolStripMenuItem.Name = "systemTrayToolStripMenuItem";
		this.systemTrayToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
		this.systemTrayToolStripMenuItem.Text = "System Tray";
		this.systemTrayToolStripMenuItem.Click += new System.EventHandler(this.MenuHandler);
		this.settingsDropDown.AutoSize = false;
		this.settingsDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[10] { this.versionMenuItem, this.responseTimeToolStripMenuItem, this.loadDelay, this.startHidden, this.systemFont, this.itemHeader, this.refreshCharViewToolStripMenuItem, this.closeGameexeToolStripMenuItem, this.serverToggle, this.debugMode });
		this.settingsDropDown.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.settingsDropDown.Image = (System.Drawing.Image)resources.GetObject("settingsDropDown.Image");
		this.settingsDropDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.settingsDropDown.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.settingsDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.settingsDropDown.Margin = new System.Windows.Forms.Padding(0);
		this.settingsDropDown.Name = "settingsDropDown";
		this.settingsDropDown.Size = new System.Drawing.Size(145, 36);
		this.settingsDropDown.Text = "  Settings";
		this.settingsDropDown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.versionMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.versionMenuItem.Image = (System.Drawing.Image)resources.GetObject("versionMenuItem.Image");
		this.versionMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.versionMenuItem.Name = "versionMenuItem";
		this.versionMenuItem.Size = new System.Drawing.Size(185, 22);
		this.versionMenuItem.Text = "D2 Version";
		this.responseTimeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.secondsToolStripMenuItem, this.secondsToolStripMenuItem1, this.secondsToolStripMenuItem2, this.secondsToolStripMenuItem3, this.secondsToolStripMenuItem4 });
		this.responseTimeToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.responseTimeToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("responseTimeToolStripMenuItem.Image");
		this.responseTimeToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.responseTimeToolStripMenuItem.Name = "responseTimeToolStripMenuItem";
		this.responseTimeToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
		this.responseTimeToolStripMenuItem.Text = "Response Time";
		this.secondsToolStripMenuItem.CheckOnClick = true;
		this.secondsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.secondsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.secondsToolStripMenuItem.Name = "secondsToolStripMenuItem";
		this.secondsToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
		this.secondsToolStripMenuItem.Text = "10 Seconds";
		this.secondsToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
		this.secondsToolStripMenuItem.Click += new System.EventHandler(this.MenuHandler);
		this.secondsToolStripMenuItem1.CheckOnClick = true;
		this.secondsToolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.secondsToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.secondsToolStripMenuItem1.Name = "secondsToolStripMenuItem1";
		this.secondsToolStripMenuItem1.Size = new System.Drawing.Size(143, 22);
		this.secondsToolStripMenuItem1.Text = "15 Seconds";
		this.secondsToolStripMenuItem1.Click += new System.EventHandler(this.MenuHandler);
		this.secondsToolStripMenuItem2.CheckOnClick = true;
		this.secondsToolStripMenuItem2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.secondsToolStripMenuItem2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.secondsToolStripMenuItem2.Name = "secondsToolStripMenuItem2";
		this.secondsToolStripMenuItem2.Size = new System.Drawing.Size(143, 22);
		this.secondsToolStripMenuItem2.Text = "20 Seconds";
		this.secondsToolStripMenuItem2.Click += new System.EventHandler(this.MenuHandler);
		this.secondsToolStripMenuItem3.CheckOnClick = true;
		this.secondsToolStripMenuItem3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.secondsToolStripMenuItem3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.secondsToolStripMenuItem3.Name = "secondsToolStripMenuItem3";
		this.secondsToolStripMenuItem3.Size = new System.Drawing.Size(143, 22);
		this.secondsToolStripMenuItem3.Text = "25 Seconds";
		this.secondsToolStripMenuItem3.Click += new System.EventHandler(this.MenuHandler);
		this.secondsToolStripMenuItem4.CheckOnClick = true;
		this.secondsToolStripMenuItem4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.secondsToolStripMenuItem4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.secondsToolStripMenuItem4.Name = "secondsToolStripMenuItem4";
		this.secondsToolStripMenuItem4.Size = new System.Drawing.Size(143, 22);
		this.secondsToolStripMenuItem4.Text = "30 Seconds";
		this.secondsToolStripMenuItem4.Click += new System.EventHandler(this.MenuHandler);
		this.loadDelay.CheckOnClick = true;
		this.loadDelay.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.ms100, this.ms250, this.ms500, this.ms1000, this.ms2000, this.ms5000 });
		this.loadDelay.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.loadDelay.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.loadDelay.Name = "loadDelay";
		this.loadDelay.Size = new System.Drawing.Size(185, 22);
		this.loadDelay.Text = "Load Delay";
		this.loadDelay.Click += new System.EventHandler(this.MenuHandler);
		this.ms100.CheckOnClick = true;
		this.ms100.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.ms100.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.ms100.Name = "ms100";
		this.ms100.Size = new System.Drawing.Size(125, 22);
		this.ms100.Text = "100 ms";
		this.ms100.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
		this.ms100.Click += new System.EventHandler(this.MenuHandler);
		this.ms250.CheckOnClick = true;
		this.ms250.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.ms250.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.ms250.Name = "ms250";
		this.ms250.Size = new System.Drawing.Size(125, 22);
		this.ms250.Text = "250 ms";
		this.ms250.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
		this.ms250.Click += new System.EventHandler(this.MenuHandler);
		this.ms500.CheckOnClick = true;
		this.ms500.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.ms500.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.ms500.Name = "ms500";
		this.ms500.Size = new System.Drawing.Size(125, 22);
		this.ms500.Text = "500 ms";
		this.ms500.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
		this.ms500.Click += new System.EventHandler(this.MenuHandler);
		this.ms1000.CheckOnClick = true;
		this.ms1000.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.ms1000.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.ms1000.Name = "ms1000";
		this.ms1000.Size = new System.Drawing.Size(125, 22);
		this.ms1000.Text = "1000 ms";
		this.ms1000.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
		this.ms1000.Click += new System.EventHandler(this.MenuHandler);
		this.ms2000.CheckOnClick = true;
		this.ms2000.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.ms2000.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.ms2000.Name = "ms2000";
		this.ms2000.Size = new System.Drawing.Size(125, 22);
		this.ms2000.Text = "2000 ms";
		this.ms2000.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
		this.ms2000.Click += new System.EventHandler(this.MenuHandler);
		this.ms5000.CheckOnClick = true;
		this.ms5000.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.ms5000.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.ms5000.Name = "ms5000";
		this.ms5000.Size = new System.Drawing.Size(125, 22);
		this.ms5000.Text = "5000 ms";
		this.ms5000.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
		this.ms5000.Click += new System.EventHandler(this.MenuHandler);
		this.startHidden.CheckOnClick = true;
		this.startHidden.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.startHidden.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.startHidden.Name = "startHidden";
		this.startHidden.Size = new System.Drawing.Size(185, 22);
		this.startHidden.Text = "Start Hidden";
		this.startHidden.Click += new System.EventHandler(this.MenuHandler);
		this.systemFont.CheckOnClick = true;
		this.systemFont.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.systemFont.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.systemFont.Name = "systemFont";
		this.systemFont.Size = new System.Drawing.Size(185, 22);
		this.systemFont.Text = "Use System Font";
		this.systemFont.Click += new System.EventHandler(this.MenuHandler);
		this.itemHeader.CheckOnClick = true;
		this.itemHeader.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.itemHeader.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.itemHeader.Name = "itemHeader";
		this.itemHeader.Size = new System.Drawing.Size(185, 22);
		this.itemHeader.Text = "Show Item Header";
		this.itemHeader.Click += new System.EventHandler(this.MenuHandler);
		this.refreshCharViewToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.refreshCharViewToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("refreshCharViewToolStripMenuItem.Image");
		this.refreshCharViewToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.refreshCharViewToolStripMenuItem.Name = "refreshCharViewToolStripMenuItem";
		this.refreshCharViewToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
		this.refreshCharViewToolStripMenuItem.Text = "Refresh Char View";
		this.refreshCharViewToolStripMenuItem.Click += new System.EventHandler(this.RefreshCharViewToolStripMenuItem_Click);
		this.closeGameexeToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.closeGameexeToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.closeGameexeToolStripMenuItem.Name = "closeGameexeToolStripMenuItem";
		this.closeGameexeToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
		this.closeGameexeToolStripMenuItem.Text = "Close Game.exe";
		this.closeGameexeToolStripMenuItem.Click += new System.EventHandler(this.CloseGameexeToolStripMenuItem_Click);
		this.serverToggle.CheckOnClick = true;
		this.serverToggle.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.serverToggle.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.serverToggle.Name = "serverToggle";
		this.serverToggle.Size = new System.Drawing.Size(185, 22);
		this.serverToggle.Text = "Run API Server";
		this.serverToggle.Click += new System.EventHandler(this.MenuHandler);
		this.debugMode.CheckOnClick = true;
		this.debugMode.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.debugMode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.debugMode.Name = "debugMode";
		this.debugMode.Size = new System.Drawing.Size(185, 22);
		this.debugMode.Text = "Developer Mode";
		this.debugMode.Click += new System.EventHandler(this.MenuHandler);
		this.guiRemove.AutoSize = false;
		this.guiRemove.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.guiRemove.Image = (System.Drawing.Image)resources.GetObject("guiRemove.Image");
		this.guiRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.guiRemove.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.guiRemove.ImageTransparentColor = System.Drawing.Color.White;
		this.guiRemove.Margin = new System.Windows.Forms.Padding(0);
		this.guiRemove.Name = "guiRemove";
		this.guiRemove.Size = new System.Drawing.Size(145, 36);
		this.guiRemove.Text = "  Delete";
		this.guiRemove.ToolTipText = "Delete Selected Profiles";
		this.guiRemove.Click += new System.EventHandler(this.Remove);
		this.guiAddIRC.AutoSize = false;
		this.guiAddIRC.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.guiAddIRC.Image = (System.Drawing.Image)resources.GetObject("guiAddIRC.Image");
		this.guiAddIRC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.guiAddIRC.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.guiAddIRC.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.guiAddIRC.Margin = new System.Windows.Forms.Padding(0);
		this.guiAddIRC.Name = "guiAddIRC";
		this.guiAddIRC.Size = new System.Drawing.Size(145, 36);
		this.guiAddIRC.Text = "  Add IRC";
		this.guiAddIRC.ToolTipText = "Add IRC Profile";
		this.guiAddIRC.Click += new System.EventHandler(this.AddIRC);
		this.keysEdit.AutoSize = false;
		this.keysEdit.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.keysEdit.Image = (System.Drawing.Image)resources.GetObject("keysEdit.Image");
		this.keysEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.keysEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.keysEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.keysEdit.Margin = new System.Windows.Forms.Padding(0);
		this.keysEdit.Name = "keysEdit";
		this.keysEdit.Size = new System.Drawing.Size(145, 36);
		this.keysEdit.Text = "  Keys";
		this.keysEdit.ToolTipText = "Create or Import Keylist";
		this.keysEdit.Click += new System.EventHandler(this.KeysEdit_Click);
		this.scheduleEdit.AutoSize = false;
		this.scheduleEdit.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.scheduleEdit.Image = (System.Drawing.Image)resources.GetObject("scheduleEdit.Image");
		this.scheduleEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.scheduleEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.scheduleEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.scheduleEdit.Margin = new System.Windows.Forms.Padding(0);
		this.scheduleEdit.Name = "scheduleEdit";
		this.scheduleEdit.Size = new System.Drawing.Size(145, 36);
		this.scheduleEdit.Text = "  Schedules";
		this.scheduleEdit.ToolTipText = "Create or Import Schedule";
		this.scheduleEdit.Click += new System.EventHandler(this.ScheduleEdit_Click);
		this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
		this.mainContainer.Location = new System.Drawing.Point(148, 1);
		this.mainContainer.Margin = new System.Windows.Forms.Padding(0);
		this.mainContainer.Name = "mainContainer";
		this.mainContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.mainContainer.Panel1.Controls.Add(this.pictureBox1);
		this.mainContainer.Panel1.Controls.Add((System.Windows.Forms.Control)(object)this.objectProfileList);
		this.mainContainer.Panel2.Controls.Add(this.PrintTab);
		this.mainContainer.Size = new System.Drawing.Size(755, 559);
		this.mainContainer.SplitterDistance = 245;
		this.mainContainer.SplitterWidth = 5;
		this.mainContainer.TabIndex = 5;
		this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.InitialImage = (System.Drawing.Image)resources.GetObject("pictureBox1.InitialImage");
		this.pictureBox1.Location = new System.Drawing.Point(0, 145);
		this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(430, 100);
		this.pictureBox1.TabIndex = 7;
		this.pictureBox1.TabStop = false;
		this.pictureBox1.Visible = false;
		((System.Windows.Forms.ListView)(object)this.objectProfileList).Activation = System.Windows.Forms.ItemActivation.OneClick;
		this.objectProfileList.AllColumns.Add(this.profileCol);
		this.objectProfileList.AllColumns.Add(this.statusCol);
		this.objectProfileList.AllColumns.Add(this.runsCol);
		this.objectProfileList.AllColumns.Add(this.chickensCol);
		this.objectProfileList.AllColumns.Add(this.deathsCol);
		this.objectProfileList.AllColumns.Add(this.crashesCol);
		this.objectProfileList.AllColumns.Add(this.restartsCol);
		this.objectProfileList.AllColumns.Add(this.keyCol);
		this.objectProfileList.AllColumns.Add(this.gameExe);
		this.objectProfileList.AlternateRowBackColor = System.Drawing.SystemColors.Control;
		((System.Windows.Forms.ListView)(object)this.objectProfileList).AutoArrange = false;
		((System.Windows.Forms.ListView)(object)this.objectProfileList).BorderStyle = System.Windows.Forms.BorderStyle.None;
		((System.Windows.Forms.Control)(object)this.objectProfileList).CausesValidation = false;
		this.objectProfileList.CellEditUseWholeCell = false;
		this.objectProfileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[9]
		{
			(System.Windows.Forms.ColumnHeader)(object)this.profileCol,
			(System.Windows.Forms.ColumnHeader)(object)this.statusCol,
			(System.Windows.Forms.ColumnHeader)(object)this.runsCol,
			(System.Windows.Forms.ColumnHeader)(object)this.chickensCol,
			(System.Windows.Forms.ColumnHeader)(object)this.deathsCol,
			(System.Windows.Forms.ColumnHeader)(object)this.crashesCol,
			(System.Windows.Forms.ColumnHeader)(object)this.restartsCol,
			(System.Windows.Forms.ColumnHeader)(object)this.keyCol,
			(System.Windows.Forms.ColumnHeader)(object)this.gameExe
		});
		((System.Windows.Forms.Control)(object)this.objectProfileList).ContextMenuStrip = this.contextMenuStrip1;
		this.objectProfileList.CopySelectionOnControlC = false;
		this.objectProfileList.CopySelectionOnControlCUsesDragSource = false;
		((System.Windows.Forms.Control)(object)this.objectProfileList).Cursor = System.Windows.Forms.Cursors.Default;
		((System.Windows.Forms.Control)(object)this.objectProfileList).Dock = System.Windows.Forms.DockStyle.Fill;
		this.objectProfileList.EmptyListMsg = "";
		((System.Windows.Forms.Control)(object)this.objectProfileList).Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		((System.Windows.Forms.Control)(object)this.objectProfileList).ForeColor = System.Drawing.SystemColors.WindowText;
		((System.Windows.Forms.ListView)(object)this.objectProfileList).FullRowSelect = true;
		((System.Windows.Forms.ListView)(object)this.objectProfileList).GridLines = true;
		this.objectProfileList.HasCollapsibleGroups = false;
		this.objectProfileList.HeaderMaximumHeight = 24;
		((System.Windows.Forms.ListView)(object)this.objectProfileList).HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
		this.objectProfileList.HeaderUsesThemes = true;
		this.objectProfileList.IsSearchOnSortColumn = false;
		((System.Windows.Forms.Control)(object)this.objectProfileList).Location = new System.Drawing.Point(0, 0);
		((System.Windows.Forms.Control)(object)this.objectProfileList).Margin = new System.Windows.Forms.Padding(0);
		((System.Windows.Forms.Control)(object)this.objectProfileList).Name = "objectProfileList";
		this.objectProfileList.RowHeight = 21;
		this.objectProfileList.SelectedBackColor = System.Drawing.SystemColors.MenuHighlight;
		this.objectProfileList.ShowGroups = false;
		this.objectProfileList.ShowSortIndicators = false;
		((System.Windows.Forms.Control)(object)this.objectProfileList).Size = new System.Drawing.Size(755, 245);
		this.objectProfileList.SmallImageList = this.profileImages;
		this.objectProfileList.SortGroupItemsByPrimaryColumn = false;
		((System.Windows.Forms.Control)(object)this.objectProfileList).TabIndex = 1;
		this.objectProfileList.UseAlternatingBackColors = true;
		((System.Windows.Forms.ListView)(object)this.objectProfileList).UseCompatibleStateImageBehavior = false;
		this.objectProfileList.UseHotControls = false;
		this.objectProfileList.UseOverlays = false;
		this.objectProfileList.View = System.Windows.Forms.View.Details;
		this.objectProfileList.ModelDropped += new System.EventHandler<ModelDropEventArgs>(ObjectProfileList_ModelDropped);
		((System.Windows.Forms.Control)(object)this.objectProfileList).DoubleClick += new System.EventHandler(this.Edit);
		this.profileCol.AspectName = "Name";
		this.profileCol.AutoCompleteEditor = false;
		this.profileCol.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
		this.profileCol.Groupable = false;
		this.profileCol.GroupWithItemCountFormat = "";
		this.profileCol.GroupWithItemCountSingularFormat = "";
		this.profileCol.Hideable = false;
		this.profileCol.ImageAspectName = "";
		this.profileCol.MinimumWidth = 60;
		this.profileCol.Searchable = false;
		this.profileCol.Sortable = false;
		((System.Windows.Forms.ColumnHeader)(object)this.profileCol).Text = "Profile";
		this.profileCol.UseFiltering = false;
		this.profileCol.Width = 125;
		this.statusCol.AspectName = "State";
		this.statusCol.AutoCompleteEditor = false;
		this.statusCol.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
		this.statusCol.Groupable = false;
		this.statusCol.MinimumWidth = 120;
		this.statusCol.Searchable = false;
		this.statusCol.Sortable = false;
		((System.Windows.Forms.ColumnHeader)(object)this.statusCol).Text = "Status";
		this.statusCol.UseFiltering = false;
		this.statusCol.Width = 120;
		this.runsCol.AspectName = "Runs";
		this.runsCol.AutoCompleteEditor = false;
		this.runsCol.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
		this.runsCol.Groupable = false;
		this.runsCol.MaximumWidth = 50;
		this.runsCol.MinimumWidth = 50;
		this.runsCol.Searchable = false;
		((System.Windows.Forms.ColumnHeader)(object)this.runsCol).Text = "Runs";
		this.runsCol.UseFiltering = false;
		this.runsCol.Width = 50;
		this.chickensCol.AspectName = "Chickens";
		this.chickensCol.AutoCompleteEditor = false;
		this.chickensCol.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
		this.chickensCol.Groupable = false;
		this.chickensCol.MaximumWidth = 50;
		this.chickensCol.MinimumWidth = 50;
		this.chickensCol.Searchable = false;
		((System.Windows.Forms.ColumnHeader)(object)this.chickensCol).Text = "Exits";
		this.chickensCol.UseFiltering = false;
		this.chickensCol.Width = 50;
		this.deathsCol.AspectName = "Deaths";
		this.deathsCol.AutoCompleteEditor = false;
		this.deathsCol.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
		this.deathsCol.Groupable = false;
		this.deathsCol.MaximumWidth = 55;
		this.deathsCol.MinimumWidth = 55;
		this.deathsCol.Searchable = false;
		((System.Windows.Forms.ColumnHeader)(object)this.deathsCol).Text = "Deaths";
		this.deathsCol.UseFiltering = false;
		this.deathsCol.Width = 55;
		this.crashesCol.AspectName = "Crashes";
		this.crashesCol.AutoCompleteEditor = false;
		this.crashesCol.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
		this.crashesCol.Groupable = false;
		this.crashesCol.MaximumWidth = 60;
		this.crashesCol.MinimumWidth = 60;
		this.crashesCol.Searchable = false;
		((System.Windows.Forms.ColumnHeader)(object)this.crashesCol).Text = "Crashes";
		this.crashesCol.UseFiltering = false;
		this.restartsCol.AspectName = "Restarts";
		this.restartsCol.AutoCompleteEditor = false;
		this.restartsCol.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
		this.restartsCol.Groupable = false;
		this.restartsCol.MaximumWidth = 60;
		this.restartsCol.MinimumWidth = 60;
		this.restartsCol.Searchable = false;
		((System.Windows.Forms.ColumnHeader)(object)this.restartsCol).Text = "Restarts";
		this.restartsCol.UseFiltering = false;
		this.keyCol.AspectName = "Key";
		this.keyCol.AutoCompleteEditor = false;
		this.keyCol.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
		this.keyCol.Groupable = false;
		this.keyCol.MaximumWidth = 150;
		this.keyCol.MinimumWidth = 80;
		this.keyCol.Searchable = false;
		((System.Windows.Forms.ColumnHeader)(object)this.keyCol).Text = "Key";
		this.keyCol.UseFiltering = false;
		this.keyCol.Width = 80;
		this.gameExe.AspectName = "GameExe";
		this.gameExe.AutoCompleteEditor = false;
		this.gameExe.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
		this.gameExe.Groupable = false;
		this.gameExe.Hideable = false;
		this.gameExe.MinimumWidth = 110;
		this.gameExe.Searchable = false;
		this.gameExe.Sortable = false;
		((System.Windows.Forms.ColumnHeader)(object)this.gameExe).Text = "Target";
		this.gameExe.UseFiltering = false;
		this.gameExe.Width = 120;
		this.profileImages.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("profileImages.ImageStream");
		this.profileImages.TransparentColor = System.Drawing.Color.Transparent;
		this.profileImages.Images.SetKeyName(0, "bot.ico");
		this.profileImages.Images.SetKeyName(1, "Console.ico");
		this.profileImages.Images.SetKeyName(2, "greenbot.ico");
		this.profileImages.Images.SetKeyName(3, "Bot.ico");
		this.PrintTab.Controls.Add(this.Console);
		this.PrintTab.Controls.Add(this.charView);
		this.PrintTab.Controls.Add(this.KeyAnalysis);
		this.PrintTab.Dock = System.Windows.Forms.DockStyle.Fill;
		this.PrintTab.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.PrintTab.ItemSize = new System.Drawing.Size(90, 20);
		this.PrintTab.Location = new System.Drawing.Point(0, 0);
		this.PrintTab.Margin = new System.Windows.Forms.Padding(0);
		this.PrintTab.Multiline = true;
		this.PrintTab.Name = "PrintTab";
		this.PrintTab.Padding = new System.Drawing.Point(0, 0);
		this.PrintTab.SelectedIndex = 0;
		this.PrintTab.Size = new System.Drawing.Size(755, 309);
		this.PrintTab.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
		this.PrintTab.TabIndex = 1;
		this.PrintTab.Selected += new System.Windows.Forms.TabControlEventHandler(PrintTab_Selected);
		this.Console.Controls.Add(this.splitContainer1);
		this.Console.Location = new System.Drawing.Point(4, 24);
		this.Console.Margin = new System.Windows.Forms.Padding(0);
		this.Console.Name = "Console";
		this.Console.Size = new System.Drawing.Size(747, 281);
		this.Console.TabIndex = 0;
		this.Console.Text = "Console";
		this.Console.UseVisualStyleBackColor = true;
		this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.Location = new System.Drawing.Point(0, 0);
		this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.ConsoleBox);
		this.splitContainer1.Panel2.Controls.Add(this.ItemLogger);
		this.splitContainer1.Size = new System.Drawing.Size(747, 281);
		this.splitContainer1.SplitterDistance = 425;
		this.splitContainer1.TabIndex = 7;
		this.ItemLogger.AutoArrange = false;
		this.ItemLogger.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.ItemLogger.Columns.AddRange(new System.Windows.Forms.ColumnHeader[2] { this.ItemDateProfile, this.ItemLogColumn });
		this.ItemLogger.ContextMenuStrip = this.contextMenuStrip4;
		this.ItemLogger.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ItemLogger.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ItemLogger.FullRowSelect = true;
		this.ItemLogger.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.ItemLogger.LabelWrap = false;
		this.ItemLogger.Location = new System.Drawing.Point(0, 0);
		this.ItemLogger.Margin = new System.Windows.Forms.Padding(0);
		this.ItemLogger.MultiSelect = false;
		this.ItemLogger.Name = "ItemLogger";
		this.ItemLogger.ShowGroups = false;
		this.ItemLogger.Size = new System.Drawing.Size(318, 281);
		this.ItemLogger.TabIndex = 5;
		this.ItemLogger.UseCompatibleStateImageBehavior = false;
		this.ItemLogger.View = System.Windows.Forms.View.Details;
		this.ItemLogger.SelectedIndexChanged += new System.EventHandler(this.ItemLogger_SelectedIndexChanged);
		this.ItemLogger.MouseLeave += new System.EventHandler(this.ItemLogger_MouseLeave);
		this.ItemLogger.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Mouse_Over);
		this.ItemDateProfile.Text = "";
		this.ItemDateProfile.Width = 25;
		this.ItemLogColumn.Text = "";
		this.ItemLogColumn.Width = 100;
		this.charView.Controls.Add(this.charContainer);
		this.charView.Location = new System.Drawing.Point(4, 24);
		this.charView.Margin = new System.Windows.Forms.Padding(0);
		this.charView.Name = "charView";
		this.charView.Size = new System.Drawing.Size(747, 281);
		this.charView.TabIndex = 2;
		this.charView.Text = "Char Viewer";
		this.charView.UseVisualStyleBackColor = true;
		this.charContainer.Dock = System.Windows.Forms.DockStyle.Fill;
		this.charContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.charContainer.IsSplitterFixed = true;
		this.charContainer.Location = new System.Drawing.Point(0, 0);
		this.charContainer.Margin = new System.Windows.Forms.Padding(0);
		this.charContainer.Name = "charContainer";
		this.charContainer.Panel1.Controls.Add(this.CharTree);
		this.charContainer.Panel2.Controls.Add(this.tableLayoutPanel2);
		this.charContainer.Size = new System.Drawing.Size(747, 281);
		this.charContainer.SplitterDistance = 265;
		this.charContainer.SplitterWidth = 1;
		this.charContainer.TabIndex = 0;
		this.CharTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.CharTree.Dock = System.Windows.Forms.DockStyle.Fill;
		this.CharTree.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharTree.Location = new System.Drawing.Point(0, 0);
		this.CharTree.Margin = new System.Windows.Forms.Padding(0);
		this.CharTree.Name = "CharTree";
		this.CharTree.Size = new System.Drawing.Size(265, 281);
		this.CharTree.TabIndex = 0;
		this.CharTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(CharTree_NodeMouseClick);
		this.tableLayoutPanel2.ColumnCount = 1;
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel2.Controls.Add(this.charSearchBar, 0, 1);
		this.tableLayoutPanel2.Controls.Add(this.CharItems, 0, 0);
		this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
		this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
		this.tableLayoutPanel2.Name = "tableLayoutPanel2";
		this.tableLayoutPanel2.RowCount = 2;
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27f));
		this.tableLayoutPanel2.Size = new System.Drawing.Size(481, 281);
		this.tableLayoutPanel2.TabIndex = 13;
		this.charSearchBar.ColumnCount = 2;
		this.charSearchBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.charSearchBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27f));
		this.charSearchBar.Controls.Add(this.SearchBox, 0, 0);
		this.charSearchBar.Controls.Add(this.SearchButton, 1, 0);
		this.charSearchBar.Dock = System.Windows.Forms.DockStyle.Fill;
		this.charSearchBar.Location = new System.Drawing.Point(0, 254);
		this.charSearchBar.Margin = new System.Windows.Forms.Padding(0);
		this.charSearchBar.Name = "charSearchBar";
		this.charSearchBar.RowCount = 1;
		this.charSearchBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.charSearchBar.Size = new System.Drawing.Size(481, 27);
		this.charSearchBar.TabIndex = 0;
		this.SearchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.SearchBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SearchBox.Font = new System.Drawing.Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.SearchBox.Location = new System.Drawing.Point(0, 0);
		this.SearchBox.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
		this.SearchBox.Name = "SearchBox";
		this.SearchBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.SearchBox.Size = new System.Drawing.Size(453, 27);
		this.SearchBox.TabIndex = 9;
		this.SearchBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(SearchEnter);
		this.SearchButton.BackColor = System.Drawing.SystemColors.Window;
		this.SearchButton.BackgroundImage = (System.Drawing.Image)resources.GetObject("SearchButton.BackgroundImage");
		this.SearchButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.SearchButton.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SearchButton.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
		this.SearchButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonFace;
		this.SearchButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
		this.SearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.SearchButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		this.SearchButton.Location = new System.Drawing.Point(454, 0);
		this.SearchButton.Margin = new System.Windows.Forms.Padding(0);
		this.SearchButton.MaximumSize = new System.Drawing.Size(27, 27);
		this.SearchButton.MinimumSize = new System.Drawing.Size(27, 27);
		this.SearchButton.Name = "SearchButton";
		this.SearchButton.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
		this.SearchButton.Size = new System.Drawing.Size(27, 27);
		this.SearchButton.TabIndex = 11;
		this.SearchButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.SearchButton.UseVisualStyleBackColor = false;
		this.SearchButton.Click += new System.EventHandler(this.QueryItem);
		this.CharItems.BackColor = System.Drawing.SystemColors.Window;
		this.CharItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.CharItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { this.CharItemColumn });
		this.CharItems.ContextMenuStrip = this.contextMenuStrip4;
		this.CharItems.Dock = System.Windows.Forms.DockStyle.Fill;
		this.CharItems.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharItems.FullRowSelect = true;
		this.CharItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.CharItems.LabelWrap = false;
		this.CharItems.Location = new System.Drawing.Point(0, 0);
		this.CharItems.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
		this.CharItems.MultiSelect = false;
		this.CharItems.Name = "CharItems";
		this.CharItems.Size = new System.Drawing.Size(481, 253);
		this.CharItems.TabIndex = 6;
		this.CharItems.UseCompatibleStateImageBehavior = false;
		this.CharItems.View = System.Windows.Forms.View.Details;
		this.CharItems.VirtualListSize = 25;
		this.CharItems.SelectedIndexChanged += new System.EventHandler(this.CharLogger_SelectedIndexChanged);
		this.CharItems.MouseLeave += new System.EventHandler(this.ItemLogger_MouseLeave);
		this.CharItems.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Mouse_Over);
		this.CharItemColumn.Width = 300;
		this.KeyAnalysis.Controls.Add(this.keyWizardContainer);
		this.KeyAnalysis.Location = new System.Drawing.Point(4, 24);
		this.KeyAnalysis.Margin = new System.Windows.Forms.Padding(0);
		this.KeyAnalysis.Name = "KeyAnalysis";
		this.KeyAnalysis.Size = new System.Drawing.Size(747, 281);
		this.KeyAnalysis.TabIndex = 3;
		this.KeyAnalysis.Text = "Key Wizard";
		this.KeyAnalysis.UseVisualStyleBackColor = true;
		this.keyWizardContainer.Dock = System.Windows.Forms.DockStyle.Fill;
		this.keyWizardContainer.Location = new System.Drawing.Point(0, 0);
		this.keyWizardContainer.Name = "keyWizardContainer";
		this.keyWizardContainer.Panel1.BackColor = System.Drawing.Color.Transparent;
		this.keyWizardContainer.Panel1.Controls.Add(this.keyWizBorder);
		this.keyWizardContainer.Panel2.Controls.Add(this.KeyData);
		this.keyWizardContainer.Size = new System.Drawing.Size(747, 281);
		this.keyWizardContainer.SplitterDistance = 204;
		this.keyWizardContainer.SplitterWidth = 1;
		this.keyWizardContainer.TabIndex = 3;
		this.keyWizBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.keyWizBorder.Controls.Add(this.dupeList);
		this.keyWizBorder.Dock = System.Windows.Forms.DockStyle.Fill;
		this.keyWizBorder.Location = new System.Drawing.Point(0, 0);
		this.keyWizBorder.Margin = new System.Windows.Forms.Padding(0);
		this.keyWizBorder.Name = "keyWizBorder";
		this.keyWizBorder.Size = new System.Drawing.Size(204, 281);
		this.keyWizBorder.TabIndex = 3;
		this.dupeList.AllowUserToAddRows = false;
		this.dupeList.AllowUserToDeleteRows = false;
		this.dupeList.AllowUserToResizeRows = false;
		this.dupeList.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
		this.dupeList.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.dupeList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
		this.dupeList.Columns.AddRange(this.keyDupe, this.profileDupe);
		this.dupeList.ContextMenuStrip = this.contextMenuStrip5;
		this.dupeList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dupeList.GridColor = System.Drawing.SystemColors.ActiveBorder;
		this.dupeList.Location = new System.Drawing.Point(0, 0);
		this.dupeList.Margin = new System.Windows.Forms.Padding(0);
		this.dupeList.Name = "dupeList";
		this.dupeList.RowHeadersVisible = false;
		this.dupeList.RowHeadersWidth = 10;
		this.dupeList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dupeList.RowTemplate.Height = 16;
		this.dupeList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.dupeList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.dupeList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.dupeList.ShowCellErrors = false;
		this.dupeList.ShowCellToolTips = false;
		this.dupeList.ShowEditingIcon = false;
		this.dupeList.ShowRowErrors = false;
		this.dupeList.Size = new System.Drawing.Size(202, 279);
		this.dupeList.TabIndex = 2;
		this.keyDupe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.keyDupe.FillWeight = 30f;
		this.keyDupe.HeaderText = "Keys";
		this.keyDupe.MinimumWidth = 60;
		this.keyDupe.Name = "keyDupe";
		this.keyDupe.ReadOnly = true;
		this.profileDupe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.profileDupe.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
		this.profileDupe.FillWeight = 59.39086f;
		this.profileDupe.HeaderText = "List Name(s)";
		this.profileDupe.MinimumWidth = 100;
		this.profileDupe.Name = "profileDupe";
		this.KeyData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.KeyData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[5] { this.KeyProfile, this.KeyName, this.KeyInUse, this.KeyRD, this.KeyDisabled });
		this.KeyData.ContextMenuStrip = this.contextMenuStrip2;
		this.KeyData.Dock = System.Windows.Forms.DockStyle.Fill;
		this.KeyData.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.KeyData.FullRowSelect = true;
		this.KeyData.GridLines = true;
		this.KeyData.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
		this.KeyData.HideSelection = false;
		this.KeyData.Location = new System.Drawing.Point(0, 0);
		this.KeyData.Margin = new System.Windows.Forms.Padding(0);
		this.KeyData.Name = "KeyData";
		this.KeyData.Size = new System.Drawing.Size(542, 281);
		this.KeyData.Sorting = System.Windows.Forms.SortOrder.Ascending;
		this.KeyData.TabIndex = 0;
		this.KeyData.UseCompatibleStateImageBehavior = false;
		this.KeyData.View = System.Windows.Forms.View.Details;
		this.KeyProfile.Text = "Profile";
		this.KeyProfile.Width = 100;
		this.KeyName.Text = "Key";
		this.KeyName.Width = 110;
		this.KeyInUse.Text = "In Use";
		this.KeyInUse.Width = 90;
		this.KeyRD.Text = "RDs";
		this.KeyRD.Width = 80;
		this.KeyDisabled.Text = "Disabled";
		this.KeyDisabled.Width = 100;
		this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
		this.tableLayoutPanel1.ColumnCount = 2;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 146f));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.mainContainer, 1, 0);
		this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 1;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(904, 561);
		this.tableLayoutPanel1.TabIndex = 6;
		this.ConsoleBox.AcceptsTab = true;
		this.ConsoleBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.ConsoleBox.ContextMenuStrip = this.contextMenuStrip3;
		this.ConsoleBox.DetectUrls = true;
		this.ConsoleBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ConsoleBox.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ConsoleBox.Location = new System.Drawing.Point(0, 0);
		this.ConsoleBox.Margin = new System.Windows.Forms.Padding(0);
		this.ConsoleBox.Name = "ConsoleBox";
		this.ConsoleBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
		this.ConsoleBox.Size = new System.Drawing.Size(425, 281);
		this.ConsoleBox.TabIndex = 0;
		this.ConsoleBox.Text = "";
		this.ConsoleBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(LinkClicked);
		this.AutoScaleDimensions = new System.Drawing.SizeF(8f, 20f);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(904, 561);
		this.Controls.Add(this.tableLayoutPanel1);
		this.DoubleBuffered = true;
		this.Font = new System.Drawing.Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.Location = new System.Drawing.Point(200, 150);
		this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.MinimumSize = new System.Drawing.Size(920, 600);
		this.Name = "Main";
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "D2Bot #";
		this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_Close);
		this.Load += new System.EventHandler(this.Main_Load);
		this.contextMenuStrip1.ResumeLayout(false);
		this.contextMenuStrip3.ResumeLayout(false);
		this.contextMenuStrip4.ResumeLayout(false);
		this.contextMenuStrip5.ResumeLayout(false);
		this.contextMenuStrip2.ResumeLayout(false);
		this.toolStrip1.ResumeLayout(false);
		this.toolStrip1.PerformLayout();
		this.mainContainer.Panel1.ResumeLayout(false);
		this.mainContainer.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.mainContainer).EndInit();
		this.mainContainer.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.objectProfileList).EndInit();
		this.PrintTab.ResumeLayout(false);
		this.Console.ResumeLayout(false);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		this.charView.ResumeLayout(false);
		this.charContainer.Panel1.ResumeLayout(false);
		this.charContainer.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.charContainer).EndInit();
		this.charContainer.ResumeLayout(false);
		this.tableLayoutPanel2.ResumeLayout(false);
		this.charSearchBar.ResumeLayout(false);
		this.charSearchBar.PerformLayout();
		this.KeyAnalysis.ResumeLayout(false);
		this.keyWizardContainer.Panel1.ResumeLayout(false);
		this.keyWizardContainer.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.keyWizardContainer).EndInit();
		this.keyWizardContainer.ResumeLayout(false);
		this.keyWizBorder.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dupeList).EndInit();
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		this.ResumeLayout(false);
	}
}
