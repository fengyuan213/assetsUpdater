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

namespace assetsUpdater.UI.WinForms
{
    public partial class ModifyAddressBuilderForm : Form
    {
        public IAddressBuilder AddressBuilder { get; set; } 
        public ModifyAddressBuilderForm(IAddressBuilder addressBuilder)
        {
            InitializeComponent();
            AddressBuilder = addressBuilder;
        }

        private void Confirm_Btn_Click(object sender, System.EventArgs e)
        {
            AddressBuilder.RootDownloadAddress = RootDownloadAddr_TextBox.Text;
            this.Close();
        }
    }
}
