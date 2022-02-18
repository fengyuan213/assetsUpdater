using assetsUpdater.Interfaces;
using assetsUpdater.StorageProvider;

using NLog;

using System;
using System.Windows.Forms;

namespace assetsUpdater.UI.WinForms.Dialogs
{
    public partial class DownloadDatabaseForm : Form
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的 属性“StorageProvider”必须包含非 null 值。请考虑将 属性 声明为可以为 null。

        public DownloadDatabaseForm()
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的 属性“StorageProvider”必须包含非 null 值。请考虑将 属性 声明为可以为 null。
        {
            InitializeComponent();
        }

        public IStorageProvider StorageProvider { get; set; }

        private async void Download_Btn_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (IsTencentCdn_CheckBox.Checked)
                {
                }
                StorageProvider = new FileDatabase();
                var addr = Address_TextBox.Text;

                await StorageProvider.Download(addr).ConfigureAwait(false);

                DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }
    }
}