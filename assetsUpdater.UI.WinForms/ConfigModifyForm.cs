using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace assetsUpdater.UI.WinForms
{
    public partial class ConfigModifyForm : Form
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public DataManager DataManager { get; set; }


        public ConfigModifyForm(DataManager dataManager)
        {
            
            DataManager = dataManager;
            InitializeComponent();
            InitializeUi();
        }

        private void InitializeUi()
        {
            var data = DataManager.StorageProvider.GetBuildInDbData();

            MajV_TextBox.Text = data.Config.MajorVersion.ToString();
            MirrorV_TextBox.Text = data.Config.MirrorVersion.ToString();
            UpdateUrl_TextBox.Text = data.Config.UpdateUrl?.ToString();

           Logger.Debug("MajorV:{0}",MajV_TextBox.Text);
            Logger.Debug("MirrorV:{0}", MirrorV_TextBox.Text);
            Logger.Debug("UpdateUrl:{0}", UpdateUrl_TextBox.Text);
        }

        private void UpdateResult()
        {
            var data = DataManager.StorageProvider.GetBuildInDbData();
            data.Config.MirrorVersion = int.TryParse(MirrorV_TextBox.Text, out int result) ? result : data.Config.MirrorVersion;

            data.Config.MajorVersion = int.TryParse(MajV_TextBox.Text, out int r) ? r : data.Config.MajorVersion;
            data.Config.UpdateUrl = UpdateUrl_TextBox.Text;


        }
        private void Confirm_Btn_Click(object sender, System.EventArgs e)
        {
            UpdateResult();
            Logger.Debug("MajorV:{0}", DataManager.StorageProvider.GetBuildInDbData().Config.MajorVersion);
            Logger.Debug("MirrorV:{0}", DataManager.StorageProvider.GetBuildInDbData().Config.MirrorVersion);
            Logger.Debug("UpdateUrl:{0}", DataManager.StorageProvider.GetBuildInDbData().Config.UpdateUrl);
            this.DialogResult=DialogResult.OK;
            this.Close();
        }

        private void ConfigModifyForm_Load(object sender, System.EventArgs e)
        {

        }

        private void ModifyDirList_Btn_Click(object sender, System.EventArgs e)
        {
            try
            {
                var result = new ModifyDirListForm(DataManager).ShowDialog(this);
                if (result == DialogResult.OK)

                {

                }   
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
             
            }
           
        }

        private void ModifyAddressBuilder_Btn_Click(object sender, System.EventArgs e)
        {
            var maf = new ModifyAddressBuilderForm(DataManager.StorageProvider.GetBuildInDbData().Config.DownloadAddressBuilder)
            {

            };
            maf.ShowDialog();

        }
    }
}
