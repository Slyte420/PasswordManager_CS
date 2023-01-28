namespace PasswordManager.Forms
{
    partial class MainMenu
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
            this.dataGridViewData = new System.Windows.Forms.DataGridView();
            this.comboBoxGroup = new System.Windows.Forms.ComboBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCopyClipBoard = new System.Windows.Forms.Button();
            this.buttonAddGroup = new System.Windows.Forms.Button();
            this.buttonEditGroup = new System.Windows.Forms.Button();
            this.buttonDeleteGroup = new System.Windows.Forms.Button();
            this.buttonShowPass = new System.Windows.Forms.Button();
            this.buttonFindEntry = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewData)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewData
            // 
            this.dataGridViewData.AllowUserToAddRows = false;
            this.dataGridViewData.AllowUserToDeleteRows = false;
            this.dataGridViewData.AllowUserToResizeColumns = false;
            this.dataGridViewData.AllowUserToResizeRows = false;
            this.dataGridViewData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewData.Location = new System.Drawing.Point(108, 80);
            this.dataGridViewData.Name = "dataGridViewData";
            this.dataGridViewData.ReadOnly = true;
            this.dataGridViewData.RowTemplate.Height = 25;
            this.dataGridViewData.Size = new System.Drawing.Size(581, 329);
            this.dataGridViewData.TabIndex = 0;
            // 
            // comboBoxGroup
            // 
            this.comboBoxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGroup.FormattingEnabled = true;
            this.comboBoxGroup.Location = new System.Drawing.Point(12, 31);
            this.comboBoxGroup.Name = "comboBoxGroup";
            this.comboBoxGroup.Size = new System.Drawing.Size(121, 23);
            this.comboBoxGroup.TabIndex = 1;
            this.comboBoxGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxGroup_SelectedIndexChanged);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(139, 30);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(220, 30);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 3;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(301, 31);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(699, 31);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // buttonCopyClipBoard
            // 
            this.buttonCopyClipBoard.Location = new System.Drawing.Point(12, 415);
            this.buttonCopyClipBoard.Name = "buttonCopyClipBoard";
            this.buttonCopyClipBoard.Size = new System.Drawing.Size(166, 23);
            this.buttonCopyClipBoard.TabIndex = 6;
            this.buttonCopyClipBoard.Text = "Copy Password to Clipboard";
            this.buttonCopyClipBoard.UseVisualStyleBackColor = true;
            // 
            // buttonAddGroup
            // 
            this.buttonAddGroup.Location = new System.Drawing.Point(382, 31);
            this.buttonAddGroup.Name = "buttonAddGroup";
            this.buttonAddGroup.Size = new System.Drawing.Size(75, 23);
            this.buttonAddGroup.TabIndex = 7;
            this.buttonAddGroup.Text = "Add Group";
            this.buttonAddGroup.UseVisualStyleBackColor = true;
            // 
            // buttonEditGroup
            // 
            this.buttonEditGroup.Location = new System.Drawing.Point(463, 31);
            this.buttonEditGroup.Name = "buttonEditGroup";
            this.buttonEditGroup.Size = new System.Drawing.Size(75, 23);
            this.buttonEditGroup.TabIndex = 8;
            this.buttonEditGroup.Text = "Edit Group";
            this.buttonEditGroup.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteGroup
            // 
            this.buttonDeleteGroup.Location = new System.Drawing.Point(544, 31);
            this.buttonDeleteGroup.Name = "buttonDeleteGroup";
            this.buttonDeleteGroup.Size = new System.Drawing.Size(91, 23);
            this.buttonDeleteGroup.TabIndex = 9;
            this.buttonDeleteGroup.Text = "Delete Group";
            this.buttonDeleteGroup.UseVisualStyleBackColor = true;
            // 
            // buttonShowPass
            // 
            this.buttonShowPass.Location = new System.Drawing.Point(184, 415);
            this.buttonShowPass.Name = "buttonShowPass";
            this.buttonShowPass.Size = new System.Drawing.Size(166, 23);
            this.buttonShowPass.TabIndex = 10;
            this.buttonShowPass.Text = "Show Password";
            this.buttonShowPass.UseVisualStyleBackColor = true;
            // 
            // buttonFindEntry
            // 
            this.buttonFindEntry.Location = new System.Drawing.Point(356, 415);
            this.buttonFindEntry.Name = "buttonFindEntry";
            this.buttonFindEntry.Size = new System.Drawing.Size(166, 23);
            this.buttonFindEntry.TabIndex = 11;
            this.buttonFindEntry.Text = "Find Entry";
            this.buttonFindEntry.UseVisualStyleBackColor = true;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonFindEntry);
            this.Controls.Add(this.buttonShowPass);
            this.Controls.Add(this.buttonDeleteGroup);
            this.Controls.Add(this.buttonEditGroup);
            this.Controls.Add(this.buttonAddGroup);
            this.Controls.Add(this.buttonCopyClipBoard);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.comboBoxGroup);
            this.Controls.Add(this.dataGridViewData);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dataGridViewData;
        private ComboBox comboBoxGroup;
        private Button buttonAdd;
        private Button buttonEdit;
        private Button buttonDelete;
        private Button buttonSave;
        private Button buttonCopyClipBoard;
        private Button buttonAddGroup;
        private Button buttonEditGroup;
        private Button buttonDeleteGroup;
        private Button buttonShowPass;
        private Button buttonFindEntry;
    }
}