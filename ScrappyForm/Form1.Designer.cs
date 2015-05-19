namespace ScrappyForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCurrentFolder = new System.Windows.Forms.TextBox();
            this.txtNewFolder = new System.Windows.Forms.TextBox();
            this.txtCourseUrl = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Folder";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "New Folder";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Course Url";
            // 
            // txtCurrentFolder
            // 
            this.txtCurrentFolder.Location = new System.Drawing.Point(93, 13);
            this.txtCurrentFolder.Name = "txtCurrentFolder";
            this.txtCurrentFolder.Size = new System.Drawing.Size(350, 20);
            this.txtCurrentFolder.TabIndex = 3;
            // 
            // txtNewFolder
            // 
            this.txtNewFolder.Location = new System.Drawing.Point(93, 40);
            this.txtNewFolder.Name = "txtNewFolder";
            this.txtNewFolder.Size = new System.Drawing.Size(350, 20);
            this.txtNewFolder.TabIndex = 4;
            // 
            // txtCourseUrl
            // 
            this.txtCourseUrl.Location = new System.Drawing.Point(93, 67);
            this.txtCourseUrl.Name = "txtCourseUrl";
            this.txtCourseUrl.Size = new System.Drawing.Size(350, 20);
            this.txtCourseUrl.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(271, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(171, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Download Metadata and Reload";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 130);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtCourseUrl);
            this.Controls.Add(this.txtNewFolder);
            this.Controls.Add(this.txtCurrentFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Pluralsight Metadata Downloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCurrentFolder;
        private System.Windows.Forms.TextBox txtNewFolder;
        private System.Windows.Forms.TextBox txtCourseUrl;
        private System.Windows.Forms.Button button1;
    }
}

