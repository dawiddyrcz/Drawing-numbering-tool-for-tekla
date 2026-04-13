using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace MakraODS_InstallerClass
{
    [RunInstaller(true)]
    public partial class InstallerClass : System.Configuration.Install.Installer
    {
        public InstallerClass()
        {
            //You can add versions from TS18.1 to TS2020i
            //tSInstallFiles.AddTeklaVersion("18.0");
            tSInstallFiles.AddTeklaVersion("18.1");
            tSInstallFiles.AddTeklaVersion("19.0");
            tSInstallFiles.AddTeklaVersion("19.1");
            tSInstallFiles.AddTeklaVersion("20.0");
            tSInstallFiles.AddTeklaVersion("20.1");
            tSInstallFiles.AddTeklaVersion("21.0");
            tSInstallFiles.AddTeklaVersion("21.1");
           

            InitializeComponent();
        }

        TSInstallFiles tSInstallFiles = new TSInstallFiles();

        private string subFolder1 = Path.Combine("plugins", "Tekla", "Drawings", "DrawingNumberingApp");
        private string old_subFolder1 = Path.Combine("applications", "Tekla", "DrawingNumberingApp");

        private List<string> filesToCopy_ToTSAppSubFolder1 = new List<string>()
            {
                "DrawingNumberingApp.exe",
                "DrawingNumberingPlugin2.dll",
                "License.rtf"
            };

        private List<string> directioriesToCopy_ToTSAppSubFolder1 = new List<string>()
        {

        };

        private string subFolder2 = Path.Combine("applications", "Tekla", "ApplicationStartup");

        private List<string> filesToCopy_ToTSAppSubFolder2 = new List<string>()
        {

        };


        private List<string> filesToCopy_ToPluginFolder = new List<string>()
            {
                "DrawingNumberingPlugin2.dll",
            };

        private List<string> filesToCopy_ToBitmapFolder = new List<string>()
            {
                "Drawings\\et_element_Drawing Numbering Tool 2.bmp",
            };

        private List<string> filesToCopy_ToCommonSystemFolder = new List<string>()
            {
                "Settings\\DrawingNuberingTool_ComponentCatalog.ac.xml",
                "Settings\\standard.DrawingNumberingPlugin2.MainForm.xml",
            };

        private void CorrectFilePathes(string targetDir)
        {
            for (int i = 0; i < filesToCopy_ToTSAppSubFolder1.Count; i++)
            {
                filesToCopy_ToTSAppSubFolder1[i] = Path.Combine(targetDir, filesToCopy_ToTSAppSubFolder1[i]);
            }

            for (int i = 0; i < filesToCopy_ToTSAppSubFolder2.Count; i++)
            {
                filesToCopy_ToTSAppSubFolder2[i] = Path.Combine(targetDir, filesToCopy_ToTSAppSubFolder2[i]);
            }

            for (int i = 0; i < directioriesToCopy_ToTSAppSubFolder1.Count; i++)
            {
                directioriesToCopy_ToTSAppSubFolder1[i] = Path.Combine(targetDir, directioriesToCopy_ToTSAppSubFolder1[i]);
            }

            for (int i = 0; i < filesToCopy_ToPluginFolder.Count; i++)
            {
                filesToCopy_ToPluginFolder[i] = Path.Combine(targetDir, filesToCopy_ToPluginFolder[i]);
            }

            for (int i = 0; i < filesToCopy_ToBitmapFolder.Count; i++)
            {
                filesToCopy_ToBitmapFolder[i] = Path.Combine(targetDir, filesToCopy_ToBitmapFolder[i]);
            }

            for (int i = 0; i < filesToCopy_ToCommonSystemFolder.Count; i++)
            {
                filesToCopy_ToCommonSystemFolder[i] = Path.Combine(targetDir, filesToCopy_ToCommonSystemFolder[i]);
            }
        }

        private void CloseTeklaMessage()
        {
        AGAIN:
            int tsProcessesCount = 0;

            foreach (var currentProces in Process.GetProcesses())
            {
                if (currentProces.ProcessName == "TeklaStructures")
                {
                    if (currentProces != null) tsProcessesCount++;
                }
            }

            if (tsProcessesCount > 0)
            {
                var msgBoxResult = System.Windows.Forms.MessageBox.Show("Close all Tekla Structures processes!", "Close Tekla Structures", System.Windows.Forms.MessageBoxButtons.RetryCancel);

                if (msgBoxResult == System.Windows.Forms.DialogResult.Retry) goto AGAIN;
                else throw new Exception("Instalation canceled by user");
            }
        }

        public override void Install(IDictionary stateSaver)
        {
            CloseTeklaMessage();

            var targetDirParam = Context.Parameters["TargetDir"];
            if (targetDirParam == null) throw new Exception("TargetDir is null\n\n add /TargetDir=\"[TARGETDIR]\\\" to custom action data");
            var targetDir = targetDirParam.ToString();  //add to custom action data:  /TargetDir="[TARGETDIR]\"
            CorrectFilePathes(targetDir);

            try
            {
                tSInstallFiles.DeleteFilesFrom_TSBinDirectory(filesToCopy_ToTSAppSubFolder1, old_subFolder1);
                tSInstallFiles.DeleteConfigFiles(filesToCopy_ToPluginFolder, "plugins");
                tSInstallFiles.DeleteConfigFiles(filesToCopy_ToTSAppSubFolder1, old_subFolder1);
                tSInstallFiles.DeleteFilesFrom_TSPluginDirectory(filesToCopy_ToPluginFolder);
            }
            catch { }

            try
            {
                tSInstallFiles.CopyFilesTo_TSBinDirectory(filesToCopy_ToTSAppSubFolder1, subFolder1);
                tSInstallFiles.CopyDirectoriesTo_TSBinDirectory(directioriesToCopy_ToTSAppSubFolder1, subFolder1);

                tSInstallFiles.CopyFilesTo_TSBinDirectory(filesToCopy_ToTSAppSubFolder2, subFolder2);

                tSInstallFiles.CopyFilesTo_EnvBmpDirectory(filesToCopy_ToBitmapFolder);
                tSInstallFiles.CopyFilesTo_EnvCommonSystemDirectory(filesToCopy_ToCommonSystemFolder);
                //tSInstallFiles.CopyFilesTo_TSPluginDirectory(filesToCopy_ToPluginFolder);

                //tSInstallFiles.CopyTeklaStructuresConfigFilesToBinSubFolder(filesToCopy_ToPluginFolder, "plugins");
                tSInstallFiles.CopyTeklaStructuresConfigFilesToBinSubFolder(filesToCopy_ToTSAppSubFolder1, subFolder1);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            base.Install(stateSaver);
        }

        public override void Uninstall(IDictionary savedState)
        {
            CloseTeklaMessage();

            var targetDirParam = Context.Parameters["TargetDir"];
            if (targetDirParam == null) throw new Exception("TargetDir is null\n\n add /TargetDir=\"[TARGETDIR]\\\" to custom action data");
            var targetDir = targetDirParam.ToString();  //add to custom action data:  /TargetDir="[TARGETDIR]\"
            CorrectFilePathes(targetDir);

            try
            {
                tSInstallFiles.DeleteFilesFrom_TSBinDirectory(filesToCopy_ToTSAppSubFolder1, subFolder1);
                tSInstallFiles.DeleteDirectoriesFrom_TSBinDirectory(directioriesToCopy_ToTSAppSubFolder1, subFolder1);

                tSInstallFiles.DeleteFilesFrom_TSBinDirectory(filesToCopy_ToTSAppSubFolder2, subFolder2);

                tSInstallFiles.DeleteFilesFrom_EnvBmpDirectory(filesToCopy_ToBitmapFolder);
                tSInstallFiles.DeleteFilesFrom_EnvCommonSystemDirectory(filesToCopy_ToCommonSystemFolder);
                tSInstallFiles.DeleteFilesFrom_TSPluginDirectory(filesToCopy_ToPluginFolder);

                tSInstallFiles.DeleteConfigFiles(filesToCopy_ToPluginFolder, "plugins");
                tSInstallFiles.DeleteConfigFiles(filesToCopy_ToTSAppSubFolder1, subFolder1);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            base.Uninstall(savedState);
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion


        /// <summary>
        /// This class copy (or delete) files from the directories of Tekla Structures Application
        /// </summary>
        class TSInstallFiles
        {
            public TSInstallFiles()
            {

            }

            private List<TeklaData> tsData = new List<TeklaData>();

            /// <summary>
            /// Adds Tekla Version to internal list and gets information from the system registry
            /// </summary>
            /// <param name="version"></param>
            public void AddTeklaVersion(string version)
            {
                //Bellow you can add another TS version with its registry key
                //Version 17.0 dont have redirections in TeklaStructures.exe.config file so it will not working
                //Version 18.0 not confirmed
                //Version 2019, 2019i, 2020, 2020i added in 2018 year so not confirmed

                switch (version)
                {
                    case "18.0":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "18.0", @"SOFTWARE\Tekla\Structures\18.0\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "18.1":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "18.1", @"SOFTWARE\Tekla\Structures\18.1\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "19.0":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "19.0", @"SOFTWARE\Tekla\Structures\19.0\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "19.1":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "19.1", @"SOFTWARE\Tekla\Structures\19.1\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "20.0":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "20.0", @"SOFTWARE\Tekla\Structures\20.0\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "20.1":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "20.1", @"SOFTWARE\Tekla\Structures\20.1\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "21.0":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "21.0", @"SOFTWARE\Tekla\Structures\21.0\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "21.1":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "21.1", @"SOFTWARE\Tekla\Structures\21.1\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "2016":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "2016", @"SOFTWARE\Tekla\Structures\2016\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "2016i":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "2016i", @"SOFTWARE\Tekla\Structures\2016i\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "2017":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "2017", @"SOFTWARE\Tekla\Structures\2017\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "2017i":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "2017i", @"SOFTWARE\Tekla\Structures\2017i\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "2018":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "2018", @"SOFTWARE\Tekla\Structures\2018\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "2018i":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "2018i", @"SOFTWARE\Tekla\Structures\2018i\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "2019":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "2019.0", @"SOFTWARE\TRIMBLE\Tekla Structures\2019.0\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "2019i":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "2019.1", @"SOFTWARE\TRIMBLE\Tekla Structures\2019.1\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "2020":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "2020.0", @"SOFTWARE\TRIMBLE\Tekla Structures\2020.0\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "2020i":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "2020.1", @"SOFTWARE\TRIMBLE\Tekla Structures\2020.1\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "2016 Learning":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "2016", @"SOFTWARE\Tekla\Structures\2016 Learning\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "2016i Learning":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "2016i", @"SOFTWARE\Tekla\Structures\2016i Learning\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "2017 Learning":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "2017", @"SOFTWARE\Tekla\Structures\2017 Learning\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "2017i Learning":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "2017i", @"SOFTWARE\Tekla\Structures\2017i Learning\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "2018 Learning":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "2018", @"SOFTWARE\Tekla\Structures\2018 Learning\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "2018i Learning":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "2018i", @"SOFTWARE\Tekla\Structures\2018i Learning\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "2019 Learning":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "2019", @"SOFTWARE\Tekla\Structures\2019 Learning\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "2019i Learning":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "2019i", @"SOFTWARE\Tekla\Structures\2019i Learning\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "2020 Learning":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "2020", @"SOFTWARE\Tekla\Structures\2020 Learning\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    case "2020i Learning":
                        {
                            var exsistingTSDataIndex = this.tsData.FindIndex(x => x.Version == version);
                            var newTSData = new TeklaData(version, "2020i", @"SOFTWARE\Tekla\Structures\2020i Learning\setup");

                            if (exsistingTSDataIndex < 0) this.tsData.Add(newTSData);
                            else this.tsData[exsistingTSDataIndex] = newTSData;

                            break;
                        }
                    default:
                        break;
                }
            }

            /// <summary>
            /// Copies all files from the list to the Teklas directory: TSDirectory
            /// </summary>
            /// <param name="filesFullPathList"></param>
            public void CopyFilesTo_TSDirectory(List<string> filesFullPathList)
            {
                foreach (var currentFilePath in filesFullPathList)
                {
                    foreach (var currentTeklaData in this.tsData)
                    {
                        if (currentTeklaData.IsTeklaInstaled) CopyFileToDirectory(currentFilePath, currentTeklaData.TSDirectory);
                    }
                }
            }

            /// <summary>
            /// Delete all files from the list from the Teklas directory: TSDirectory
            /// </summary>
            /// <param name="filesFullPathList"></param>
            public void DeleteFilesFrom_TSDirectory(List<string> filesFullPathList)
            {
                foreach (var currentFilePath in filesFullPathList)
                {
                    foreach (var currentTeklaData in this.tsData)
                    {
                        if (currentTeklaData.IsTeklaInstaled) DeleteFileFromDirectory(currentFilePath, currentTeklaData.TSDirectory);
                    }
                }
            }

            /// <summary>
            /// Copies all files from the list to the Teklas directory: TSPluginDirectory
            /// </summary>
            /// <param name="filesFullPathList"></param>
            public void CopyFilesTo_TSPluginDirectory(List<string> filesFullPathList)
            {
                foreach (var currentFilePath in filesFullPathList)
                {
                    foreach (var currentTeklaData in this.tsData)
                    {
                        if (currentTeklaData.IsTeklaInstaled) CopyFileToDirectory(currentFilePath, currentTeklaData.TSPluginDirectory);
                    }
                }
            }

            /// <summary>
            /// Copies all files from the list to the Teklas directory: TSBinDirectory
            /// </summary>
            /// <param name="filesFullPathList"></param>
            public void CopyFilesTo_TSBinDirectory(List<string> filesFullPathList, string subFolderPath)
            {
                foreach (var currentFilePath in filesFullPathList)
                {
                    foreach (var currentTeklaData in this.tsData)
                    {
                        if (currentTeklaData.IsTeklaInstaled)
                        {
                            string directory = "";
                            bool createSubFolder = false;

                            if (subFolderPath == "")
                            {
                                directory = currentTeklaData.TSBinDirectory;
                                createSubFolder = false;
                            }
                            else
                            {
                                directory = Path.Combine(currentTeklaData.TSBinDirectory, subFolderPath);
                                createSubFolder = true;
                            }

                            CopyFileToDirectory(currentFilePath, directory, createSubFolder);
                        }
                    }
                }
            }

            /// <summary>
            /// Copies all files from the list to the Teklas directory: TSBinDirectory
            /// </summary>
            /// <param name="directoriesFullPathList"></param>
            public void CopyDirectoriesTo_TSBinDirectory(List<string> directoriesFullPathList, string subFolderPath)
            {
                foreach (var currentDirectoryPath in directoriesFullPathList)
                {
                    foreach (var currentTeklaData in this.tsData)
                    {
                        if (currentTeklaData.IsTeklaInstaled)
                        {
                            string directory = "";
                            bool createSubFolder = false;

                            if (subFolderPath == "")
                            {
                                directory = currentTeklaData.TSBinDirectory;
                                createSubFolder = false;
                            }
                            else
                            {
                                directory = Path.Combine(currentTeklaData.TSBinDirectory, subFolderPath);
                                createSubFolder = true;
                            }

                            CopyDirectoryToDirectory(currentDirectoryPath, directory, createSubFolder);
                        }
                    }
                }
            }

            /// <summary>
            /// Delete all files from the list from the Teklas directory: TSPluginDirectory
            /// </summary>
            /// <param name="filesFullPathList"></param>
            public void DeleteFilesFrom_TSPluginDirectory(List<string> filesFullPathList)
            {
                foreach (var currentFilePath in filesFullPathList)
                {
                    foreach (var currentTeklaData in this.tsData)
                    {
                        if (currentTeklaData.IsTeklaInstaled) DeleteFileFromDirectory(currentFilePath, currentTeklaData.TSPluginDirectory);
                    }
                }
            }

            /// <summary>
            /// Delete all files from the list from the Teklas directory: TSBinDirectory
            /// </summary>
            /// <param name="filesFullPathList"></param>
            public void DeleteFilesFrom_TSBinDirectory(List<string> filesFullPathList, string subFolderPath)
            {
                foreach (var currentFilePath in filesFullPathList)
                {
                    foreach (var currentTeklaData in this.tsData)
                    {
                        if (currentTeklaData.IsTeklaInstaled)
                        {
                            string directory = "";

                            if (subFolderPath == "")
                            {
                                directory = currentTeklaData.TSBinDirectory;
                            }
                            else
                            {
                                directory = Path.Combine(currentTeklaData.TSBinDirectory, subFolderPath);
                            }
                            DeleteFileFromDirectory(currentFilePath, directory);
                        }
                    }
                }
            }

            public void DeleteDirectoriesFrom_TSBinDirectory(List<string> directoriesFullPathList, string subFolderPath)
            {
                foreach (var currentDirPath in directoriesFullPathList)
                {
                    foreach (var currentTeklaData in this.tsData)
                    {
                        if (currentTeklaData.IsTeklaInstaled)
                        {
                            string directory = "";

                            if (subFolderPath == "")
                            {
                                directory = currentTeklaData.TSBinDirectory;
                            }
                            else
                            {
                                directory = Path.Combine(currentTeklaData.TSBinDirectory, subFolderPath);
                            }
                            DeleteDirectoryFromDirectory(currentDirPath, directory);
                        }
                    }
                }
            }

            /// <summary>
            /// Copies all files from the list to the Teklas directory: EnvCommonSystemDirectory
            /// </summary>
            /// <param name="filesFullPathList"></param>
            public void CopyFilesTo_EnvCommonSystemDirectory(List<string> filesFullPathList)
            {
                foreach (var currentFilePath in filesFullPathList)
                {
                    foreach (var currentTeklaData in this.tsData)
                    {
                        if (currentTeklaData.IsTeklaInstaled) CopyFileToDirectory(currentFilePath, currentTeklaData.EnvCommonSystemDirectory);
                    }
                }
            }

            /// <summary>
            /// Delete all files from the list from the Teklas directory: EnvCommonSystemDirectory
            /// </summary>
            /// <param name="filesFullPathList"></param>
            public void DeleteFilesFrom_EnvCommonSystemDirectory(List<string> filesFullPathList)
            {
                foreach (var currentFilePath in filesFullPathList)
                {
                    foreach (var currentTeklaData in this.tsData)
                    {
                        if (currentTeklaData.IsTeklaInstaled) DeleteFileFromDirectory(currentFilePath, currentTeklaData.EnvCommonSystemDirectory);
                    }
                }
            }

            /// <summary>
            /// Copies all files from the list to the Teklas directory: EnvBmpDirectory
            /// </summary>
            /// <param name="filesFullPathList"></param>
            public void CopyFilesTo_EnvBmpDirectory(List<string> filesFullPathList)
            {
                foreach (var currentFilePath in filesFullPathList)
                {
                    foreach (var currentTeklaData in this.tsData)
                    {
                        if (currentTeklaData.IsTeklaInstaled) CopyFileToDirectory(currentFilePath, currentTeklaData.EnvBmpDirectory);
                    }
                }
            }

            /// <summary>
            /// Delete all files from the list from the Teklas directory: EnvBmpDirectory
            /// </summary>
            /// <param name="filesFullPathList"></param>
            public void DeleteFilesFrom_EnvBmpDirectory(List<string> filesFullPathList)
            {
                foreach (var currentFilePath in filesFullPathList)
                {
                    foreach (var currentTeklaData in this.tsData)
                    {
                        if (currentTeklaData.IsTeklaInstaled) DeleteFileFromDirectory(currentFilePath, currentTeklaData.EnvBmpDirectory);
                    }
                }
            }

            private void CopyFileToDirectory(string sourceFilePath, string destinationDirectory, bool createSubfolder = false)
            {
                if (File.Exists(sourceFilePath))
                {
                    try
                    {
                        if (createSubfolder)
                        {
                            Directory.CreateDirectory(destinationDirectory);
                        }

                        if (Directory.Exists(destinationDirectory))
                        {
                            var fileName = new FileInfo(sourceFilePath).Name;
                            File.Copy(sourceFilePath, Path.Combine(destinationDirectory, fileName), true);
                        }
                    }
                    catch (Exception e) { Debug.WriteLine(e.ToString()); }
                }
            }

            private void CopyDirectoryToDirectory(string sourceDirPath, string destinationDirectory, bool createSubfolder = false)
            {
                if (Directory.Exists(sourceDirPath))
                {
                    try
                    {
                        if (createSubfolder)
                        {
                            Directory.CreateDirectory(destinationDirectory);
                        }

                        if (Directory.Exists(destinationDirectory))
                        {
                            var dirName = new DirectoryInfo(sourceDirPath).Name;
                            DirectoryTools.Copy(sourceDirPath, Path.Combine(destinationDirectory, dirName));
                        }
                    }
                    catch (Exception e) { Debug.WriteLine(e.ToString()); }
                }
            }

            private void DeleteFileFromDirectory(string sourceFilePath, string destinationDirectory)
            {
                try
                {

                    var fileName = new FileInfo(sourceFilePath).Name;
                    string fileFullName = Path.Combine(destinationDirectory, fileName);

                    if (File.Exists(fileFullName))
                    {
                        File.Delete(fileFullName);
                    }
                }
                catch (Exception e) { Debug.WriteLine(e.ToString()); }
            }

            private void DeleteDirectoryFromDirectory(string sourceDirPath, string destinationDirectory)
            {
                try
                {
                    var dirName = new DirectoryInfo(sourceDirPath).Name;

                    string dirFullName = Path.Combine(destinationDirectory, dirName);

                    if (Directory.Exists(dirFullName))
                    {
                        DirectoryTools.Delete(dirFullName);
                    }
                }
                catch (Exception e) { Debug.WriteLine(e.ToString()); }
            }

            public void CopyTeklaStructuresConfigFilesToBinSubFolder(List<string> filesFullPathList, string subFolder)
            {
                foreach (var currentFilePath in filesFullPathList)
                {
                    foreach (var currentTSData in this.tsData)
                    {
                        if (currentTSData.IsTeklaInstaled && File.Exists(currentFilePath) && File.Exists(currentTSData.TSConfigFileFullPath))
                        {
                            ChangeTeklaStructuresConfigFileIfNescesary(currentTSData.TSConfigFileFullPath);

                            var currentFileName = new FileInfo(currentFilePath).Name;
                            var configDllFileName = Path.Combine(currentTSData.TSBinDirectory, subFolder, currentFileName + ".config");

                            File.Copy(currentTSData.TSConfigFileFullPath, configDllFileName, true);
                        }
                    }
                }
            }

            public void DeleteConfigFiles(List<string> filesFullPathList, string subFolder)
            {
                foreach (var currentFilePath in filesFullPathList)
                {
                    foreach (var currentTSData in this.tsData)
                    {
                        if (currentTSData.IsTeklaInstaled && File.Exists(currentTSData.TSConfigFileFullPath))
                        {
                            var currentFileName = new FileInfo(currentFilePath).Name;
                            var configDllFileName = Path.Combine(currentTSData.TSBinDirectory, subFolder, currentFileName + ".config");

                            if (File.Exists(configDllFileName)) File.Delete(configDllFileName);
                        }
                    }
                }
            }

            private void ChangeTeklaStructuresConfigFileIfNescesary(string configFileFullName)
            {
                //For Tekla Structures up to 21.1 redirection in config is to version 99.1.0.0 of assemblies. It is needed to change it to 9999.1.0.0
                if (File.Exists(configFileFullName))
                {
                    string configFileText = File.ReadAllText(configFileFullName);

                    if (configFileText.Contains("-99.1.0.0"))
                    {
                        var bakConfilFileFullName = Path.Combine(configFileFullName + ".bak");
                        if (!File.Exists(bakConfilFileFullName)) File.Copy(configFileFullName, bakConfilFileFullName, true);
                        configFileText = configFileText.Replace("-99.1.0.0", "-9999.1.0.0");
                        File.WriteAllText(configFileFullName, configFileText);
                    }
                }
            }

            ///https://stackoverflow.com/questions/58744/copy-the-entire-contents-of-a-directory-in-c-sharp
            class DirectoryTools
            {
                public static void Copy(string sourceDirectory, string targetDirectory)
                {
                    DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
                    DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

                    CopyAll(diSource, diTarget);
                }

                public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
                {
                    Directory.CreateDirectory(target.FullName);

                    // Copy each file into the new directory.
                    foreach (FileInfo fi in source.GetFiles())
                    {
                        //Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                        fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
                    }

                    // Copy each subdirectory using recursion.
                    foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
                    {
                        DirectoryInfo nextTargetSubDir =
                            target.CreateSubdirectory(diSourceSubDir.Name);
                        CopyAll(diSourceSubDir, nextTargetSubDir);
                    }
                }

                public static void Delete(string targetDirectory)
                {
                    DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

                    DeleteAll(diTarget);
                }

                public static void DeleteAll(DirectoryInfo target)
                {
                    foreach (DirectoryInfo diSubDir in target.GetDirectories())
                    {
                        DeleteAll(diSubDir);
                    }

                    foreach (FileInfo fi in target.GetFiles())
                    {
                        fi.Delete();
                    }

                    Directory.Delete(target.FullName);
                }
            }


            /// <summary>
            /// This class gets Tekla Structures information from registry
            /// </summary>
            class TeklaData
            {
                /// <summary>
                /// Gets Tekla Structures folders information from registry and save it in properties
                /// </summary>
                /// <param name="version">Version of Tekla Structures eg: "20.1", "21.0", "2016", "2017i"</param>
                /// <param name="tsVersionDir">Name of Tekla Structures instalation directory eg: "20.1", "21.0", "2016", "2017i"</param>
                /// <param name="regKey">Registry key (in HKLM) where data is stored eg: @"SOFTWARE\Tekla\Structures\2016\setup" </param>
                public TeklaData(string version, string tsVersionDir, string regKey)
                {
                    this.Version = version;
                    this.tsVersionDir = tsVersionDir;
                    this.regKey = regKey;
                    this.IsTeklaInstaled = false;

                    GetDataFromRegistry();
                    if (this.IsTeklaInstaled) SetOtherDirectories();
                }

                public string Version { get; internal set; }
                public bool IsTeklaInstaled { get; internal set; }

                private readonly string tsVersionDir;
                private readonly string regKey;
                public const string configFileName = "TeklaStructures.exe.config";

                //Registry values
                public string AppDir { get; internal set; }
                public string EnvDir { get; internal set; }
                //public string HelpLocation { get; internal set; }
                public string ModelDir { get; internal set; }

                //Other values
                public string TSDirectory { get; internal set; }
                public string TSBinDirectory { get; internal set; }
                public string TSPluginDirectory { get; internal set; }
                public string EnvCommonSystemDirectory { get; internal set; }
                public string EnvBmpDirectory { get; internal set; }
                public string TSConfigFileFullPath { get; internal set; }

                private void GetDataFromRegistry()
                {
                    try
                    {
                        RegistryKey localKey32 =
                                        RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine,
                                            RegistryView.Registry32);
                        localKey32 = localKey32.OpenSubKey(this.regKey);

                        if (localKey32 != null)
                        {
                            this.AppDir = string.Empty;
                            this.AppDir = localKey32.GetValue("MainDir").ToString();
                            this.EnvDir = string.Empty;
                            this.EnvDir = localKey32.GetValue("EnvDir").ToString();
                            //this.HelpLocation = string.Empty;
                            //this.HelpLocation = localKey32.GetValue("HelpLocation").ToString();
                            this.ModelDir = string.Empty;
                            this.ModelDir = localKey32.GetValue("ModelDir").ToString();
                            localKey32.Close();

                            if (this.AppDir != string.Empty & this.EnvDir != string.Empty /*& this.HelpLocation != string.Empty*/ & this.ModelDir != string.Empty)
                                this.IsTeklaInstaled = true;
                            else
                                this.IsTeklaInstaled = false;
                        }
                        else
                        {
                            //If localkey 32 bit is null than check 64 bit
                            RegistryKey localKey64 =
                                RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine,
                                    RegistryView.Registry64);
                            localKey64 = localKey64.OpenSubKey(this.regKey);

                            if (localKey64 != null)
                            {
                                this.AppDir = string.Empty;
                                this.AppDir = localKey64.GetValue("MainDir").ToString();
                                this.EnvDir = string.Empty;
                                this.EnvDir = localKey64.GetValue("EnvDir").ToString();
                                //this.HelpLocation = string.Empty;
                                //this.HelpLocation = localKey64.GetValue("HelpLocation").ToString();
                                this.ModelDir = string.Empty;
                                this.ModelDir = localKey64.GetValue("ModelDir").ToString();
                                localKey64.Close();

                                if (this.AppDir != string.Empty & this.EnvDir != string.Empty /*& this.HelpLocation != string.Empty*/ & this.ModelDir != string.Empty)
                                    this.IsTeklaInstaled = true;
                                else
                                    this.IsTeklaInstaled = false;
                            }
                            else
                            {
                                //No 32 bit and no 64 bit key
                                this.IsTeklaInstaled = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Could not get data from registry " + this.regKey + " exception thrown " + ex.Message);
                    }
                }

                private void SetOtherDirectories()
                {
                    this.TSDirectory = Path.Combine(this.AppDir, this.tsVersionDir);
                    this.TSBinDirectory = Path.Combine(this.TSDirectory, "nt", "bin");
                    this.TSPluginDirectory = Path.Combine(this.TSBinDirectory, "plugins");
                    this.EnvCommonSystemDirectory = Path.Combine(this.EnvDir, this.tsVersionDir, "Environments", "common", "system");
                    this.EnvBmpDirectory = Path.Combine(this.EnvDir, this.tsVersionDir, "Bitmaps");

                    this.TSConfigFileFullPath = Path.Combine(this.TSBinDirectory, configFileName);
                }
            }

        }
    }
}
