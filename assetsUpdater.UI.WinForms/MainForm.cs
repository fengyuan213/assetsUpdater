#region Using

using System;
using System.ComponentModel;
using System.Windows.Forms;
using NLog;

#endregion

namespace assetsUpdater.UI.WinForms
{
    public partial class MainForm : Form
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private ReleaseVersionForm _releaseVersionForm;

        public MainForm()
        {
            InitializeComponent();
        }

        public ReleaseVersionForm ReleaseVersionForm
        {
            get => _releaseVersionForm ??= new ReleaseVersionForm();
            set => _releaseVersionForm = value;
        }

        private void ReleaseNewVersion_Btn_Click(object sender, System.EventArgs e)
        {
            try
            {
                var result = ReleaseVersionForm.ShowDialog(this);
                Logger.Trace(result);
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }

        private void InputDb_openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
        }

        private void CreateFScrach_Btn_Click(object sender, System.EventArgs e)
        {
            try
            {
                var form = new CreateDatabaseForm();
                var result = form.ShowDialog(this);
                Logger.Trace(result);
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }
    }
}