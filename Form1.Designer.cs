namespace ZhuJiangDataMigration
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
            this.Year = new System.Windows.Forms.Label();
            this.Month = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbStartYear = new System.Windows.Forms.ComboBox();
            this.cmbStartMonth = new System.Windows.Forms.ComboBox();
            this.cmbDataSource = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_mapperExclePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_SelectMapper = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbEndYear = new System.Windows.Forms.ComboBox();
            this.cmbEndMonth = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Year
            // 
            this.Year.AutoSize = true;
            this.Year.Location = new System.Drawing.Point(204, 52);
            this.Year.Name = "Year";
            this.Year.Size = new System.Drawing.Size(22, 15);
            this.Year.TabIndex = 0;
            this.Year.Text = "年";
            // 
            // Month
            // 
            this.Month.AutoSize = true;
            this.Month.Location = new System.Drawing.Point(290, 52);
            this.Month.Name = "Month";
            this.Month.Size = new System.Drawing.Size(22, 15);
            this.Month.TabIndex = 1;
            this.Month.Text = "月";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(389, 215);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(487, 215);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cmbStartYear
            // 
            this.cmbStartYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStartYear.FormattingEnabled = true;
            this.cmbStartYear.Items.AddRange(new object[] {
            "2020"});
            this.cmbStartYear.Location = new System.Drawing.Point(119, 51);
            this.cmbStartYear.Name = "cmbStartYear";
            this.cmbStartYear.Size = new System.Drawing.Size(79, 23);
            this.cmbStartYear.TabIndex = 4;
            // 
            // cmbStartMonth
            // 
            this.cmbStartMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStartMonth.FormattingEnabled = true;
            this.cmbStartMonth.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cmbStartMonth.Location = new System.Drawing.Point(226, 50);
            this.cmbStartMonth.Name = "cmbStartMonth";
            this.cmbStartMonth.Size = new System.Drawing.Size(65, 23);
            this.cmbStartMonth.TabIndex = 5;
            // 
            // cmbDataSource
            // 
            this.cmbDataSource.DisplayMember = "辅助核算发放表,员工工资发放表";
            this.cmbDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataSource.FormattingEnabled = true;
            this.cmbDataSource.Items.AddRange(new object[] {
            "辅助核算发放表",
            "员工工资发放表"});
            this.cmbDataSource.Location = new System.Drawing.Point(121, 91);
            this.cmbDataSource.Name = "cmbDataSource";
            this.cmbDataSource.Size = new System.Drawing.Size(441, 23);
            this.cmbDataSource.TabIndex = 6;
            this.cmbDataSource.ValueMember = "1,2";
            this.cmbDataSource.SelectedIndexChanged += new System.EventHandler(this.cmbDataSource_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "源单据";
            // 
            // txt_mapperExclePath
            // 
            this.txt_mapperExclePath.Enabled = false;
            this.txt_mapperExclePath.Location = new System.Drawing.Point(119, 150);
            this.txt_mapperExclePath.Name = "txt_mapperExclePath";
            this.txt_mapperExclePath.Size = new System.Drawing.Size(364, 25);
            this.txt_mapperExclePath.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "薪酬项目对照表";
            // 
            // btn_SelectMapper
            // 
            this.btn_SelectMapper.Location = new System.Drawing.Point(487, 150);
            this.btn_SelectMapper.Name = "btn_SelectMapper";
            this.btn_SelectMapper.Size = new System.Drawing.Size(75, 23);
            this.btn_SelectMapper.TabIndex = 10;
            this.btn_SelectMapper.Text = "选择";
            this.btn_SelectMapper.UseVisualStyleBackColor = true;
            this.btn_SelectMapper.Click += new System.EventHandler(this.btn_SelectMapper_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(430, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "年";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(539, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "月";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "时间";
            // 
            // cmbEndYear
            // 
            this.cmbEndYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEndYear.FormattingEnabled = true;
            this.cmbEndYear.Items.AddRange(new object[] {
            "2020"});
            this.cmbEndYear.Location = new System.Drawing.Point(355, 49);
            this.cmbEndYear.Name = "cmbEndYear";
            this.cmbEndYear.Size = new System.Drawing.Size(66, 23);
            this.cmbEndYear.TabIndex = 14;
            // 
            // cmbEndMonth
            // 
            this.cmbEndMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEndMonth.FormattingEnabled = true;
            this.cmbEndMonth.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cmbEndMonth.Location = new System.Drawing.Point(464, 49);
            this.cmbEndMonth.Name = "cmbEndMonth";
            this.cmbEndMonth.Size = new System.Drawing.Size(65, 23);
            this.cmbEndMonth.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(324, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 15);
            this.label6.TabIndex = 16;
            this.label6.Text = "至";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 280);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbEndMonth);
            this.Controls.Add(this.cmbEndYear);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_SelectMapper);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_mapperExclePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbDataSource);
            this.Controls.Add(this.cmbStartMonth);
            this.Controls.Add(this.cmbStartYear);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.Month);
            this.Controls.Add(this.Year);
            this.Name = "Form1";
            this.Text = "珠江医院 数据迁移";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Year;
        private System.Windows.Forms.Label Month;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbStartYear;
        private System.Windows.Forms.ComboBox cmbStartMonth;
        private System.Windows.Forms.ComboBox cmbDataSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_mapperExclePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_SelectMapper;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbEndYear;
        private System.Windows.Forms.ComboBox cmbEndMonth;
        private System.Windows.Forms.Label label6;
    }
}

