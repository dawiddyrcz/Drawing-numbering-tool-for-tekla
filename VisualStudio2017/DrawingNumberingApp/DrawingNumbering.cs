using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Plugins;

namespace DrawingNumberingPlugin
{
    public class DrawingNumberingPlugin_StructuresData
    {
        public string _Prefix;
        public int _StartNumber;
        public int _Digits;
        public string _Postfix;
        public int _Title;
        public int _OnlyPrefix;

        public DrawingNumberingPlugin_StructuresData()
        {

        }

        public void CorrectValues()
        {
            var value = new ValueCorrector();
            if (value.IsDefaultValue(_Prefix) || _Prefix == "")
                _Prefix = "";
            if (value.IsDefaultValue(_StartNumber) || _StartNumber < 1)
                _StartNumber = 1;
            if (value.IsDefaultValue(_Digits) || _Digits < 1)
                _Digits = 1;
            if (value.IsDefaultValue(_Postfix) || _Postfix == "")
                _Postfix = "";
            if (value.IsDefaultValue(_Title) || _Title < 0)
                _Title = 0;
            if (value.IsDefaultValue(_OnlyPrefix))
                _OnlyPrefix = 0;
        }


        private class ValueCorrector : PluginBase
        {
            public override List<InputDefinition> DefineInput()
            {
                throw new NotImplementedException();
            }

            public override bool Run(List<InputDefinition> Input)
            {
                throw new NotImplementedException();
            }
        }
    }

    public delegate void ProgressDelegate(int count, int max, string message);

    public class DrawingNumbering
    {
        private readonly Model _model;
        private readonly DrawingNumberingPlugin_StructuresData data;
        public event ProgressDelegate Progress;

        public DrawingNumbering(DrawingNumberingPlugin_StructuresData data)
        {
            this._model = new Model();
            this.data = data;
        }

        private string GetCurrentNumberWithPrefixAndPostFix(string prefix, string postfix, int currentNumber)
        {
            if (data._OnlyPrefix == 1) return prefix;
            else return prefix + GetCurrentNumber(currentNumber) + postfix;
        }

        private string GetCurrentNumber(int currentNumber)
        {
            var digits = data._Digits;

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

        private void UpdateProgress(int count, int max, string message)
        {
            max = Math.Max(1, max);
            count = Math.Max(0, count);
            count = Math.Min(count, max);
            if (string.IsNullOrEmpty(message)) message = string.Empty;

            Progress?.Invoke(count, max, message);
        }

        public bool Run(DoWorkEventArgs e)
        {
            int succesfulModified = 0;
            var dh = new Tekla.Structures.Drawing.DrawingHandler();
            dh.CloseActiveDrawing();
            var selectedDrawings = dh.GetDrawingSelector().GetSelected();
            var drawingsCount = selectedDrawings.GetSize();
            if (drawingsCount == 0)
            {
                MessageBox.Show("No drawings selected", "No drawings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure do you want to change attributes in " + selectedDrawings.GetSize() + " drawings?",
                "Are you sure ?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            if (result == DialogResult.Yes)
            {
                var stopwatch = new System.Diagnostics.Stopwatch();
                try
                {
                    UpdateProgress(0, 1, "Trying to start");

                    stopwatch.Start();
                    int currentNumber = data._StartNumber;
                    int checkedDrawings = 0;
                    double medTimeForOne = 0;

                    while (selectedDrawings.MoveNext())
                    {
                        if(e.Cancel ) break;

                        var currentDrawing = selectedDrawings.Current as Tekla.Structures.Drawing.Drawing;
                        currentDrawing.Select();
                        bool modified = false;
                        var currentNumberString = GetCurrentNumberWithPrefixAndPostFix(data._Prefix, data._Postfix, currentNumber);

                        switch (data._Title)
                        {
                            case (0):
                                if (currentDrawing.Name != currentNumberString)
                                {
                                    currentDrawing.Name = currentNumberString;
                                    modified = true;
                                }
                                break;

                            case (1):
                                if (currentDrawing.Title1 != currentNumberString)
                                {
                                    currentDrawing.Title1 = currentNumberString;
                                    modified = true;
                                }
                                break;
                            case (2):
                                if (currentDrawing.Title2 != currentNumberString)
                                {
                                    currentDrawing.Title2 = currentNumberString;
                                    modified = true;
                                }
                                break;
                            case (3):
                                if (currentDrawing.Title3 != currentNumberString)
                                {
                                    currentDrawing.Title3 = currentNumberString;
                                    modified = true;
                                }
                                break;
                            default:
                                try
                                {
                                    var udaHandler = new UDAHandler();
                                    var udaName = udaHandler.GetUDAByIndex(data._Title - 4);

                                    if (udaName != "")
                                    {
                                        modified = currentDrawing.SetUserProperty(udaName, currentNumberString);
                                    }
                                }
                                catch (Exception) { }
                                break;
                        }

                        if (modified)
                        {
                            currentDrawing.Modify();
                            succesfulModified++;
                        }
                        currentNumber++;
                        checkedDrawings++;
                        medTimeForOne = stopwatch.Elapsed.TotalMinutes / checkedDrawings;

                        string message = checkedDrawings + "/" + drawingsCount + " " + Math.Round(medTimeForOne * (drawingsCount - checkedDrawings)) + " minutes left";
                        UpdateProgress(checkedDrawings, drawingsCount, message);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    stopwatch.Stop();
                    string message = "Task has been completed. Modified drawings: " + succesfulModified.ToString() + ". Time elapsed: " 
                        + Math.Round(stopwatch.Elapsed.TotalSeconds).ToString() + " seconds";
                    UpdateProgress(0,1,message);
                }
            }

            return true;
        }
    }
}
