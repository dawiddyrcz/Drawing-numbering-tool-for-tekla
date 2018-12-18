/* Copyright 2018 Dawid Dyrcz 
 * See License.txt file 
 */

using System;
using Tekla.Structures.Plugins;
using Tekla.Structures.Model;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DrawingNumberingPlugin
{
    public class DrawingNumberingPlugin_StructuresData
    {
        [StructuresField("prefix")]
        public string Prefix;
        [StructuresField("startNumber")]
        public int StartNumber;
        [StructuresField("digits")]
        public int Digits;
        [StructuresField("postfix")]
        public string Postfix;
        [StructuresField("title")]
        public int Title;
        [StructuresField("onlyPrefix")]
        public int OnlyPrefix;
    }

    [Plugin("Drawing Numbering Tool")] 
    [PluginUserInterface("DrawingNumberingPlugin.DrawingNumberingPluginForm")]
    [InputObjectDependency(PluginBase.InputObjectDependency.NOT_DEPENDENT)]
   
    public class DrawingNumberingPlugin : PluginBase
    {
        public string _Prefix;
        public int _StartNumber;
        public int _Digits;
        public string _Postfix;
        public int _Title;
        public int _OnlyPrefix;

        private readonly Model _model;
        private readonly DrawingNumberingPlugin_StructuresData _data;
        
        public DrawingNumberingPlugin(DrawingNumberingPlugin_StructuresData data)
        {
            _model = new Model();
            _data = data;
        }
        
        private void GetValuesFromDialog()
        {
            _Prefix = _data.Prefix;
            _StartNumber = _data.StartNumber;
            _Digits = _data.Digits;
            _Postfix = _data.Postfix;
            _Title = _data.Title;
            _OnlyPrefix = _data.OnlyPrefix;

            if (IsDefaultValue(_Prefix) || _Prefix == "")
                _Prefix = "";
            if (IsDefaultValue(_StartNumber) || _StartNumber < 1)
                _StartNumber = 1;
            if (IsDefaultValue(_Digits) || _Digits < 1)
                _Digits = 1;
            if (IsDefaultValue(_Postfix) || _Postfix == "")
                _Postfix = "";
            if (IsDefaultValue(_Title) || _Title < 0)
                _Title = 0;
            if (IsDefaultValue(_OnlyPrefix))
                _OnlyPrefix = 0;

        }

        private string GetCurrentNumberWithPrefixAndPostFix(string prefix, string postfix, int currentNumber)
        {
            if (_OnlyPrefix == 1) return prefix;
            else  return prefix + GetCurrentNumber(currentNumber) + postfix;
        }

        private string GetCurrentNumber(int currentNumber)
        {
            var digits = _Digits;
         
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
        
        public override bool Run(List<InputDefinition> Input)
        {
            GetValuesFromDialog();
            int succesfulModified = 0;

            var selectedDrawings = new Tekla.Structures.Drawing.DrawingHandler().GetDrawingSelector().GetSelected();
            var drawingsCount = selectedDrawings.GetSize();
            if (drawingsCount == 0)
            {
                MessageBox.Show("No drawings selected", "No drawings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure do you want to change attributes in "+selectedDrawings.GetSize()+" drawings?",
                "Are you sure ?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            if (result == DialogResult.Yes)
            {
                var progress = new Tekla.Structures.Model.Operations.Operation.ProgressBar();

                var stopwatch = new System.Diagnostics.Stopwatch();
                try
                {
                    progress.Display(10, "Task is in progress", "Task is in progress", "Cancel", " ");
                    stopwatch.Start();
                    int currentNumber = _StartNumber;
                    int checkedDrawings = 0;
                    double medTimeForOne = 0;

                    while (selectedDrawings.MoveNext())
                    {
                        if (progress.Canceled()) break;

                        var currentDrawing = selectedDrawings.Current as Tekla.Structures.Drawing.Drawing;
                        bool modified = false;
                        var currentNumberString = GetCurrentNumberWithPrefixAndPostFix(_Prefix, _Postfix, currentNumber);

                        switch (_Title)
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
                                    var udaName = udaHandler.GetUDAByIndex(_Title - 4);

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
                        progress.SetProgress(checkedDrawings + "/" + drawingsCount + "\t" + Math.Round(medTimeForOne*(drawingsCount - checkedDrawings)) + " minutes left", 100 * checkedDrawings / drawingsCount);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    progress.Close();
                    stopwatch.Stop();
                    MessageBox.Show("Task has been completed.\nModified drawings: " + succesfulModified.ToString() + ".\nTime elapsed: " + Math.Round(stopwatch.Elapsed.TotalSeconds).ToString() + " seconds");
                }

            }

            return true;
        }

        public override List<InputDefinition> DefineInput()
        {
            return new List<InputDefinition>();
        }
    }
}
