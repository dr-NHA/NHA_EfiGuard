namespace N_EFI
{
    partial class MainUI
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
            this.Test = new System.Windows.Forms.Button();
            this.Get = new System.Windows.Forms.Button();
            this.TopTable = new System.Windows.Forms.TableLayoutPanel();
            this.BottomTable = new System.Windows.Forms.TableLayoutPanel();
            this.DISABLE = new System.Windows.Forms.Button();
            this.INSTALL = new System.Windows.Forms.Button();
            this.CenterPanel = new System.Windows.Forms.Panel();
            this.DSE_INFO = new System.Windows.Forms.RichTextBox();
            this.TopTable.SuspendLayout();
            this.BottomTable.SuspendLayout();
            this.CenterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Test
            // 
            this.Test.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Test.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Test.Location = new System.Drawing.Point(4, 4);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(220, 26);
            this.Test.TabIndex = 1;
            this.Test.Text = "Test EFI Backdoor";
            this.Test.UseVisualStyleBackColor = true;
            // 
            // Get
            // 
            this.Get.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Get.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Get.Location = new System.Drawing.Point(231, 4);
            this.Get.Name = "Get";
            this.Get.Size = new System.Drawing.Size(221, 26);
            this.Get.TabIndex = 2;
            this.Get.Text = "Get g_CiOptions + System Info";
            this.Get.UseVisualStyleBackColor = true;
            // 
            // TopTable
            // 
            this.TopTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.TopTable.ColumnCount = 2;
            this.TopTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopTable.Controls.Add(this.Test, 0, 0);
            this.TopTable.Controls.Add(this.Get, 1, 0);
            this.TopTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopTable.Location = new System.Drawing.Point(5, 5);
            this.TopTable.Margin = new System.Windows.Forms.Padding(0);
            this.TopTable.Name = "TopTable";
            this.TopTable.RowCount = 1;
            this.TopTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopTable.Size = new System.Drawing.Size(456, 34);
            this.TopTable.TabIndex = 8;
            // 
            // BottomTable
            // 
            this.BottomTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.BottomTable.ColumnCount = 2;
            this.BottomTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.BottomTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.BottomTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.BottomTable.Controls.Add(this.DISABLE, 0, 0);
            this.BottomTable.Controls.Add(this.INSTALL, 0, 0);
            this.BottomTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomTable.Location = new System.Drawing.Point(5, 204);
            this.BottomTable.Margin = new System.Windows.Forms.Padding(0);
            this.BottomTable.Name = "BottomTable";
            this.BottomTable.RowCount = 1;
            this.BottomTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.BottomTable.Size = new System.Drawing.Size(456, 37);
            this.BottomTable.TabIndex = 10;
            // 
            // DISABLE
            // 
            this.DISABLE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DISABLE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DISABLE.ForeColor = System.Drawing.Color.Red;
            this.DISABLE.Location = new System.Drawing.Point(118, 4);
            this.DISABLE.Name = "DISABLE";
            this.DISABLE.Size = new System.Drawing.Size(334, 29);
            this.DISABLE.TabIndex = 11;
            this.DISABLE.Text = "(DSE) Driver Sign Enforcement: DISABLE";
            this.DISABLE.UseVisualStyleBackColor = true;
            // 
            // INSTALL
            // 
            this.INSTALL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.INSTALL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.INSTALL.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.INSTALL.Location = new System.Drawing.Point(4, 4);
            this.INSTALL.Name = "INSTALL";
            this.INSTALL.Size = new System.Drawing.Size(107, 29);
            this.INSTALL.TabIndex = 10;
            this.INSTALL.Text = "INSTALL";
            this.INSTALL.UseVisualStyleBackColor = true;
            // 
            // CenterPanel
            // 
            this.CenterPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CenterPanel.Controls.Add(this.DSE_INFO);
            this.CenterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CenterPanel.Location = new System.Drawing.Point(5, 39);
            this.CenterPanel.Name = "CenterPanel";
            this.CenterPanel.Size = new System.Drawing.Size(456, 165);
            this.CenterPanel.TabIndex = 11;
            // 
            // DSE_INFO
            // 
            this.DSE_INFO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(16)))), ((int)(((byte)(4)))));
            this.DSE_INFO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DSE_INFO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DSE_INFO.ForeColor = System.Drawing.Color.Gainsboro;
            this.DSE_INFO.Location = new System.Drawing.Point(0, 0);
            this.DSE_INFO.Name = "DSE_INFO";
            this.DSE_INFO.ReadOnly = true;
            this.DSE_INFO.Size = new System.Drawing.Size(454, 163);
            this.DSE_INFO.TabIndex = 1;
            this.DSE_INFO.Text = "";
            this.DSE_INFO.WordWrap = false;
            // 
            // BF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(466, 246);
            this.Controls.Add(this.CenterPanel);
            this.Controls.Add(this.BottomTable);
            this.Controls.Add(this.TopTable);
            this.Font = new System.Drawing.Font("Leelawadee UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "BF";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "      NHA EFI UI";
            this.TopTable.ResumeLayout(false);
            this.BottomTable.ResumeLayout(false);
            this.CenterPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Test;
        private System.Windows.Forms.Button Get;
        private System.Windows.Forms.TableLayoutPanel TopTable;
        private System.Windows.Forms.TableLayoutPanel BottomTable;
        private System.Windows.Forms.Panel CenterPanel;
        private System.Windows.Forms.RichTextBox DSE_INFO;
        private System.Windows.Forms.Button INSTALL;
        private System.Windows.Forms.Button DISABLE;
    }
}

