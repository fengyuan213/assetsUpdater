using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accessibility;
using NLog;

namespace assetsUpdater.UI.WinForms
{
    public partial class MainForm : Form
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private ReleaseVersionForm _releaseVersionForm;
        public ReleaseVersionForm ReleaseVersionForm
        {
            get => _releaseVersionForm ??= new ReleaseVersionForm();
            set => _releaseVersionForm = value;
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private  void ReleaseNewVersion_Btn_Click(object sender, System.EventArgs e)
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
    }
}
