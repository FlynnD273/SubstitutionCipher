using System;

namespace SubstitutionCipher
{
    partial class CryptoForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.KeyLabel = new System.Windows.Forms.Label();
            this.KeyBox = new System.Windows.Forms.TextBox();
            this.CopyButton = new System.Windows.Forms.Button();
            this.SourceFileBox = new System.Windows.Forms.TextBox();
            this.SourceFileLabel = new System.Windows.Forms.Label();
            this.SelectFilebutton = new System.Windows.Forms.Button();
            this.EncodeButton = new System.Windows.Forms.Button();
            this.DecodeButton = new System.Windows.Forms.Button();
            this.CrackButton = new System.Windows.Forms.Button();
            this.RandomKeyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // KeyLabel
            // 
            this.KeyLabel.AutoSize = true;
            this.KeyLabel.Location = new System.Drawing.Point(12, 9);
            this.KeyLabel.Name = "KeyLabel";
            this.KeyLabel.Size = new System.Drawing.Size(61, 25);
            this.KeyLabel.TabIndex = 0;
            this.KeyLabel.Text = "Offset";
            // 
            // KeyBox
            // 
            this.KeyBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.KeyBox.Location = new System.Drawing.Point(12, 37);
            this.KeyBox.Name = "KeyBox";
            this.KeyBox.Size = new System.Drawing.Size(386, 31);
            this.KeyBox.TabIndex = 0;
            this.KeyBox.TextChanged += new System.EventHandler(this.KeyBox_TextChanged);
            // 
            // CopyButton
            // 
            this.CopyButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyButton.Location = new System.Drawing.Point(403, 37);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(113, 34);
            this.CopyButton.TabIndex = 1;
            this.CopyButton.Text = "Copy Key";
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // SourceFileBox
            // 
            this.SourceFileBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SourceFileBox.Location = new System.Drawing.Point(12, 99);
            this.SourceFileBox.Name = "SourceFileBox";
            this.SourceFileBox.Size = new System.Drawing.Size(621, 31);
            this.SourceFileBox.TabIndex = 3;
            this.SourceFileBox.TextChanged += new System.EventHandler(this.SourceFileBox_TextChanged);
            this.SourceFileBox.Validated += new System.EventHandler(this.SourceFileBox_TextChanged);
            // 
            // SourceFileLabel
            // 
            this.SourceFileLabel.AutoSize = true;
            this.SourceFileLabel.Location = new System.Drawing.Point(12, 71);
            this.SourceFileLabel.Name = "SourceFileLabel";
            this.SourceFileLabel.Size = new System.Drawing.Size(97, 25);
            this.SourceFileLabel.TabIndex = 0;
            this.SourceFileLabel.Text = "Source File";
            // 
            // SelectFilebutton
            // 
            this.SelectFilebutton.Location = new System.Drawing.Point(12, 136);
            this.SelectFilebutton.Name = "SelectFilebutton";
            this.SelectFilebutton.Size = new System.Drawing.Size(112, 34);
            this.SelectFilebutton.TabIndex = 4;
            this.SelectFilebutton.Text = "Select File";
            this.SelectFilebutton.UseVisualStyleBackColor = true;
            this.SelectFilebutton.Click += new System.EventHandler(this.SelectFilebutton_Click);
            // 
            // EncodeButton
            // 
            this.EncodeButton.Location = new System.Drawing.Point(12, 200);
            this.EncodeButton.Name = "EncodeButton";
            this.EncodeButton.Size = new System.Drawing.Size(112, 34);
            this.EncodeButton.TabIndex = 5;
            this.EncodeButton.Text = "Encode File";
            this.EncodeButton.UseVisualStyleBackColor = true;
            this.EncodeButton.Click += new System.EventHandler(this.EncodeButton_Click);
            // 
            // DecodeButton
            // 
            this.DecodeButton.Location = new System.Drawing.Point(130, 200);
            this.DecodeButton.Name = "DecodeButton";
            this.DecodeButton.Size = new System.Drawing.Size(112, 34);
            this.DecodeButton.TabIndex = 6;
            this.DecodeButton.Text = "Decode File";
            this.DecodeButton.UseVisualStyleBackColor = true;
            this.DecodeButton.Click += new System.EventHandler(this.DecodeButton_Click);
            // 
            // CrackButton
            // 
            this.CrackButton.Location = new System.Drawing.Point(248, 200);
            this.CrackButton.Name = "CrackButton";
            this.CrackButton.Size = new System.Drawing.Size(149, 34);
            this.CrackButton.TabIndex = 7;
            this.CrackButton.Text = "Crack Cipher";
            this.CrackButton.UseVisualStyleBackColor = true;
            this.CrackButton.Click += new System.EventHandler(this.CrackButton_Click);
            // 
            // RandomKeyButton
            // 
            this.RandomKeyButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RandomKeyButton.Location = new System.Drawing.Point(522, 37);
            this.RandomKeyButton.Name = "RandomKeyButton";
            this.RandomKeyButton.Size = new System.Drawing.Size(113, 34);
            this.RandomKeyButton.TabIndex = 2;
            this.RandomKeyButton.Text = "Random";
            this.RandomKeyButton.UseVisualStyleBackColor = true;
            this.RandomKeyButton.Click += new System.EventHandler(this.RandomKeyButton_Click);
            // 
            // CryptoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 246);
            this.Controls.Add(this.RandomKeyButton);
            this.Controls.Add(this.CrackButton);
            this.Controls.Add(this.DecodeButton);
            this.Controls.Add(this.EncodeButton);
            this.Controls.Add(this.SelectFilebutton);
            this.Controls.Add(this.SourceFileLabel);
            this.Controls.Add(this.SourceFileBox);
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.KeyBox);
            this.Controls.Add(this.KeyLabel);
            this.Name = "CryptoForm";
            this.Text = "Cipher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox KeyBox;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.Label KeyLabel;
        private System.Windows.Forms.TextBox SourceFileBox;
        private System.Windows.Forms.Label SourceFileLabel;
        private System.Windows.Forms.Button SelectFilebutton;
        private System.Windows.Forms.Button EncodeButton;
        private System.Windows.Forms.Button DecodeButton;
        private System.Windows.Forms.Button CrackButton;
        private System.Windows.Forms.Button RandomKeyButton;
    }
}

