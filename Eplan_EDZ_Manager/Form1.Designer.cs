namespace Eplan_EDZ_Manager
{
    partial class Form1
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.Btn_SourceFile = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Btn_Listele = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.gBox_export = new System.Windows.Forms.GroupBox();
            this.Lbl_DownloadInfo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.Lbl_Find = new System.Windows.Forms.Label();
            this.Btn_EdzExport = new System.Windows.Forms.Button();
            this.Lbl_Type = new System.Windows.Forms.Label();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.Lbl_Marka = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.gBox_import = new System.Windows.Forms.GroupBox();
            this.Lbl_SBoyut = new System.Windows.Forms.Label();
            this.Lbl_ToplamBoyut = new System.Windows.Forms.Label();
            this.comboBoxLanguages = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Language = new System.Windows.Forms.Label();
            this.gBox_export.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gBox_import.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(8, 24);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(341, 465);
            this.treeView1.TabIndex = 0;
            // 
            // Btn_SourceFile
            // 
            this.Btn_SourceFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Btn_SourceFile.Location = new System.Drawing.Point(355, 24);
            this.Btn_SourceFile.Name = "Btn_SourceFile";
            this.Btn_SourceFile.Size = new System.Drawing.Size(150, 29);
            this.Btn_SourceFile.TabIndex = 1;
            this.Btn_SourceFile.Text = "İçe aktar (.edz)";
            this.Btn_SourceFile.UseVisualStyleBackColor = true;
            this.Btn_SourceFile.Click += new System.EventHandler(this.Btn_SourceFile_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(3, 496);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(609, 21);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 3;
            // 
            // Btn_Listele
            // 
            this.Btn_Listele.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Btn_Listele.Location = new System.Drawing.Point(68, 92);
            this.Btn_Listele.Name = "Btn_Listele";
            this.Btn_Listele.Size = new System.Drawing.Size(153, 31);
            this.Btn_Listele.TabIndex = 1;
            this.Btn_Listele.Text = "Listele";
            this.Btn_Listele.UseVisualStyleBackColor = true;
            this.Btn_Listele.Click += new System.EventHandler(this.Btn_Listele_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox1.Location = new System.Drawing.Point(68, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(362, 26);
            this.textBox1.TabIndex = 6;
            // 
            // gBox_export
            // 
            this.gBox_export.BackColor = System.Drawing.Color.LightGray;
            this.gBox_export.Controls.Add(this.Lbl_DownloadInfo);
            this.gBox_export.Controls.Add(this.pictureBox1);
            this.gBox_export.Controls.Add(this.progressBar2);
            this.gBox_export.Controls.Add(this.Lbl_Find);
            this.gBox_export.Controls.Add(this.Btn_EdzExport);
            this.gBox_export.Controls.Add(this.Btn_Listele);
            this.gBox_export.Controls.Add(this.Lbl_Type);
            this.gBox_export.Controls.Add(this.treeView2);
            this.gBox_export.Controls.Add(this.Lbl_Marka);
            this.gBox_export.Controls.Add(this.textBox2);
            this.gBox_export.Controls.Add(this.textBox1);
            this.gBox_export.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gBox_export.Location = new System.Drawing.Point(625, 4);
            this.gBox_export.Name = "gBox_export";
            this.gBox_export.Size = new System.Drawing.Size(594, 520);
            this.gBox_export.TabIndex = 7;
            this.gBox_export.TabStop = false;
            this.gBox_export.Text = "Dışa Aktar";
            // 
            // Lbl_DownloadInfo
            // 
            this.Lbl_DownloadInfo.BackColor = System.Drawing.Color.Lime;
            this.Lbl_DownloadInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_DownloadInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold);
            this.Lbl_DownloadInfo.Location = new System.Drawing.Point(147, 150);
            this.Lbl_DownloadInfo.Name = "Lbl_DownloadInfo";
            this.Lbl_DownloadInfo.Size = new System.Drawing.Size(300, 36);
            this.Lbl_DownloadInfo.TabIndex = 8;
            this.Lbl_DownloadInfo.Text = "Dosya Aktarılıyor";
            this.Lbl_DownloadInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lbl_DownloadInfo.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Eplan_EDZ_Manager.Properties.Resources.loading_symbol;
            this.pictureBox1.Location = new System.Drawing.Point(147, 189);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 300);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // progressBar2
            // 
            this.progressBar2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar2.Location = new System.Drawing.Point(3, 496);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(588, 21);
            this.progressBar2.Step = 1;
            this.progressBar2.TabIndex = 6;
            // 
            // Lbl_Find
            // 
            this.Lbl_Find.AutoSize = true;
            this.Lbl_Find.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Find.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Lbl_Find.Location = new System.Drawing.Point(6, 129);
            this.Lbl_Find.Name = "Lbl_Find";
            this.Lbl_Find.Size = new System.Drawing.Size(128, 18);
            this.Lbl_Find.TabIndex = 7;
            this.Lbl_Find.Text = "0 Dosya bulundu.";
            // 
            // Btn_EdzExport
            // 
            this.Btn_EdzExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Btn_EdzExport.Location = new System.Drawing.Point(443, 102);
            this.Btn_EdzExport.Name = "Btn_EdzExport";
            this.Btn_EdzExport.Size = new System.Drawing.Size(145, 43);
            this.Btn_EdzExport.TabIndex = 6;
            this.Btn_EdzExport.Text = "Dışa aktar (.edz)";
            this.Btn_EdzExport.UseVisualStyleBackColor = true;
            this.Btn_EdzExport.Click += new System.EventHandler(this.Btn_EdzExport_Click);
            // 
            // Lbl_Type
            // 
            this.Lbl_Type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Type.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Lbl_Type.Location = new System.Drawing.Point(6, 64);
            this.Lbl_Type.Name = "Lbl_Type";
            this.Lbl_Type.Size = new System.Drawing.Size(56, 18);
            this.Lbl_Type.TabIndex = 4;
            this.Lbl_Type.Text = "Kod:";
            this.Lbl_Type.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // treeView2
            // 
            this.treeView2.Location = new System.Drawing.Point(6, 150);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(582, 339);
            this.treeView2.TabIndex = 0;
            this.treeView2.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView2_NodeMouseClick);
            // 
            // Lbl_Marka
            // 
            this.Lbl_Marka.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Marka.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Lbl_Marka.Location = new System.Drawing.Point(6, 28);
            this.Lbl_Marka.Name = "Lbl_Marka";
            this.Lbl_Marka.Size = new System.Drawing.Size(56, 18);
            this.Lbl_Marka.TabIndex = 4;
            this.Lbl_Marka.Text = "Marka:";
            this.Lbl_Marka.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox2.Location = new System.Drawing.Point(68, 60);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(362, 26);
            this.textBox2.TabIndex = 6;
            // 
            // gBox_import
            // 
            this.gBox_import.BackColor = System.Drawing.Color.LightGray;
            this.gBox_import.Controls.Add(this.Lbl_SBoyut);
            this.gBox_import.Controls.Add(this.Lbl_ToplamBoyut);
            this.gBox_import.Controls.Add(this.treeView1);
            this.gBox_import.Controls.Add(this.Btn_SourceFile);
            this.gBox_import.Controls.Add(this.progressBar1);
            this.gBox_import.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gBox_import.Location = new System.Drawing.Point(4, 4);
            this.gBox_import.Name = "gBox_import";
            this.gBox_import.Size = new System.Drawing.Size(615, 520);
            this.gBox_import.TabIndex = 7;
            this.gBox_import.TabStop = false;
            this.gBox_import.Text = "İçe Aktar";
            // 
            // Lbl_SBoyut
            // 
            this.Lbl_SBoyut.AutoSize = true;
            this.Lbl_SBoyut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_SBoyut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Lbl_SBoyut.Location = new System.Drawing.Point(350, 471);
            this.Lbl_SBoyut.Name = "Lbl_SBoyut";
            this.Lbl_SBoyut.Size = new System.Drawing.Size(186, 18);
            this.Lbl_SBoyut.TabIndex = 5;
            this.Lbl_SBoyut.Text = "Sıkıştırılmış Boyut: 0 Byte ";
            // 
            // Lbl_ToplamBoyut
            // 
            this.Lbl_ToplamBoyut.AutoSize = true;
            this.Lbl_ToplamBoyut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_ToplamBoyut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Lbl_ToplamBoyut.Location = new System.Drawing.Point(350, 441);
            this.Lbl_ToplamBoyut.Name = "Lbl_ToplamBoyut";
            this.Lbl_ToplamBoyut.Size = new System.Drawing.Size(160, 18);
            this.Lbl_ToplamBoyut.TabIndex = 4;
            this.Lbl_ToplamBoyut.Text = "Toplam Boyut:  0 Byte";
            // 
            // comboBoxLanguages
            // 
            this.comboBoxLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLanguages.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.comboBoxLanguages.FormattingEnabled = true;
            this.comboBoxLanguages.Items.AddRange(new object[] {
            "EN",
            "TR"});
            this.comboBoxLanguages.Location = new System.Drawing.Point(86, 527);
            this.comboBoxLanguages.Name = "comboBoxLanguages";
            this.comboBoxLanguages.Size = new System.Drawing.Size(121, 20);
            this.comboBoxLanguages.TabIndex = 6;
            this.comboBoxLanguages.SelectedIndexChanged += new System.EventHandler(this.comboBoxLanguages_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 527);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1225, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Language
            // 
            this.Language.AutoSize = true;
            this.Language.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Language.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Language.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Language.Location = new System.Drawing.Point(3, 528);
            this.Language.Name = "Language";
            this.Language.Size = new System.Drawing.Size(78, 18);
            this.Language.TabIndex = 6;
            this.Language.Text = "Language";
            this.Language.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(1225, 549);
            this.Controls.Add(this.Language);
            this.Controls.Add(this.comboBoxLanguages);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gBox_export);
            this.Controls.Add(this.gBox_import);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Eplan_EDZ_Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gBox_export.ResumeLayout(false);
            this.gBox_export.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gBox_import.ResumeLayout(false);
            this.gBox_import.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button Btn_SourceFile;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button Btn_Listele;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox gBox_export;
        private System.Windows.Forms.GroupBox gBox_import;
        private System.Windows.Forms.Label Lbl_ToplamBoyut;
        private System.Windows.Forms.Label Lbl_SBoyut;
        private System.Windows.Forms.Label Lbl_Type;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.Label Lbl_Marka;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button Btn_EdzExport;
        private System.Windows.Forms.Label Lbl_Find;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label Lbl_DownloadInfo;
        private System.Windows.Forms.ComboBox comboBoxLanguages;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label Language;
    }
}

