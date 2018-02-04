namespace FastDownload
{
    partial class Main_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_form));
            this.lv_state = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pbox_new = new System.Windows.Forms.PictureBox();
            this.pbox_start = new System.Windows.Forms.PictureBox();
            this.pbox_pause = new System.Windows.Forms.PictureBox();
            this.pbox_delete = new System.Windows.Forms.PictureBox();
            this.pbox_continue = new System.Windows.Forms.PictureBox();
            this.pbox_set = new System.Windows.Forms.PictureBox();
            this.pbox_close = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_new)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_start)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_pause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_delete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_continue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_set)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_close)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lv_state
            // 
            this.lv_state.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lv_state.Location = new System.Drawing.Point(3, 88);
            this.lv_state.Name = "lv_state";
            this.lv_state.Size = new System.Drawing.Size(865, 400);
            this.lv_state.TabIndex = 0;
            this.lv_state.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "文件名";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "文件大小";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "下载进度";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "下载完成量";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "已用时间";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "文件类型";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "创建时间";
            // 
            // pbox_new
            // 
            this.pbox_new.Image = ((System.Drawing.Image)(resources.GetObject("pbox_new.Image")));
            this.pbox_new.Location = new System.Drawing.Point(3, 32);
            this.pbox_new.Name = "pbox_new";
            this.pbox_new.Size = new System.Drawing.Size(45, 50);
            this.pbox_new.TabIndex = 1;
            this.pbox_new.TabStop = false;
            // 
            // pbox_start
            // 
            this.pbox_start.Image = ((System.Drawing.Image)(resources.GetObject("pbox_start.Image")));
            this.pbox_start.Location = new System.Drawing.Point(54, 32);
            this.pbox_start.Name = "pbox_start";
            this.pbox_start.Size = new System.Drawing.Size(45, 50);
            this.pbox_start.TabIndex = 1;
            this.pbox_start.TabStop = false;
            // 
            // pbox_pause
            // 
            this.pbox_pause.Image = ((System.Drawing.Image)(resources.GetObject("pbox_pause.Image")));
            this.pbox_pause.Location = new System.Drawing.Point(105, 32);
            this.pbox_pause.Name = "pbox_pause";
            this.pbox_pause.Size = new System.Drawing.Size(45, 50);
            this.pbox_pause.TabIndex = 1;
            this.pbox_pause.TabStop = false;
            // 
            // pbox_delete
            // 
            this.pbox_delete.Image = ((System.Drawing.Image)(resources.GetObject("pbox_delete.Image")));
            this.pbox_delete.Location = new System.Drawing.Point(156, 32);
            this.pbox_delete.Name = "pbox_delete";
            this.pbox_delete.Size = new System.Drawing.Size(45, 50);
            this.pbox_delete.TabIndex = 1;
            this.pbox_delete.TabStop = false;
            // 
            // pbox_continue
            // 
            this.pbox_continue.Image = ((System.Drawing.Image)(resources.GetObject("pbox_continue.Image")));
            this.pbox_continue.Location = new System.Drawing.Point(207, 32);
            this.pbox_continue.Name = "pbox_continue";
            this.pbox_continue.Size = new System.Drawing.Size(45, 50);
            this.pbox_continue.TabIndex = 1;
            this.pbox_continue.TabStop = false;
            // 
            // pbox_set
            // 
            this.pbox_set.Image = ((System.Drawing.Image)(resources.GetObject("pbox_set.Image")));
            this.pbox_set.Location = new System.Drawing.Point(258, 32);
            this.pbox_set.Name = "pbox_set";
            this.pbox_set.Size = new System.Drawing.Size(45, 50);
            this.pbox_set.TabIndex = 1;
            this.pbox_set.TabStop = false;
            // 
            // pbox_close
            // 
            this.pbox_close.Image = ((System.Drawing.Image)(resources.GetObject("pbox_close.Image")));
            this.pbox_close.Location = new System.Drawing.Point(848, 0);
            this.pbox_close.Name = "pbox_close";
            this.pbox_close.Size = new System.Drawing.Size(26, 25);
            this.pbox_close.TabIndex = 1;
            this.pbox_close.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 499);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "[0 KB/s]";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 499);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "[0 KB/s]";
            this.label2.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 496);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(79, 496);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // Main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 528);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbox_close);
            this.Controls.Add(this.pbox_set);
            this.Controls.Add(this.pbox_continue);
            this.Controls.Add(this.pbox_delete);
            this.Controls.Add(this.pbox_pause);
            this.Controls.Add(this.pbox_start);
            this.Controls.Add(this.pbox_new);
            this.Controls.Add(this.lv_state);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Main_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FastDownload_Z";
            this.Load += new System.EventHandler(this.Main_form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbox_new)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_start)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_pause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_delete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_continue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_set)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_close)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lv_state;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.PictureBox pbox_new;
        private System.Windows.Forms.PictureBox pbox_start;
        private System.Windows.Forms.PictureBox pbox_pause;
        private System.Windows.Forms.PictureBox pbox_delete;
        private System.Windows.Forms.PictureBox pbox_continue;
        private System.Windows.Forms.PictureBox pbox_set;
        private System.Windows.Forms.PictureBox pbox_close;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}