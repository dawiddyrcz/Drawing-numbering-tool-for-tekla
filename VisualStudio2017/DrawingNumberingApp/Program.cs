using System;
using System.Windows.Forms;

namespace DrawingNumberingPlugin
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                IntPtr h1 = Tekla.Structures.Dialog.MainWindow.Frame.Handle;
                var mainForm = new MainForm();
                mainForm.Show(new WindowWrapper(h1));
                Application.Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                try
                {
                    System.IO.File.WriteAllText(System.Environment.SpecialFolder.CommonApplicationData + "\\drawingNumberingTool_error.txt", ex.ToString());
                }
                catch { }
            }
}

        private static bool IsTeklaRunning()
        {
            var model = new Tekla.Structures.Model.Model();
            return model.GetConnectionStatus();
        }
    }

    public class WindowWrapper : System.Windows.Forms.IWin32Window
    {
        public WindowWrapper(IntPtr handle)
        {
            _hwnd = handle;
        }

        public IntPtr Handle
        {
            get { return _hwnd; }
        }

        private IntPtr _hwnd;
    }
}
