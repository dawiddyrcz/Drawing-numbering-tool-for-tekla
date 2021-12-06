using System;
using System.IO;
using Tekla.Structures.Dialog;

namespace DrawingNumberingPlugin
{
    public partial class StarterForm : PluginFormBase
    {
        private readonly string _appName = "DrawingNumberingApp.exe";
        private readonly string _tsepDirName = "DrawingNumberingPlugin";
        private readonly string _appDirectory = System.IO.Path.Combine("applications", "Tekla", "DrawingNumberingApp");
        public StarterForm()
        {
            InitializeComponent();
        }

        private void Start_button_Click(object sender, EventArgs e)
        {
            try
            {
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

            Invoke(new Action(() =>
            {
                try
                {
                    System.Threading.Thread.Sleep(50);
                    this.Close();
                }
                catch { }
            }
            ));
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
    }
}
