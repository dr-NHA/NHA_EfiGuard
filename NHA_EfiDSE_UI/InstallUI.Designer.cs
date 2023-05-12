namespace N_EFI
{
    partial class InstallUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ITEMS = new System.Windows.Forms.ListView();
            this.Menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.InstallToDrive_M = new System.Windows.Forms.ToolStripMenuItem();
            this.TopSplitter = new System.Windows.Forms.TableLayoutPanel();
            this.INFO = new System.Windows.Forms.RichTextBox();
            this.RightSplitter = new System.Windows.Forms.TableLayoutPanel();
            this.REFRESH = new System.Windows.Forms.Button();
            this.INCOMPATIBLE = new System.Windows.Forms.CheckBox();
            this.Menu.SuspendLayout();
            this.TopSplitter.SuspendLayout();
            this.RightSplitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // ITEMS
            // 
            this.ITEMS.BackColor = System.Drawing.Color.Black;
            this.ITEMS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ITEMS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ITEMS.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.ITEMS.FullRowSelect = true;
            this.ITEMS.GridLines = true;
            this.ITEMS.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ITEMS.HideSelection = false;
            this.ITEMS.Location = new System.Drawing.Point(4, 83);
            this.ITEMS.Name = "ITEMS";
            this.ITEMS.Size = new System.Drawing.Size(675, 180);
            this.ITEMS.TabIndex = 0;
            this.ITEMS.UseCompatibleStateImageBehavior = false;
            this.ITEMS.View = System.Windows.Forms.View.Details;
            // 
            // Menu
            // 
            this.Menu.BackColor = System.Drawing.Color.Black;
            this.Menu.Font = new System.Drawing.Font("Leelawadee UI", 8.25F);
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InstallToDrive_M});
            this.Menu.Name = "Menu";
            this.Menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.Menu.ShowImageMargin = false;
            this.Menu.Size = new System.Drawing.Size(125, 26);
            // 
            // InstallToDrive_M
            // 
            this.InstallToDrive_M.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.InstallToDrive_M.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.InstallToDrive_M.Name = "InstallToDrive_M";
            this.InstallToDrive_M.Size = new System.Drawing.Size(124, 22);
            this.InstallToDrive_M.Text = "Install To Drive";
            this.InstallToDrive_M.ToolTipText = "Install The EFI Driver Setup To The Drive You Selected";
            // 
            // TopSplitter
            // 
            this.TopSplitter.ColumnCount = 2;
            this.TopSplitter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.10619F));
            this.TopSplitter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.8938F));
            this.TopSplitter.Controls.Add(this.RightSplitter, 1, 0);
            this.TopSplitter.Controls.Add(this.INFO, 0, 0);
            this.TopSplitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopSplitter.Location = new System.Drawing.Point(4, 4);
            this.TopSplitter.Name = "TopSplitter";
            this.TopSplitter.RowCount = 1;
            this.TopSplitter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TopSplitter.Size = new System.Drawing.Size(675, 79);
            this.TopSplitter.TabIndex = 2;
            // 
            // INFO
            // 
            this.INFO.BackColor = System.Drawing.Color.Black;
            this.INFO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.INFO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.INFO.ForeColor = System.Drawing.Color.PaleGreen;
            this.INFO.Location = new System.Drawing.Point(3, 3);
            this.INFO.Name = "INFO";
            this.INFO.Size = new System.Drawing.Size(507, 73);
            this.INFO.TabIndex = 0;
            this.INFO.Text = "";
            this.INFO.WordWrap = false;
            // 
            // RightSplitter
            // 
            this.RightSplitter.ColumnCount = 1;
            this.RightSplitter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.RightSplitter.Controls.Add(this.REFRESH, 0, 0);
            this.RightSplitter.Controls.Add(this.INCOMPATIBLE, 0, 1);
            this.RightSplitter.Location = new System.Drawing.Point(516, 3);
            this.RightSplitter.Name = "RightSplitter";
            this.RightSplitter.RowCount = 2;
            this.RightSplitter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.RightSplitter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.RightSplitter.Size = new System.Drawing.Size(156, 73);
            this.RightSplitter.TabIndex = 3;
            // 
            // REFRESH
            // 
            this.REFRESH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.REFRESH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.REFRESH.Location = new System.Drawing.Point(3, 3);
            this.REFRESH.Name = "REFRESH";
            this.REFRESH.Size = new System.Drawing.Size(150, 30);
            this.REFRESH.TabIndex = 2;
            this.REFRESH.Text = "Refresh";
            this.REFRESH.UseVisualStyleBackColor = true;
            // 
            // INCOMPATIBLE
            // 
            this.INCOMPATIBLE.AutoSize = true;
            this.INCOMPATIBLE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.INCOMPATIBLE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.INCOMPATIBLE.Location = new System.Drawing.Point(3, 39);
            this.INCOMPATIBLE.Name = "INCOMPATIBLE";
            this.INCOMPATIBLE.Size = new System.Drawing.Size(150, 31);
            this.INCOMPATIBLE.TabIndex = 3;
            this.INCOMPATIBLE.Text = "Show Incompatible";
            this.INCOMPATIBLE.UseVisualStyleBackColor = true;
            // 
            // InstallUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(683, 267);
            this.Controls.Add(this.ITEMS);
            this.Controls.Add(this.TopSplitter);
            this.Font = new System.Drawing.Font("Leelawadee UI", 8.25F);
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InstallUI";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NHA EFI Install / Setup";
            this.Menu.ResumeLayout(false);
            this.TopSplitter.ResumeLayout(false);
            this.RightSplitter.ResumeLayout(false);
            this.RightSplitter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ITEMS;
        private System.Windows.Forms.ContextMenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem InstallToDrive_M;
        private System.Windows.Forms.TableLayoutPanel TopSplitter;
        private System.Windows.Forms.RichTextBox INFO;
        private System.Windows.Forms.TableLayoutPanel RightSplitter;
        private System.Windows.Forms.Button REFRESH;
        private System.Windows.Forms.CheckBox INCOMPATIBLE;
    }
}