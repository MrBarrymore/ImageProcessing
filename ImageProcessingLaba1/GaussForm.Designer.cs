namespace ImageProcessingLaba1
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RB_WrapImage = new System.Windows.Forms.RadioButton();
            this.RB_EdgeReflection = new System.Windows.Forms.RadioButton();
            this.RB_EdgeCoppy = new System.Windows.Forms.RadioButton();
            this.RB_Zero = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(489, 379);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox2.Location = new System.Drawing.Point(524, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(489, 379);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 473);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 37);
            this.button1.TabIndex = 1;
            this.button1.Text = "Посчитать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(97, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "3";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 397);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 54);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Значение сигмы:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RB_WrapImage);
            this.groupBox2.Controls.Add(this.RB_EdgeReflection);
            this.groupBox2.Controls.Add(this.RB_EdgeCoppy);
            this.groupBox2.Controls.Add(this.RB_Zero);
            this.groupBox2.Location = new System.Drawing.Point(245, 397);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(264, 113);
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
            // GaussForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 522);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "GaussForm";
            this.Text = "GaussForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton RB_WrapImage;
        private System.Windows.Forms.RadioButton RB_EdgeReflection;
        private System.Windows.Forms.RadioButton RB_EdgeCoppy;
        private System.Windows.Forms.RadioButton RB_Zero;
    }
}