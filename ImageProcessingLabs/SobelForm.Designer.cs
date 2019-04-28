namespace ImageProcessingLabs
{
    partial class SobelForm
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
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.RB_Sobel = new System.Windows.Forms.RadioButton();
            this.RB_Pruitt = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RB_Shchar = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RB_WrapImage = new System.Windows.Forms.RadioButton();
            this.RB_EdgeReflection = new System.Windows.Forms.RadioButton();
            this.RB_EdgeCoppy = new System.Windows.Forms.RadioButton();
            this.RB_Zero = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(444, 48);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(421, 337);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(413, 337);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 482);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 38);
            this.button1.TabIndex = 7;
            this.button1.Text = "Посчитать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(890, 48);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(422, 337);
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(124, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Производная по X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(574, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Производная по Y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(984, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(227, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Нормализованный градиент";
            // 
            // RB_Sobel
            // 
            this.RB_Sobel.AutoSize = true;
            this.RB_Sobel.Checked = true;
            this.RB_Sobel.Location = new System.Drawing.Point(6, 22);
            this.RB_Sobel.Name = "RB_Sobel";
            this.RB_Sobel.Size = new System.Drawing.Size(62, 17);
            this.RB_Sobel.TabIndex = 8;
            this.RB_Sobel.TabStop = true;
            this.RB_Sobel.Text = "Собель";
            this.RB_Sobel.UseVisualStyleBackColor = true;
            // 
            // RB_Pruitt
            // 
            this.RB_Pruitt.AutoSize = true;
            this.RB_Pruitt.Location = new System.Drawing.Point(6, 45);
            this.RB_Pruitt.Name = "RB_Pruitt";
            this.RB_Pruitt.Size = new System.Drawing.Size(63, 17);
            this.RB_Pruitt.TabIndex = 8;
            this.RB_Pruitt.Text = "Прюитт";
            this.RB_Pruitt.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RB_Shchar);
            this.groupBox1.Controls.Add(this.RB_Pruitt);
            this.groupBox1.Controls.Add(this.RB_Sobel);
            this.groupBox1.Location = new System.Drawing.Point(191, 405);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 113);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выбор способа расчета частных производных";
            // 
            // RB_Shchar
            // 
            this.RB_Shchar.AutoSize = true;
            this.RB_Shchar.Location = new System.Drawing.Point(6, 68);
            this.RB_Shchar.Name = "RB_Shchar";
            this.RB_Shchar.Size = new System.Drawing.Size(53, 17);
            this.RB_Shchar.TabIndex = 8;
            this.RB_Shchar.Text = "Щарр";
            this.RB_Shchar.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RB_WrapImage);
            this.groupBox2.Controls.Add(this.RB_EdgeReflection);
            this.groupBox2.Controls.Add(this.RB_EdgeCoppy);
            this.groupBox2.Controls.Add(this.RB_Zero);
            this.groupBox2.Location = new System.Drawing.Point(490, 405);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(264, 113);
            this.groupBox2.TabIndex = 9;
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
            // SobelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1324, 530);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "SobelForm";
            this.Text = "Преобразование изображения через оператор Собеля";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton RB_Sobel;
        private System.Windows.Forms.RadioButton RB_Pruitt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RB_Shchar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton RB_WrapImage;
        private System.Windows.Forms.RadioButton RB_EdgeReflection;
        private System.Windows.Forms.RadioButton RB_EdgeCoppy;
        private System.Windows.Forms.RadioButton RB_Zero;
    }
}