using System;
using System.IO;
using Tekla.Structures.Dialog;

namespace DrawingNumberingPlugin
{
    public partial class StarterForm : PluginFormBase
    {
        private readonly string _appName = "DrawingNumberingApp.exe";
        private readonly string _tsepDirName = "DDBIMDrawingNumberingTool";
        private readonly string _appDirectory = System.IO.Path.Combine("applications", "Tekla", "DrawingNumberingApp");
        public StarterForm()
        {
            this.Shown += DummyForm_Shown;
            InitializeComponent();
        }

        private void DummyForm_Shown(object sender, EventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(50);
                try
                {
                    CloseAllProcesses();
                }
                catch { }

                var appFullPath = GetApplicationExePath();
                System.Diagnostics.Process.Start(appFullPath);

                Tekla.Structures.Model.Operations.Operation.DisplayPrompt(
                    DateTime.Now.ToString("HH:mm:ss.fff") + " Trying to start application: " + appFullPath);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }

            System.Threading.Tasks.Task.Run(() =>
            {
                Invoke(new Action(() =>
                {
                    try
                    {
                        System.Threading.Thread.Sleep(1000);
                        this.Close();
                    }
                    catch { }
                }
                    ));
            });
        }

        private string GetApplicationExePath()
        {
            string appFileFullPath = "";

            string XS_Variable = System.Environment.GetEnvironmentVariable("XSDATADIR");
            string extensionDirectory = Path.Combine(XS_Variable, "Environments", "common", "extensions");

            if (!string.IsNullOrWhiteSpace(XS_Variable))
                appFileFullPath = Path.Combine(extensionDirectory, _tsepDirName, _appName);

            if (File.Exists(appFileFullPath)) return appFileFullPath;

            var teklaBinDir = Tekla.Structures.Dialog.StructuresInstallation.BinFolder;
            if (!string.IsNullOrWhiteSpace(teklaBinDir))
                appFileFullPath = Path.Combine(teklaBinDir, _appDirectory, _appName);

            if (File.Exists(appFileFullPath)) return appFileFullPath;


            var assemblyFile = System.Reflection.Assembly.GetExecutingAssembly().Location; // sometimes returns string empty
            if (!string.IsNullOrWhiteSpace(assemblyFile))
                appFileFullPath = Path.Combine(Path.GetDirectoryName(assemblyFile), _appName);

            if (File.Exists(appFileFullPath)) return appFileFullPath;

            throw new Exception($"Could not find {_appName} file");
        }


        private void CloseAllProcesses()
        {
            var processName = Path.GetFileNameWithoutExtension(_appName);
            var processes = System.Diagnostics.Process.GetProcessesByName(processName);

            if (processes != null)
            {
                foreach (var process in processes)
                {
                    process.Kill();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DummyForm_Shown(this, new EventArgs());
        }
    }
}
