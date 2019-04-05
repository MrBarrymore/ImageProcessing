namespace ImageProcessingLabs
{
    partial class GaussForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RB_WrapImage = new System.Windows.Forms.RadioButton();
            this.RB_EdgeReflection = new System.Windows.Forms.RadioButton();
            this.RB_EdgeCoppy = new System.Windows.Forms.RadioButton();
            this.RB_Zero = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lbl_LevelsInOctava = new System.Windows.Forms.Label();
            this.lbl_CountOctavas = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.lbl_EffectiveSigmaValue = new System.Windows.Forms.Label();
            this.lbl_SigmaValue = new System.Windows.Forms.Label();
            this.checkBoxRealSize = new System.Windows.Forms.CheckBox();
            this.btn_showImage = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox1.Location = new System.Drawing.Point(12, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(553, 389);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox2.Location = new System.Drawing.Point(593, 11);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(553, 389);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RB_WrapImage);
            this.groupBox2.Controls.Add(this.RB_EdgeReflection);
            this.groupBox2.Controls.Add(this.RB_EdgeCoppy);
            this.groupBox2.Controls.Add(this.RB_Zero);
            this.groupBox2.Location = new System.Drawing.Point(278, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 122);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Способ  обработки краевых эффектов";
            // 
            // RB_WrapImage
            // 
            this.RB_WrapImage.AutoSize = true;
            this.RB_WrapImage.Location = new System.Drawing.Point(6, 88);
            this.RB_WrapImage.Name = "RB_WrapImage";
            this.RB_WrapImage.Size = new System.Drawing.Size(177, 17);
            this.RB_WrapImage.TabIndex = 8;
            this.RB_WrapImage.TabStop = true;
            this.RB_WrapImage.Text = "\"Заварачивать\" изображение";
            this.RB_WrapImage.UseVisualStyleBackColor = true;
            // 
            // RB_EdgeReflection
            // 
            this.RB_EdgeReflection.AutoSize = true;
            this.RB_EdgeReflection.Checked = true;
            this.RB_EdgeReflection.Location = new System.Drawing.Point(6, 68);
            this.RB_EdgeReflection.Name = "RB_EdgeReflection";
            this.RB_EdgeReflection.Size = new System.Drawing.Size(181, 17);
            this.RB_EdgeReflection.TabIndex = 8;
            this.RB_EdgeReflection.TabStop = true;
            this.RB_EdgeReflection.Text = "Отражать изображение у края";
            this.RB_EdgeReflection.UseVisualStyleBackColor = true;
            // 
            // RB_EdgeCoppy
            // 
            this.RB_EdgeCoppy.AutoSize = true;
            this.RB_EdgeCoppy.Location = new System.Drawing.Point(6, 45);
            this.RB_EdgeCoppy.Name = "RB_EdgeCoppy";
            this.RB_EdgeCoppy.Size = new System.Drawing.Size(180, 17);
            this.RB_EdgeCoppy.TabIndex = 8;
            this.RB_EdgeCoppy.TabStop = true;
            this.RB_EdgeCoppy.Text = "Значения с края изображения";
            this.RB_EdgeCoppy.UseVisualStyleBackColor = true;
            // 
            // RB_Zero
            // 
            this.RB_Zero.AutoSize = true;
            this.RB_Zero.Location = new System.Drawing.Point(6, 22);
            this.RB_Zero.Name = "RB_Zero";
            this.RB_Zero.Size = new System.Drawing.Size(78, 17);
            this.RB_Zero.TabIndex = 8;
            this.RB_Zero.TabStop = true;
            this.RB_Zero.Text = "Снаружи 0";
            this.RB_Zero.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 192);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(142, 37);
            this.button2.TabIndex = 1;
            this.button2.Text = "Построить пирамиду";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.comboBox2);
            this.groupBox3.Location = new System.Drawing.Point(12, 406);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(553, 240);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Построение пирамид";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lbl_LevelsInOctava);
            this.groupBox6.Controls.Add(this.lbl_CountOctavas);
            this.groupBox6.Location = new System.Drawing.Point(278, 159);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(269, 75);
            this.groupBox6.TabIndex = 15;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Выходные данные";
            // 
            // lbl_LevelsInOctava
            // 
            this.lbl_LevelsInOctava.AutoSize = true;
            this.lbl_LevelsInOctava.Location = new System.Drawing.Point(7, 44);
            this.lbl_LevelsInOctava.Name = "lbl_LevelsInOctava";
            this.lbl_LevelsInOctava.Size = new System.Drawing.Size(101, 13);
            this.lbl_LevelsInOctava.TabIndex = 0;
            this.lbl_LevelsInOctava.Text = "Уровней в октаве:";
            // 
            // lbl_CountOctavas
            // 
            this.lbl_CountOctavas.AutoSize = true;
            this.lbl_CountOctavas.Location = new System.Drawing.Point(6, 21);
            this.lbl_CountOctavas.Name = "lbl_CountOctavas";
            this.lbl_CountOctavas.Size = new System.Drawing.Size(76, 13);
            this.lbl_CountOctavas.TabIndex = 0;
            this.lbl_CountOctavas.Text = "Кол-во октав:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Число уровней в октаве";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.textBox2);
            this.groupBox4.Controls.Add(this.textBox3);
            this.groupBox4.Location = new System.Drawing.Point(6, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(182, 89);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Значения сигмы";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Сигма A";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Сигма 0";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(65, 18);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 13;
            this.textBox2.Text = "0,5";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(65, 45);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 13;
            this.textBox3.Text = "1,6";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.comboBox2.Location = new System.Drawing.Point(11, 130);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(136, 21);
            this.comboBox2.TabIndex = 12;
            this.comboBox2.Text = "3";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox7);
            this.groupBox5.Controls.Add(this.checkBoxRealSize);
            this.groupBox5.Controls.Add(this.btn_showImage);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.comboBox3);
            this.groupBox5.Controls.Add(this.comboBox4);
            this.groupBox5.Location = new System.Drawing.Point(593, 406);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(553, 240);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Демонстрация изображений";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.lbl_EffectiveSigmaValue);
            this.groupBox7.Controls.Add(this.lbl_SigmaValue);
            this.groupBox7.Location = new System.Drawing.Point(9, 126);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(276, 68);
            this.groupBox7.TabIndex = 15;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Выходные данные";
            // 
            // lbl_EffectiveSigmaValue
            // 
            this.lbl_EffectiveSigmaValue.AutoSize = true;
            this.lbl_EffectiveSigmaValue.Location = new System.Drawing.Point(11, 43);
            this.lbl_EffectiveSigmaValue.Name = "lbl_EffectiveSigmaValue";
            this.lbl_EffectiveSigmaValue.Size = new System.Drawing.Size(166, 13);
            this.lbl_EffectiveSigmaValue.TabIndex = 15;
            this.lbl_EffectiveSigmaValue.Text = "Эффективное значение сигмы:";
            // 
            // lbl_SigmaValue
            // 
            this.lbl_SigmaValue.AutoSize = true;
            this.lbl_SigmaValue.Location = new System.Drawing.Point(9, 21);
            this.lbl_SigmaValue.Name = "lbl_SigmaValue";
            this.lbl_SigmaValue.Size = new System.Drawing.Size(97, 13);
            this.lbl_SigmaValue.TabIndex = 15;
            this.lbl_SigmaValue.Text = "Значение сигмы: ";
            // 
            // checkBoxRealSize
            // 
            this.checkBoxRealSize.AutoSize = true;
            this.checkBoxRealSize.Location = new System.Drawing.Point(10, 105);
            this.checkBoxRealSize.Name = "checkBoxRealSize";
            this.checkBoxRealSize.Size = new System.Drawing.Size(189, 17);
            this.checkBoxRealSize.TabIndex = 16;
            this.checkBoxRealSize.Text = "Реальный размер изображения";
            this.checkBoxRealSize.UseVisualStyleBackColor = true;
            // 
            // btn_showImage
            // 
            this.btn_showImage.Location = new System.Drawing.Point(9, 197);
            this.btn_showImage.Name = "btn_showImage";
            this.btn_showImage.Size = new System.Drawing.Size(142, 37);
            this.btn_showImage.TabIndex = 1;
            this.btn_showImage.Text = "Показать изображение";
            this.btn_showImage.UseVisualStyleBackColor = true;
            this.btn_showImage.Click += new System.EventHandler(this.btn_showImage_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Номер уровня в октаве";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Номер октавы";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(10, 34);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(136, 21);
            this.comboBox3.TabIndex = 12;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.ShowNewImage);
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(10, 78);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(136, 21);
            this.comboBox4.TabIndex = 12;
            this.comboBox4.SelectedIndexChanged += new System.EventHandler(this.ShowNewImage);
            // 
            // GaussForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 650);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "GaussForm";
            this.Text = "GaussForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton RB_WrapImage;
        private System.Windows.Forms.RadioButton RB_EdgeReflection;
        private System.Windows.Forms.RadioButton RB_EdgeCoppy;
        private System.Windows.Forms.RadioButton RB_Zero;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lbl_SigmaValue;
        private System.Windows.Forms.Button btn_showImage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lbl_LevelsInOctava;
        private System.Windows.Forms.Label lbl_CountOctavas;
        private System.Windows.Forms.CheckBox checkBoxRealSize;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label lbl_EffectiveSigmaValue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox2;
    }
}