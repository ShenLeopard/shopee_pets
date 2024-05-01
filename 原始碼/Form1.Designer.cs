namespace 蝦皮總動員
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.captureButton = new System.Windows.Forms.Button();
            this.excuteButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureALabel = new System.Windows.Forms.Label();
            this.pictureBLabel = new System.Windows.Forms.Label();
            this.captureSwitchLabel = new System.Windows.Forms.Label();
            this.pictureBoxA = new System.Windows.Forms.PictureBox();
            this.pictureBoxB = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxB)).BeginInit();
            this.SuspendLayout();
            // 
            // captureButton
            // 
            this.captureButton.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.captureButton.Location = new System.Drawing.Point(23, 28);
            this.captureButton.Margin = new System.Windows.Forms.Padding(4);
            this.captureButton.Name = "captureButton";
            this.captureButton.Size = new System.Drawing.Size(134, 49);
            this.captureButton.TabIndex = 0;
            this.captureButton.Text = "擷取畫面";
            this.captureButton.UseVisualStyleBackColor = true;
            this.captureButton.Click += new System.EventHandler(this.CaptureButton_Click);
            // 
            // excuteButton
            // 
            this.excuteButton.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.excuteButton.Location = new System.Drawing.Point(203, 28);
            this.excuteButton.Margin = new System.Windows.Forms.Padding(4);
            this.excuteButton.Name = "excuteButton";
            this.excuteButton.Size = new System.Drawing.Size(134, 49);
            this.excuteButton.TabIndex = 0;
            this.excuteButton.Text = "顯示差異圖片";
            this.excuteButton.UseVisualStyleBackColor = true;
            this.excuteButton.Click += new System.EventHandler(this.Excute_Button_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 29);
            this.label1.TabIndex = 8;
            // 
            // pictureALabel
            // 
            this.pictureALabel.AutoSize = true;
            this.pictureALabel.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pictureALabel.Location = new System.Drawing.Point(30, 126);
            this.pictureALabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.pictureALabel.Name = "pictureALabel";
            this.pictureALabel.Size = new System.Drawing.Size(69, 27);
            this.pictureALabel.TabIndex = 4;
            this.pictureALabel.Text = "圖片A";
            // 
            // pictureBLabel
            // 
            this.pictureBLabel.AutoSize = true;
            this.pictureBLabel.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pictureBLabel.Location = new System.Drawing.Point(346, 126);
            this.pictureBLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.pictureBLabel.Name = "pictureBLabel";
            this.pictureBLabel.Size = new System.Drawing.Size(67, 27);
            this.pictureBLabel.TabIndex = 4;
            this.pictureBLabel.Text = "圖片B";
            // 
            // captureSwitchLabel
            // 
            this.captureSwitchLabel.Location = new System.Drawing.Point(0, 0);
            this.captureSwitchLabel.Name = "captureSwitchLabel";
            this.captureSwitchLabel.Size = new System.Drawing.Size(88, 23);
            this.captureSwitchLabel.TabIndex = 2;
            // 
            // pictureBoxA
            // 
            this.pictureBoxA.Location = new System.Drawing.Point(35, 222);
            this.pictureBoxA.Name = "pictureBoxA";
            this.pictureBoxA.Size = new System.Drawing.Size(220, 197);
            this.pictureBoxA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxA.TabIndex = 0;
            this.pictureBoxA.TabStop = false;
            // 
            // pictureBoxB
            // 
            this.pictureBoxB.Location = new System.Drawing.Point(352, 222);
            this.pictureBoxB.Name = "pictureBoxB";
            this.pictureBoxB.Size = new System.Drawing.Size(219, 197);
            this.pictureBoxB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxB.TabIndex = 1;
            this.pictureBoxB.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.LightBlue;
            this.comboBox1.Font = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.comboBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "找碴",
            "金頭腦"});
            this.comboBox1.SelectedIndex = 0; // 預設選擇第一個選項
            this.comboBox1.Location = new System.Drawing.Point(414, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(150, 25);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 521);
            this.Controls.Add(this.pictureBoxA);
            this.Controls.Add(this.pictureBoxB);
            this.Controls.Add(this.captureSwitchLabel);
            this.Controls.Add(this.pictureALabel);
            this.Controls.Add(this.pictureBLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.captureButton);
            this.Controls.Add(this.excuteButton);
            this.Controls.Add(this.comboBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "蝦皮總動員";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button captureButton;
        private System.Windows.Forms.Button excuteButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label pictureALabel;
        private System.Windows.Forms.Label pictureBLabel;
        private System.Windows.Forms.Label captureSwitchLabel;
        private System.Windows.Forms.PictureBox pictureBoxA;
        private System.Windows.Forms.PictureBox pictureBoxB;
        private System.Windows.Forms.ComboBox comboBox1;
    }

}
