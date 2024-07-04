
namespace CreationDate
{
    partial class FormWelcome
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
            this.mNUDHours = new System.Windows.Forms.NumericUpDown();
            this.mButtonOK = new System.Windows.Forms.Button();
            this.mButtonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mNUDHours)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(143, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Change Hours:";
            // 
            // mNUDHours
            // 
            this.mNUDHours.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.mNUDHours.Location = new System.Drawing.Point(221, 38);
            this.mNUDHours.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.mNUDHours.Minimum = new decimal(new int[] {
            12,
            0,
            0,
            -2147483648});
            this.mNUDHours.Name = "mNUDHours";
            this.mNUDHours.Size = new System.Drawing.Size(61, 20);
            this.mNUDHours.TabIndex = 1;
            // 
            // mButtonOK
            // 
            this.mButtonOK.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.mButtonOK.Location = new System.Drawing.Point(140, 80);
            this.mButtonOK.Name = "mButtonOK";
            this.mButtonOK.Size = new System.Drawing.Size(75, 23);
            this.mButtonOK.TabIndex = 2;
            this.mButtonOK.Text = "OK";
            this.mButtonOK.UseVisualStyleBackColor = true;
            this.mButtonOK.Click += new System.EventHandler(this.mButtonOK_Click);
            // 
            // mButtonCancel
            // 
            this.mButtonCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.mButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mButtonCancel.Location = new System.Drawing.Point(221, 80);
            this.mButtonCancel.Name = "mButtonCancel";
            this.mButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.mButtonCancel.TabIndex = 2;
            this.mButtonCancel.Text = "Cancel";
            this.mButtonCancel.UseVisualStyleBackColor = true;
            this.mButtonCancel.Click += new System.EventHandler(this.mButtonCancel_Click);
            // 
            // FormWelcome
            // 
            this.AcceptButton = this.mButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mButtonCancel;
            this.ClientSize = new System.Drawing.Size(428, 138);
            this.Controls.Add(this.mButtonCancel);
            this.Controls.Add(this.mButtonOK);
            this.Controls.Add(this.mNUDHours);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormWelcome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome";
            ((System.ComponentModel.ISupportInitialize)(this.mNUDHours)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown mNUDHours;
        private System.Windows.Forms.Button mButtonOK;
        private System.Windows.Forms.Button mButtonCancel;
    }
}