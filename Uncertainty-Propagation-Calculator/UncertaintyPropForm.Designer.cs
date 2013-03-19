namespace Uncertainty_Propagation_Calculator {
    partial class UncertaintyPropForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.WolframApiTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.VariableEntryGrid = new System.Windows.Forms.DataGridView();
            this.VariableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VariableValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VariableUncertainty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.EquationEntryTextBox = new System.Windows.Forms.TextBox();
            this.EqationInputFmtDropdown = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.OutputFmtDropdown = new System.Windows.Forms.ComboBox();
            this.CalculateBut = new System.Windows.Forms.Button();
            this.OpenOutputImageLocBut = new System.Windows.Forms.Button();
            this.PartialDerivsGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.FinalPropEquationField = new System.Windows.Forms.TextBox();
            this.PlugPartialDerivGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GenTextEquationsChkBox = new System.Windows.Forms.CheckBox();
            this.GenEqImagesChkBox = new System.Windows.Forms.CheckBox();
            this.CheckValidityBut = new System.Windows.Forms.Button();
            this.SaveKeyBut = new System.Windows.Forms.Button();
            this.GetKeyBut = new System.Windows.Forms.Button();
            this.DataInputErrLabel = new System.Windows.Forms.Label();
            this.KeyValidityLabel = new System.Windows.Forms.Label();
            this.KeySavedLabel = new System.Windows.Forms.Label();
            this.RenderEquationBut = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.GreekLetterBut = new System.Windows.Forms.Button();
            this.ClearIOBut = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.VariableEntryGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PartialDerivsGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlugPartialDerivGrid)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // WolframApiTextBox
            // 
            this.WolframApiTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WolframApiTextBox.Location = new System.Drawing.Point(12, 25);
            this.WolframApiTextBox.Name = "WolframApiTextBox";
            this.WolframApiTextBox.Size = new System.Drawing.Size(160, 23);
            this.WolframApiTextBox.TabIndex = 1;
            this.WolframApiTextBox.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Wolfram Alpha API Key";
            // 
            // VariableEntryGrid
            // 
            this.VariableEntryGrid.AllowUserToResizeColumns = false;
            this.VariableEntryGrid.AllowUserToResizeRows = false;
            this.VariableEntryGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.VariableEntryGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VariableName,
            this.VariableValue,
            this.VariableUncertainty});
            this.VariableEntryGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.VariableEntryGrid.Location = new System.Drawing.Point(386, 73);
            this.VariableEntryGrid.MultiSelect = false;
            this.VariableEntryGrid.Name = "VariableEntryGrid";
            this.VariableEntryGrid.RowHeadersWidth = 30;
            this.VariableEntryGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.VariableEntryGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.VariableEntryGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.VariableEntryGrid.ShowEditingIcon = false;
            this.VariableEntryGrid.Size = new System.Drawing.Size(372, 117);
            this.VariableEntryGrid.TabIndex = 3;
            // 
            // VariableName
            // 
            this.VariableName.HeaderText = "Variable Name";
            this.VariableName.MinimumWidth = 110;
            this.VariableName.Name = "VariableName";
            this.VariableName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.VariableName.Width = 110;
            // 
            // VariableValue
            // 
            this.VariableValue.HeaderText = "Variable Value";
            this.VariableValue.MinimumWidth = 100;
            this.VariableValue.Name = "VariableValue";
            this.VariableValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // VariableUncertainty
            // 
            this.VariableUncertainty.HeaderText = "Variable Uncertainty";
            this.VariableUncertainty.MinimumWidth = 130;
            this.VariableUncertainty.Name = "VariableUncertainty";
            this.VariableUncertainty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.VariableUncertainty.Width = 130;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(386, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(264, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Independent Variable Entry (Scientific notation is okay)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(270, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Equation Entry (dependent variable must be on left side)";
            // 
            // EquationEntryTextBox
            // 
            this.EquationEntryTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EquationEntryTextBox.Location = new System.Drawing.Point(12, 73);
            this.EquationEntryTextBox.Multiline = true;
            this.EquationEntryTextBox.Name = "EquationEntryTextBox";
            this.EquationEntryTextBox.Size = new System.Drawing.Size(368, 57);
            this.EquationEntryTextBox.TabIndex = 6;
            // 
            // EqationInputFmtDropdown
            // 
            this.EqationInputFmtDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EqationInputFmtDropdown.Enabled = false;
            this.EqationInputFmtDropdown.FormattingEnabled = true;
            this.EqationInputFmtDropdown.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.EqationInputFmtDropdown.Items.AddRange(new object[] {
            "Normal (Calculator Style)",
            "LaTeX"});
            this.EqationInputFmtDropdown.Location = new System.Drawing.Point(389, 223);
            this.EqationInputFmtDropdown.Name = "EqationInputFmtDropdown";
            this.EqationInputFmtDropdown.Size = new System.Drawing.Size(167, 21);
            this.EqationInputFmtDropdown.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(386, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Equation Input Format";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(386, 255);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Output Format";
            // 
            // OutputFmtDropdown
            // 
            this.OutputFmtDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OutputFmtDropdown.FormattingEnabled = true;
            this.OutputFmtDropdown.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.OutputFmtDropdown.Items.AddRange(new object[] {
            "LibreMath",
            "LaTeX"});
            this.OutputFmtDropdown.Location = new System.Drawing.Point(389, 271);
            this.OutputFmtDropdown.Name = "OutputFmtDropdown";
            this.OutputFmtDropdown.Size = new System.Drawing.Size(167, 21);
            this.OutputFmtDropdown.TabIndex = 11;
            // 
            // CalculateBut
            // 
            this.CalculateBut.Location = new System.Drawing.Point(572, 271);
            this.CalculateBut.Name = "CalculateBut";
            this.CalculateBut.Size = new System.Drawing.Size(186, 41);
            this.CalculateBut.TabIndex = 12;
            this.CalculateBut.Text = "Calculate";
            this.CalculateBut.UseVisualStyleBackColor = true;
            this.CalculateBut.Click += new System.EventHandler(this.StartPropagationCalculation);
            // 
            // OpenOutputImageLocBut
            // 
            this.OpenOutputImageLocBut.Enabled = false;
            this.OpenOutputImageLocBut.Location = new System.Drawing.Point(384, 143);
            this.OpenOutputImageLocBut.Name = "OpenOutputImageLocBut";
            this.OpenOutputImageLocBut.Size = new System.Drawing.Size(185, 39);
            this.OpenOutputImageLocBut.TabIndex = 13;
            this.OpenOutputImageLocBut.Text = "Open Output Image Location";
            this.OpenOutputImageLocBut.UseVisualStyleBackColor = true;
            this.OpenOutputImageLocBut.Visible = false;
            this.OpenOutputImageLocBut.Click += new System.EventHandler(this.OpenOutputImageDirectory);
            // 
            // PartialDerivsGrid
            // 
            this.PartialDerivsGrid.AllowUserToAddRows = false;
            this.PartialDerivsGrid.AllowUserToDeleteRows = false;
            this.PartialDerivsGrid.AllowUserToResizeColumns = false;
            this.PartialDerivsGrid.AllowUserToResizeRows = false;
            this.PartialDerivsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PartialDerivsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.PartialDerivsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.PartialDerivsGrid.Enabled = false;
            this.PartialDerivsGrid.Location = new System.Drawing.Point(6, 19);
            this.PartialDerivsGrid.MultiSelect = false;
            this.PartialDerivsGrid.Name = "PartialDerivsGrid";
            this.PartialDerivsGrid.RowHeadersVisible = false;
            this.PartialDerivsGrid.RowHeadersWidth = 5;
            this.PartialDerivsGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.PartialDerivsGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PartialDerivsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.PartialDerivsGrid.ShowEditingIcon = false;
            this.PartialDerivsGrid.Size = new System.Drawing.Size(367, 109);
            this.PartialDerivsGrid.TabIndex = 14;
            // 
            // Column1
            // 
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "Partial Derivatives";
            this.Column1.MinimumWidth = 368;
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 368;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.FinalPropEquationField);
            this.groupBox1.Controls.Add(this.PlugPartialDerivGrid);
            this.groupBox1.Controls.Add(this.PartialDerivsGrid);
            this.groupBox1.Controls.Add(this.OpenOutputImageLocBut);
            this.groupBox1.Location = new System.Drawing.Point(11, 358);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(755, 196);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Results";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Final Propagation Equation";
            // 
            // FinalPropEquationField
            // 
            this.FinalPropEquationField.Enabled = false;
            this.FinalPropEquationField.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FinalPropEquationField.Location = new System.Drawing.Point(6, 159);
            this.FinalPropEquationField.Name = "FinalPropEquationField";
            this.FinalPropEquationField.ReadOnly = true;
            this.FinalPropEquationField.Size = new System.Drawing.Size(366, 23);
            this.FinalPropEquationField.TabIndex = 16;
            // 
            // PlugPartialDerivGrid
            // 
            this.PlugPartialDerivGrid.AllowUserToAddRows = false;
            this.PlugPartialDerivGrid.AllowUserToDeleteRows = false;
            this.PlugPartialDerivGrid.AllowUserToResizeColumns = false;
            this.PlugPartialDerivGrid.AllowUserToResizeRows = false;
            this.PlugPartialDerivGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PlugPartialDerivGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            this.PlugPartialDerivGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.PlugPartialDerivGrid.Enabled = false;
            this.PlugPartialDerivGrid.Location = new System.Drawing.Point(384, 19);
            this.PlugPartialDerivGrid.MultiSelect = false;
            this.PlugPartialDerivGrid.Name = "PlugPartialDerivGrid";
            this.PlugPartialDerivGrid.RowHeadersVisible = false;
            this.PlugPartialDerivGrid.RowHeadersWidth = 5;
            this.PlugPartialDerivGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.PlugPartialDerivGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PlugPartialDerivGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.PlugPartialDerivGrid.ShowEditingIcon = false;
            this.PlugPartialDerivGrid.Size = new System.Drawing.Size(363, 109);
            this.PlugPartialDerivGrid.TabIndex = 15;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "Partial Derivatives With Variables Plugged In";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 368;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 368;
            // 
            // GenTextEquationsChkBox
            // 
            this.GenTextEquationsChkBox.AutoSize = true;
            this.GenTextEquationsChkBox.Checked = true;
            this.GenTextEquationsChkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.GenTextEquationsChkBox.Enabled = false;
            this.GenTextEquationsChkBox.Location = new System.Drawing.Point(572, 223);
            this.GenTextEquationsChkBox.Name = "GenTextEquationsChkBox";
            this.GenTextEquationsChkBox.Size = new System.Drawing.Size(144, 17);
            this.GenTextEquationsChkBox.TabIndex = 17;
            this.GenTextEquationsChkBox.Text = "Generate Text Equations";
            this.GenTextEquationsChkBox.UseVisualStyleBackColor = true;
            // 
            // GenEqImagesChkBox
            // 
            this.GenEqImagesChkBox.AutoSize = true;
            this.GenEqImagesChkBox.Checked = true;
            this.GenEqImagesChkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.GenEqImagesChkBox.Enabled = false;
            this.GenEqImagesChkBox.Location = new System.Drawing.Point(572, 246);
            this.GenEqImagesChkBox.Name = "GenEqImagesChkBox";
            this.GenEqImagesChkBox.Size = new System.Drawing.Size(152, 17);
            this.GenEqImagesChkBox.TabIndex = 18;
            this.GenEqImagesChkBox.Text = "Generate Equation Images";
            this.GenEqImagesChkBox.UseVisualStyleBackColor = true;
            // 
            // CheckValidityBut
            // 
            this.CheckValidityBut.Location = new System.Drawing.Point(178, 23);
            this.CheckValidityBut.Name = "CheckValidityBut";
            this.CheckValidityBut.Size = new System.Drawing.Size(86, 25);
            this.CheckValidityBut.TabIndex = 19;
            this.CheckValidityBut.Text = "Check Validity";
            this.CheckValidityBut.UseVisualStyleBackColor = true;
            this.CheckValidityBut.Click += new System.EventHandler(this.CheckApikeyValidity);
            // 
            // SaveKeyBut
            // 
            this.SaveKeyBut.Location = new System.Drawing.Point(270, 23);
            this.SaveKeyBut.Name = "SaveKeyBut";
            this.SaveKeyBut.Size = new System.Drawing.Size(80, 24);
            this.SaveKeyBut.TabIndex = 20;
            this.SaveKeyBut.Text = "Save Key";
            this.SaveKeyBut.UseVisualStyleBackColor = true;
            this.SaveKeyBut.Click += new System.EventHandler(this.SaveApiKey);
            // 
            // GetKeyBut
            // 
            this.GetKeyBut.Location = new System.Drawing.Point(356, 23);
            this.GetKeyBut.Name = "GetKeyBut";
            this.GetKeyBut.Size = new System.Drawing.Size(56, 24);
            this.GetKeyBut.TabIndex = 23;
            this.GetKeyBut.Text = "Get Key";
            this.GetKeyBut.UseVisualStyleBackColor = true;
            this.GetKeyBut.Click += new System.EventHandler(this.GetWolframApiKey);
            // 
            // DataInputErrLabel
            // 
            this.DataInputErrLabel.AutoSize = true;
            this.DataInputErrLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataInputErrLabel.ForeColor = System.Drawing.Color.Red;
            this.DataInputErrLabel.Location = new System.Drawing.Point(9, 318);
            this.DataInputErrLabel.Name = "DataInputErrLabel";
            this.DataInputErrLabel.Size = new System.Drawing.Size(179, 20);
            this.DataInputErrLabel.TabIndex = 24;
            this.DataInputErrLabel.Text = "Data Input Error Prompt";
            this.DataInputErrLabel.Visible = false;
            // 
            // KeyValidityLabel
            // 
            this.KeyValidityLabel.AutoSize = true;
            this.KeyValidityLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyValidityLabel.Location = new System.Drawing.Point(179, 8);
            this.KeyValidityLabel.Name = "KeyValidityLabel";
            this.KeyValidityLabel.Size = new System.Drawing.Size(84, 15);
            this.KeyValidityLabel.TabIndex = 26;
            this.KeyValidityLabel.Text = "Invalid Key";
            this.KeyValidityLabel.Visible = false;
            // 
            // KeySavedLabel
            // 
            this.KeySavedLabel.AutoSize = true;
            this.KeySavedLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeySavedLabel.Location = new System.Drawing.Point(275, 9);
            this.KeySavedLabel.Name = "KeySavedLabel";
            this.KeySavedLabel.Size = new System.Drawing.Size(70, 15);
            this.KeySavedLabel.TabIndex = 27;
            this.KeySavedLabel.Text = "Key Saved";
            this.KeySavedLabel.Visible = false;
            // 
            // RenderEquationBut
            // 
            this.RenderEquationBut.Enabled = false;
            this.RenderEquationBut.Location = new System.Drawing.Point(11, 130);
            this.RenderEquationBut.Name = "RenderEquationBut";
            this.RenderEquationBut.Size = new System.Drawing.Size(118, 23);
            this.RenderEquationBut.TabIndex = 31;
            this.RenderEquationBut.Text = "Render Equation";
            this.RenderEquationBut.UseVisualStyleBackColor = true;
            this.RenderEquationBut.Visible = false;
            this.RenderEquationBut.Click += new System.EventHandler(this.StartEquationRender);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(11, 153);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(368, 159);
            this.panel1.TabIndex = 32;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(305, 71);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // GreekLetterBut
            // 
            this.GreekLetterBut.Location = new System.Drawing.Point(621, 23);
            this.GreekLetterBut.Name = "GreekLetterBut";
            this.GreekLetterBut.Size = new System.Drawing.Size(137, 24);
            this.GreekLetterBut.TabIndex = 33;
            this.GreekLetterBut.Text = "Open Greek Symbol List";
            this.GreekLetterBut.UseVisualStyleBackColor = true;
            this.GreekLetterBut.Click += new System.EventHandler(this.OpenGreekSymbolList);
            // 
            // ClearIOBut
            // 
            this.ClearIOBut.Location = new System.Drawing.Point(504, 23);
            this.ClearIOBut.Name = "ClearIOBut";
            this.ClearIOBut.Size = new System.Drawing.Size(105, 24);
            this.ClearIOBut.TabIndex = 34;
            this.ClearIOBut.Text = "Clear Input/Output";
            this.ClearIOBut.UseVisualStyleBackColor = true;
            this.ClearIOBut.Click += new System.EventHandler(this.ResetFields);
            // 
            // UncertaintyPropForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 566);
            this.Controls.Add(this.ClearIOBut);
            this.Controls.Add(this.GreekLetterBut);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RenderEquationBut);
            this.Controls.Add(this.KeySavedLabel);
            this.Controls.Add(this.KeyValidityLabel);
            this.Controls.Add(this.DataInputErrLabel);
            this.Controls.Add(this.GetKeyBut);
            this.Controls.Add(this.SaveKeyBut);
            this.Controls.Add(this.CheckValidityBut);
            this.Controls.Add(this.GenEqImagesChkBox);
            this.Controls.Add(this.GenTextEquationsChkBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CalculateBut);
            this.Controls.Add(this.OutputFmtDropdown);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.EqationInputFmtDropdown);
            this.Controls.Add(this.EquationEntryTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.VariableEntryGrid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.WolframApiTextBox);
            this.Name = "UncertaintyPropForm";
            this.Text = "Uncertainty Propagation Calculator";
            ((System.ComponentModel.ISupportInitialize)(this.VariableEntryGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PartialDerivsGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlugPartialDerivGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox WolframApiTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox EquationEntryTextBox;
        private System.Windows.Forms.ComboBox EqationInputFmtDropdown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox OutputFmtDropdown;
        private System.Windows.Forms.Button CalculateBut;
        private System.Windows.Forms.Button OpenOutputImageLocBut;
        private System.Windows.Forms.DataGridView PartialDerivsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView PlugPartialDerivGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox FinalPropEquationField;
        private System.Windows.Forms.CheckBox GenTextEquationsChkBox;
        private System.Windows.Forms.CheckBox GenEqImagesChkBox;
        private System.Windows.Forms.Button CheckValidityBut;
        private System.Windows.Forms.Button SaveKeyBut;
        private System.Windows.Forms.Button GetKeyBut;
        private System.Windows.Forms.Label DataInputErrLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn VariableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn VariableValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn VariableUncertainty;
        private System.Windows.Forms.DataGridView VariableEntryGrid;
        private System.Windows.Forms.Label KeyValidityLabel;
        private System.Windows.Forms.Label KeySavedLabel;
        private System.Windows.Forms.Button RenderEquationBut;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button GreekLetterBut;
        private System.Windows.Forms.Button ClearIOBut;
    }
}

