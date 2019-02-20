using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessMatrixClient
{
    public partial class ConfigureServerForm : Form
    {
        public ConfigureServerForm()
        {
            InitializeComponent();
            IP.Text = API.APIDict.ServerIP;
            Port.Text = API.APIDict.ServerPort;
        }

        private async void OkButton_Click(object sender, EventArgs e)
        {
            API.APIDict.ServerIP = IP.Text;
            API.APIDict.ServerPort = Port.Text;

            try
            {
                await AccessMatrixClient.API.APIHelper.GetStringAsync(API.APIURL.ForConnect);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Нет подключения к указанному серверу.\nПопробуйте снова или повторите попытку позже");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
