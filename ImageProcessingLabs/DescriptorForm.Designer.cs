namespace ImageProcessingLabs
{
    partial class DescriptorForm
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lbl_showPoints2 = new System.Windows.Forms.Label();
            this.lbl_findPoints2 = new System.Windows.Forms.Label();
            this.lbl_showPoints1 = new System.Windows.Forms.Label();
            this.lbl_findPoints1 = new System.Windows.Forms.Label();
            this.FindPointButton = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.filter_checkBox = new System.Windows.Forms.CheckBox();
            this.txb_Filter = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txb_minValue = new System.Windows.Forms.TextBox();
            this.txb_WindowSize = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Controls.Add(this.FindPointButton);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Location = new System.Drawing.Point(7, 416);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(724, 239);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Построение пирамид";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lbl_showPoints2);
            this.groupBox6.Controls.Add(this.lbl_findPoints2);
            this.groupBox6.Controls.Add(this.lbl_showPoints1);
            this.groupBox6.Controls.Add(this.lbl_findPoints1);
            this.groupBox6.Location = new System.Drawing.Point(372, 97);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(343, 133);
            this.groupBox6.TabIndex = 23;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Выходные данные";
            // 
            // lbl_showPoints2
            // 
            this.lbl_showPoints2.AutoSize = true;
            this.lbl_showPoints2.Location = new System.Drawing.Point(8, 91);
            this.lbl_showPoints2.Name = "lbl_showPoints2";
            this.lbl_showPoints2.Size = new System.Drawing.Size(191, 13);
            this.lbl_showPoints2.TabIndex = 18;
            this.lbl_showPoints2.Text = "Отображенно интересных точек (2): ";
            // 
            // lbl_findPoints2
            // 
            this.lbl_findPoints2.AutoSize = true;
            this.lbl_findPoints2.Location = new System.Drawing.Point(8, 68);
            this.lbl_findPoints2.Name = "lbl_findPoints2";
            this.lbl_findPoints2.Size = new System.Drawing.Size(169, 13);
            this.lbl_findPoints2.TabIndex = 18;
            this.lbl_findPoints2.Text = "Найдено интересных точек (2):  ";
            // 
            // lbl_showPoints1
            // 
            this.lbl_showPoints1.AutoSize = true;
            this.lbl_showPoints1.Location = new System.Drawing.Point(7, 45);
            this.lbl_showPoints1.Name = "lbl_showPoints1";
            this.lbl_showPoints1.Size = new System.Drawing.Size(191, 13);
            this.lbl_showPoints1.TabIndex = 18;
            this.lbl_showPoints1.Text = "Отображенно интересных точек (1): ";
            // 
            // lbl_findPoints1
            // 
            this.lbl_findPoints1.AutoSize = true;
            this.lbl_findPoints1.Location = new System.Drawing.Point(7, 22);
            this.lbl_findPoints1.Name = "lbl_findPoints1";
            this.lbl_findPoints1.Size = new System.Drawing.Size(169, 13);
            this.lbl_findPoints1.TabIndex = 18;
            this.lbl_findPoints1.Text = "Найдено интересных точек (1):  ";
            // 
            // FindPointButton
            // 
            this.FindPointButton.Location = new System.Drawing.Point(6, 181);
            this.FindPointButton.Name = "FindPointButton";
            this.FindPointButton.Size = new System.Drawing.Size(217, 51);
            this.FindPointButton.TabIndex = 1;
            this.FindPointButton.Text = "Найти интересные точки";
            this.FindPointButton.UseVisualStyleBackColor = true;
            this.FindPointButton.Click += new System.EventHandler(this.FindPointButton_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.filter_checkBox);
            this.groupBox5.Controls.Add(this.txb_Filter);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Location = new System.Drawing.Point(372, 24);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(333, 67);
            this.groupBox5.TabIndex = 22;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Non-Maximum Suppression";
            // 
            // filter_checkBox
            // 
            this.filter_checkBox.AutoSize = true;
            this.filter_checkBox.Location = new System.Drawing.Point(11, 45);
            this.filter_checkBox.Name = "filter_checkBox";
            this.filter_checkBox.Size = new System.Drawing.Size(128, 17);
            this.filter_checkBox.TabIndex = 15;
            this.filter_checkBox.Text = "Подключить фильтр";
            this.filter_checkBox.UseVisualStyleBackColor = true;
            // 
            // txb_Filter
            // 
            this.txb_Filter.Location = new System.Drawing.Point(228, 20);
            this.txb_Filter.Name = "txb_Filter";
            this.txb_Filter.Size = new System.Drawing.Size(100, 20);
            this.txb_Filter.TabIndex = 13;
            this.txb_Filter.Text = "250";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(217, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Максимальное кол-во интересных точек:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txb_minValue);
            this.groupBox2.Controls.Add(this.txb_WindowSize);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(6, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(345, 67);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Входные данные (Харрис)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Размер окна (пиксели):";
            // 
            // txb_minValue
            // 
            this.txb_minValue.Location = new System.Drawing.Point(223, 19);
            this.txb_minValue.Name = "txb_minValue";
            this.txb_minValue.Size = new System.Drawing.Size(100, 20);
            this.txb_minValue.TabIndex = 13;
            this.txb_minValue.Text = "0,01";
            // 
            // txb_WindowSize
            // 
            this.txb_WindowSize.Location = new System.Drawing.Point(223, 45);
            this.txb_WindowSize.Name = "txb_WindowSize";
            this.txb_WindowSize.Size = new System.Drawing.Size(100, 20);
            this.txb_WindowSize.TabIndex = 13;
            this.txb_WindowSize.Text = "3";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Пороговое значение:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox1.Location = new System.Drawing.Point(13, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1143, 385);
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // DescriptorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 662);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pictureBox1);
            this.Name = "DescriptorForm";
            this.Text = "DescriptorForm";
            this.groupBox3.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lbl_showPoints2;
        private System.Windows.Forms.Label lbl_findPoints2;
        private System.Windows.Forms.Label lbl_showPoints1;
        private System.Windows.Forms.Label lbl_findPoints1;
        private System.Windows.Forms.Button FindPointButton;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox filter_checkBox;
        private System.Windows.Forms.TextBox txb_Filter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txb_minValue;
        private System.Windows.Forms.TextBox txb_WindowSize;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}