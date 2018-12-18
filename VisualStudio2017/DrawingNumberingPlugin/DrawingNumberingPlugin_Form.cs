/* Copyright 2018 Dawid Dyrcz 
 * See License.txt file 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Tekla.Structures.Dialog;

namespace DrawingNumberingPlugin
{
    public partial class DrawingNumberingPluginForm : PluginFormBase
    {
        public DrawingNumberingPluginForm()
        {
            InitializeComponent();
            GenerateExampleNumbers();
            EnableDisableControls();
            FillTitleCombobox();
        }

        private void FillTitleCombobox()
        {
            this.title_comboBox.Items.Clear();
            //DO NOT CHANGE THIS CODE
            this.title_comboBox.Items.Add("NAME");
            this.title_comboBox.Items.Add("TITLE1");
            this.title_comboBox.Items.Add("TITLE2");
            this.title_comboBox.Items.Add("TITLE3");

            //This try is stupid but i dont know how to test it
            try
            {
                var udaLabels = new UDAHandler().GetAllUDALabels();
                this.title_comboBox.Items.AddRange(udaLabels.ToArray());
            }
            catch (Exception) { }
        }

        private void okApplyModifyGetOnOffCancel1_OnOffClicked(object sender, EventArgs e)
        {
            this.ToggleSelection();
        }

        private void GenerateExampleNumbers()
        {
            if (int.TryParse(this.startNumber_numericUpDown.Text, out int startNumber))
            {
                if (startNumber >= 0)
                {
                    string exampleText = "";

                    for (int i = startNumber; i < startNumber + 7; i++)
                    {
                        string newLine = GetCurrentNumberWithPrefixAndPostFix(this.prefix_textBox.Text, this.postfix_textBox.Text, i);
                        exampleText = exampleText + newLine + "\n";
                    }

                    this.example_label.Text = exampleText; 
                }
            }
        }

        private string GetCurrentNumberWithPrefixAndPostFix(string prefix, string postfix, int currentNumber)
        {
            if (onlyPrefix_checkBox.Checked) return prefix;
            else return prefix + GetCurrentNumber(currentNumber) + postfix;
        }

        private string GetCurrentNumber(int currentNumber)
        {
            if (int.TryParse(this.digits_numericUpDown.Text, out int digits))
            {
                if (digits > 0)
                {
                    //Check number of digits in currentnumber and corrects var digits
                    int.TryParse(Math.Max(digits, Math.Floor(Math.Log10(currentNumber) + 1)).ToString(), out digits);
                    
                    string formatString = "";

                    for (int j = 0; j < digits; j++)
                    {
                        formatString = formatString + "0";
                    }

                    return currentNumber.ToString(formatString);
                }
                else
                    return "";
            }
            else
                return "";
        }

        private void prefix_textBox_TextChanged(object sender, EventArgs e)
        {
            GenerateExampleNumbers();
        }

        private void startNumber_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            GenerateExampleNumbers();
        }

        private void digits_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            GenerateExampleNumbers();
        }

        private void postfix_textBox_TextChanged(object sender, EventArgs e)
        {
            GenerateExampleNumbers();
        }

        private void createApplyCancel1_ApplyClicked(object sender, EventArgs e)
        {
            GenerateExampleNumbers();
            this.Apply();
        }

        private void createApplyCancel1_CancelClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void createApplyCancel1_CreateClicked(object sender, EventArgs e)
        {
            this.Apply();
            this.Create();
        }

        private void startNumber_numericUpDown_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GenerateExampleNumbers();
        }

        private void digits_numericUpDown_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GenerateExampleNumbers();
        }

        private void onlyPrefix_checkBox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GenerateExampleNumbers();
            EnableDisableControls();
        }

        private void EnableDisableControls()
        {
            if (!onlyPrefix_checkBox.Checked)
            {
                startNumber_numericUpDown.Enabled = true;
                postfix_textBox.Enabled = true;
                digits_numericUpDown.Enabled = true;
            }
            else
            {
                startNumber_numericUpDown.Enabled = false;
                postfix_textBox.Enabled = false;
                digits_numericUpDown.Enabled = false;
            }
        }

        private void saveLoad1_AttributesLoaded(object sender, EventArgs e)
        {
            GenerateExampleNumbers();
            EnableDisableControls();
        }
    }
}
