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
using assetsUpdater.Interfaces;
using assetsUpdater.StorageProvider;
using assetsUpdater.Utils;
using NLog;

namespace assetsUpdater.UI.WinForms
{
    public partial class ReleaseVersionForm : Form
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public DataManager? ChosenDataManager { get; set; } = null;
        public DataManager? OriginalDataManager { get; set; } = null;
        public ReleaseVersionForm()
        {
            InitializeComponent();
            InitUi();
        
        }

        private void ConditionIsEnableControl(bool condition)
        {
            VcsFolder_Textbox.Enabled = condition;
            BrowseVcsFolder_Btn.Enabled = condition;
            DbModify_Btn.Enabled = condition;
            Release_Btn.Enabled = condition;
        }
        private void InitUi()
        {
            //Disable Default Control
            ConditionIsEnableControl(false);

            InputDb_openFileDialog.CheckFileExists = true;
            InputDb_openFileDialog.CheckPathExists = true;
            ldbName_Label.Text = "";

            vcs_folderBrowserDialog.AutoUpgradeEnabled = true;
       
            vcs_folderBrowserDialog.ShowNewFolderButton = true;
            vcs_folderBrowserDialog.Description = "请选择根目录，通常为(.minecraft)或.minecraft上级目录";
            vcs_folderBrowserDialog.UseDescriptionForTitle = true;
        }
        private void Ldb_Btn_Click(object sender, System.EventArgs e)
        {

            var r = InputDb_openFileDialog.ShowDialog();
            Console.WriteLine(r);
            Console.WriteLine(InputDb_openFileDialog.FileName);
            
            ChosenDataManager = new LocalDataManager(InputDb_openFileDialog.FileName);
            var result= ChosenDataManager.IsDataValid().Result;
            
            if (!result)
            {
             
                MessageBox.Show("LDB不存在");
                return;
            }
      
            ldbName_Label.Text = Path.GetFileName(ChosenDataManager.DatabasePath);
            VcsFolder_Textbox.Text = ChosenDataManager.StorageProvider.GetBuildInDbData().Config.VersionControlFolder;


            OriginalDataManager = new LocalDataManager((IStorageProvider)ChosenDataManager.StorageProvider.Clone());            //ReEnableControl

            ConditionIsEnableControl(true);
        }

        private void Release_Btn_Click(object sender, System.EventArgs e)
        {
            try
            {
                var result = new ReleaseConfirmationForm(ChosenDataManager ?? throw new ArgumentNullException(nameof(ChosenDataManager), "Db can't be null"), OriginalDataManager ?? ChosenDataManager ?? throw new ArgumentNullException(nameof(OriginalDataManager), "Db can't be null")).ShowDialog(this);

                if (result == DialogResult.OK)
                {

                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
               
            }
         

        }

        private void DbModify_Btn_Click(object sender, System.EventArgs e)
        {
            if (ChosenDataManager==null)
            {
                MessageBox.Show("请选择一个版本的数据库.");
                return;
            }
            var result = new ConfigModifyForm(ChosenDataManager).ShowDialog(this);
            
            Console.WriteLine(result);

            if (result == DialogResult.OK)
            {
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            GC.Collect();
        }

        private void BrowseVcsFolder_Btn_Click(object sender, System.EventArgs e)
        {
            if (ChosenDataManager == null)
            {
                MessageBox.Show("请先选择一个数据库");

                return;
            }
            var result = vcs_folderBrowserDialog.ShowDialog();
            Logger.Debug("BrowseVcsFolder_Btn_Click Result: {0}",result);
            if (result==DialogResult.OK)
            {
                //Update Properties
                
                
                ChosenDataManager.StorageProvider.GetBuildInDbData().Config.VersionControlFolder = vcs_folderBrowserDialog.SelectedPath;
                
              
                //Update UI
                VcsFolder_Textbox.Text = vcs_folderBrowserDialog.SelectedPath;
                
            }
        }

        private void ReleaseVersionForm_Load(object sender, System.EventArgs e)
        {

        }
    }
}
