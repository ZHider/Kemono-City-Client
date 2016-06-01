namespace Kemono_City_Client
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox_Show = new System.Windows.Forms.PictureBox();
            this.button_Load = new System.Windows.Forms.Button();
            this.textBox_Introduce = new System.Windows.Forms.TextBox();
            this.numericUpDown_Page = new System.Windows.Forms.NumericUpDown();
            this.listView_Marks = new System.Windows.Forms.ListView();
            this.contextMenuStrip_listView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.加载此MarkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除MarksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.刷新最大集数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除缓存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重置数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_Mark = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Show)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Page)).BeginInit();
            this.contextMenuStrip_listView.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_Show
            // 
            this.pictureBox_Show.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_Show.Location = new System.Drawing.Point(12, 12);
            this.pictureBox_Show.Name = "pictureBox_Show";
            this.pictureBox_Show.Size = new System.Drawing.Size(581, 445);
            this.pictureBox_Show.TabIndex = 0;
            this.pictureBox_Show.TabStop = false;
            // 
            // button_Load
            // 
            this.button_Load.Location = new System.Drawing.Point(660, 39);
            this.button_Load.Name = "button_Load";
            this.button_Load.Size = new System.Drawing.Size(85, 27);
            this.button_Load.TabIndex = 1;
            this.button_Load.Text = "加载";
            this.button_Load.UseVisualStyleBackColor = true;
            this.button_Load.Click += new System.EventHandler(this.button_Load_Click);
            // 
            // textBox_Introduce
            // 
            this.textBox_Introduce.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Introduce.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_Introduce.Location = new System.Drawing.Point(12, 463);
            this.textBox_Introduce.Multiline = true;
            this.textBox_Introduce.Name = "textBox_Introduce";
            this.textBox_Introduce.ReadOnly = true;
            this.textBox_Introduce.Size = new System.Drawing.Size(581, 55);
            this.textBox_Introduce.TabIndex = 3;
            this.textBox_Introduce.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numericUpDown_Page
            // 
            this.numericUpDown_Page.Location = new System.Drawing.Point(599, 12);
            this.numericUpDown_Page.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Page.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Page.Name = "numericUpDown_Page";
            this.numericUpDown_Page.Size = new System.Drawing.Size(146, 21);
            this.numericUpDown_Page.TabIndex = 6;
            this.numericUpDown_Page.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown_Page.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // listView_Marks
            // 
            this.listView_Marks.ContextMenuStrip = this.contextMenuStrip_listView;
            this.listView_Marks.FullRowSelect = true;
            this.listView_Marks.Location = new System.Drawing.Point(599, 72);
            this.listView_Marks.Name = "listView_Marks";
            this.listView_Marks.Size = new System.Drawing.Size(146, 446);
            this.listView_Marks.TabIndex = 7;
            this.listView_Marks.UseCompatibleStateImageBehavior = false;
            this.listView_Marks.View = System.Windows.Forms.View.List;
            this.listView_Marks.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_Marks_MouseDoubleClick);
            // 
            // contextMenuStrip_listView
            // 
            this.contextMenuStrip_listView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加载此MarkToolStripMenuItem,
            this.删除MarksToolStripMenuItem,
            this.toolStripMenuItem1,
            this.刷新最大集数ToolStripMenuItem,
            this.清除缓存ToolStripMenuItem,
            this.重置数据ToolStripMenuItem});
            this.contextMenuStrip_listView.Name = "contextMenuStrip_listView";
            this.contextMenuStrip_listView.Size = new System.Drawing.Size(149, 120);
            // 
            // 加载此MarkToolStripMenuItem
            // 
            this.加载此MarkToolStripMenuItem.Name = "加载此MarkToolStripMenuItem";
            this.加载此MarkToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.加载此MarkToolStripMenuItem.Text = "加载此Mark";
            this.加载此MarkToolStripMenuItem.Click += new System.EventHandler(this.加载此MarkToolStripMenuItem_Click);
            // 
            // 删除MarksToolStripMenuItem
            // 
            this.删除MarksToolStripMenuItem.Name = "删除MarksToolStripMenuItem";
            this.删除MarksToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.删除MarksToolStripMenuItem.Text = "删除Marks";
            this.删除MarksToolStripMenuItem.Click += new System.EventHandler(this.删除MarksToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(145, 6);
            // 
            // 刷新最大集数ToolStripMenuItem
            // 
            this.刷新最大集数ToolStripMenuItem.Name = "刷新最大集数ToolStripMenuItem";
            this.刷新最大集数ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.刷新最大集数ToolStripMenuItem.Text = "刷新最大集数";
            this.刷新最大集数ToolStripMenuItem.Click += new System.EventHandler(this.刷新最大集数ToolStripMenuItem_Click);
            // 
            // 清除缓存ToolStripMenuItem
            // 
            this.清除缓存ToolStripMenuItem.Name = "清除缓存ToolStripMenuItem";
            this.清除缓存ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.清除缓存ToolStripMenuItem.Text = "清除缓存";
            this.清除缓存ToolStripMenuItem.Click += new System.EventHandler(this.清除缓存ToolStripMenuItem_Click);
            // 
            // 重置数据ToolStripMenuItem
            // 
            this.重置数据ToolStripMenuItem.Name = "重置数据ToolStripMenuItem";
            this.重置数据ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.重置数据ToolStripMenuItem.Text = "重置数据";
            this.重置数据ToolStripMenuItem.Click += new System.EventHandler(this.重置数据ToolStripMenuItem_Click);
            // 
            // button_Mark
            // 
            this.button_Mark.Location = new System.Drawing.Point(599, 39);
            this.button_Mark.Name = "button_Mark";
            this.button_Mark.Size = new System.Drawing.Size(55, 27);
            this.button_Mark.TabIndex = 9;
            this.button_Mark.Text = "标记";
            this.button_Mark.UseVisualStyleBackColor = true;
            this.button_Mark.Click += new System.EventHandler(this.button_Mark_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 530);
            this.Controls.Add(this.button_Mark);
            this.Controls.Add(this.listView_Marks);
            this.Controls.Add(this.numericUpDown_Page);
            this.Controls.Add(this.textBox_Introduce);
            this.Controls.Add(this.button_Load);
            this.Controls.Add(this.pictureBox_Show);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Kemono City Client";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Show)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Page)).EndInit();
            this.contextMenuStrip_listView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Show;
        private System.Windows.Forms.Button button_Load;
        private System.Windows.Forms.TextBox textBox_Introduce;
        private System.Windows.Forms.NumericUpDown numericUpDown_Page;
        private System.Windows.Forms.ListView listView_Marks;
        private System.Windows.Forms.Button button_Mark;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_listView;
        private System.Windows.Forms.ToolStripMenuItem 加载此MarkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除MarksToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 刷新最大集数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除缓存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重置数据ToolStripMenuItem;
    }
}

