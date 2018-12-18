/* Copyright 2018 Dawid Dyrcz 
 * See License.txt file 
 */
namespace DrawingNumberingPlugin
{
    partial class DrawingNumberingPluginForm
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
            this.prefix_textBox = new System.Windows.Forms.TextBox();
            this.postfix_textBox = new System.Windows.Forms.TextBox();
            this.startNumber_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.digits_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.saveLoad1 = new Tekla.Structures.Dialog.UIControls.SaveLoad();
            this.createApplyCancel1 = new Tekla.Structures.Dialog.UIControls.CreateApplyCancel();
            this.preifx_label = new System.Windows.Forms.Label();
            this.startNumber_label = new System.Windows.Forms.Label();
            this.postfix_label = new System.Windows.Forms.Label();
            this.digits_label = new System.Windows.Forms.Label();
            this.example_label = new System.Windows.Forms.Label();
            this.writeTo_label = new System.Windows.Forms.Label();
            this.title_comboBox = new System.Windows.Forms.ComboBox();
            this.onlyPrefix_checkBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.startNumber_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.digits_numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // prefix_textBox
            // 
            this.structuresExtender.SetAttributeName(this.prefix_textBox, "prefix");
            this.structuresExtender.SetAttributeTypeName(this.prefix_textBox, "String");
            this.structuresExtender.SetBindPropertyName(this.prefix_textBox, null);
            this.prefix_textBox.Location = new System.Drawing.Point(103, 49);
            this.prefix_textBox.Name = "prefix_textBox";
            this.prefix_textBox.Size = new System.Drawing.Size(359, 20);
            this.prefix_textBox.TabIndex = 0;
            this.prefix_textBox.Text = "78UKH-YJKO23-";
            this.prefix_textBox.TextChanged += new System.EventHandler(this.prefix_textBox_TextChanged);
            // 
            // postfix_textBox
            // 
            this.structuresExtender.SetAttributeName(this.postfix_textBox, "postfix");
            this.structuresExtender.SetAttributeTypeName(this.postfix_textBox, "String");
            this.structuresExtender.SetBindPropertyName(this.postfix_textBox, null);
            this.postfix_textBox.Location = new System.Drawing.Point(103, 101);
            this.postfix_textBox.Name = "postfix_textBox";
            this.postfix_textBox.Size = new System.Drawing.Size(359, 20);
            this.postfix_textBox.TabIndex = 1;
            this.postfix_textBox.Text = "-UIL";
            this.postfix_textBox.TextChanged += new System.EventHandler(this.postfix_textBox_TextChanged);
            // 
            // startNumber_numericUpDown
            // 
            this.structuresExtender.SetAttributeName(this.startNumber_numericUpDown, "startNumber");
            this.structuresExtender.SetAttributeTypeName(this.startNumber_numericUpDown, "Integer");
            this.structuresExtender.SetBindPropertyName(this.startNumber_numericUpDown, "Text");
            this.startNumber_numericUpDown.Location = new System.Drawing.Point(103, 75);
            this.startNumber_numericUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.startNumber_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.startNumber_numericUpDown.Name = "startNumber_numericUpDown";
            this.startNumber_numericUpDown.Size = new System.Drawing.Size(228, 20);
            this.startNumber_numericUpDown.TabIndex = 2;
            this.startNumber_numericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.startNumber_numericUpDown.ValueChanged += new System.EventHandler(this.startNumber_numericUpDown_ValueChanged);
            this.startNumber_numericUpDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.startNumber_numericUpDown_MouseUp);
            // 
            // digits_numericUpDown
            // 
            this.structuresExtender.SetAttributeName(this.digits_numericUpDown, "digits");
            this.structuresExtender.SetAttributeTypeName(this.digits_numericUpDown, "Integer");
            this.structuresExtender.SetBindPropertyName(this.digits_numericUpDown, "Text");
            this.digits_numericUpDown.Location = new System.Drawing.Point(376, 75);
            this.digits_numericUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.digits_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.digits_numericUpDown.Name = "digits_numericUpDown";
            this.digits_numericUpDown.Size = new System.Drawing.Size(86, 20);
            this.digits_numericUpDown.TabIndex = 3;
            this.digits_numericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.digits_numericUpDown.ValueChanged += new System.EventHandler(this.digits_numericUpDown_ValueChanged);
            this.digits_numericUpDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.digits_numericUpDown_MouseUp);
            // 
            // saveLoad1
            // 
            this.structuresExtender.SetAttributeName(this.saveLoad1, null);
            this.structuresExtender.SetAttributeTypeName(this.saveLoad1, null);
            this.saveLoad1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.structuresExtender.SetBindPropertyName(this.saveLoad1, null);
            this.saveLoad1.Dock = System.Windows.Forms.DockStyle.Top;
            this.saveLoad1.HelpFileType = Tekla.Structures.Dialog.UIControls.SaveLoad.HelpFileTypeEnum.General;
            this.saveLoad1.HelpKeyword = "";
            this.saveLoad1.HelpUrl = "";
            this.saveLoad1.Location = new System.Drawing.Point(0, 0);
            this.saveLoad1.Name = "saveLoad1";
            this.saveLoad1.SaveAsText = "";
            this.saveLoad1.Size = new System.Drawing.Size(474, 43);
            this.saveLoad1.TabIndex = 4;
            this.saveLoad1.UserDefinedHelpFilePath = null;
            this.saveLoad1.AttributesLoaded += new System.EventHandler(this.saveLoad1_AttributesLoaded);
            // 
            // createApplyCancel1
            // 
            this.structuresExtender.SetAttributeName(this.createApplyCancel1, null);
            this.structuresExtender.SetAttributeTypeName(this.createApplyCancel1, null);
            this.structuresExtender.SetBindPropertyName(this.createApplyCancel1, null);
            this.createApplyCancel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.createApplyCancel1.Location = new System.Drawing.Point(0, 251);
            this.createApplyCancel1.Name = "createApplyCancel1";
            this.createApplyCancel1.Size = new System.Drawing.Size(474, 30);
            this.createApplyCancel1.TabIndex = 5;
            this.createApplyCancel1.CreateClicked += new System.EventHandler(this.createApplyCancel1_CreateClicked);
            this.createApplyCancel1.ApplyClicked += new System.EventHandler(this.createApplyCancel1_ApplyClicked);
            this.createApplyCancel1.CancelClicked += new System.EventHandler(this.createApplyCancel1_CancelClicked);
            // 
            // preifx_label
            // 
            this.structuresExtender.SetAttributeName(this.preifx_label, null);
            this.structuresExtender.SetAttributeTypeName(this.preifx_label, null);
            this.preifx_label.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.preifx_label, null);
            this.preifx_label.Location = new System.Drawing.Point(12, 56);
            this.preifx_label.Name = "preifx_label";
            this.preifx_label.Size = new System.Drawing.Size(33, 13);
            this.preifx_label.TabIndex = 6;
            this.preifx_label.Text = "Prefix";
            // 
            // startNumber_label
            // 
            this.structuresExtender.SetAttributeName(this.startNumber_label, null);
            this.structuresExtender.SetAttributeTypeName(this.startNumber_label, null);
            this.startNumber_label.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.startNumber_label, null);
            this.startNumber_label.Location = new System.Drawing.Point(12, 79);
            this.startNumber_label.Name = "startNumber_label";
            this.startNumber_label.Size = new System.Drawing.Size(67, 13);
            this.startNumber_label.TabIndex = 7;
            this.startNumber_label.Text = "Start number";
            // 
            // postfix_label
            // 
            this.structuresExtender.SetAttributeName(this.postfix_label, null);
            this.structuresExtender.SetAttributeTypeName(this.postfix_label, null);
            this.postfix_label.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.postfix_label, null);
            this.postfix_label.Location = new System.Drawing.Point(12, 105);
            this.postfix_label.Name = "postfix_label";
            this.postfix_label.Size = new System.Drawing.Size(38, 13);
            this.postfix_label.TabIndex = 8;
            this.postfix_label.Text = "Postfix";
            // 
            // digits_label
            // 
            this.structuresExtender.SetAttributeName(this.digits_label, null);
            this.structuresExtender.SetAttributeTypeName(this.digits_label, null);
            this.digits_label.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.digits_label, null);
            this.digits_label.Location = new System.Drawing.Point(337, 77);
            this.digits_label.Name = "digits_label";
            this.digits_label.Size = new System.Drawing.Size(33, 13);
            this.digits_label.TabIndex = 9;
            this.digits_label.Text = "Digits";
            // 
            // example_label
            // 
            this.structuresExtender.SetAttributeName(this.example_label, null);
            this.structuresExtender.SetAttributeTypeName(this.example_label, null);
            this.example_label.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.example_label, null);
            this.example_label.Location = new System.Drawing.Point(12, 162);
            this.example_label.Name = "example_label";
            this.example_label.Size = new System.Drawing.Size(89, 13);
            this.example_label.TabIndex = 10;
            this.example_label.Text = "example numbers";
            // 
            // writeTo_label
            // 
            this.structuresExtender.SetAttributeName(this.writeTo_label, null);
            this.structuresExtender.SetAttributeTypeName(this.writeTo_label, null);
            this.writeTo_label.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.writeTo_label, null);
            this.writeTo_label.Location = new System.Drawing.Point(12, 133);
            this.writeTo_label.Name = "writeTo_label";
            this.writeTo_label.Size = new System.Drawing.Size(47, 13);
            this.writeTo_label.TabIndex = 11;
            this.writeTo_label.Text = "Write to ";
            // 
            // title_comboBox
            // 
            this.structuresExtender.SetAttributeName(this.title_comboBox, "title");
            this.structuresExtender.SetAttributeTypeName(this.title_comboBox, "Integer");
            this.structuresExtender.SetBindPropertyName(this.title_comboBox, "SelectedIndex");
            this.title_comboBox.Location = new System.Drawing.Point(103, 128);
            this.title_comboBox.Name = "title_comboBox";
            this.title_comboBox.Size = new System.Drawing.Size(267, 21);
            this.title_comboBox.TabIndex = 12;
            // 
            // onlyPrefix_checkBox
            // 
            this.structuresExtender.SetAttributeName(this.onlyPrefix_checkBox, "onlyPrefix");
            this.structuresExtender.SetAttributeTypeName(this.onlyPrefix_checkBox, "Integer");
            this.onlyPrefix_checkBox.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.onlyPrefix_checkBox, "Checked");
            this.onlyPrefix_checkBox.Location = new System.Drawing.Point(387, 130);
            this.onlyPrefix_checkBox.Name = "onlyPrefix_checkBox";
            this.onlyPrefix_checkBox.Size = new System.Drawing.Size(75, 17);
            this.onlyPrefix_checkBox.TabIndex = 13;
            this.onlyPrefix_checkBox.Text = "Only prefix";
            this.onlyPrefix_checkBox.UseVisualStyleBackColor = true;
            this.onlyPrefix_checkBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.onlyPrefix_checkBox_MouseUp);
            // 
            // DrawingNumberingPluginForm
            // 
            this.structuresExtender.SetAttributeName(this, null);
            this.structuresExtender.SetAttributeTypeName(this, null);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.structuresExtender.SetBindPropertyName(this, null);
            this.ClientSize = new System.Drawing.Size(474, 281);
            this.Controls.Add(this.onlyPrefix_checkBox);
            this.Controls.Add(this.title_comboBox);
            this.Controls.Add(this.writeTo_label);
            this.Controls.Add(this.example_label);
            this.Controls.Add(this.digits_label);
            this.Controls.Add(this.postfix_label);
            this.Controls.Add(this.startNumber_label);
            this.Controls.Add(this.preifx_label);
            this.Controls.Add(this.createApplyCancel1);
            this.Controls.Add(this.saveLoad1);
            this.Controls.Add(this.digits_numericUpDown);
            this.Controls.Add(this.startNumber_numericUpDown);
            this.Controls.Add(this.postfix_textBox);
            this.Controls.Add(this.prefix_textBox);
            this.MaximumSize = new System.Drawing.Size(490, 320);
            this.MinimumSize = new System.Drawing.Size(490, 320);
            this.Name = "DrawingNumberingPluginForm";
            this.Text = "Drawing numbering";
            ((System.ComponentModel.ISupportInitialize)(this.startNumber_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.digits_numericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox prefix_textBox;
        private System.Windows.Forms.TextBox postfix_textBox;
        private System.Windows.Forms.NumericUpDown startNumber_numericUpDown;
        private System.Windows.Forms.NumericUpDown digits_numericUpDown;
        private Tekla.Structures.Dialog.UIControls.SaveLoad saveLoad1;
        private Tekla.Structures.Dialog.UIControls.CreateApplyCancel createApplyCancel1;
        private System.Windows.Forms.Label preifx_label;
        private System.Windows.Forms.Label startNumber_label;
        private System.Windows.Forms.Label postfix_label;
        private System.Windows.Forms.Label digits_label;
        private System.Windows.Forms.Label example_label;
        private System.Windows.Forms.Label writeTo_label;
        private System.Windows.Forms.ComboBox title_comboBox;
        private System.Windows.Forms.CheckBox onlyPrefix_checkBox;
    }
}