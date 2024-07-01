namespace ChrisQuestionHelper
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.Subject = new System.Windows.Forms.ComboBox();
            this.YearLevel = new System.Windows.Forms.ComboBox();
            this.textBoxQuestions = new System.Windows.Forms.TextBox();
            this.Edit = new System.Windows.Forms.Button();
            this.comboBoxNumQuestions = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 233);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(242, 56);
            this.button1.TabIndex = 0;
            this.button1.Text = "Generate Questions";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Subject
            // 
            this.Subject.FormattingEnabled = true;
            this.Subject.Location = new System.Drawing.Point(7, 127);
            this.Subject.Name = "Subject";
            this.Subject.Size = new System.Drawing.Size(242, 21);
            this.Subject.TabIndex = 1;
            this.Subject.Text = "Subject?";
            // 
            // YearLevel
            // 
            this.YearLevel.FormattingEnabled = true;
            this.YearLevel.Location = new System.Drawing.Point(7, 72);
            this.YearLevel.Name = "YearLevel";
            this.YearLevel.Size = new System.Drawing.Size(242, 21);
            this.YearLevel.TabIndex = 2;
            this.YearLevel.Text = "Year Level?";
            // 
            // textBoxQuestions
            // 
            this.textBoxQuestions.Location = new System.Drawing.Point(255, 72);
            this.textBoxQuestions.Multiline = true;
            this.textBoxQuestions.Name = "textBoxQuestions";
            this.textBoxQuestions.Size = new System.Drawing.Size(630, 692);
            this.textBoxQuestions.TabIndex = 3;
            // 
            // Edit
            // 
            this.Edit.Location = new System.Drawing.Point(7, 325);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(242, 56);
            this.Edit.TabIndex = 4;
            this.Edit.Text = "Edit Questions";
            this.Edit.UseVisualStyleBackColor = true;
            this.Edit.Click += new System.EventHandler(this.Edit_Click_1);
            // 
            // comboBoxNumQuestions
            // 
            this.comboBoxNumQuestions.FormattingEnabled = true;
            this.comboBoxNumQuestions.Location = new System.Drawing.Point(7, 185);
            this.comboBoxNumQuestions.Name = "comboBoxNumQuestions";
            this.comboBoxNumQuestions.Size = new System.Drawing.Size(242, 21);
            this.comboBoxNumQuestions.TabIndex = 5;
            this.comboBoxNumQuestions.Text = "Number of questions?";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(107, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(704, 39);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 776);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.comboBoxNumQuestions);
            this.Controls.Add(this.Edit);
            this.Controls.Add(this.textBoxQuestions);
            this.Controls.Add(this.YearLevel);
            this.Controls.Add(this.Subject);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Christoffer\'s Questions Generator";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox Subject;
        private System.Windows.Forms.ComboBox YearLevel;
        private System.Windows.Forms.TextBox textBoxQuestions;
        private System.Windows.Forms.Button Edit;
        private System.Windows.Forms.ComboBox comboBoxNumQuestions;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

