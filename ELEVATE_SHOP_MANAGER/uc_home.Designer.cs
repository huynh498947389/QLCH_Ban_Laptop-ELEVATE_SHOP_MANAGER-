namespace ELEVATE_SHOP_MANAGER
{
    partial class uc_home
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
            this.gridviewsp = new System.Windows.Forms.DataGridView();
            this.txttimkiem = new System.Windows.Forms.TextBox();
            this.bttimkiem = new System.Windows.Forms.Button();
            this.lbtensp = new System.Windows.Forms.Label();
            this.lbmasp = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbgiaban = new System.Windows.Forms.Label();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbhang = new System.Windows.Forms.Label();
            this.lbsoluong = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbtinhtrang = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewsp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridviewsp
            // 
            this.gridviewsp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridviewsp.Location = new System.Drawing.Point(738, 62);
            this.gridviewsp.Name = "gridviewsp";
            this.gridviewsp.RowHeadersWidth = 51;
            this.gridviewsp.RowTemplate.Height = 24;
            this.gridviewsp.Size = new System.Drawing.Size(865, 650);
            this.gridviewsp.TabIndex = 0;
            this.gridviewsp.SelectionChanged += new System.EventHandler(this.gridviewsp_SelectionChanged);
            // 
            // txttimkiem
            // 
            this.txttimkiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttimkiem.Location = new System.Drawing.Point(581, 6);
            this.txttimkiem.Name = "txttimkiem";
            this.txttimkiem.Size = new System.Drawing.Size(461, 27);
            this.txttimkiem.TabIndex = 1;
            this.txttimkiem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bttimkiem
            // 
            this.bttimkiem.BackColor = System.Drawing.Color.DarkGray;
            this.bttimkiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttimkiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bttimkiem.Location = new System.Drawing.Point(1048, 6);
            this.bttimkiem.Name = "bttimkiem";
            this.bttimkiem.Size = new System.Drawing.Size(137, 27);
            this.bttimkiem.TabIndex = 2;
            this.bttimkiem.Text = "Tìm kiếm ";
            this.bttimkiem.UseVisualStyleBackColor = false;
            this.bttimkiem.Click += new System.EventHandler(this.bttimkiem_Click);
            // 
            // lbtensp
            // 
            this.lbtensp.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtensp.ForeColor = System.Drawing.Color.DimGray;
            this.lbtensp.Location = new System.Drawing.Point(37, 62);
            this.lbtensp.Name = "lbtensp";
            this.lbtensp.Size = new System.Drawing.Size(302, 75);
            this.lbtensp.TabIndex = 4;
            this.lbtensp.Text = "Tên sản phẩm";
            // 
            // lbmasp
            // 
            this.lbmasp.AutoSize = true;
            this.lbmasp.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbmasp.ForeColor = System.Drawing.Color.DimGray;
            this.lbmasp.Location = new System.Drawing.Point(145, 147);
            this.lbmasp.Name = "lbmasp";
            this.lbmasp.Size = new System.Drawing.Size(92, 16);
            this.lbmasp.TabIndex = 5;
            this.lbmasp.Text = "Mã sản phẩm";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(26, 285);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "Giá Bán (VNĐ) :";
            // 
            // lbgiaban
            // 
            this.lbgiaban.AutoSize = true;
            this.lbgiaban.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbgiaban.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbgiaban.Location = new System.Drawing.Point(182, 285);
            this.lbgiaban.Name = "lbgiaban";
            this.lbgiaban.Size = new System.Drawing.Size(21, 24);
            this.lbgiaban.TabIndex = 7;
            this.lbgiaban.Text = "0";
            this.lbgiaban.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // richTextBox
            // 
            this.richTextBox.Enabled = false;
            this.richTextBox.Location = new System.Drawing.Point(25, 388);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(693, 324);
            this.richTextBox.TabIndex = 8;
            this.richTextBox.Text = "";
            this.richTextBox.TextChanged += new System.EventHandler(this.richTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(36, 207);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = " Hãng :  ";
            // 
            // lbhang
            // 
            this.lbhang.AutoSize = true;
            this.lbhang.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbhang.ForeColor = System.Drawing.Color.DimGray;
            this.lbhang.Location = new System.Drawing.Point(139, 207);
            this.lbhang.Name = "lbhang";
            this.lbhang.Size = new System.Drawing.Size(15, 16);
            this.lbhang.TabIndex = 10;
            this.lbhang.Text = "?";
            // 
            // lbsoluong
            // 
            this.lbsoluong.AutoSize = true;
            this.lbsoluong.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbsoluong.ForeColor = System.Drawing.Color.DimGray;
            this.lbsoluong.Location = new System.Drawing.Point(143, 177);
            this.lbsoluong.Name = "lbsoluong";
            this.lbsoluong.Size = new System.Drawing.Size(15, 16);
            this.lbsoluong.TabIndex = 12;
            this.lbsoluong.Text = "?";
            this.lbsoluong.Click += new System.EventHandler(this.lbsoluong_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(39, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Số lượng  :  ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(39, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Mã sản phẩm :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(36, 237);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "Tình trạng :  ";
            // 
            // lbtinhtrang
            // 
            this.lbtinhtrang.AutoSize = true;
            this.lbtinhtrang.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtinhtrang.ForeColor = System.Drawing.Color.DimGray;
            this.lbtinhtrang.Location = new System.Drawing.Point(139, 237);
            this.lbtinhtrang.Name = "lbtinhtrang";
            this.lbtinhtrang.Size = new System.Drawing.Size(15, 16);
            this.lbtinhtrang.TabIndex = 15;
            this.lbtinhtrang.Text = "?";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(27, 369);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "Cấu hình sản phẩm  :  ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(371, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(188, 27);
            this.label7.TabIndex = 17;
            this.label7.Text = "TÊN SẢN PHẨM";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(493, 293);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 16);
            this.label10.TabIndex = 46;
            this.label10.Text = "Ảnh sản phẩm";
            // 
            // button1
            // 
            this.button1.Image = global::ELEVATE_SHOP_MANAGER.Properties.Resources.refresh_10027438__1_;
            this.button1.Location = new System.Drawing.Point(1566, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(37, 38);
            this.button1.TabIndex = 47;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(356, 86);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(362, 204);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // uc_home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbtinhtrang);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbsoluong);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbhang);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.lbgiaban);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbmasp);
            this.Controls.Add(this.lbtensp);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.bttimkiem);
            this.Controls.Add(this.txttimkiem);
            this.Controls.Add(this.gridviewsp);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "uc_home";
            this.Size = new System.Drawing.Size(1626, 903);
            this.Load += new System.EventHandler(this.uc_home_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridviewsp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridviewsp;
        private System.Windows.Forms.TextBox txttimkiem;
        private System.Windows.Forms.Button bttimkiem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbtensp;
        private System.Windows.Forms.Label lbmasp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbgiaban;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbhang;
        private System.Windows.Forms.Label lbsoluong;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbtinhtrang;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button1;
    }
}
