namespace PasswordManager.Forms
{
    partial class GeneratePasswordDialog
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
            this.checkBoxUpperCase = new System.Windows.Forms.CheckBox();
            this.checkBoxLowerCase = new System.Windows.Forms.CheckBox();
            this.checkBoxNumbers = new System.Windows.Forms.CheckBox();
            this.checkBoxSpecial = new System.Windows.Forms.CheckBox();
            this.numericUpDownLength = new System.Windows.Forms.NumericUpDown();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelLength = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLength)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxUpperCase
            // 
            this.checkBoxUpperCase.AutoSize = true;
            this.checkBoxUpperCase.Checked = true;
            this.checkBoxUpperCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUpperCase.Location = new System.Drawing.Point(99, 80);
            this.checkBoxUpperCase.Name = "checkBoxUpperCase";
            this.checkBoxUpperCase.Size = new System.Drawing.Size(119, 19);
            this.checkBoxUpperCase.TabIndex = 0;
            this.checkBoxUpperCase.Text = "Uppercase Letters";
            this.checkBoxUpperCase.UseVisualStyleBackColor = true;
            // 
            // checkBoxLowerCase
            // 
            this.checkBoxLowerCase.AutoSize = true;
            this.checkBoxLowerCase.Checked = true;
            this.checkBoxLowerCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLowerCase.Location = new System.Drawing.Point(99, 105);
            this.checkBoxLowerCase.Name = "checkBoxLowerCase";
            this.checkBoxLowerCase.Size = new System.Drawing.Size(119, 19);
            this.checkBoxLowerCase.TabIndex = 1;
            this.checkBoxLowerCase.Text = "Lowercase Letters";
            this.checkBoxLowerCase.UseVisualStyleBackColor = true;
            // 
            // checkBoxNumbers
            // 
            this.checkBoxNumbers.AutoSize = true;
            this.checkBoxNumbers.Checked = true;
            this.checkBoxNumbers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxNumbers.Location = new System.Drawing.Point(99, 130);
            this.checkBoxNumbers.Name = "checkBoxNumbers";
            this.checkBoxNumbers.Size = new System.Drawing.Size(75, 19);
            this.checkBoxNumbers.TabIndex = 2;
            this.checkBoxNumbers.Text = "Numbers";
            this.checkBoxNumbers.UseVisualStyleBackColor = true;
            // 
            // checkBoxSpecial
            // 
            this.checkBoxSpecial.AutoSize = true;
            this.checkBoxSpecial.Checked = true;
            this.checkBoxSpecial.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSpecial.Location = new System.Drawing.Point(99, 155);
            this.checkBoxSpecial.Name = "checkBoxSpecial";
            this.checkBoxSpecial.Size = new System.Drawing.Size(122, 19);
            this.checkBoxSpecial.TabIndex = 3;
            this.checkBoxSpecial.Text = "Special Characters";
            this.checkBoxSpecial.UseVisualStyleBackColor = true;
            // 
            // numericUpDownLength
            // 
            this.numericUpDownLength.Location = new System.Drawing.Point(99, 177);
            this.numericUpDownLength.Maximum = new decimal(new int[] {
            36,
            0,
            0,
            0});
            this.numericUpDownLength.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownLength.Name = "numericUpDownLength";
            this.numericUpDownLength.Size = new System.Drawing.Size(96, 23);
            this.numericUpDownLength.TabIndex = 4;
            this.numericUpDownLength.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(61, 264);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(142, 264);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // labelLength
            // 
            this.labelLength.AutoSize = true;
            this.labelLength.Location = new System.Drawing.Point(46, 179);
            this.labelLength.Name = "labelLength";
            this.labelLength.Size = new System.Drawing.Size(47, 15);
            this.labelLength.TabIndex = 7;
            this.labelLength.Text = "Length:";
            // 
            // GeneratePasswordDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 379);
            this.Controls.Add(this.labelLength);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.numericUpDownLength);
            this.Controls.Add(this.checkBoxSpecial);
            this.Controls.Add(this.checkBoxNumbers);
            this.Controls.Add(this.checkBoxLowerCase);
            this.Controls.Add(this.checkBoxUpperCase);
            this.Name = "GeneratePasswordDialog";
            this.Text = "GeneratePasswordDialog";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox checkBoxUpperCase;
        private CheckBox checkBoxLowerCase;
        private CheckBox checkBoxNumbers;
        private CheckBox checkBoxSpecial;
        private NumericUpDown numericUpDownLength;
        private Button buttonCancel;
        private Button buttonOK;
        private Label labelLength;
    }
}