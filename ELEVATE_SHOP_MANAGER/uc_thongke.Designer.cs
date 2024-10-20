namespace ELEVATE_SHOP_MANAGER
{
    partial class uc_thongke
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.inPhiếuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tạoPhiếuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtbatdau = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtketthuc = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.làmMớiTrangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Silver;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inPhiếuToolStripMenuItem,
            this.tạoPhiếuToolStripMenuItem,
            this.làmMớiTrangToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1633, 31);
            this.menuStrip1.TabIndex = 44;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // inPhiếuToolStripMenuItem
            // 
            this.inPhiếuToolStripMenuItem.Image = global::ELEVATE_SHOP_MANAGER.Properties.Resources.graph_5128126;
            this.inPhiếuToolStripMenuItem.Name = "inPhiếuToolStripMenuItem";
            this.inPhiếuToolStripMenuItem.Size = new System.Drawing.Size(190, 27);
            this.inPhiếuToolStripMenuItem.Text = "Báo cáo doanh thu";
            this.inPhiếuToolStripMenuItem.Click += new System.EventHandler(this.inPhiếuToolStripMenuItem_Click);
            // 
            // tạoPhiếuToolStripMenuItem
            // 
            this.tạoPhiếuToolStripMenuItem.Image = global::ELEVATE_SHOP_MANAGER.Properties.Resources.clipboard_8571046;
            this.tạoPhiếuToolStripMenuItem.Name = "tạoPhiếuToolStripMenuItem";
            this.tạoPhiếuToolStripMenuItem.Size = new System.Drawing.Size(189, 27);
            this.tạoPhiếuToolStripMenuItem.Text = "Danh sách tồn kho";
            this.tạoPhiếuToolStripMenuItem.Click += new System.EventHandler(this.tạoPhiếuToolStripMenuItem_Click);
            // 
            // txtbatdau
            // 
            this.txtbatdau.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtbatdau.Location = new System.Drawing.Point(156, 40);
            this.txtbatdau.Name = "txtbatdau";
            this.txtbatdau.Size = new System.Drawing.Size(140, 27);
            this.txtbatdau.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 47;
            this.label1.Text = "Từ ngày   :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtketthuc);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtbatdau);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(36, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 216);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Khoảng thời gian thống kê doanh thu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 20);
            this.label2.TabIndex = 48;
            this.label2.Text = "Đến ngày :";
            // 
            // txtketthuc
            // 
            this.txtketthuc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtketthuc.Location = new System.Drawing.Point(156, 95);
            this.txtketthuc.Name = "txtketthuc";
            this.txtketthuc.Size = new System.Drawing.Size(140, 27);
            this.txtketthuc.TabIndex = 49;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Teal;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(58, 145);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(193, 50);
            this.button1.TabIndex = 50;
            this.button1.Text = "Xuất báo cáo";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ELEVATE_SHOP_MANAGER.baocaodoanhthu.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(455, 34);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(794, 680);
            this.reportViewer1.TabIndex = 49;
            // 
            // làmMớiTrangToolStripMenuItem
            // 
            this.làmMớiTrangToolStripMenuItem.Image = global::ELEVATE_SHOP_MANAGER.Properties.Resources.refresh_10027438__1_;
            this.làmMớiTrangToolStripMenuItem.Name = "làmMớiTrangToolStripMenuItem";
            this.làmMớiTrangToolStripMenuItem.Size = new System.Drawing.Size(156, 27);
            this.làmMớiTrangToolStripMenuItem.Text = "Làm mới trang";
            this.làmMớiTrangToolStripMenuItem.Click += new System.EventHandler(this.làmMớiTrangToolStripMenuItem_Click_1);
            // 
            // uc_thongke
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "uc_thongke";
            this.Size = new System.Drawing.Size(1633, 952);
            this.Load += new System.EventHandler(this.uc_thongke_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem inPhiếuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tạoPhiếuToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker txtbatdau;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker txtketthuc;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.ToolStripMenuItem làmMớiTrangToolStripMenuItem;
    }
}
