namespace ImageProcessingLabs
{
    partial class HoughForm
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
            this.FindPointButton = new System.Windows.Forms.Button();
            this.OutputPictureBoxMatches = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.InputSample_pictureBox = new System.Windows.Forms.PictureBox();
            this.InputFull_PictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OutputPictureBoxVotes = new System.Windows.Forms.PictureBox();
            this.OutputPictureBoxResult = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.OutputPictureBoxMatches)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InputSample_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InputFull_PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OutputPictureBoxVotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OutputPictureBoxResult)).BeginInit();
            this.SuspendLayout();
            // 
            // FindPointButton
            // 
            this.FindPointButton.Location = new System.Drawing.Point(710, 606);
            this.FindPointButton.Name = "FindPointButton";
            this.FindPointButton.Size = new System.Drawing.Size(187, 51);
            this.FindPointButton.TabIndex = 1;
            this.FindPointButton.Text = "Поиск";
            this.FindPointButton.UseVisualStyleBackColor = true;
            this.FindPointButton.Click += new System.EventHandler(this.FindPointButton_Click);
            // 
            // OutputPictureBoxMatches
            // 
            this.OutputPictureBoxMatches.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.OutputPictureBoxMatches.Location = new System.Drawing.Point(12, 12);
            this.OutputPictureBoxMatches.Name = "OutputPictureBoxMatches";
            this.OutputPictureBoxMatches.Size = new System.Drawing.Size(391, 354);
            this.OutputPictureBoxMatches.TabIndex = 25;
            this.OutputPictureBoxMatches.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.InputFull_PictureBox);
            this.groupBox1.Controls.Add(this.InputSample_pictureBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 372);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(680, 285);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Входные данные";
            // 
            // InputSample_pictureBox
            // 
            this.InputSample_pictureBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.InputSample_pictureBox.Location = new System.Drawing.Point(6, 19);
            this.InputSample_pictureBox.Name = "InputSample_pictureBox";
            this.InputSample_pictureBox.Size = new System.Drawing.Size(274, 238);
            this.InputSample_pictureBox.TabIndex = 0;
            this.InputSample_pictureBox.TabStop = false;
            // 
            // InputFull_PictureBox
            // 
            this.InputFull_PictureBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.InputFull_PictureBox.Location = new System.Drawing.Point(297, 19);
            this.InputFull_PictureBox.Name = "InputFull_PictureBox";
            this.InputFull_PictureBox.Size = new System.Drawing.Size(367, 238);
            this.InputFull_PictureBox.TabIndex = 0;
            this.InputFull_PictureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Эталонное изображение";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(413, 260);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Полное изображение";
            // 
            // OutputPictureBoxVotes
            // 
            this.OutputPictureBoxVotes.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.OutputPictureBoxVotes.Location = new System.Drawing.Point(410, 12);
            this.OutputPictureBoxVotes.Name = "OutputPictureBoxVotes";
            this.OutputPictureBoxVotes.Size = new System.Drawing.Size(391, 354);
            this.OutputPictureBoxVotes.TabIndex = 25;
            this.OutputPictureBoxVotes.TabStop = false;
            // 
            // OutputPictureBoxResult
            // 
            this.OutputPictureBoxResult.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.OutputPictureBoxResult.Location = new System.Drawing.Point(807, 12);
            this.OutputPictureBoxResult.Name = "OutputPictureBoxResult";
            this.OutputPictureBoxResult.Size = new System.Drawing.Size(391, 354);
            this.OutputPictureBoxResult.TabIndex = 25;
            this.OutputPictureBoxResult.TabStop = false;
            // 
            // HoughForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 669);
            this.Controls.Add(this.FindPointButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.OutputPictureBoxResult);
            this.Controls.Add(this.OutputPictureBoxVotes);
            this.Controls.Add(this.OutputPictureBoxMatches);
            this.Name = "HoughForm";
            this.Text = "HoughForm";
            ((System.ComponentModel.ISupportInitialize)(this.OutputPictureBoxMatches)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InputSample_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InputFull_PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OutputPictureBoxVotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OutputPictureBoxResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button FindPointButton;
        private System.Windows.Forms.PictureBox OutputPictureBoxMatches;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox InputFull_PictureBox;
        private System.Windows.Forms.PictureBox InputSample_pictureBox;
        private System.Windows.Forms.PictureBox OutputPictureBoxVotes;
        private System.Windows.Forms.PictureBox OutputPictureBoxResult;
    }
}