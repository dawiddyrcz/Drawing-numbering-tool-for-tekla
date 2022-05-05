/* Copyright 2018 Dawid Dyrcz 
 * See License.txt file 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Tekla.Structures.Dialog;

namespace DrawingNumberingPlugin
{
    public partial class MainForm : ApplicationFormBase
    {
        private BackgroundWorker backgroundWorker = new BackgroundWorker();
        public MainForm()
        {
            InitializeComponent();
            GenerateExampleNumbers();
            EnableDisableControls();
            FillTitleCombobox();

            this.InitializeForm();
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;

            this.Shown += DrawingNumberingPluginForm_Shown;
            progress_label.Text = "Ready";
        }

        private void DrawingNumberingPluginForm_Shown(object sender, EventArgs e)
        {
            var model = new Tekla.Structures.Model.Model();
            if (model.GetConnectionStatus())
            {
                var events = new Tekla.Structures.Model.Events();
                events.TeklaStructuresExit += Events_TeklaStructuresExit;
                events.Register();
            }
        }

        private void Events_TeklaStructuresExit()
        {
            Application.Exit();
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            workInProgress = false;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var drawingNumbering = new DrawingNumbering(data);
                drawingNumbering.Progress += (count, max, message) =>
                {
                    Invoke(new Action(() => 
                    { 
                        progress_label.Text = message;
                        progressBar1.Value = (int) (100.0 * count / max);
                    }));
                };

                drawingNumbering.Run(e);
            }
            catch (Exception ex) { HandleException(ex); }
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
            try
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
            catch (Exception ex)
            {
                HandleException(ex);
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
            if (workInProgress)
                backgroundWorker.CancelAsync();
        }

        bool workInProgress = false;
        private DrawingNumberingPlugin_StructuresData data;
        private void createApplyCancel1_CreateClicked(object sender, EventArgs e)
        {
            try
            {
                this.Apply();
                workInProgress = true;
                data = new DrawingNumberingPlugin_StructuresData();
                data._Prefix = this.prefix_textBox.Text;
                data._StartNumber = (int)this.startNumber_numericUpDown.Value;
                data._Digits = (int)this.digits_numericUpDown.Value;
                data._Postfix = this.postfix_textBox.Text;
                data._Title = this.title_comboBox.SelectedIndex;
                data._OnlyPrefix = this.onlyPrefix_checkBox.Checked ? 1 : 0;
                data.CorrectValues();
                backgroundWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void HandleException(Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(ex.ToString());
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

        private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(linkLabel1.Text);
            }
            catch { }
        }
    }
}
