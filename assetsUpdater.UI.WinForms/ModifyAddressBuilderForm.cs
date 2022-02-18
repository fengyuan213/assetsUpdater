using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using assetsUpdater.Interfaces;
using NLog;

namespace assetsUpdater.UI.WinForms
{
    public partial class ModifyAddressBuilderForm : Form
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public IAddressBuilder AddressBuilder { get; set; } 
        public ModifyAddressBuilderForm(IAddressBuilder addressBuilder)
        {
            InitializeComponent();
            AddressBuilder = addressBuilder;
            InitializeUi();
        }

        private void InitializeUi()
        {
            CurrentRootAddress_Label.Text ="当前值:"+ AddressBuilder.RootDownloadAddress;

        }
        private void Confirm_Btn_Click(object sender, System.EventArgs e)
        {
            var previousValue = AddressBuilder.RootDownloadAddress.Normalize();

            AddressBuilder.RootDownloadAddress = RootDownloadAddr_TextBox.Text;
            Logger.Info($"RootDownloadAddress modified:{previousValue} -> {AddressBuilder.RootDownloadAddress}");

            this.Close();
        }

        private void CurrentRootAddress_Label_Click(object sender, System.EventArgs e)
        {

        }
    }
}
