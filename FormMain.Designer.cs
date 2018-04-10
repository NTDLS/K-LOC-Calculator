namespace K_LOC_Calculator
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.listBoxMasks = new System.Windows.Forms.ListBox();
            this.comboBoxMask = new System.Windows.Forms.ComboBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.labelSourceCodeLocation = new System.Windows.Forms.Label();
            this.textBoxSourceCodeLocation = new System.Windows.Forms.TextBox();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.listViewProgress = new System.Windows.Forms.ListView();
            this.Mask = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Lines = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxMasks
            // 
            this.listBoxMasks.FormattingEnabled = true;
            this.listBoxMasks.Location = new System.Drawing.Point(7, 48);
            this.listBoxMasks.Name = "listBoxMasks";
            this.listBoxMasks.Size = new System.Drawing.Size(161, 160);
            this.listBoxMasks.TabIndex = 0;
            // 
            // comboBoxMask
            // 
            this.comboBoxMask.FormattingEnabled = true;
            this.comboBoxMask.Location = new System.Drawing.Point(6, 21);
            this.comboBoxMask.Name = "comboBoxMask";
            this.comboBoxMask.Size = new System.Drawing.Size(162, 21);
            this.comboBoxMask.TabIndex = 1;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(174, 19);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(174, 48);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 3;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxMask);
            this.groupBox1.Controls.Add(this.buttonRemove);
            this.groupBox1.Controls.Add(this.listBoxMasks);
            this.groupBox1.Controls.Add(this.buttonAdd);
            this.groupBox1.Location = new System.Drawing.Point(15, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 221);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Types";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(327, 23);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(24, 23);
            this.buttonBrowse.TabIndex = 5;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // labelSourceCodeLocation
            // 
            this.labelSourceCodeLocation.AutoSize = true;
            this.labelSourceCodeLocation.Location = new System.Drawing.Point(12, 9);
            this.labelSourceCodeLocation.Name = "labelSourceCodeLocation";
            this.labelSourceCodeLocation.Size = new System.Drawing.Size(113, 13);
            this.labelSourceCodeLocation.TabIndex = 6;
            this.labelSourceCodeLocation.Text = "Source Code Location";
            // 
            // textBoxSourceCodeLocation
            // 
            this.textBoxSourceCodeLocation.Location = new System.Drawing.Point(15, 25);
            this.textBoxSourceCodeLocation.Name = "textBoxSourceCodeLocation";
            this.textBoxSourceCodeLocation.Size = new System.Drawing.Size(306, 20);
            this.textBoxSourceCodeLocation.TabIndex = 7;
            // 
            // buttonCalculate
            // 
            this.buttonCalculate.Location = new System.Drawing.Point(295, 136);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(75, 51);
            this.buttonCalculate.TabIndex = 8;
            this.buttonCalculate.Text = "Calculate";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
            // 
            // listViewProgress
            // 
            this.listViewProgress.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Mask,
            this.Lines});
            this.listViewProgress.FullRowSelect = true;
            this.listViewProgress.Location = new System.Drawing.Point(15, 278);
            this.listViewProgress.Name = "listViewProgress";
            this.listViewProgress.Size = new System.Drawing.Size(371, 263);
            this.listViewProgress.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewProgress.TabIndex = 9;
            this.listViewProgress.UseCompatibleStateImageBehavior = false;
            this.listViewProgress.View = System.Windows.Forms.View.Details;
            // 
            // Mask
            // 
            this.Mask.Text = "Mask";
            this.Mask.Width = 218;
            // 
            // Lines
            // 
            this.Lines.Text = "Lines";
            this.Lines.Width = 131;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 555);
            this.Controls.Add(this.listViewProgress);
            this.Controls.Add(this.buttonCalculate);
            this.Controls.Add(this.textBoxSourceCodeLocation);
            this.Controls.Add(this.labelSourceCodeLocation);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "K-LOC Calculator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxMasks;
        private System.Windows.Forms.ComboBox comboBoxMask;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Label labelSourceCodeLocation;
        private System.Windows.Forms.TextBox textBoxSourceCodeLocation;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.ListView listViewProgress;
        private System.Windows.Forms.ColumnHeader Mask;
        private System.Windows.Forms.ColumnHeader Lines;
    }
}

